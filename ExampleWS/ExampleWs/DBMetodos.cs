using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace ExampleWS.ExampleWs
{

    public partial class dbMetodos
    {
        public class DBProvider
        {
            #region private members
            private string _connectionstring = "";
            private DbConnection _connection;
            private DbCommand _command;

            internal void CreateDbObjects(object p, object oracle)
            {
                throw new NotImplementedException();
            }

            private DbProviderFactory _factory;
            #endregion

            #region properties

            /// <summary>
            /// Gets or Sets the connection string for the database
            /// </summary>
            public string Connectionstring
            {
                get
                {
                    return _connectionstring;
                }
                set
                {
                    if (value != "")
                    {
                        _connectionstring = value;
                    }
                }
            }

            /// <summary>
            /// Gets the connection object for the database
            /// </summary>
            public DbConnection Connection
            {
                get
                {
                    return _connection;
                }
            }

            /// <summary>
            /// Gets the command object for the database
            /// </summary>
            public DbCommand command
            {
                get
                {
                    return _command;
                }
            }

            #endregion

            #region methods

            /// <summary>
            /// Determina el proveedor correcto para usar y configura la conexión y el comando
            /// Objetos para su uso en otros métodos.
            /// </summary>
            /// <param name="connectString">La cadena de conexión completa a la base de datos.</param>
            /// <param name="providerlist">El valor de enumeración de los proveedores de dbutilities.Providers</param>
            public void CreateDbObjects(string connectString, Providers providerList)
            {
                //CreateDBObjects(connectString, providerList, null);
                switch (providerList)
                {
                    case Providers.SqlServer:
                        _factory = SqlClientFactory.Instance;
                        break;
                    case Providers.OleDb:
                        _factory = OleDbFactory.Instance;
                        break;
                    case Providers.Odbc:
                        _factory = OdbcFactory.Instance;
                        break;
                }

                _connection = _factory.CreateConnection();
                _command = _factory.CreateCommand();

                _connection.ConnectionString = connectString;
                _command.Connection = Connection;
            }

            #region parameters

            /// <summary>
            /// Creates a parameter and adds it to the command object
            /// </summary>
            /// <param name="name">The parameter name</param>
            /// <param name="value">The paremeter value</param>
            /// <returns></returns>
            public int AddParameter(string name, object value)
            {
                DbParameter parm = _factory.CreateParameter();
                parm.ParameterName = name;
                parm.Value = value;
                return command.Parameters.Add(parm);
            }

            /// <summary>
            /// Creates a parameter and adds it to the command object
            /// </summary>
            /// <param name="parameter">A parameter object</param>
            /// <returns></returns>
            public int AddParameter(DbParameter parameter)
            {
                return command.Parameters.Add(parameter);
            }

            #endregion

            #region transactions

            /// <summary>
            /// Starts a transaction for the command object
            /// </summary>
            private void BeginTransaction()
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                command.Transaction = Connection.BeginTransaction();
            }

            /// <summary>
            /// Commits a transaction for the command object
            /// </summary>
            private void CommitTransaction()
            {
                command.Transaction.Commit();
                Connection.Close();
            }

            /// <summary>
            /// Rolls back the transaction for the command object
            /// </summary>
            private void RollbackTransaction()
            {
                command.Transaction.Rollback();
                Connection.Close();
            }

            #endregion

            #region execute database functions

            /// <summary>
            /// Executes a statement that does not return a result set, such as an INSERT, UPDATE, DELETE, or a data definition statement
            /// </summary>
            /// <param name="query">The query, either SQL or Procedures</param>
            /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
            /// <param name="connectionstate">The connection state</param>
            /// <returns>An integer value</returns>
            public int ExecuteNonQuery(string query, CommandType commandtype, ConnectionState connectionstate)
            {
                command.CommandText = query;
                command.CommandType = commandtype;
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();

                    }

                    BeginTransaction();

                    var i = command.ExecuteNonQuery();
                    return i;
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    return 0;
                }
                finally
                {
                    CommitTransaction();
                    //command.Parameters.Clear();

                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                        Connection.Dispose();
                        //command.Dispose();
                    }
                }

            }

            /// <summary>
            /// Executes a statement that returns a single value.
            /// If this method is called on a query that returns multiple rows and columns, only the first column of the first row is returned.
            /// </summary>
            /// <param name="query">The query, either SQL or Procedures</param>
            /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
            /// <param name="connectionstate">The connection state</param>
            /// <exception cref="OracleException"></exception>
            /// <returns>An object that holds the return value(s) from the query</returns>
            public string ExecuteScaler(string query, CommandType commandtype, ConnectionState connectionstate)
            {
                command.CommandText = query;
                command.CommandType = commandtype;
                object obj;
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                    BeginTransaction();
                    obj = (string)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    //RollbackTransaction();
                    obj = ex.Message;
                }
                finally
                {
                    //CommitTransaction();
                    command.Parameters.Clear();

                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                        Connection.Dispose();
                        command.Dispose();
                    }
                }

                return (string)obj;
            }

            /// <summary>
            /// Executes a SQL statement that returns a result set.
            /// </summary>
            /// <param name="query">The query, either SQL or Procedures</param>
            /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
            /// <param name="connectionstate">The connection state</param>
            /// <returns>A datareader object</returns>
            public DbDataReader ExecuteReader(string query, CommandType commandtype, ConnectionState connectionstate)
            {
                command.CommandText = query;
                command.CommandType = commandtype;
                DbDataReader reader;
                try
                {
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    reader = connectionstate == ConnectionState.Open ? command.ExecuteReader(CommandBehavior.CloseConnection) : command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    command.Parameters.Clear();
                }

                return reader;
            }

            /// <summary>
            /// Generates a dataset
            /// </summary>
            /// <param name="query">The query, either SQL or Procedures</param>
            /// <param name="commandtype">The command type, text, storedprocedure, or tabledirect</param>
            /// <param name="connectionstate">The connection state</param>
            /// <returns>A dataset containing data from the database</returns>
            public DataSet GetDataSet(string query, CommandType commandtype, ConnectionState connectionstate)
            {
                var adapter = _factory.CreateDataAdapter();
                command.CommandText = query;
                command.CommandType = commandtype;
                adapter.SelectCommand = command;
                var ds = new DataSet();
                try
                {
                    adapter.Fill(ds);

                }
                catch (Exception ex)
                {
                    var w32ex = ex as Win32Exception;
                    int code = 0;
                    if (w32ex == null)
                    {
                        w32ex = ex.InnerException as Win32Exception;
                    }
                    if (w32ex != null)
                    {
                        code = w32ex.ErrorCode;
                        // do stuff
                    }

                    var dtError = new DataTable();
                    var dsError = new DataSet();
                    dtError.Columns.Add("K_IdError", typeof(string));
                    dtError.Columns.Add("K_MsgError", typeof(string));
                    dtError.Rows.Add(code.ToString(), ex.Message);
                    ds.Tables.Add(dtError);
                }
                finally
                {
                    command.Parameters.Clear();

                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                        Connection.Dispose();
                        command.Dispose();
                    }
                }
                return ds;
            }

            #endregion

            #endregion

            #region enums

            /// <summary>
            /// A list of data providers
            /// </summary>
            public enum Providers
            {
                SqlServer,
                OleDb,
                Odbc,
            }

            #endregion
        }
    }

}