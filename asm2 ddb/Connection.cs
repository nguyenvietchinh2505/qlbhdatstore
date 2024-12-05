using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace asm2_ddb
{
    internal class Connection
    {

        private static string connectionString = @"Data Source=LAPTOP-C6H2FCNV\SQLEXPRESS;Initial Catalog=QLBH12;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
        
        
    }


}
