using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using ExampleWS.ExampleWs;

namespace ExampleWS
{
    /// <summary>
    /// Descripción breve de ExampleWSJ
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // [System.Web.Script.Services.ScriptService]
    public class ExampleWSJ : System.Web.Services.WebService
    {


    
      

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }


        #region countries 
        [WebMethod]
        public DataSet GetConutries()
        {
            try
            {
                ClCountries clCountry = new ClCountries();
                return clCountry.GetConutries();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region  CustomerMethods

        [WebMethod]
        public DataSet GetCustomers()
        {
            try
            {
                ClCustomer ClCustomers = new ClCustomer();
                return ClCustomers.GetCustomers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public Boolean InsertCustomer(String CompanyName, String ContactName, String Address, String City, String PostalCode, String idCountry, String Phone)
        {
            try
            {
                ClCustomer ClCustomers = new ClCustomer();
                return ClCustomers.InsertCustomer(CompanyName, ContactName, Address, City, PostalCode, idCountry, Phone);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool DeleteCustomer(int CustomerID)
        {
            try
            {
                ClCustomer ClCustomers = new ClCustomer();
                return ClCustomers.DeleteCustomer(CustomerID);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [WebMethod]
        public Boolean UpdateCustomer(int CustomerID, String CompanyName, String ContactName, String Address, String City, String PostalCode, string Country, String Phone)
        {
            {
                try
                {
                    ClCustomer ClCustomers = new ClCustomer();
                    return ClCustomers.UpdateCustomer(CustomerID, CompanyName, ContactName, Address, City, PostalCode, Country, Phone);
                }
                catch (Exception)
                {

                    return false;
                }

            }

        }

        #endregion

    }
}
