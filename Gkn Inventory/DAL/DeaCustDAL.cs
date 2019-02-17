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
    class DeaCustDAL
    {
        //creating static string method for DB connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for Dealer and customer module
        public DataTable Select()
        {
            //creating database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //writing sql query to get all the data from database
                string sql = "SELECT * FROM tbl_dea_cust";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //open database connection
                conn.Open();
                //adding the value from adapter to datatable dt
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
        #region Method to Add details for dealer and customer
        public bool Insert(DeaCustBLL dc)
        {
            //create a boolean variable and set its dafault value to false
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //writing query to add new dealer and customer
                string sql = "INSERT INTO tbl_dea_cust(type, name, email, contact, address, added_date, added_by) VALUES(@type, @name, @email, @contact, @address, @added_date, @added_by)";
                //creating sql command to pass valuesin our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
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
        #region  Method to update for dealer and customer Module 
        public bool Update(DeaCustBLL dc)
        {
            //create a boolean variable and set its dafault value to false
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //query to update dealer and customer
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, contact=@contact, address=@address,  added_date=@added_date, added_by=@added_by WHERE id=@id";
                //creating sql command to pass values in our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);
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
        #region Method to delete Dealer and customer from database
        public bool Delete(DeaCustBLL dc)
        {
            //create a boolean variable and set its dafault value to false
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql query to delete from database
                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";
                //creating sql command to pass valuesin our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@id", dc.id);
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
        #region Search method for Dealer and Customer module
        public DataTable Search(string keywords)
        {
            //sql connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //creating datatable to hold the data from database temporarily
            DataTable dt = new DataTable();
            try
            {
                //sql query to search categories from database
                string sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%" + keywords + "%' OR type LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%'";
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
        #region method to search Dealer or Customer for transaction module
        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
            //create an object for DeaCustBLL class
            DeaCustBLL dc = new DeaCustBLL();
            SqlConnection conn = new SqlConnection(myconnstrng );
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT name, email, contact, address from tbl_dea_cust WHERE id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
                conn.Open();
                adapter.Fill(dt);
                //if we have value on dt we need to save it in dealerCustomer BLL
                if(dt.Rows.Count>0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email= dt.Rows[0]["email"].ToString();
                    dc.contact= dt.Rows[0]["contact"].ToString();
                    dc.address= dt.Rows[0]["address"].ToString();
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
            return dc;
        }
        #endregion
        #region Method to get id of the dealer or customer based on Name
        public DeaCustBLL GetDeaCustIdFromName(string Name)
        {
            //first create an object of deaCustBLL and return it
            DeaCustBLL dc = new DeaCustBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT id FROM tbl_dea_cust WHERE name='" + Name + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapter.Fill(dt);
                if(dt.Rows .Count >0)
                {
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());
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
            return dc;
        }
        #endregion

    }
}
