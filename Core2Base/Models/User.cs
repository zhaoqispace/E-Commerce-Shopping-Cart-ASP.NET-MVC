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
      
        public Guid UserId { get;set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string UserImg { get; set; }
        public string Salutation { get; set; }

        public AccountType AccountType;
    
        public int SaveDetails()
        {
            SqlConnection conn = new SqlConnection("Server=(local);Database=CA2db_Version3; Integrated Security=true");
            string query = "INSERT INTO [User](FirstName, LastName, Email, Password, Gender, Salutation, Address) values ('" + FirstName + "','" + LastName + "','" + Email + "','" + Password + "','" + Gender + "','" + Salutation + "','" + Address + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;

        }

    }
    public enum AccountType
    {
        customer, 
        admin
    }
    
}
