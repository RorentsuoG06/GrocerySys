using System.Security.Cryptography.X509Certificates;
using GrocerySysModels;
using GrocerySysAppService;

namespace Grocery_System___Item_Inventory_Management
{
    internal class Program
    {
        static GroceryAppService appService = new GroceryAppService();
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

                int choice = Convert.ToInt16(Console.ReadLine());


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
                lowItemNotification();
                Console.Write("Do you want to continue? y/n: ");
                string choiceContinue = Console.ReadLine().ToUpper();
                if (choiceContinue == "Y" || choiceContinue == "YES")
                {
                    isContinue = true;
                }
                else if (choiceContinue == "N" || choiceContinue == "NO")
                {
                    isContinue = false;
                    Console.WriteLine("Thank you for using our Grocery Item Management System.");
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

            Console.Write("Item ID: ");
            int item_id = Convert.ToInt16(Console.ReadLine());
            Console.Write("Item Name: ");
            string item_name = Console.ReadLine();
            Console.Write("Item Quantity: ");
            int item_quantity = Convert.ToInt16(Console.ReadLine());
            Console.Write("Item Location: ");
            string item_location = Console.ReadLine();

            appService.addItems(item_id, item_name, item_quantity, item_location);
            Console.WriteLine("Item Added Successfuly!");
        }

        static void displayItems()
        {
            Console.WriteLine("Current List of Items: ");
            var items = appService.GetItems();
            foreach (var item in items)
            {
                Console.WriteLine($"Item ID: {item.ItemId} | Item Name: {item.ItemName} | Item Quantity: | {item.ItemQuantity} | Item Location: {item.ItemLocation}");
            }
        }

        static void searchItems()
        {
            Console.Write("Item ID: ");
            int item_id = Convert.ToInt16(Console.ReadLine());

            var item = appService.FindItem(item_id);

            if (item != null)
            {
                Console.WriteLine($"Found Item: {item.ItemId} | {item.ItemName} | {item.ItemQuantity} | {item.ItemLocation}");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        static void updateItems()
        {

            Console.Write("Item ID: ");
            int item_id = Convert.ToInt16(Console.ReadLine());

            
            var item = appService.FindItem(item_id);
            Console.WriteLine($"Item Found: {item.ItemName} | {item.ItemQuantity} | {item.ItemLocation} ");

            if (item != null)
            {
                Console.WriteLine("\nWhat do you want to update?");
                Console.WriteLine("[1] Item Name");
                Console.WriteLine("[2] Item Quantity");
                Console.WriteLine("[3] Item Location");
                Console.Write("Choice: ");
                int updateChoice = Convert.ToInt16(Console.ReadLine());

                if (updateChoice == 1)
                {
                    Console.Write("New Item Name: ");
                    string newName = Console.ReadLine();
                    appService.UpdateItemName(item_id, newName);
                }
                else if (updateChoice == 2)
                {
                    Console.Write("New Item Quantity: ");
                    string quantityInput = Console.ReadLine();
                    int? newQuantity = string.IsNullOrWhiteSpace(quantityInput) ? null : Convert.ToInt16(quantityInput);
                    appService.UpdateItemQuantity(item_id, newQuantity);
                }
                else if (updateChoice == 3)
                {
                    Console.Write("New Item Location: ");
                    string newLocation = Console.ReadLine();
                    appService.UpdateItemLocation(item_id, newLocation);
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    return;
                }
            }

        }

        static void deleteItems()
        {
            Console.Write("Item ID: ");
            int item_id = Convert.ToInt16(Console.ReadLine());

            bool deleted = appService.DeleteItem(item_id);

            if (deleted)
            {
                Console.WriteLine("Item sucessfully deleted.");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        static void lowItemNotification()
        {
            bool ite = appService.HasLowStockItems();

            if (ite)
            {
                var items = appService.GetLowStockItems();
                Console.WriteLine("Low Stock Items (< 5): ");

                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.ItemId} | Name: {item.ItemName} | Qty: {item.ItemQuantity}");
                }
            }

        }
    }
}