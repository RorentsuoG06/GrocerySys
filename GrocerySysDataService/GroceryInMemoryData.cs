using GrocerySysModels;
namespace GrocerySysDataService
{
    public class GroceryInMemoryData : IGroceryDataService
    {
        public List<Items> itemList = new List<Items>();

        //CRUD Methods
        public void AddItem(Items item)
        {
            itemList.Add(item);
        }

        public List<Items> GetItems()
        {
            return itemList;
        }

        public Items FindItem(string id)
        {
            return itemList.FirstOrDefault(x => x.ItemId.Equals(id));
        }

        public bool UpdateItemName(string id, string newName)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(newName))
            {
                item.ItemName = newName;
            }
            return true;
        }

        public bool UpdateItemQuantity(string id, int? newQuantity)
        {
            var item = FindItem(id);

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

        public bool UpdateItemLocation(string id, string newLocation)
        {
            var item = FindItem(id);

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

        public bool DeleteItem(string id)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }

            itemList.Remove(item);
            return true;
        }

        public List<Items> GetLowStockItems()
        {
            return itemList.Where(x => x.ItemQuantity < 5).ToList();
        }

        public bool HasLowStockItems()
        {
            return itemList.Any(x => x.ItemQuantity < 5);
        }
    }
}