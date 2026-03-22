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

        public Items FindItem(int id)
        {
            return _dataService.FindItem(id);
        }

        public bool DeleteItem(int id)
        {
            return _dataService.DeleteItem(id);
        }
    }
}
