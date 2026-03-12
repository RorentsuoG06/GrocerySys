using GrocerySysModels;
namespace GrocerySysDataService
{
    public class GroceryDataService
    {
        List<Items> itemList = new List<Items>();
        
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
