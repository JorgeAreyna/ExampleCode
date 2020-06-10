using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;

namespace ExampleWS.ExampleWs
{
    public partial class ClCustomer
    {
        dbMetodos.DBProvider DBMethods = new dbMetodos.DBProvider();

        public Boolean InsertCustomer(String CompanyName, String ContactName, String Address, String City, String PostalCode, String idCountry, String Phone)
        {
            Boolean answer = false;
            try
            {
                DBMethods.CreateDbObjects(ConexionDB.GetConnectionString(), dbMetodos.DBProvider.Providers.SqlServer);
                DBMethods.AddParameter("CompanyName", CompanyName);
                DBMethods.AddParameter("ContactName", ContactName);
                DBMethods.AddParameter("Address", Address);
                DBMethods.AddParameter("City", City);
                DBMethods.AddParameter("PostalCode", PostalCode);
                DBMethods.AddParameter("Country", idCountry);
                DBMethods.AddParameter("Phone", Phone);
                DBMethods.ExecuteNonQuery("[dbo].[SP_insertCustomer]", CommandType.StoredProcedure, ConnectionState.Closed);
                answer = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return answer;
            }
            return answer;
        }


        public bool DeleteCustomer(int CustomerID)
        {
            bool answer = false;
            try
            {
                DBMethods.CreateDbObjects(ConexionDB.GetConnectionString(), dbMetodos.DBProvider.Providers.SqlServer);
                DBMethods.AddParameter("CustomerID", CustomerID);

                DBMethods.ExecuteNonQuery("SP_deleteCustomer", CommandType.StoredProcedure, ConnectionState.Closed);
                answer = true;
                return answer;
            }
            catch (Exception ex)
            {
                return answer;
            }

        }

        public DataSet GetCustomers()
        {
            try
            {
                DBMethods.CreateDbObjects(ConexionDB.GetConnectionString(), dbMetodos.DBProvider.Providers.SqlServer);
                DataSet DataSet = null;

                DataSet = DBMethods.GetDataSet("SP_GetCustomers", CommandType.StoredProcedure, ConnectionState.Closed);
                return DataSet;
            }
            catch (Exception ex)
            {

                return null;
            }


        }


        public bool UpdateCustomer(int CustomerID, String CompanyName, String ContactName, String Address, String City, String PostalCode, String Country, String Phone)
        {
            bool answer = false;
            try
            {
                DBMethods.CreateDbObjects(ConexionDB.GetConnectionString(), dbMetodos.DBProvider.Providers.SqlServer);
                DBMethods.AddParameter("CustomerID", CustomerID);
                DBMethods.AddParameter("CompanyName", CompanyName);
                DBMethods.AddParameter("ContactName", ContactName);
                DBMethods.AddParameter("Address", Address);
                DBMethods.AddParameter("City", City);
                DBMethods.AddParameter("PostalCode", PostalCode);
                DBMethods.AddParameter("Country", Country);
                DBMethods.AddParameter("Phone", Phone);

                DBMethods.ExecuteNonQuery("[dbo].[SP_UpdateCustomer]", CommandType.StoredProcedure, ConnectionState.Closed);
                answer = true;
                return answer;
            }
            catch (Exception ex)
            {
                return answer;
            }

        }


    }
}