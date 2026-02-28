using System.Security.Cryptography.X509Certificates;

namespace Grocery_System___Item_Inventory_Management
{
    internal class Program
    {
        static int choice = 0;
        static int item_id = 0, item_quantity = 0;
        static string item_name = "", item_location = "", choiceContinue = "", newName = "", newQuantity = "", newLocation = "";
        static List<string> itemList = new List<string>();
        static void Main(string[] args)
        {
            Console.WriteLine("Grocery System - Item Inventory Management");
            CRUD_Feature();

        }

        static void CRUD_Feature()
        {
            bool isContinue = false;

            //Create, Retrieve, Update, Delete Items
            //Item Notification for stocks of items < 5

            do
            {
                Console.WriteLine("Choices:");
                Console.WriteLine("[1] Create new Item");
                Console.WriteLine("[2] View all Items");
                Console.WriteLine("[3] Search/Retrieve Item");
                Console.WriteLine("[4] Update Item");
                Console.WriteLine("[5] Delete Item");
                Console.Write("Choice: ");

                choice = Convert.ToInt16(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        create_Item();
                        break;
                    case 2:
                        displayItems();
                        break;
                    case 3:
                        searchItems();
                        break;
                    case 4:
                        updateItems();
                        break;
                    case 5:
                        deleteItems();
                        break;
                    default:
                        Console.WriteLine("You have entered a choice that is not in the list, please try again.");
                        break;
                }
                Console.Write("Do you want to continue? y/n: ");
                choiceContinue = Console.ReadLine().ToUpper();
                if (choiceContinue == "Y" || choiceContinue == "YES")
                {
                    isContinue = true;
                }
                else if (choiceContinue == "N" || choiceContinue == "NO")
                {
                    isContinue = false;
                }
                else
                {
                    Console.WriteLine("Wrong Input. Please try again.");
                }
            } while (isContinue);

        }

        static void create_Item()
        {
            /* Create Item 
                         Item_ID
                         Item_Name
                         Item_Quantity
                         Item_Location
                         */

            Console.WriteLine("Item ID: ");
            item_id = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Item Name: ");
            item_name = Console.ReadLine();
            Console.WriteLine("Item Quantity: ");
            item_quantity = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Item Location");
            item_location = Console.ReadLine();

            addItems(item_id, item_name, item_quantity, item_location);
            Console.WriteLine("Item Added Successfuly!");
        }

        static void addItems(int id, string name, int quantity, string location)
        {
            itemList.Add($"Item ID: {item_id}, Item Name: {item_name}, Item Quantity: {item_quantity}, Item Location: {item_location} ");
        }

        static void displayItems()
        {
            Console.WriteLine("Current List of Items: ");
            foreach (var item in itemList)
            {
                Console.WriteLine(item);
            }
        }

        static void searchItems()
        {
            Console.WriteLine("Item ID: ");
            item_id = Convert.ToInt16(Console.ReadLine());

            var foundItem = itemList.Find(x => x.Contains($"Item ID: {item_id},"));

            if (foundItem != null)
            {
                Console.WriteLine("Item Found:");
                Console.WriteLine(foundItem);
            }
            else
            {
                Console.WriteLine("Item not found.");
            }

        }

        static void updateItems()
        {
            newName = "";
            newQuantity = "";
            newLocation = "";

            Console.Write("Item ID: ");
            item_id = Convert.ToInt16(Console.ReadLine());

            int index = itemList.FindIndex(x => x.Contains($"Item ID: {item_id},"));
            if (index == -1)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            Console.WriteLine("Item Found: ");
            Console.WriteLine(itemList[index]);

            Console.WriteLine("\nWhat do you want to update?");
            Console.WriteLine("[1] Item Name");
            Console.WriteLine("[2] Item Quantity");
            Console.WriteLine("[3] Item Location");
            Console.Write("Choice: ");
            int updatedChoice = Convert.ToInt16(Console.ReadLine());

            if (updatedChoice == 1)
            {
                Console.Write("New Item Name: ");
                newName = Console.ReadLine();
            }
            else if (updatedChoice == 2)
            {
                Console.Write("New Item Quantity: ");
                newQuantity = Console.ReadLine();
            }
            else if (updatedChoice == 3)
            {
                Console.Write("New Item Location: ");
                newLocation = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            string current = itemList[index];

            string oldName = GetValue(current, "Item Name: ", ", Item Quantity:");
            string oldQty = GetValue(current, "Item Quantity: ", ", Item Location:");
            string oldLoc = GetValue(current, "Item Location: ", " ");

            if (!string.IsNullOrWhiteSpace(newName)) oldName = newName;
            if (!string.IsNullOrWhiteSpace(newQuantity)) oldQty = newQuantity;
            if (!string.IsNullOrWhiteSpace(newLocation)) oldLoc = newLocation;

            itemList[index] = $"Item ID: {item_id}, Item Name: {oldName}, Item Quantity: {oldQty}, Item Location: {oldLoc} ";

            Console.WriteLine("Item Updated Successfully!");
            Console.WriteLine(itemList[index]);
        }

        static string GetValue(string source, string start, string end)
            {
                int startIndex = source.IndexOf(start);
                if (startIndex == -1) return "";

                startIndex += start.Length;
                int endIndex = source.IndexOf(end, startIndex);
                if (endIndex == -1) endIndex = source.Length;

                return source.Substring(startIndex, endIndex - startIndex).Trim();
            }
        
       
        static void deleteItems()
        {
            Console.WriteLine("Item ID: ");
            item_id = Convert.ToInt16(Console.ReadLine());

            var foundItem = itemList.Find(x => x.Contains($"Item ID: {item_id},"));

            if (foundItem != null)
            {
                Console.WriteLine("Item Deleted:");
                itemList.Remove(foundItem);
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        static void lowItemNotification()
        {

        }
    }
}
