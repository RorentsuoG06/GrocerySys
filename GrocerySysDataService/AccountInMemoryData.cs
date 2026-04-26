using System;
using System.Collections.Generic;
using System.Text;
using GrocerySysModels;

namespace GrocerySysDataService
{
    public class AccountInMemoryData : IAccountDataService
    {
        public List<Accounts> adminAccount = new List<Accounts>();
        public List<Accounts> employeeAccounts = new List<Accounts>();
        public List<AccessLogs> accessLogs = new List<AccessLogs>();

        public AccountInMemoryData()
        {
            Accounts adminAccount1 = new Accounts { AccountID = Guid.NewGuid(), Username = "admin", Password = "admin123!", Role = "Admin" };
            Accounts employeeAccount1 = new Accounts { AccountID = Guid.NewGuid(), Username = "emp", Password = "emp123!", Role = "Employee" };

            adminAccount.Add(adminAccount1);
            employeeAccounts.Add(employeeAccount1);

        }

        public void Add(Accounts account)
        {
            employeeAccounts.Add(account);
        }

        public Accounts GetEmployee (string username, string password)
        {
            return employeeAccounts.FirstOrDefault(a => a.Username == username  && a.Password == password);
        }

        public Accounts GetAdmin(string username, string password)
        {
            return adminAccount.FirstOrDefault(a => a.Username == username && a.Password == password);
        }

        public bool UsernameExists(string username)
        {
            return employeeAccounts.Any(a => a.Username == username);
        }

        public bool AccountExists(string username, string password)
        {
            return employeeAccounts.Any(a => a.Username == username && a.Password == password);
        }

        public Accounts? GetByUsername(string username)
        {
            return employeeAccounts.FirstOrDefault(a => a.Username == username);
        }

        public List<Accounts> GetAccounts()
        {
            return employeeAccounts;
        }

        public bool UpdateUsername(string username, string newUsername)
        {
            var account = GetByUsername(username);

            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                account.Username = newUsername;
            }
            return true;
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            var account = GetByUsername(username);

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                account.Password = newPassword;
            }
            return true;
        }
        
        public bool RemoveEmployee(string username)
        {
            var account = GetByUsername(username);

            if (!string.IsNullOrWhiteSpace(username))
            {
                employeeAccounts.Remove(account);
            }
            return true;
        }

        public void AddAccessLog(AccessLogs acccessLog)
        {
            accessLogs.Add(acccessLog);
        }

        public List<AccessLogs> GetAccessLogs()
        {
            return accessLogs;
        }
    }

    
}
