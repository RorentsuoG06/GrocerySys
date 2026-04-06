using GrocerySysModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace GrocerySysDataService
{
    public class AccountDBData : IAccountDataService
    {
        private readonly string connectionString =
            "Data Source=localhost\\SQLEXPRESS;Initial Catalog=GroceryItems;Integrated Security=True;TrustServerCertificate=True;";

        public AccountDBData()
        {
            AddSeeds();
        }

        private void AddSeeds()
        {
            var existing = GetAccounts();

            if (existing.Count == 0)
            {
                Accounts adminAccount = new Accounts
                {
                    AccountID = Guid.NewGuid(),
                    Username = "admin",
                    Password = "admin123!",
                    Role = "Admin"
                };

                Accounts employeeAccount = new Accounts
                {
                    AccountID = Guid.NewGuid(),
                    Username = "emp",
                    Password = "emp123!",
                    Role = "Employee"
                };

                Add(adminAccount);
                Add(employeeAccount);
            }
        }

        public void Add(Accounts account)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = @"
                INSERT INTO Account (AccountID, Username, Password, Role)
                VALUES (@AccountID, @Username, @Password, @Role)";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@AccountID", account.AccountID);
            command.Parameters.AddWithValue("@Username", account.Username);
            command.Parameters.AddWithValue("@Password", account.Password);
            command.Parameters.AddWithValue("@Role", account.Role);

            command.ExecuteNonQuery();
        }

        public Accounts? GetEmployee(string username, string password)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = @"
                SELECT AccountID, Username, Password, Role
                FROM Account
                WHERE Username = @Username AND Password = @Password AND Role = 'Employee'";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Accounts
                {
                    AccountID = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Role = reader.GetString(3)
                };
            }

            return null;
        }

        public Accounts? GetAdmin(string username, string password)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = @"
                SELECT AccountID, Username, Password, Role
                FROM Account
                WHERE Username = @Username AND Password = @Password AND Role = 'Admin'";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Accounts
                {
                    AccountID = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Role = reader.GetString(3)
                };
            }

            return null;
        }

        public bool UsernameExists(string username)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = "SELECT COUNT(*) FROM Account WHERE Username = @Username";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@Username", username);

            int count = (int)command.ExecuteScalar();
            return count > 0;
        }

        public Accounts? GetByUsername(string username)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = @"
                SELECT AccountID, Username, Password, Role
                FROM Account
                WHERE Username = @Username";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@Username", username);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Accounts
                {
                    AccountID = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Role = reader.GetString(3)
                };
            }

            return null;
        }

        public List<Accounts> GetAccounts()
        {
            List<Accounts> accounts = new List<Accounts>();

            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = "SELECT AccountID, Username, Password, Role FROM Account";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                accounts.Add(new Accounts
                {
                    AccountID = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Role = reader.GetString(3)
                });
            }

            return accounts;
        }

        public bool UpdateUsername(string username, string newUsername)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = @"
                UPDATE Account
                SET Username = @NewUsername
                WHERE Username = @Username";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@NewUsername", newUsername);
            command.Parameters.AddWithValue("@Username", username);

            return command.ExecuteNonQuery() > 0;
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = @"
                UPDATE Account
                SET Password = @NewPassword
                WHERE Username = @Username";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@NewPassword", newPassword);
            command.Parameters.AddWithValue("@Username", username);

            return command.ExecuteNonQuery() > 0;
        }

        public bool RemoveEmployee(string username)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = "DELETE FROM Account WHERE Username = @Username AND Role = 'Employee'";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@Username", username);

            return command.ExecuteNonQuery() > 0;
        }

        public void AddAccessLog(AccessLogs accessLog)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = @"
                INSERT INTO AccessLogs (Username, Role, Status)
                VALUES (@Username, @Role, @Status)";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@Username", accessLog.Username);
            command.Parameters.AddWithValue("@Role", accessLog.Role);
            command.Parameters.AddWithValue("@Status", accessLog.Status);

            command.ExecuteNonQuery();
        }

        public List<AccessLogs> GetAccessLogs()
        {
            List<AccessLogs> logs = new List<AccessLogs>();

            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = "SELECT Username, Role, Status FROM dbo.AccessLogs";

            using SqlCommand command = new SqlCommand(query, sqlConnection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                logs.Add(new AccessLogs
                {
                    Username = reader.GetString(0),
                    Role = reader.GetString(1),
                    Status = reader.GetBoolean(2)
                });
            }

            return logs;
        }
    }
}