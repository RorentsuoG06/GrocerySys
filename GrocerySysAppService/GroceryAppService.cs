using GrocerySysModels;
using GrocerySysDataService;

namespace GrocerySysAppService
{
    public class GroceryAppService
    {
        GroceryDataService dataService = new GroceryDataService(new GroceryDBData());

        // Refactored to accept the fully structured Items object from the Program console
        public void addItems(Items item)
        {
            var items = dataService.GetItems();

            // Auto-generate the sequential ID before saving
            string newId = GenerateItemId(items);
            item.ItemId = newId;

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

        // New Update Methods for the expanded data fields
        public bool UpdateItemDepartment(string id, ProductDepartment newDept)
        {
            // Assuming dataService has a matching implementation or generic save
            return dataService.UpdateItemDepartment(id, newDept);
        }

        public bool UpdateItemWeightValue(string id, double newWeightValue)
        {
            return dataService.UpdateItemWeightValue(id, newWeightValue);
        }

        public bool UpdateItemUnit(string id, MeasurementUnit newUnit)
        {
            return dataService.UpdateItemUnit(id, newUnit);
        }

        public bool UpdateItemCostPrice(string id, decimal newCostPrice)
        {
            return dataService.UpdateItemCostPrice(id, newCostPrice);
        }

        public bool UpdateItemSellingPrice(string id, decimal newSellingPrice)
        {
            return dataService.UpdateItemSellingPrice(id, newSellingPrice);
        }

        public bool UpdateItemExpirationDate(string id, DateTime? newExpirationDate)
        {
            return dataService.UpdateItemExpirationDate(id, newExpirationDate);
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
            return dataService.GetLowStockItems();
        }

        public bool HasLowStockItems()
        {
            return dataService.HasLowStockItems();
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