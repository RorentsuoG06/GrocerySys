using GrocerySysModels;
using GrocerySysAppService;

namespace Grocery_System___Item_Inventory_Management
{
    internal class Program
    {
        static GroceryAppService appService = new GroceryAppService();
        static AccountAppService accountAppService = new AccountAppService();
        static void Main(string[] args)
        {
            Console.WriteLine("Grocery System - Item Inventory Management");

            bool isLogin = LoginOption();

            while (isLogin)
            {
                Login();

                isLogin = LoginOption();
            }
        }

        static bool LoginOption()
        {
            bool isLogin = false;
            Console.Write("Do you want to login? y/n: ");
            string loginInput = Console.ReadLine();

            switch (loginInput)
            {
                case "y":
                    isLogin = true;
                    break;
                case "n":
                    isLogin = false;
                    Console.WriteLine("System Shutdown.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Incorrect Input. The system will now exit.");
                    Environment.Exit(0);
                    break;
            }

            return isLogin;
        }

        static void Login()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Enter username: ");
                string usernameInput = Console.ReadLine();
                Console.Write("Enter password: ");
                string passwordInput = Console.ReadLine();

                Accounts currentUser = accountAppService.Authenticate(usernameInput, passwordInput);

                bool isMatched; 

                if (currentUser == null)
                {
                    isMatched = false;
                    string role = "null";
                    AddAccessLogs(usernameInput, role, isMatched);
                    Console.WriteLine("Invalid Login!");
                    Console.WriteLine($"Attempts left: {2 - i}");
                    continue;
                }

                Console.WriteLine($"Welcome {currentUser.Username}");


                if (currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    isMatched = true;
                    string role = currentUser.Role;
                    AddAccessLogs(usernameInput, role, isMatched);
                    adminMenuChoices();
                }
                else if (currentUser.Role.Equals("Employee", StringComparison.OrdinalIgnoreCase))
                {
                    isMatched = true;
                    string role = currentUser.Role;
                    AddAccessLogs(usernameInput, role, isMatched);
                    showEmployeeCRUD();
                }

