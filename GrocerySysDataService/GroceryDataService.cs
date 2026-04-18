using GrocerySysModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocerySysDataService
{
    public class GroceryDataService
    {
        IGroceryDataService _dataService;
        public GroceryDataService(IGroceryDataService dataService) 
        {
            _dataService = dataService;
        }

        public void AddItem(Items item)
        {
            _dataService.AddItem(item);
        }

        public List<Items> GetItems()
        {
            return _dataService.GetItems();
        }

        public Items FindItem(string id)
        {
            return _dataService.FindItem(id);
        }

        public bool UpdateItemName(string id, string newName)
        {
           return _dataService.UpdateItemName(id, newName);
        }

        public bool UpdateItemQuantity(string id, int? newQuantity)
        {
            return _dataService.UpdateItemQuantity(id, newQuantity);
        }
        public bool UpdateItemLocation(string id, string newLocation)
        {
            return _dataService.UpdateItemLocation(id, newLocation);
        }

        public bool DeleteItem(string id)
        {
            return _dataService.DeleteItem(id);
        }

        public List<Items> GetLowStockItems()
        {
            return _dataService.GetLowStockItems();
        }

        public bool HasLowStockItems()
        {
            return _dataService.HasLowStockItems();
        }
    }
}
