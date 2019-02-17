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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcdal = new DeaCustDAL();

        userDAL uDal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            dc.type = cmbType.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            //getting id in added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUser);
            //passing the id of logged in user in added by field
            dc.added_by = usr.id;
            //creating boolean method to insert data into database
            bool success = dcdal.Insert(dc);

            if (success == true)
            {
                MessageBox.Show("Dealer or Customer Added Successfully.");
                Clear();
                //refresh data grid view
                DataTable dt = dcdal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Add New Dealer or customer.");
            }

        }
        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact .Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            //refresh data grid view
            DataTable dt = dcdal.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //finding the row index of the row clicked on data grid view
            int RowIndex = e.RowIndex;
            txtDeaCustID .Text = dgvDeaCust.Rows[RowIndex].Cells[0].Value.ToString();
            cmbType .Text= dgvDeaCust.Rows[RowIndex].Cells[1].Value.ToString();
            txtName .Text = dgvDeaCust.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail .Text= dgvDeaCust.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact .Text = dgvDeaCust.Rows[RowIndex].Cells[4].Value.ToString();
            txtAddress .Text = dgvDeaCust.Rows[RowIndex].Cells[5].Value.ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the values from product form
            dc.id = int.Parse(txtDeaCustID.Text);
            dc.type = cmbType.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            //getting id in added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUser);
            //passing the id of logged in user in added by field
            dc.added_by = usr.id;
            //creating boolean variable to upade dea_cust and check
            bool success = dcdal.Update(dc);
            if (success == true)
            {
                MessageBox.Show("Dealer or Customer Updated Successfully.");
                Clear();
                DataTable dt = dcdal.Select();
                dgvDeaCust.DataSource = dt;
            }

            else
            {
                MessageBox.Show("Failed to Update Dealer or Customer");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the id of the category from which we want to delete
            dc.id = int.Parse(txtDeaCustID.Text);
            bool success = dcdal.Delete(dc);
            if (success == true)
            {
                MessageBox.Show("Dealer or Customer Deleted Successfully");
                Clear();
                //refresh data grid view
                DataTable dt = dcdal.Select();
                dgvDeaCust.DataSource = dt;

            }

            else
            {
                MessageBox.Show("Failed to Delete Dealer or Customer");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get the keywords
            string keywords = txtSearch.Text;
            //fill the categories based on keywords
            if (keywords != null)
            {
                DataTable dt = dcdal.Search(keywords);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                DataTable dt = dcdal.Select();
                dgvDeaCust.DataSource = dt;
            }
        }
    }
}
