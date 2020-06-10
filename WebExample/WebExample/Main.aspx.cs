using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace WebExample
{
    public partial class Main : System.Web.UI.Page
    {
   
        WStest.ExampleWSJSoap Wsexample01 = new WStest.ExampleWSJSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCountry();
            LoadCustomers();
        }


        public void LoadCountry()
        {
            Cmb_Country.DataSource = Wsexample01.GetConutries();
            Cmb_Country.DataValueField = "CountryID";
            Cmb_Country.DataTextField = "CountryDescription";
            Cmb_Country.DataBind();

        }

        public void LoadCustomers()
        {

            Gv_Customers.DataSource = Wsexample01.GetCustomers();
            Gv_Customers.DataBind();

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            try
            {
                Boolean success;
                String CompanyName, ContactName, Address, City, PostalCode, idCountry, Phone, idcustomer;
                idcustomer = HidCustomer.Value;
                CompanyName = Txt_CompanyName.Text;
                ContactName = Txt_ContactName.Text;
                Address = Txt_Address.Text;
                City = Txt_City.Text;
                PostalCode = Txt_Zip.Text;
                idCountry = Cmb_Country.SelectedValue;
                Phone = Txt_phone.Text;

                if (hdAction.Value == "1"  )
                {

                    hdAction.Value = "0";
                    success = Wsexample01.UpdateCustomer(Convert.ToInt32(idcustomer), CompanyName, ContactName, Address, City, PostalCode, idCountry, Phone);
                    if (success)
                    {
                        LoadCustomers();
                    }
                }
                else
                {
   
                    success = Wsexample01.InsertCustomer(CompanyName, ContactName, Address, City, PostalCode, idCountry, Phone);
                    if (success)
                    {
                        LoadCustomers();
                    }                
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void Gv_Customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gv_Customers.SelectedRow;
            HidCustomer.Value = Gv_Customers.SelectedRow.Cells[1].Text;
            hdAction.Value = "1";

            Txt_CompanyName.Text = Gv_Customers.SelectedRow.Cells[2].Text;
            Txt_ContactName.Text = Gv_Customers.SelectedRow.Cells[3].Text;
            Txt_Address.Text = Gv_Customers.SelectedRow.Cells[4].Text;
            Txt_City.Text = Gv_Customers.SelectedRow.Cells[5].Text;
            Txt_Zip.Text = Gv_Customers.SelectedRow.Cells[6].Text;
            Cmb_Country.SelectedValue = Gv_Customers.SelectedRow.Cells[7].Text ;
            Txt_phone.Text= Gv_Customers.SelectedRow.Cells[8].Text;


        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string idcustomer = "";
            Boolean success;

            idcustomer = HidCustomer.Value.Trim();
            if( idcustomer.All(char.IsDigit))
            {
                success = Wsexample01.DeleteCustomer(Convert.ToInt16(idcustomer));
                if (success)
                {
                    LoadCustomers();
                }
            }
        }
    }
}