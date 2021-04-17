using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Base.Data
{
    public class PaymentData:Data
    {
        public static int InsertCardInfo(string cardNumber, string userID)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT * FROM [UserPayment] WHERE UserID = '" + userID + "' and CreditCardNum ='"+ cardNumber+"'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    conn.Close();
                    return 0;
                }
                else
                {
                    conn.Close();

                    string query = "INSERT INTO [UserPayment](CreditCardNum,UserID) values ('" + cardNumber + "','" + userID+"')";
                    SqlCommand cmd2 = new SqlCommand(query, conn);
                    conn.Open();
                    int i = cmd2.ExecuteNonQuery();
                    conn.Close();

                    return i;
                }
            }
        }
    }
}
