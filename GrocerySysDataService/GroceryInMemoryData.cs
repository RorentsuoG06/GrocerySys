using GrocerySysModels;
namespace GrocerySysDataService
{
    public class GroceryInMemoryData : IGroceryDataService
    {
        public List<Items> itemList = new List<Items>();

        public void AddItem(Items item)
        {
            itemList.Add(item);
        }

        public List<Items> GetItems()
        {
            return itemList;
        }

        public Items FindItem(int id)
        {
            return itemList.FirstOrDefault(x => x.ItemId == id);
        }

        public bool UpdateItemName(int id, string newName)
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

        public bool UpdateItemQuantity(int id, int? newQuantity)
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

        public bool UpdateItemLocation(int id, string newLocation)
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

        public bool DeleteItem(int id)
        {
            var item = FindItem(id);

            if (item == null)
            {
                return false;
            }

            itemList.Remove(item);
            return true;
        }
    }
}