using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace EleccionesWS
{
    public class ConnectionFactory
    {
        public static SqlConnection getConnection()
        {
            string connString = "Data Source=(local);Initial Catalog=elecciones;Integrated Security=SSPI;";
            return new SqlConnection(connString);
        } 
    }
}