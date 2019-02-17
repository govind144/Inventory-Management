using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gkn_Inventory.UI;

namespace Gkn_Inventory
{
	public partial class FrmUserDashboard : Form
	{
		public FrmUserDashboard()
		{
			InitializeComponent();
		}
        //set a public static methodto specify whether the form is purchase or sales
        public static string transactionType;
        private void FrmUserDashboard_Load(object sender, EventArgs e)
        {
            lblloggedInUser.Text = frmLogin.loggedIn;
        }

        private void FrmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set value on transactionType static method
            transactionType = "Purchase";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();
            
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set the value to transactionType method to sales
            transactionType = "Sales";
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();
            
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }
    }
}
