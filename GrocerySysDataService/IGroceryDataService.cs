using GrocerySysModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocerySysDataService
{
    public interface IGroceryDataService
    {
        public void AddItem(Items item);

        public List<Items> GetItems();

        public Items FindItem(string id);

        public bool DeleteItem(string id);

        public bool UpdateItemName(string id, string newName);

        public bool UpdateItemQuantity(string id, int? newQuantity);

        public bool UpdateItemLocation(string id, string newLocation);
    }
}
