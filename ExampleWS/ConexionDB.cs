using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ExampleWS
{
    public static class ConexionDB
    {
        public static string GetConnectionString() =>
            ConfigurationManager.ConnectionStrings["SqlConnect"].ToString();
    }

}