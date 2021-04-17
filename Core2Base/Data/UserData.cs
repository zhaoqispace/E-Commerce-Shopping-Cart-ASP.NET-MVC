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
        public static User GetUserInfo(string Email)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"Select * FROM [User] where Email = @Email";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", Email);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User()
                    {
                        UserId = Convert.ToString(reader["UserId"]),
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        DateOfBirth =(DateTime)reader["DateOfBirth"],
                        Password = (string)reader["Password"],
                        Salutation = (string)reader["Salutation"],
                        Address = (string)reader["Address"],
                        PostalCode = (string)reader["PostalCode"],
                        UserImg = (string)reader["UserImg"]
                    };
                }
                return user;
            }
        }

        public static List<User> UserAuth()
        {
            return null; // placeholder
        }
    }
}