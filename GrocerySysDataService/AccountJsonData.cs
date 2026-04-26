using GrocerySysModels;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace GrocerySysDataService
{
    public class AccountJsonData : IAccountDataService
    {
        public List<Accounts> adminAccount = new List<Accounts>();
        public List<Accounts> employeeAccounts = new List<Accounts>();
        public List<AccessLogs> accessLogs = new List<AccessLogs>();
        private string _jsonFileName;

        public AccountJsonData()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/Accounts.json";

            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
           RetrieveDataFromJsonFile();

            if (adminAccount.Count <= 0 && employeeAccounts.Count <= 0)
            {
                Accounts adminAccount1 = new Accounts { AccountID = Guid.NewGuid(), Username = "admin", Password = "admin123!", Role = "Admin" };
                Accounts employeeAccount1 = new Accounts { AccountID = Guid.NewGuid(), Username = "emp", Password = "emp123!", Role = "Employee" };

                adminAccount.Add(adminAccount1);
                employeeAccounts.Add(employeeAccount1);
                SaveDataToJsonFile();
            }
        }
        
        private void RetrieveDataFromJsonFile()
        {
            using (var jsonFileReader = File.OpenText(_jsonFileName))
            {
                var jsonContent = jsonFileReader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    adminAccount = new List<Accounts>();
                    employeeAccounts = new List<Accounts>();
                    accessLogs = new List<AccessLogs>();
                    return;
                }

                var data = JsonSerializer.Deserialize<AccountJsonStore>(
                    jsonContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                adminAccount = data?.AdminAccounts ?? new List<Accounts>();
                employeeAccounts = data?.EmployeeAccounts ?? new List<Accounts>();
                accessLogs = data?.AccessLogs ?? new List<AccessLogs>();
            }
        }

        private void SaveDataToJsonFile()
        {
            var accountData = new AccountJsonStore
            {
                AdminAccounts = adminAccount,
                EmployeeAccounts = employeeAccounts,
                AccessLogs = accessLogs
            };

            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    ,accountData);
            }
        }

        public class AccountJsonStore
        {
            public List<Accounts> AdminAccounts { get; set; } = new List<Accounts>();
            public List<Accounts> EmployeeAccounts { get; set; } = new List<Accounts>();
            public List<AccessLogs> AccessLogs { get; set; } = new List<AccessLogs>();
        }

        public void Add(Accounts account)
        {
            employeeAccounts.Add(account);
            SaveDataToJsonFile();
        }

        public Accounts GetEmployee(string username, string password)
        {
            return employeeAccounts.FirstOrDefault(a => a.Username == username && a.Password == password);
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
                SaveDataToJsonFile();
            }
            return true;
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            var account = GetByUsername(username);

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                account.Password = newPassword;
                SaveDataToJsonFile();
            }
            return true;
        }

        public bool RemoveEmployee(string username)
        {
            var account = GetByUsername(username);

            if (account == null)
            {
                return false;
            }

            employeeAccounts.Remove(account);
            SaveDataToJsonFile();
            return true;
        }

        public void AddAccessLog(AccessLogs acccessLog)
        {
            accessLogs.Add(acccessLog);
            SaveDataToJsonFile();
        }

        public List<AccessLogs> GetAccessLogs()
        {
            return accessLogs;
        }
    }
}

