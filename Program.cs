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

        //Login Function
        static bool LoginOption()
        {
            while (true)
            {
                bool isLogin = false;
                Console.Write("Do you want to login? [Y]|[N]: ");
                string loginInput = Console.ReadLine().ToUpper();

                switch (loginInput)
                {
                    case "Y":
                        isLogin = true;
                        break;
                    case "N":
                        isLogin = false;
                        Console.WriteLine("System Shutdown.");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Write("Incorrect Input. Would you like to try again? [Y]|[N]: ");
                        string retry = Console.ReadLine().ToUpper();
                        if (retry == "Y")
                        {
                            continue;
                        }
                        else if(retry == "N") 
                        {
                            Console.WriteLine("The System will now exit.");
                            Environment.Exit(0);
                        }
                        break;
                }

                return isLogin;
            }
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

        //Menus
        static void adminMenuChoices()
        {

            bool running = true;

            while (running)
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
                    continue;
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
                        running = false;
                        break;
                    default:
                        Console.WriteLine("You have entered a choice that is not in the list, please try again.");
                        break;
                }
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
                    continue;
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
                        updateEmployeeAdmin();
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
                    continue;
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
                        displayItems();
                        updateItems();
                        break;
                    case 5:
                        displayItems();
                        deleteItems();
                        break;
                    case 6:
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
                    Console.WriteLine("Going back to Admin Menu.");
                    return;
                }
                else
                {
                    Console.WriteLine("Wrong Input. Please try again.");
                    isContinue = true;
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
                Console.WriteLine("[4] Change Username/Password");
                Console.WriteLine("[5] Exit");
                Console.Write("Choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
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
                        displayItems();
                        updateItems();
                        break;
                    case 4:
                        updateEmployeeEmp();
                        break;
                    case 5:
                        Console.WriteLine("Thank you for using our Grocery Item Management System.");
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
                    isContinue = true;
                }
            } while (isContinue);
        }

        //Admin Methods
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

        static void updateEmployeeAdmin()
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

        static void updateEmployeeEmp()
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
                    updateEmployeeUsernameEmp();
                    break;
                case 2:
                    updateEmployeePasswordEmp();
                    break;
                case 3:
                    showEmployeeCRUD();
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
            } else if (!isMatched)
            {
                Console.WriteLine("Username does not exist.");
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
            } else if (!isMatched)
            {
                Console.WriteLine("Username does not exist.");
            }
        }

        static void updateEmployeeUsernameEmp()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            bool isMatched = accountAppService.ValidateAccount(username, password);

            if (isMatched)
            {
                Console.Write("Enter new username: ");
                string newUsername = Console.ReadLine();
                accountAppService.UpdateUsername(username, newUsername);
                Console.WriteLine("Successfully updated!");
            }
            else if (!isMatched)
            {
                Console.WriteLine("You may have entered the wrong username.");
            }
        }

        static void updateEmployeePasswordEmp()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            bool isMatched = accountAppService.ValidateAccount(username, password);

            if (isMatched)
            {
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();
                accountAppService.UpdatePassword(username, newPassword);
                Console.WriteLine("Successfully updated!");
            }
            else if (!isMatched)
            {
                Console.WriteLine("You may have entered the wrong username.");
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

        //Access Logs Functionality
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

        //Grocery CRUD Methods
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
            int item_quantity;
                if (!int.TryParse(Console.ReadLine(), out item_quantity))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    return;
                }
            Console.Write("Item Location: ");
            string item_location = Console.ReadLine();

            appService.addItems(item_name, item_quantity, item_location);
            Console.WriteLine("Item Added Successfuly!");
        }

        static void displayItems()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Current List of Items: ");
            var items = appService.GetItems();
            foreach (var item in items)
            {
                Console.WriteLine($"Item ID: {item.ItemId} | Item Name: {item.ItemName} | Item Quantity: | {item.ItemQuantity} | Item Location: {item.ItemLocation}");
            }
            Console.WriteLine("======================================");
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

            Console.Write("Enter Item ID: ");
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
                            if (parsedQty < 0)
                            {
                                Console.WriteLine("Quantity cannot be negative.");
                                return;
                            }
                            newQuantity = parsedQty;
                        }
                        else                        {
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