                return;
            }
            Console.WriteLine("Too many failed login attempts.");
        }

        static void adminMenuChoices()
        {
            Console.WriteLine("========= Admin Menu =========");
            Console.WriteLine("Choices:");
            Console.WriteLine("[1] Employee Account Management");
            Console.WriteLine("[2] Item Inventory Management");
            Console.WriteLine("[3] Exit");
            Console.Write("Choice: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return;
            }

            switch (choice)
            {
                case 1:
                    showAdminEmpAccCRUD();
                    break;
                case 2:
                    showAdminItemCRUD();
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("You have entered a choice that is not in the list, please try again.");
                    break;
            }
        }

        static void showAdminEmpAccCRUD()
        {
            //Add, View, Update, Delete Employees, Display Access Logs

            bool running = true;

            while (running)
            {
                Console.WriteLine("========= Admin Menu =========");
                Console.WriteLine("Choices:");
                Console.WriteLine("[1] Add Employee");
                Console.WriteLine("[2] View Employees");
                Console.WriteLine("[3] Update Employee Account");
                Console.WriteLine("[4] Remove Employee");
                Console.WriteLine("[5] Display Access Logs");
                Console.WriteLine("[6] Exit");
                Console.Write("Choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    return;
                }

                switch (choice)
                {
                    case 1:
                        addEmployee();
                        break;
                    case 2:
                        viewEmployees();
                        break;
                    case 3:
                        updateEmployee();
                        break;
                    case 4:
                        removeEmployee();
                        break;
                    case 5:
                        displayLogs();
                        break;
                    case 6:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("You have entered a choice that is not in the list, please try again.");
                        break;
                }
            }
               
                
        }

        static void showAdminItemCRUD()
        {
            //Create, Retrieve, Update, Delete Items
            //Item Notification for stocks of items < 5

            bool isContinue = false;

            do
            {
                Console.WriteLine("========= Admin Menu =========");
                Console.WriteLine("Choices:");
                Console.WriteLine("[1] Create new Item");
                Console.WriteLine("[2] View all Items");
                Console.WriteLine("[3] Search/Retrieve Item");
                Console.WriteLine("[4] Update Item");
                Console.WriteLine("[5] Delete Item");
                Console.WriteLine("[6] Exit");
                Console.Write("Choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    return;
                }


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
                    case 6:
                        adminMenuChoices();
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
                    Console.WriteLine("Going back to Admin Menu.");
                    adminMenuChoices();
                }
                else
                {
                    Console.WriteLine("Wrong Input. Please try again.");
                }
            } while (isContinue);

        }

        static void showEmployeeCRUD()
        {
            //Retrieve & Update Items
            //Item Notification for stocks of items < 5

            bool isContinue = false;
            do
            {
                Console.WriteLine("========= Menu =========");
                Console.WriteLine("Choices:");
                Console.WriteLine("[1] View all Items");
                Console.WriteLine("[2] Search/Retrieve Item");
                Console.WriteLine("[3] Update Item");
                Console.WriteLine("[4] Exit");
                Console.Write("Choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    return;
                }

                switch (choice)
                {
                    case 1:
                        displayItems();
                        break;
                    case 2:
                        searchItems();
                        break;
                    case 3:
                        updateItems();
                        break;
                    case 4:
                        return;
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

        static void addEmployee()
        {
            Console.WriteLine("Add Employee - Enter information");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Accounts newAccount = new Accounts { AccountID = Guid.NewGuid(), Username = username, Password = password, Role = "Employee" };

            if (accountAppService.ValidateUsername(newAccount))
            {
                accountAppService.Register(newAccount);
            }
            else
            {
                Console.WriteLine("User already exists.");
            }
        }

        static void viewEmployees()
        {
            Console.WriteLine("Employee List:");

            var accounts = accountAppService.GetAccounts();

            foreach (var acc in accounts)
            {
                Console.WriteLine($"ID: {acc.AccountID}, Username: {acc.Username}, Password: {acc.Password}");
            }

        }

        static void updateEmployee()
        {
            Console.WriteLine("Update Employee Account - Select what to update");
            Console.WriteLine("Choices: ");
            Console.WriteLine("[1] Username");
            Console.WriteLine("[2] Password");
            Console.WriteLine("[3] Exit");
            Console.Write("Choice: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return;
            }

            switch (choice)
            {
                case 1:
                    updateEmployeeUsername();
                    break;
                case 2:
                    updateEmployeePassword();
                    break;
                case 3:
                    showAdminEmpAccCRUD();
                    break;
                default:
                    Console.WriteLine("You have entered a choice that is not in the list, please try again.");
                    break;
            }
        }

        static void updateEmployeeUsername()
        {
            Console.Write("Enter the username of the employee you want to update: ");
            string username = Console.ReadLine();
            bool isMatched = accountAppService.GetUsername(username);

            if(isMatched)
            {
                Console.Write("Enter new username: ");
                string newUsername = Console.ReadLine();
                accountAppService.UpdateUsername(username, newUsername);
                Console.WriteLine("Successfully updated!");
            }

        }

        static void updateEmployeePassword()
        {
            Console.Write("Enter the username of the employee you want to update: ");
            string username = Console.ReadLine();
            bool isMatched = accountAppService.GetUsername(username);

            if (isMatched)
            {
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();
                accountAppService.UpdatePassword(username, newPassword);
                Console.WriteLine("Successfully updated!");
            }
        }

        static void removeEmployee()
        {
            Console.Write("Enter the username of the employee you want to remove: ");
            string username = Console.ReadLine();
            bool isMatched = accountAppService.GetUsername(username);

            if (isMatched)
            {
                accountAppService.RemoveEmployee(username);
                Console.WriteLine("Successfully Removed Employee " + username);
            }
            else
            {
                Console.WriteLine("Employee does not exist.");
            }
        }

        static void displayLogs()
        {
            Console.WriteLine("Access Logs:");

            var accessLogs = accountAppService.GetAccessLogs();

            foreach (var acc in accessLogs)
            {
                Console.WriteLine($"Username: {acc.Username}, Role: {acc.Role}, Status: {(acc.Status ? "Success" : "Failed")}");
            }
        }

        static void AddAccessLogs(string username, string role, bool status)
        {
            AccessLogs accessLog = new AccessLogs() { Username = username, Role = role, Status = status };
            accountAppService.AddAccessLog(accessLog);
        }

        //CRUD Methods
        static void create_Item()
        {
            /* Create Item 
                Item_ID
                Item_Name
                Item_Quantity
                Item_Location
            */

          
            Console.Write("Item Name: ");
            string item_name = Console.ReadLine();
            Console.Write("Item Quantity: ");
            int item_quantity = Convert.ToInt16(Console.ReadLine());
            Console.Write("Item Location: ");
            string item_location = Console.ReadLine();

            appService.addItems(item_name, item_quantity, item_location);
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
            string item_id = Console.ReadLine();

            var item = appService.FindItem(item_id);

            if (item == null)
            {
                Console.WriteLine("Item not found.");
            } else {
                Console.WriteLine($"Found Item: {item.ItemId} | {item.ItemName} | {item.ItemQuantity} | {item.ItemLocation}"); 
            }
            
        }

        static void updateItems()
        {

            Console.Write("Item ID: ");
            string item_id = Console.ReadLine();


            var item = appService.FindItem(item_id);
            

            if (item != null)
            {
                Console.WriteLine($"Item Found: {item.ItemName} | {item.ItemQuantity} | {item.ItemLocation} ");
                Console.WriteLine("\nWhat do you want to update?");
                Console.WriteLine("[1] Item Name");
                Console.WriteLine("[2] Item Quantity");
                Console.WriteLine("[3] Item Location");
                Console.Write("Choice: ");
                int updateChoice;
                if (!int.TryParse(Console.ReadLine(), out updateChoice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    return;
                }

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
                    int? newQuantity = null;

                    if (!string.IsNullOrWhiteSpace(quantityInput))
                    {
                        if (int.TryParse(quantityInput, out int parsedQty))
                        {
                            newQuantity = parsedQty;
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity.");
                            return;
                        }
                    }
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
            string item_id = Console.ReadLine();

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