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
    class transactionDAL
    {
        //creating static string method for DB connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region Insert Transaction Method
        public bool Insert_Transaction(transactionsBLL t, out int transactionID)
        {
            bool isSuccess = false;
            //set the out transactionID value to -1
            transactionID = -1;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "INSERT INTO tbl_transactions (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY";
                //creating sql command to pass values in our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id );
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal );
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date );
                cmd.Parameters.AddWithValue("@tax", t.tax );
                cmd.Parameters.AddWithValue("@discount", t.discount );
                cmd.Parameters.AddWithValue("@added_by", t.added_by);
                //open database connection
                conn.Open();
                // execute  the query
                object o = cmd.ExecuteScalar();
                if (o!=null)
                {
                    transactionID = int.Parse(o.ToString());
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
        #region Method to display all the Transaction
        public DataTable DisplayAllTransactions()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //write the sql query to display all transactions
                string sql = "SELECT * FROM tbl_transactions";
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
        #region Method to display transaction based on Transaction Type
        public DataTable DisplayTransactionByType(string type)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //write the sql query to display all transactions
                string sql = "SELECT * FROM tbl_transactions WHERE type='"+type+"'";
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
