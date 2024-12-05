using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace asm2_ddb
{
    internal class Modify
    {
        public Modify() 
        {
            
        }
      
        SqlCommand sqlCommand;//dung de truy van cac cau lenh insert ,update ,delete....
        SqlDataReader dataReader;// dung de doc du lieu trong bang

        public List <Acount> Acount (string query)//check tai khoan
        {
            List<Acount> Acount   = new List<Acount>();
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while(dataReader.Read())
                {
                   Acount.Add(new Acount(dataReader.GetString(0),dataReader.GetString(1)));
                }
                sqlConnection.Close();  
            }
            return Acount;
        }
        public void  Command(string query)// dung de dang ki tai khoan
        {
            using  ( SqlConnection sqlConnection = Connection.GetSqlConnection())
            {

                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }

        }
    }
}
