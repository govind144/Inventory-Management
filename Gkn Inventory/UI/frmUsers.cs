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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }
        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblFirstName_Click(object sender, EventArgs e)
        {

        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            

            //getting data from UI
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.Email_id = txtEmail_id.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;

            //getting username of the logged in user
            string loggedUser = frmLogin.loggedIn;

            userBLL usr = dal.GetIDFromUsername(loggedUser);
            u.added_by = usr.id;

            //inserting data into database
            bool success = dal.Insert(u);
            //if the data is successfully inserted then the value of success will be trueelse it will be false
            if (success == true)
            {
                //data successfully inserted
                MessageBox.Show("user successfully created");
                clear();

            }
            else
            {
                //failed to inserted data
                MessageBox.Show("failed to add new user");

            }
            //refreshing data grid view
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;




        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }
        private void clear()
        {
            txtUserId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail_id.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";


        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the index of particular row
            int rowIndex = e.RowIndex;
            txtUserId.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail_id.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();



        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the values from user UI
            u.id = Convert.ToInt32(txtUserId.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.Email_id = txtEmail_id.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;

            //updating data into database
            bool success = dal.update(u);
            //if data is updated successfully then the value of succeess will be true else it will be false
            if (success == true)
            {
                //data successfully updated
                MessageBox.Show("User successfully updated");
                clear();

            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to update user");


            }
            //refreshing data grid view
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //getting user Id from UI
            u.id = Convert.ToInt32(txtUserId.Text);
            //updating data into database
            bool success = dal.Delete(u);
            //if data is deleted successfully then it will be true else it is false
            if (success == true)
            {
                //user sucessfully deleted
                MessageBox.Show("User successfully deleted");
                clear();

            }
            else
            {
                //failed to delete user
                MessageBox.Show("Failed to delete user");

            }
            //refreshing data grid view
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get keyword from textbox
            string keywords = txtSearch.Text;

            //check if the keywords has value or not
            if(keywords!=null)
            {
                //show user based on keywords
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;

            }
            else
            {
                //show all users from database
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }
    }
}

