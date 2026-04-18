using GrocerySysModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GrocerySysDataService
{
    public class GroceryDBData : IGroceryDataService
    {
        private string connectionString
           = "Data Source =localhost\\SQLEXPRESS; Initial Catalog = GroceryItems; Integrated Security = True; TrustServerCertificate=True;";
        private SqlConnection sqlConnection;

        public GroceryDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
            AddSeeds();
        }

        private void AddSeeds()
        {
            var existing = GetItems();

            if (existing.Count == 0)
            {
                Items item1 = new Items { ItemId = "0001", ItemName = "Apple", ItemQuantity = 6, ItemLocation = "Shelf A" };
                Items item2 = new Items { ItemId = "0002", ItemName = "Mango", ItemQuantity = 7, ItemLocation = "Shelf B" };
                Items item3 = new Items { ItemId = "0003", ItemName = "Orange", ItemQuantity = 8, ItemLocation = "Shelf C" };

                AddItem(item1);
                AddItem(item2);
                AddItem(item3);
            }
        }

        public void AddItem(Items item)
        {
            var insertStatement = "INSERT INTO Items (ItemId, ItemName, ItemQuantity, ItemLocation) VALUES (@ItemId, @ItemName, @ItemQuantity, @ItemLocation)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@ItemId", item.ItemId);
            insertCommand.Parameters.AddWithValue("@ItemName", item.ItemName);
            insertCommand.Parameters.AddWithValue("@ItemQuantity", item.ItemQuantity);
            insertCommand.Parameters.AddWithValue("@ItemLocation", item.ItemLocation);

            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<Items> GetItems()
        {
            string selectStatement = "SELECT ItemId, ItemName, ItemQuantity, ItemLocation FROM Items";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var items = new List<Items>();
            while (reader.Read())
            {
                Items item = new Items
                {
                    ItemId = reader["ItemId"].ToString(),
                    ItemName = reader["ItemName"].ToString(),
                    ItemQuantity = int.Parse(reader["ItemQuantity"].ToString()),
                    ItemLocation = reader["ItemLocation"].ToString()
                };

                items.Add(item);
            }

            sqlConnection.Close();
            return items;
        }

        public Items FindItem(string id)
        {
            string selectStatement = "SELECT ItemId, ItemName, ItemQuantity, ItemLocation FROM Items WHERE ItemId = @ItemId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@ItemId", id);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            Items item = null;
            if (reader.Read())
            {
                item = new Items
                {
                    ItemId = reader["ItemId"].ToString(),
                    ItemName = reader["ItemName"].ToString(),
                    ItemQuantity = int.Parse(reader["ItemQuantity"].ToString()),
                    ItemLocation = reader["ItemLocation"].ToString()
                };
            }

            sqlConnection.Close();
            return item;
        }

        public bool UpdateItemName(string id, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                return false;

            string updateStmt = "UPDATE Items SET ItemName = @ItemName WHERE ItemId = @ItemId";
            SqlCommand cmd = new SqlCommand(updateStmt, sqlConnection);
            cmd.Parameters.AddWithValue("@ItemId", id);
            cmd.Parameters.AddWithValue("@ItemName", newName);

            sqlConnection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rowsAffected > 0;
        }

        public bool UpdateItemQuantity(string id, int? newQuantity)
        {
            if (!newQuantity.HasValue)
                return false; 

            string updateStmt = "UPDATE Items SET ItemQuantity = @ItemQuantity WHERE ItemId = @ItemId";
            SqlCommand cmd = new SqlCommand(updateStmt, sqlConnection);
            cmd.Parameters.AddWithValue("@ItemId", id);
            cmd.Parameters.AddWithValue("@ItemQuantity", newQuantity.Value);

            sqlConnection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rowsAffected > 0;
        }

        public bool UpdateItemLocation(string id, string newLocation)
        {
            if (string.IsNullOrWhiteSpace(newLocation))
                return false;

            string updateStmt = "UPDATE Items SET ItemLocation = @ItemLocation WHERE ItemId = @ItemId";
            SqlCommand cmd = new SqlCommand(updateStmt, sqlConnection);
            cmd.Parameters.AddWithValue("@ItemId", id);
            cmd.Parameters.AddWithValue("@ItemLocation", newLocation);

            sqlConnection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rowsAffected > 0;
        }

        public bool DeleteItem(string id)
        {
            var item = FindItem(id);
            if (item == null)
                return false;

            string deleteStatement = "DELETE FROM Items WHERE ItemId = @ItemId";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            deleteCommand.Parameters.AddWithValue("@ItemId", id);

            sqlConnection.Open();
            int rowsAffected = deleteCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return rowsAffected > 0;
        }

        public List<Items> GetLowStockItems()
        {
            string selectStatement = "SELECT ItemId, ItemName, ItemQuantity, ItemLocation FROM Items WHERE ItemQuantity < 5";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            List<Items> itemsList = new List<Items>();
            while (reader.Read())
            {
                Items item = new Items
                {
                    ItemId = reader["ItemId"].ToString(),
                    ItemName = reader["ItemName"].ToString(),
                    ItemQuantity = int.Parse(reader["ItemQuantity"].ToString()),
                    ItemLocation = reader["ItemLocation"].ToString()
                };

                itemsList.Add(item);
            }

            sqlConnection.Close();
            return itemsList;
        }

        public bool HasLowStockItems()
        {
            string query = "SELECT COUNT(1) FROM Items WHERE ItemQuantity < 5";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}