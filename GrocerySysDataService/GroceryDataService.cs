using GrocerySysModels;

namespace GrocerySysDataService
{
    public class GroceryDataService
    {
        private readonly IGroceryDataService _dataService;

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

        // Forwarding methods for the new fields down to the injected provider
        public bool UpdateItemDepartment(string id, ProductDepartment newDept)
        {
            return _dataService.UpdateItemDepartment(id, newDept);
        }

        public bool UpdateItemWeightValue(string id, double newWeightValue)
        {
            return _dataService.UpdateItemWeightValue(id, newWeightValue);
        }

        public bool UpdateItemUnit(string id, MeasurementUnit newUnit)
        {
            return _dataService.UpdateItemUnit(id, newUnit);
        }

        public bool UpdateItemCostPrice(string id, decimal newCostPrice)
        {
            return _dataService.UpdateItemCostPrice(id, newCostPrice);
        }

        public bool UpdateItemSellingPrice(string id, decimal newSellingPrice)
        {
            return _dataService.UpdateItemSellingPrice(id, newSellingPrice);
        }

        public bool UpdateItemExpirationDate(string id, DateTime? newExpirationDate)
        {
            return _dataService.UpdateItemExpirationDate(id, newExpirationDate);
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