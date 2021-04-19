using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Core2Base.Data;

namespace Core2Base.Models
{
    public class User
    {
      
        public string UserId { get;set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string UserImg { get; set; }
        public string DateOfBirth { get; set; }
        public string Salutation { get; set; }

        public int SaveDetails()
        {
            SqlConnection conn = new SqlConnection("Server=(local);Database=CA2db_VersionFinal; Integrated Security=true");
            SqlCommand checkUserEmail = new SqlCommand("SELECT * FROM [User] WHERE (email = '" + Email + "')", conn);
            conn.Open();
            SqlDataReader reader = checkUserEmail.ExecuteReader();
            if (reader.HasRows)
            {
                conn.Close();
                return 0;
            }
            else
            {
                conn.Close();

                string query = "INSERT INTO [User](FirstName, LastName, Email, Password, Gender, DateOfBirth, Salutation, Address, PostalCode) values (@FirstName, @LastName, @Email, @Password, @Gender, @DateOfBirth, @Salutation, @Address, @PostalCode)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                cmd.Parameters.AddWithValue("@Salutation", Salutation);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@PostalCode", PostalCode);

                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();

                return i;
            }
        }

    }
    public enum AccountType
    {
        customer, 
        admin
    }
    
}
