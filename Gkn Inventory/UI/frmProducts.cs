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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();           
        }
        categoriesDAL cdal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
            //creating datatable to hold the categories from database
            DataTable categoriesDT = cdal.Select();
            cmbCategory.DataSource = categoriesDT;           
            cmbCategory.DisplayMember = "category";
            cmbCategory.ValueMember = "category";
            cmbSubCategory.DataSource = categoriesDT;
            cmbSubCategory.DisplayMember = "Sub_Category";
            cmbSubCategory.ValueMember = "Sub_Category";


            //load all the products in data grid view
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get the values from product form
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.Sub_Category = cmbSubCategory.Text;
            p.description = txtDescription.Text; 
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;
            //getting id in added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            //passing the idof logged in user in added by field
            p.added_by = usr.id;
            //creating boolean method to insert data into database
            bool success = pdal.Insert(p);

            if (success == true)
            {
                MessageBox.Show("Product Added Successfully.");
                Clear();
                //refresh data grid view
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Add New Product.");
            }

        }
        public void Clear()
        {
            txtProductID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //finding the row index of the row clicked on data grid view
            int RowIndex = e.RowIndex;
            txtProductID.Text = dgvProducts.Rows[RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[RowIndex].Cells[1].Value.ToString();
            cmbCategory .Text = dgvProducts.Rows[RowIndex].Cells[2].Value.ToString();
            cmbSubCategory.Text = dgvProducts.Rows[RowIndex].Cells[3].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[RowIndex].Cells[4].Value.ToString();
            txtRate .Text= dgvProducts.Rows[RowIndex].Cells[5].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the values from product form
            p.id = int.Parse(txtProductID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.Sub_Category = cmbSubCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;

            //getting id in added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            //passing the id of logged in user in added by field
            p.added_by = usr.id;
            //creating boolean variable to upade categories and check
            bool success = pdal.Update(p);
            if (success == true)
            {
                MessageBox.Show("Product Updated Successfully.");
                Clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }

            else
            {
                MessageBox.Show("Failed to Update Product");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the id of the product from which we want to delete
            p.id = int.Parse(txtProductID.Text);
            bool success = pdal.Delete(p);
            if (success == true)
            {
                MessageBox.Show("Product Deleted Successfully");
                Clear();
                //refresh data grid view
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;

            }

            else
            {
                MessageBox.Show("Failed to Delete Product");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get the keywords
            string keywords = txtSearch.Text;
            //fill the categories based on keywords
            if (keywords != null)
            {
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
        }
    }
}
