using System.Security.Cryptography.X509Certificates;

namespace Grocery_System___Item_Inventory_Management
{
    internal class Program
    {
       static int choice = 0;
       static int item_id = 0, item_quantity = 0;
       static string item_name = "", item_location = "", choiceContinue = "";
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

            do { 
                Console.WriteLine("Choices:");
                Console.WriteLine("[1] Create new Item");
                Console.WriteLine("[2] Search/Retrieve Item");
                Console.WriteLine("[3] Update Item");
                Console.WriteLine("[4] Delete Item");
                Console.Write("Choice: ");

                choice = Convert.ToInt16(Console.ReadLine());


                switch (choice)
                {
                    case 1:
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

                        break;
                    case 2:
                        /* Search/Retrieve Item 
                         Item_ID
                         */

                        Console.WriteLine("Item ID: ");
                        item_id = Convert.ToInt16(Console.ReadLine());
                        break;
                    case 3:
                        /* Update Item
                         Item_ID
                         IF TRUE
                           Select what to update
                            Item_Name
                            Item_Quantity
                            Item_Location
                    
                         */
                        Console.WriteLine("Item ID: ");
                        item_id = Convert.ToInt16(Console.ReadLine());
                        break;
                    case 4:
                        /* Delete Item
                         Item_ID
                         */
                        Console.WriteLine("Item ID: ");
                        item_id = Convert.ToInt16(Console.ReadLine());
                        break;
                    default:
                        Console.WriteLine("You have entered a choice that is not in the list, please try again.");
                        break;
                }
                Console.Write("Do you want to continue? y/n: ");
                choiceContinue = Console.ReadLine();
                if(choiceContinue == "y")
                {
                    isContinue = true;
                }
                else
                {
                    isContinue = false; 
                }
            } while (isContinue);

        }
    }
}
