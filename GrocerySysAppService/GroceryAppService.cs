﻿using GrocerySysModels;
using GrocerySysDataService;

namespace GrocerySysAppService
{
    public class GroceryAppService
    {
        GroceryDataService dataService = new GroceryDataService(new GroceryDBData());
        
        public void addItems(string name, int quantity, string location)
        {
            Items item = new Items();
            var items = dataService.GetItems();

            string newId = GenerateItemId(items);
            item.ItemId = newId;
            item.ItemName = name;
            item.ItemQuantity = quantity;
            item.ItemLocation = location;  
            dataService.AddItem(item);
        }
        public List<Items> GetItems()
        {
            return dataService.GetItems(); 
        }

        public Items FindItem(string id)
        {
            return dataService.FindItem(id);
        }
        public bool UpdateItemName(string id, string newName)
        {
            return dataService.UpdateItemName(id, newName);
        }

        public bool UpdateItemQuantity(string id, int? newQuantity)
        {
            return dataService.UpdateItemQuantity(id, newQuantity);
        }

        public bool UpdateItemLocation(string id, string newLocation)
        {
           return dataService.UpdateItemLocation(id, newLocation); 
        }

        public bool DeleteItem(string id)
        {
            return dataService.DeleteItem(id);
        }

        public List<Items> GetLowStockItems()
        {
            return dataService.GetItems().Where(x => x.ItemQuantity < 5).ToList();
        }
        public bool HasLowStockItems()
        {
            return dataService.GetItems().Any(x => x.ItemQuantity < 5);
        }

        public string GenerateItemId(List<Items> items)
        {
            if (items.Count == 0)
                return "0001";

            int maxId = items
                .Where(i => !string.IsNullOrEmpty(i.ItemId)) 
                .Select(i => int.TryParse(i.ItemId, out int num) ? num : 0) 
                .DefaultIfEmpty(0) 
                .Max();

            return (maxId + 1).ToString("D4");
        }
    }
}