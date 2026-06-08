using GrocerySysModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
                Items item1 = new Items { ItemId = "0001", ItemName = "Apple", Department = ProductDepartment.Produce, ItemLocation = "Shelf A", ItemQuantity = 6, WeightValue = 1, Unit = MeasurementUnit.Kilograms, CostPrice = 50m, SellingPrice = 75m };
                Items item2 = new Items { ItemId = "0002", ItemName = "Mango", Department = ProductDepartment.Produce, ItemLocation = "Shelf B", ItemQuantity = 7, WeightValue = 500, Unit = MeasurementUnit.Grams, CostPrice = 60m, SellingPrice = 90m };
                Items item3 = new Items { ItemId = "0003", ItemName = "Orange", Department = ProductDepartment.Produce, ItemLocation = "Shelf C", ItemQuantity = 8, WeightValue = 1, Unit = MeasurementUnit.Kilograms, CostPrice = 40m, SellingPrice = 60m };

                AddItem(item1);
                AddItem(item2);
                AddItem(item3);
            }
        }

        public void AddItem(Items item)
        {
            var insertStatement = @"INSERT INTO Items 
                (ItemId, ItemName, ItemQuantity, ItemLocation, Department, WeightValue, Unit, CostPrice, SellingPrice, ExpirationDate) 
                VALUES 
                (@ItemId, @ItemName, @ItemQuantity, @ItemLocation, @Department, @WeightValue, @Unit, @CostPrice, @SellingPrice, @ExpirationDate)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@ItemId", item.ItemId);
            insertCommand.Parameters.AddWithValue("@ItemName", item.ItemName);
            insertCommand.Parameters.AddWithValue("@ItemQuantity", item.ItemQuantity);
            insertCommand.Parameters.AddWithValue("@ItemLocation", item.ItemLocation);
            insertCommand.Parameters.AddWithValue("@Department", (int)item.Department);
            insertCommand.Parameters.AddWithValue("@WeightValue", item.WeightValue);
            insertCommand.Parameters.AddWithValue("@Unit", (int)item.Unit);
            insertCommand.Parameters.AddWithValue("@CostPrice", item.CostPrice);
            insertCommand.Parameters.AddWithValue("@SellingPrice", item.SellingPrice);
            insertCommand.Parameters.AddWithValue("@ExpirationDate", (object)item.ExpirationDate ?? DBNull.Value);

            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<Items> GetItems()
        {
            string selectStatement = "SELECT ItemId, ItemName, ItemQuantity, ItemLocation, Department, WeightValue, Unit, CostPrice, SellingPrice, ExpirationDate FROM Items";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var items = new List<Items>();
            while (reader.Read())
            {
                items.Add(MapRowToItem(reader));
            }

            sqlConnection.Close();
            return items;
        }

        public Items FindItem(string id)
        {
            string selectStatement = "SELECT ItemId, ItemName, ItemQuantity, ItemLocation, Department, WeightValue, Unit, CostPrice, SellingPrice, ExpirationDate FROM Items WHERE ItemId = @ItemId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@ItemId", id);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            Items item = null;
            if (reader.Read())
            {
                item = MapRowToItem(reader);
            }

            sqlConnection.Close();
            return item;
        }

        public bool UpdateItemName(string id, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                return false;

            string updateStmt = "UPDATE Items SET ItemName = @ItemName WHERE ItemId = @ItemId";
            return ExecuteUpdate(updateStmt, new KeyValuePair<string, object>("@ItemName", newName), id);
        }

        public bool UpdateItemQuantity(string id, int? newQuantity)
        {
            if (!newQuantity.HasValue)
                return false;

            string updateStmt = "UPDATE Items SET ItemQuantity = @ItemQuantity WHERE ItemId = @ItemId";
            return ExecuteUpdate(updateStmt, new KeyValuePair<string, object>("@ItemQuantity", newQuantity.Value), id);
        }

        public bool UpdateItemLocation(string id, string newLocation)
        {
            if (string.IsNullOrWhiteSpace(newLocation))
                return false;

            string updateStmt = "UPDATE Items SET ItemLocation = @ItemLocation WHERE ItemId = @ItemId";
            return ExecuteUpdate(updateStmt, new KeyValuePair<string, object>("@ItemLocation", newLocation), id);
        }

        // New Field Implementations
        public bool UpdateItemDepartment(string id, ProductDepartment newDept)
        {
            string updateStmt = "UPDATE Items SET Department = @Department WHERE ItemId = @ItemId";
            return ExecuteUpdate(updateStmt, new KeyValuePair<string, object>("@Department", (int)newDept), id);
        }

        public bool UpdateItemWeightValue(string id, double newWeightValue)
        {
            string updateStmt = "UPDATE Items SET WeightValue = @WeightValue WHERE ItemId = @ItemId";
            return ExecuteUpdate(updateStmt, new KeyValuePair<string, object>("@WeightValue", newWeightValue), id);
        }

        public bool UpdateItemUnit(string id, MeasurementUnit newUnit)
        {
            string updateStmt = "UPDATE Items SET Unit = @Unit WHERE ItemId = @ItemId";
            return ExecuteUpdate(updateStmt, new KeyValuePair<string, object>("@Unit", (int)newUnit), id);
        }

        public bool UpdateItemCostPrice(string id, decimal newCostPrice)
        {
            string updateStmt = "UPDATE Items SET CostPrice = @CostPrice WHERE ItemId = @ItemId";
            return ExecuteUpdate(updateStmt, new KeyValuePair<string, object>("@CostPrice", newCostPrice), id);
        }

        public bool UpdateItemSellingPrice(string id, decimal newSellingPrice)
        {
            string updateStmt = "UPDATE Items SET SellingPrice = @SellingPrice WHERE ItemId = @ItemId";
            return ExecuteUpdate(updateStmt, new KeyValuePair<string, object>("@SellingPrice", newSellingPrice), id);
        }

        public bool UpdateItemExpirationDate(string id, DateTime? newExpirationDate)
        {
            string updateStmt = "UPDATE Items SET ExpirationDate = @ExpirationDate WHERE ItemId = @ItemId";
            object dbValue = (object)newExpirationDate ?? DBNull.Value;
            return ExecuteUpdate(updateStmt, new KeyValuePair<string, object>("@ExpirationDate", dbValue), id);
        }

        public bool DeleteItem(string id)
        {
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
            string selectStatement = "SELECT ItemId, ItemName, ItemQuantity, ItemLocation, Department, WeightValue, Unit, CostPrice, SellingPrice, ExpirationDate FROM Items WHERE ItemQuantity < 5";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            List<Items> itemsList = new List<Items>();
            while (reader.Read())
            {
                itemsList.Add(MapRowToItem(reader));
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

        // Helper method to keep your mapping DRY (Don't Repeat Yourself)
        private Items MapRowToItem(SqlDataReader reader)
        {
            return new Items
            {
                ItemId = reader["ItemId"].ToString(),
                ItemName = reader["ItemName"].ToString(),
                ItemQuantity = Convert.ToInt32(reader["ItemQuantity"]),
                ItemLocation = reader["ItemLocation"].ToString(),
                Department = (ProductDepartment)Convert.ToInt32(reader["Department"]),
                WeightValue = Convert.ToDouble(reader["WeightValue"]),
                Unit = (MeasurementUnit)Convert.ToInt32(reader["Unit"]),
                CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                SellingPrice = Convert.ToDecimal(reader["SellingPrice"]),
                ExpirationDate = reader["ExpirationDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["ExpirationDate"])
            };
        }

        // Helper method to execute isolated update commands safely
        private bool ExecuteUpdate(string query, KeyValuePair<string, object> parameter, string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ItemId", id);
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}