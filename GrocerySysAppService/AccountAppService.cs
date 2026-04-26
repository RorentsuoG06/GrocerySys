using GrocerySysDataService;
using GrocerySysModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocerySysAppService
{
    public class AccountAppService
    {
        AccountDataService dataService = new AccountDataService(new AccountInMemoryData());

        public Accounts Authenticate(string username, string password)
        {
            if (username == "admin")
            {
                return dataService.GetAdmin(username, password);
            }
            return dataService.GetEmployee(username, password);
        }

        public bool Register(Accounts newAccount)
        {
            if (dataService.UsernameExists(newAccount.Username))
            {
                return false;
            }

            dataService.Add(newAccount);
            return true;
        }

        public bool ValidateUsername(Accounts newAccount)
        {
            if (dataService.UsernameExists(newAccount.Username))
            {
                return false;
            }
            return true;
        }

        public bool ValidateAccount(string username, string password)
        {
            if (dataService.AccountExists(username, password))
            {
                return true;
            }
            return false;
        }
        public List<Accounts> GetAccounts()
        {
            return dataService.GetAccounts();
        }

        public bool GetUsername(string username)
        {
            var account = dataService.GetByUsername(username);

            if(account == null)
            {
                return false;
            }
            return true;
        }

        public bool UpdateUsername(string username, string newUsername)
        {
            return dataService.UpdateUsername(username, newUsername);
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            return dataService.UpdatePassword(username, newPassword);
        }

        public bool RemoveEmployee(string username)
        {
            return dataService.RemoveEmployee(username);
        }

        public void AddAccessLog(AccessLogs accessLog)
        {
            dataService.AddAccessLog(accessLog);
        }

        public List<AccessLogs> GetAccessLogs()
        {
            return dataService.GetAccessLogs();
        }
    }
}
