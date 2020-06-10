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
    public  partial class ClCountries
    {
        dbMetodos.DBProvider DBMethods = new dbMetodos.DBProvider();

        public DataSet GetConutries()
        {
            DataSet DataSet = null;
            DBMethods.CreateDbObjects(ConexionDB.GetConnectionString(), dbMetodos.DBProvider.Providers.SqlServer);


            DataSet = DBMethods.GetDataSet("SP_GetContries", CommandType.StoredProcedure, ConnectionState.Closed);
            return DataSet;
        }

    }
}