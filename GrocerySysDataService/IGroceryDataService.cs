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

        public Items FindItem(int id);

        public bool DeleteItem(int id);

        public bool UpdateItemName(int id, string newName);

        public bool UpdateItemQuantity(int id, int? newQuantity);

        public bool UpdateItemLocation(int id, string newLocation);
    }
}
