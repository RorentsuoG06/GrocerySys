using GrocerySysModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocerySysDataService
{
    public class AccountDataService
    {
        IAccountDataService _dataService;
        public AccountDataService(IAccountDataService accountDataService)
        {
            _dataService = accountDataService;
        }

        public void Add(Accounts account)
        {
           _dataService.Add(account);
        }

        public Accounts GetEmployee(string username, string password)
        {
            return _dataService.GetEmployee(username, password);
        }

        public Accounts GetAdmin(string username, string password)
        {
            return _dataService.GetAdmin(username, password);
        }

        public bool UsernameExists(string username)
        {
            return _dataService.UsernameExists(username);
        }

        public bool AccountExists(string username, string password)
        {
            return _dataService.AccountExists(username, password);
        }

        public Accounts? GetByUsername(string username)
        {
            return _dataService.GetByUsername(username);
        }

        public List<Accounts> GetAccounts()
        {
            return _dataService.GetAccounts();
        }

        public bool UpdateUsername(string username, string newUsername)
        {
            return _dataService.UpdateUsername(username, newUsername);
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            return _dataService.UpdatePassword(username, newPassword);
        }

        public bool RemoveEmployee(string username)
        {
            return _dataService.RemoveEmployee(username);
        }

        public void AddAccessLog(AccessLogs acccessLog)
        {
            _dataService.AddAccessLog(acccessLog);
        }

        public List<AccessLogs> GetAccessLogs()
        {
            return _dataService.GetAccessLogs();
        }
    }
}
