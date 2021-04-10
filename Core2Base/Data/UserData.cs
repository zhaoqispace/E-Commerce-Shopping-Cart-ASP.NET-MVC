using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Core2Base.Models;

namespace Core2Base.Data
{
    public class UserData : Data
    {
        public static List<User> GetUserInfo()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"Select * FROM User";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User()
                    {
                        UserId = (string)reader["UserId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        Password = (string)reader["Password"],
                        AddressId = (string)reader["AddressId"],
                        UserImg = (string)reader["UserImg"],
                    };
                    users.Add(user);
                }
                return users;
            }
        }

        public static List<User> UserAuth()
        {
            return null; // placeholder
        }
    }
}