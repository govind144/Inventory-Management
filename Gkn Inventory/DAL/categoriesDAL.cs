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
    class categoriesDAL
    {
        //static string method for database connection string
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        public DataTable Select()
        {
            //creating database connection
            SqlConnection conn = new SqlConnection(myconnstrng );
            DataTable dt = new DataTable();
            try
            {
                //writing sql query to get all the data from database
                string sql = "SELECT *FROM tbl_categories";
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //open database connection
                conn.Open();
                //adding the value from adapter to datatable dt
                adapter.Fill(dt);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return dt;

        }
        #endregion
      #region Insert New Category
        public bool Insert(categoriesBLL c)
        {
            //create a boolean variable and set its dafault value to false
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //writing query to add new category
                string sql = "INSERT INTO tbl_categories(Category,Sub_Category,description,added_date,added_by) VALUES(@Category,@Sub_Category,@description,@added_date,@added_by)";
                //creating sql command to pass valuesin our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@Category",c.Category);
                cmd.Parameters.AddWithValue("@Sub_Category", c.Sub_Category);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

                //open database connection
                conn.Open();
                //creating the int variable to execute query
                int rows = cmd.ExecuteNonQuery();
                if(rows >0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;

                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region update Method
        public bool Update(categoriesBLL c)
        {
            //create a boolean variable and set its dafault value to false
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //query to update category
                string sql = "UPDATE tbl_categories SET Category=@Category,Sub_Category=@Sub_Category, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";
                //creating sql command to pass valuesin our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@Category", c.Category);
                cmd.Parameters.AddWithValue("Sub_Category", c.Sub_Category);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                //open database connection
                conn.Open();
                //creating the int variable to execute query
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region Delete category Method
        public bool Delete(categoriesBLL c)
        {
            //create a boolean variable and set its dafault value to false
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql query to deletefrom database
                string sql = "DELETE FROM tbl_categories WHERE id=@id";
                //creating sql command to pass valuesin our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@id", c.id);

                //open database connection
                conn.Open();
                //creating the int variable to execute query
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region Method for search functionality
        public DataTable Search(string keywords)
        {
            //sql connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //creating datatable to hold the data from database temporarily
            DataTable dt = new DataTable();
            try
            {
                //sql query to search categories from database
                string sql = "SELECT * FROM tbl_categories WHERE id LIKE '%" + keywords + "%' OR Category LIKE '%" + keywords + "%' OR Sub_Category LIKE '%" + keywords + "%'";
                //creating sql command to execute the query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //gettinng data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
    }
}
