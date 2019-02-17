using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using DGVPrinterHelper;
using Gkn_Inventory.BLL;
using Gkn_Inventory.DAL;

namespace Gkn_Inventory.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        DeaCustDAL dcDAL = new DeaCustDAL();
        productsDAL pDAL = new productsDAL();
        userDAL uDAL = new userDAL();
        transactionDAL tDAL = new transactionDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();
        DataTable transactionDT = new DataTable();
        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            //get the transactionType value from frmUserDashboard
            string type = FrmUserDashboard.transactionType;
            //set the value on lblTop
            lblTop.Text = type;

            //specify columns for our transactionDataTable
            transactionDT.Columns.Add("Product id");
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get the keyword from textbox
            string keyword = txtSearch.Text;
            if(keyword=="")
            {
             
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }
            //write the code to get the details and set the value on textboxes
            DeaCustBLL dc = dcDAL.SearchDealerCustomerForTransaction(keyword);
            //now transfer or set the value from DeaCustBLL to textboxes
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //get the keyword from productsearch textbox
            string keyword = txtSearchProduct.Text;
            if(keyword=="")
            {
                txtproductid.Text = "";
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtQty.Text = "";
                return;
            }
            //search the product and display on respective textboxes
            productsBLL p = pDAL.GetProductsForTransaction(keyword);
            //set the values on textboxes based on p object
            txtproductid.Text = p.id.ToString();
            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get product name, rate and quantity customer wants to buy
            int productid = int.Parse (txtproductid.Text);
            string productName = txtProductName.Text;
            decimal Rate = decimal.Parse(txtRate.Text);
            decimal Qty = decimal.Parse(txtQty.Text);

            decimal Total = Rate * Qty;

            //display the subtotal in textbox
            //get the total value from textbox
            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + Total;
            //check  whether the product is selected or not
            if(productName =="")
            {
                //display error message
                MessageBox.Show("Select the Product First. Try Again.");
            }
            else
            {
                //add product to the data grid view
                transactionDT.Rows.Add(productid,productName, Rate, Qty, Total);
                //show in dgv
                dgvAddedProducts.DataSource = transactionDT;
                //display the subtotal in textbox
                txtSubTotal.Text = subTotal.ToString();
                //clear the textboxes
                txtSearchProduct.Text = "";
                txtproductid.Text = "";
                txtProductName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "0.00";
                txtQty.Text = "0.00";
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //get the value from discount textbox
            string value = txtDiscount.Text;
            if(value =="")
            {
                //display error message
                MessageBox.Show("Please Add Discount First");
            }
            else
            {
                //get the discount in decimal value
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);

                //calculate the grandtotal based on discount
                decimal grandTotal = ((100 - discount) / 100) * subTotal;
                //display the grandtotal in textbox
                txtGrandTotal.Text = grandTotal.ToString();
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            string value = txtVat.Text;
            if (value == "")
            {
                //display error message
                MessageBox.Show("Please Add Vat First");
            }
            //check if the grandtotal has value or not if it has not value then calculate the discount first
            
            else
            {
                //calculate VAT
                //getting the VAT percent first
                decimal vat = decimal.Parse(txtVat.Text);
                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                
                decimal grandTotalWithVAT=((100+vat)/100)*previousGT;

                //displaying new grandtotal with vat
                txtGrandTotal.Text = grandTotalWithVAT.ToString();

            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            string value = txtPaidAmount.Text;
            if (value == "")
            {
                //display error message
                MessageBox.Show("Please Add Paid Amount First");
            }
            else
            {
                //get the paid amount and grandtotal
                decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
                decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

                decimal returnAmount = paidAmount - grandTotal;
                //display the return amount as well
                txtReturnAmount.Text = returnAmount.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //get the values from purchaseSales form first
            transactionsBLL transaction = new transactionsBLL();
            transaction.type = lblTop.Text;
            //get the id of dealer and customer here
            //lets get name of dealer or customer first
            string deaCustName = txtName.Text;
            DeaCustBLL dc = dcDAL.GetDeaCustIdFromName(deaCustName);
            transaction.dea_cust_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text),2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);
            //get the username of looged in user
            string username = frmLogin.loggedIn;
            userBLL u = uDAL.GetIDFromUsername(username);
            transaction.added_by = u.id;
            transaction.transactionDetails = transactionDT;

            bool success = false;
            //actual code to insert transaction and transaction detail
            using (TransactionScope scope = new TransactionScope())
            {
                int transactionID = -1;
                //create boolean value and insert transaction
                bool w = tDAL.Insert_Transaction(transaction, out transactionID);

                //use for loop to insert transaction details
                for(int i=0;i<transactionDT.Rows.Count;i++)
                {
                    // get all the details of the product
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    //get the product name and convert it to id
                    string ProductName = transactionDT.Rows[i][1].ToString();
                    productsBLL p = pDAL.GetProductIDFromName(ProductName);
                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][3].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][4].ToString()),2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //here increse or decrease product qty based on purchase or sale
                    string transactionType = lblTop.Text;

                    //lets check whether we are on purchase or sale form
                    bool x = false;
                    if(transactionType =="Purchase")
                    {
                        //Increase the product
                         x = pDAL.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }
                    else if(transactionType=="Sales")
                    {
                        //Decrease the product qty
                         x = pDAL.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);

                    }    

                    
                    //insert transactiondetail inside the database
                    bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                    success = w && x && y;
                }
                
                if (success == true)
                {
                    //Transaction complete
                    scope.Complete();

                    //code to print bill
                    DGVPrinter printer = new DGVPrinter();
                    printer.Title = "\r\n\r\n\r\n GKN INVENTORY PVT.LTD.\r\n\r\n";
                    printer.SubTitle = "Kolkata, WestBengal \r\n Phone:+91-9765XXXXXX\r\n\r\n";
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer ="Discount: "+txtDiscount .Text +"% \r\n" +"VAT: "+txtVat .Text +"% \r\n"+"Grand Total: Rs. "+txtGrandTotal .Text  + "\r\n\r\n" +"Thank you For doing business with us.";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(dgvAddedProducts);

                    MessageBox.Show("Transaction Completed Successfully.");
                    //clear the data grid view and clear all the textboxes
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();

                    txtSearch.Text = "";                    
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtSearchProduct.Text = "";
                    txtproductid.Text = "";
                    txtProductName.Text = "";
                    txtInventory.Text = "0";
                    txtRate.Text = "0";
                    txtQty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";
                }
                else
                {
                    //Transaction failed
                    MessageBox.Show("Transaction Failed.");
                }
            }
        }

        private void btnAPDelete_Click(object sender, EventArgs e)
        {
           
        }

        private void dgvAddedProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           int RowIndex = e.RowIndex;
            txtproductid .Text = dgvAddedProducts.Rows[RowIndex].Cells[0].Value.ToString();
            txtProductName .Text = dgvAddedProducts.Rows[RowIndex].Cells[1].Value.ToString();
            txtInventory .Text = dgvAddedProducts.Rows[RowIndex].Cells[2].Value.ToString();
            txtRate .Text = dgvAddedProducts.Rows[RowIndex].Cells[3].Value.ToString();
            txtQty .Text = dgvAddedProducts.Rows[RowIndex].Cells[4].Value.ToString();
        }

        private void dgvAddedProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAPDelete_Click_1(object sender, EventArgs e)
        {
           
           

        }
    }
}
