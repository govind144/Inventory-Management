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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //code to close the form
            this.Close();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUsername.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();
            //checking the login credentials
            bool sucess = dal.loginCheck(l);
            if(sucess==true)
            {
                //login successfull
                MessageBox.Show("Login Successfull");
                loggedIn = l.username;
                //Need to open resp. forms based on user type
                switch(l.user_type )
                {
                    case "Admin":
                        {
                            //display Admin dashboard
                            FrmAdminDashboard admin = new FrmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        }
                        break;

                    case "User":
                        {
                            //display user dashboard
                            FrmUserDashboard user = new FrmUserDashboard();
                            user.Show();
                            this.Hide();

                        }
                        break;

                    default:
                        {
                            //display an error message
                            MessageBox.Show("Invalid User Type");
                        }
                        break;
                }
            }
            else
            {
                //login failed
                MessageBox.Show("Login Failed! Try Again.");
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(txtPassword.UseSystemPasswordChar ==true)
            {
                //show password
                txtPassword.UseSystemPasswordChar = false;

            }
            else
            {
                //hide password
                txtPassword.UseSystemPasswordChar = true;
            }

        }
    }
}
