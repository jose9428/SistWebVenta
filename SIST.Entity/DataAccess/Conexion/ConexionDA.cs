using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIST.Entity.DataAccess
{
    public class ConexionDA
    {
        public SqlConnection GetSqlConnection()
        {
            string connectionString = "Data Source= (local) ;Initial Catalog=bdVenta ;Integrated Security=True;";
            var connection = new SqlConnection(connectionString);
            return connection;
        }

        public SqlConnection Open()
        {
            var connection = GetSqlConnection();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }
        public SqlConnection Close()
        {
            var connection = GetSqlConnection();
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
                
            return connection;
        }
    }
}
