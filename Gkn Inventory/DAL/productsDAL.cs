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
    class productsDAL
    {
        //creating static string method for DB connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region Select method for product module
        public DataTable Select ()
        {
            //creating database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //writing sql query to get all the data from database
                string sql = "SELECT * FROM tbl_products";
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
        #region Method to insert product for database
        public bool Insert(productsBLL p)
        {
            //create a boolean variable and set its dafault value to false
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //writing query to add new category
                string sql = "INSERT INTO tbl_products(name, category, Sub_Category, description, rate, qty, added_date, added_by) VALUES(@name, @category,@Sub_Category, @description, @rate, @qty, @added_date, @added_by)";
                //creating sql command to pass valuesin our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@Sub_Category", p.Sub_Category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
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
        #region  Method to update product for database
        public bool Update(productsBLL p)
        {
            //create a boolean variable and set its dafault value to false
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //query to update category
                string sql = "UPDATE tbl_products SET name=@name, category=@category, Sub_Category=@Sub_Category, description=@description, rate=@rate,  added_date=@added_date, added_by=@added_by WHERE id=@id";
                //creating sql command to pass valuesin our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@Sub_Category", p.Sub_Category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);
  

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
        #region Method to delete product from database
        public bool Delete(productsBLL p)
        {
            //create a boolean variable and set its dafault value to false
            bool isSuccess = false;
            //connecting to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql query to deletefrom database
                string sql = "DELETE FROM tbl_products WHERE id=@id";
                //creating sql command to pass valuesin our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameters
                cmd.Parameters.AddWithValue("@id", p.id);

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
        #region Search method for product module
        public DataTable Search(string keywords)
        {
            //sql connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //creating datatable to hold the data from database temporarily
            DataTable dt = new DataTable();
            try
            {
                //sql query to search categories from database
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%' OR  Sub_Category LIKE '%" + keywords + "%'";
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
        #region Method to search Product in Transaction module
        public productsBLL GetProductsForTransaction(string keyword)
        {
            //create an object of productBLL and return it
            productsBLL p = new productsBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //write the query to get the details
                string sql = "SELECT  id,name, rate, qty FROM tbl_products WHERE id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";
                //create sql data adapter to execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();
                adapter.Fill(dt);

                if(dt.Rows .Count>0)
                {
                    p.id=int.Parse (dt.Rows[0]["id"].ToString ());
                    p.name = dt.Rows[0]["name"].ToString();
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.qty= decimal.Parse(dt.Rows[0]["qty"].ToString());
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
            return p;
        }
        #endregion
        #region  Method to get product ID based on product name
        public productsBLL  GetProductIDFromName(string ProductName)
        {
            //first create an object of deaCustBLL and return it
            productsBLL  p = new productsBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT id FROM tbl_products WHERE name='" + ProductName + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
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
            return p;
        }
        #endregion
        #region Method to get current quantity from the database based on product id
        public decimal GetProductQty(int ProductID)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            //create a decimal variable and set its default to zero
            decimal qty = 0;
            DataTable dt = new DataTable();
            try
            {
                //write sql query to get quantity from database
                string sql = "SELECT qty FROM tbl_products WHERE id=" + ProductID;
                //create a sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create a sql DataAdapter to execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                //pass the value from dataAdapter to datatable
                adapter.Fill(dt);
                //lets check if the datatable has value or not
                if(dt.Rows.Count >0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());

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
            return qty;

        }
        #endregion
        #region Method to update quantity
        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //write teh sql query to update quantity
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("id", ProductID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;

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
            return success;
        }
        #endregion
        #region Method to Increase Product
        public bool IncreaseProduct(int ProductID, decimal IncreaseQty)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //get the current quantity from database based on id
                decimal currentQty = GetProductQty(ProductID);
                //increase the current quantity by the qtypurchased from dealer
                decimal NewQty = currentQty + IncreaseQty;
                //update the product qty now
                success = UpdateQuantity(ProductID, NewQty);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region Method to Decrease Product
        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //get the current quantity from database based on id
                decimal currentQty = GetProductQty(ProductID);
                //decrease the current quantity by the qtypurchased from dealer
                decimal NewQty = currentQty - Qty;
                //update the product qty now
                success = UpdateQuantity(ProductID, NewQty);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region Display Products based on categories
        public DataTable DisplayProductsByCategory(string category)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //sql query to display products based on category
                string sql = "SELECT * FROM tbl_products WHERE category='" + category + "'";
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
        #region Display Products based on subcategory
        public DataTable DisplayProductsBySub_Category(string Sub_Category)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //sql query to display products based on subcategory
                string sql = "SELECT * FROM tbl_products WHERE Sub_Category='" + Sub_Category + "'";
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
    }
}
