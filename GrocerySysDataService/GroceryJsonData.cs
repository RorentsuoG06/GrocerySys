using GrocerySysModels;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
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
            RetrieveDataFromJsonFile();

            
            if (itemList.Count <= 0)
            {
                itemList.Add(new Items {ItemId = 1, ItemName = "Apple", ItemLocation = "Shelf A", ItemQuantity = 6});
                itemList.Add(new Items {ItemId = 2, ItemName = "Mango", ItemLocation = "Shelf B", ItemQuantity = 7});
                itemList.Add(new Items {ItemId = 3, ItemName = "Orange", ItemLocation = "Shelf C", ItemQuantity = 8});

                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
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
                itemList = JsonSerializer.Deserialize<List<Items>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }

        public void AddItem(Items item)
        {
            itemList.Add(item);
        }

        public List<Items> GetItems()
        {
            return itemList;
        }

        public Items FindItem(int id)
        {
            return itemList.FirstOrDefault(x => x.ItemId == id);
        }

        public bool UpdateItemName(int id, string newName)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(newName))
            {
                item.ItemName = newName;
            }
            return true;
        }

        public bool UpdateItemQuantity(int id, int? newQuantity)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }
            if (newQuantity.HasValue)
            {
                item.ItemQuantity = newQuantity.Value;
            }

            return true;
        }

        public bool UpdateItemLocation(int id, string newLocation)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(newLocation))
            {
                item.ItemLocation = newLocation;
            }
            return true;
        }
        public bool DeleteItem(int id)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }

            itemList.Remove(item);
            return true;
        }
    }
}
