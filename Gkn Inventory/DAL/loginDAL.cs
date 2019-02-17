using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gkn_Inventory.BLL;

namespace Gkn_Inventory.DAL
{
    class loginDAL
    {
        //static string to connect database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        public bool loginCheck(loginBLL l)
        {
            //create a boolean variable and set its value to false and return it
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //sql query to check login
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";
                //creating sql command to pass value
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                //checking the rows in datatable
                if(dt.Rows .Count>0)
                {
                    //login successfull
                    isSuccess = true;
                }
                else
                {
                    //login failed
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing connection
                conn.Close();
            }
            return isSuccess;
        }
    }
}
