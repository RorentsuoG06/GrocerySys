using GrocerySysModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocerySysDataService
{
    public interface IAccountDataService
    {
        public void Add(Accounts account);
        public Accounts GetEmployee(string username, string password);
        public Accounts GetAdmin(string username, string password);
        public bool UsernameExists(string username);
        public bool AccountExists(string username, string password);
        public Accounts? GetByUsername(string username);
        public List<Accounts> GetAccounts();
        public bool UpdateUsername(string username, string newUsername);
        public bool UpdatePassword(string username, string newPassword);
        public bool RemoveEmployee(string username);
        public void AddAccessLog(AccessLogs acccessLog);
        public List<AccessLogs> GetAccessLogs();
       
    }
}
