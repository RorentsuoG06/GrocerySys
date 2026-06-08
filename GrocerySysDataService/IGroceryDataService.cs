using GrocerySysModels;

namespace GrocerySysDataService
{
    public interface IGroceryDataService
    {
        void AddItem(Items item);
        List<Items> GetItems();
        Items FindItem(string id);
        bool DeleteItem(string id);
        bool UpdateItemName(string id, string newName);
        bool UpdateItemQuantity(string id, int? newQuantity);
        bool UpdateItemLocation(string id, string newLocation);
        bool UpdateItemDepartment(string id, ProductDepartment newDept);
        bool UpdateItemWeightValue(string id, double newWeightValue);
        bool UpdateItemUnit(string id, MeasurementUnit newUnit);
        bool UpdateItemCostPrice(string id, decimal newCostPrice);
        bool UpdateItemSellingPrice(string id, decimal newSellingPrice);
        bool UpdateItemExpirationDate(string id, DateTime? newExpirationDate);

        List<Items> GetLowStockItems();
        bool HasLowStockItems();
    }
}