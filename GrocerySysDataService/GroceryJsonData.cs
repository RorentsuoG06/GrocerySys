using GrocerySysModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GrocerySysDataService
{
    public class GroceryJsonData : IGroceryDataService
    {
        private List<Items> itemList = new List<Items>();
        private string _jsonFileName;

        public GroceryJsonData()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/Items.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            // Simple check to avoid crashing if file doesn't exist yet
            if (File.Exists(_jsonFileName))
            {
                RetrieveDataFromJsonFile();
            }

            if (itemList.Count <= 0)
            {
                itemList.Add(new Items { ItemId = "0001", ItemName = "Apple", Department = ProductDepartment.Produce, ItemLocation = "Shelf A", ItemQuantity = 6, WeightValue = 1, Unit = MeasurementUnit.Kilograms, CostPrice = 50m, SellingPrice = 75m });
                itemList.Add(new Items { ItemId = "0002", ItemName = "Mango", Department = ProductDepartment.Produce, ItemLocation = "Shelf B", ItemQuantity = 7, WeightValue = 500, Unit = MeasurementUnit.Grams, CostPrice = 60m, SellingPrice = 90m });
                itemList.Add(new Items { ItemId = "0003", ItemName = "Orange", Department = ProductDepartment.Produce, ItemLocation = "Shelf C", ItemQuantity = 8, WeightValue = 1, Unit = MeasurementUnit.Kilograms, CostPrice = 40m, SellingPrice = 60m });

                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            // Note: Use FileMode.Create to overwrite old contents cleanly instead of duplicating or appending corrupted fragments
            using (var outputStream = File.Open(_jsonFileName, FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize<List<Items>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , itemList);
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            using (var jsonFileReader = File.OpenText(_jsonFileName))
            {
                var data = JsonSerializer.Deserialize<List<Items>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true });

                itemList = data ?? new List<Items>();
            }
        }

        public void AddItem(Items item)
        {
            itemList.Add(item);
            SaveDataToJsonFile();
        }

        public List<Items> GetItems()
        {
            return itemList;
        }

        public Items FindItem(string id)
        {
            return itemList.FirstOrDefault(x => x.ItemId.Equals(id));
        }

        public bool UpdateItemName(string id, string newName)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(newName))
            {
                item.ItemName = newName;
                SaveDataToJsonFile();
            }
            return true;
        }

        public bool UpdateItemQuantity(string id, int? newQuantity)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }
            if (newQuantity.HasValue)
            {
                item.ItemQuantity = newQuantity.Value;
                SaveDataToJsonFile();
            }

            return true;
        }

        public bool UpdateItemLocation(string id, string newLocation)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(newLocation))
            {
                item.ItemLocation = newLocation;
                SaveDataToJsonFile();
            }
            return true;
        }

        // New Update methods matching IGroceryDataService requirements
        public bool UpdateItemDepartment(string id, ProductDepartment newDept)
        {
            var item = FindItem(id);
            if (item == null) return false;

            item.Department = newDept;
            SaveDataToJsonFile();
            return true;
        }

        public bool UpdateItemWeightValue(string id, double newWeightValue)
        {
            var item = FindItem(id);
            if (item == null) return false;

            item.WeightValue = newWeightValue;
            SaveDataToJsonFile();
            return true;
        }

        public bool UpdateItemUnit(string id, MeasurementUnit newUnit)
        {
            var item = FindItem(id);
            if (item == null) return false;

            item.Unit = newUnit;
            SaveDataToJsonFile();
            return true;
        }

        public bool UpdateItemCostPrice(string id, decimal newCostPrice)
        {
            var item = FindItem(id);
            if (item == null) return false;

            item.CostPrice = newCostPrice;
            SaveDataToJsonFile();
            return true;
        }

        public bool UpdateItemSellingPrice(string id, decimal newSellingPrice)
        {
            var item = FindItem(id);
            if (item == null) return false;

            item.SellingPrice = newSellingPrice;
            SaveDataToJsonFile();
            return true;
        }

        public bool UpdateItemExpirationDate(string id, DateTime? newExpirationDate)
        {
            var item = FindItem(id);
            if (item == null) return false;

            item.ExpirationDate = newExpirationDate;
            SaveDataToJsonFile();
            return true;
        }

        public bool DeleteItem(string id)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }

            itemList.Remove(item);
            SaveDataToJsonFile();
            return true;
        }

        public List<Items> GetLowStockItems()
        {
            return itemList.Where(x => x.ItemQuantity < 5).ToList();
        }

        public bool HasLowStockItems()
        {
            return itemList.Any(x => x.ItemQuantity < 5);
        }
    }
}