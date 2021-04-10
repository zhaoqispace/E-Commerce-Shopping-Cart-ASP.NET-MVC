using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Base.Models
{
    public class User
    {
        public string UserId;

        public string FirstName;

        public string LastName;

        public string Email;

        public string Password;

        public string AddressId;

        public string UserImg;

        public AccountType AccountType;

    }

    public enum AccountType
    {
        customer, 
        admin
    }
    
}
