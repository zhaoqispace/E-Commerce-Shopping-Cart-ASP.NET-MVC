using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Base.Models
{
    public class User
    {
        public string UserId { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Address { set; get; }
        public string PostalCode { set; get; }
        public string UserImg { set; get; }
        public string Salutation { set; get; }

        public AccountType AccountType;
    }
    public enum AccountType
    {
        customer, 
        admin
    }
    
}
