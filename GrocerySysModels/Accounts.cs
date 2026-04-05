using System;
using System.Collections.Generic;
using System.Text;

namespace GrocerySysModels
{
    public class Accounts
    {
        public Guid AccountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
