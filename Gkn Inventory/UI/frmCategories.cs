using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gkn_Inventory.BLL;
using Gkn_Inventory.DAL;

namespace Gkn_Inventory.UI
{
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the id of the category from which we want to delete
            c.id = int.Parse(txtCategoryID.Text);
            bool success = dal.Delete(c);
            if(success==true)
            {
                MessageBox.Show("Category Deleted Successfully");
                Clear();
                //refresh data grid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;

            }

            else
            {
                MessageBox.Show("Failed to Delete Category");
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        categoriesBLL c = new categoriesBLL();
        categoriesDAL dal = new categoriesDAL();
        userDAL udal = new userDAL();
        private void btnADD_Click(object sender, EventArgs e)
        {
            //get the values from category form
            c.Category = txtCategory.Text;
            c.Sub_Category = txtSubCategory.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;
            //getting id in added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            //passing the idof logged in user in added by field
            c.added_by = usr.id;
            //creating boolean method to insert data into database
            bool success = dal.Insert(c);
            if(success ==true)
            {
                MessageBox.Show("New Category Inserted Successfully.");
                Clear();
                //refresh data grid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Inserted New Category.");
            }
        }

        public void Clear()
        {
            txtCategoryID.Text = "";
            txtCategory.Text = "";
            txtSubCategory.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            //write to code to display all the categories in data grid view
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;

        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //finding the row index of the row clicked on data grid view
            int RowIndex = e.RowIndex;
            txtCategoryID.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtCategory .Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtSubCategory.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
            txtDescription .Text = dgvCategories.Rows[RowIndex].Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the values from category form
            c.id = int.Parse(txtCategoryID.Text);
            c.Category = txtCategory.Text;
            c.Sub_Category  = txtSubCategory.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            //getting id in added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            //passing the id of logged in user in added by field
            c.added_by = usr.id;
            //creating boolean variable to upade categories and check
            bool success = dal.Update(c);
            if(success==true)
            {
                MessageBox.Show("Category Update Successfull");
                Clear();
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
         
                else
                {
                    MessageBox.Show("Failed to Update Successfull");
                }
            


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get the keywords
            string keywords = txtSearch.Text;
            //fill the categories based on keywords
            if(keywords!=null)
            {
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
