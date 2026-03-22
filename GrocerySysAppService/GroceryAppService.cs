﻿using GrocerySysModels;
using GrocerySysDataService;

namespace GrocerySysAppService
{
    public class GroceryAppService
    {
        IGroceryDataService dataService = new GroceryDBData();
        public void addItems(int id, string name, int quantity, string location)
        {   
            Items item = new Items();
            item.ItemId = id;
            item.ItemName = name;
            item.ItemQuantity = quantity;
            item.ItemLocation = location;  
            dataService.AddItem(item);
        }
        public List<Items> GetItems()
        {
            return dataService.GetItems(); 
        }

        public Items FindItem(int id)
        {
            return dataService.FindItem(id);
        }
        public bool UpdateItemName(int id, string newName)
        {
            return dataService.UpdateItemName(id, newName);
        }

        public bool UpdateItemQuantity(int id, int? newQuantity)
        {
            return dataService.UpdateItemQuantity(id, newQuantity);
        }

        public bool UpdateItemLocation(int id, string newLocation)
        {
           return dataService.UpdateItemLocation(id, newLocation); 
        }

        public bool DeleteItem(int id)
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
    }
}