using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gkn_Inventory.DAL;

namespace Gkn_Inventory.UI
{
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
            InitializeComponent();
        }
        categoriesDAL cdal = new categoriesDAL();
        productsDAL pdal = new productsDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            //display the categories in combobox
            DataTable cDt = cdal.Select();
            cmbCategories.DataSource = cDt;
            //give the value member and display member for combobox
            cmbCategories.DisplayMember = "category";
            cmbCategories.ValueMember = "category";
            cmbSubCategory.DataSource = cDt;
            //give the value member and display member for combobox
            cmbSubCategory.DisplayMember = "Sub_Category";
            cmbSubCategory.ValueMember = "Sub_Category";
            //display all the products in dgv when form is loded
            DataTable pdt = pdal.Select();
            dgvProducts.DataSource = pdt;
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dispay all the products based on selected category
            string category = cmbCategories.Text;
            DataTable dt = pdal.DisplayProductsByCategory(category);
            dgvProducts.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void cmbSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dispay all the products based on selected Sub category
            string Sub_Category = cmbSubCategory.Text;
            DataTable dt = pdal.DisplayProductsBySub_Category(Sub_Category);
            dgvProducts.DataSource = dt;
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }
    }
}
