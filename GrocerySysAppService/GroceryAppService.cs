using GrocerySysModels;
using GrocerySysDataService;

namespace GrocerySysAppService
{
    public class GroceryAppService
    {
            GroceryDataService dataService = new GroceryDataService();
        
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
            var item = dataService.FindItem(id);

            if(item == null)
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
            var item = dataService.FindItem(id);

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
            var item = dataService.FindItem(id);

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
