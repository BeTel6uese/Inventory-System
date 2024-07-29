using System;

namespace InventorySystem
{
    class Program
    {
        static decimal balance = 1000m;

        static void Main(string[] args)
        {
            DisplayWelcomeMessage();
            Shop shop = new Shop();
            PlayerInventory playerInventory = new PlayerInventory();

            bool exit = false;
            while (!exit)
            {
                ClearScreenWithWelcome();
                DisplayMainMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShopMenu(shop, playerInventory);
                        break;

                    case "2":
                        ViewInventory(playerInventory);
                        break;

                    case "3":
                        RechargeBalance();
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        PromptToContinue();
                        break;
                }
            }
        }

        static void DisplayWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine(" _       __     __                            ");
            Console.WriteLine("| |     / /__  / /________  ____ ___  ___   ");
            Console.WriteLine("| | /| / / _ \\/ / ___/ __ \\/ __ `__ \\/ _ \\");
            Console.WriteLine("| |/ |/ /  __/ / /__/ /_/ / / / / / /  __/ ");
            Console.WriteLine("|__/|__/\\___/_/\\___/\\____/_/ /_/ /_/\\____ ");
            Console.WriteLine("       to the Inventory System");
            Console.WriteLine(new string('=', 50));
        }

        static void ClearScreenWithWelcome()
        {
            Console.Clear();
            DisplayWelcomeMessage();
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Shop");
            Console.WriteLine("2. View My Inventory");
            Console.WriteLine("3. View/Recharge Balance");
            Console.WriteLine("4. Exit");
            Console.WriteLine(new string('=', 50));
        }

        static void ShopMenu(Shop shop, PlayerInventory playerInventory)
        {
            bool doneShopping = false;
            while (!doneShopping)
            {
                ClearScreenWithWelcome();
                Console.WriteLine("Shop Items:");
                shop.DisplayItemsForSale();
                Console.WriteLine(new string('=', 50));

                Console.WriteLine("\nShop Menu:");
                Console.WriteLine("1. Sort by Name");
                Console.WriteLine("2. Sort by Price");
                Console.WriteLine("3. Purchase Item");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Choose an option: ");
                string shopChoice = Console.ReadLine();

                switch (shopChoice)
                {
                    case "1":
                        shop.SortItemsByName();
                        break;
                    case "2":
                        shop.SortItemsByPrice();
                        break;
                    case "3":
                        bool validItem = false;
                        while (!validItem)
                        {
                            Console.Write("\nEnter the name of the item to purchase: ");
                            string itemName = Console.ReadLine();
                            if (shop.IsValidItem(itemName))
                            {
                                validItem = true;
                                Console.Write("Enter the quantity to purchase: ");
                                int quantity;
                                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                                {
                                    Console.Write("Please enter a valid quantity: ");
                                }

                                InventoryItem itemToPurchase = shop.GetItem(itemName);
                                decimal totalCost = itemToPurchase.Price * quantity;

                                if (balance >= totalCost)
                                {
                                    InventoryItem purchasedItem = shop.PurchaseItem(itemName, quantity);
                                    if (purchasedItem != null)
                                    {
                                        playerInventory.AddItem(purchasedItem);
                                        balance -= totalCost;
                                        Console.WriteLine($"\nPurchased {quantity} {itemName}(s) for {totalCost} Gems. Remaining balance: {balance} Gems");
                                        Console.Write("Do you want to buy more items? (yes/no): ");
                                        string continueShopping = Console.ReadLine().ToLower();
                                        if (continueShopping != "yes")
                                        {
                                            validItem = true;
                                            doneShopping = true;
                                        }
                                        else
                                        {
                                            validItem = false; // Reset to allow entering another item
                                            ClearScreenWithWelcome();
                                            Console.WriteLine("Shop Items:");
                                            shop.DisplayItemsForSale(); // Display updated shop
                                            Console.WriteLine(new string('=', 50));
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient balance. Please recharge your balance.");
                                    PromptToContinue();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Item not found. Please try again.");
                            }
                        }
                        break;
                    case "4":
                        doneShopping = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void ViewInventory(PlayerInventory playerInventory)
        {
            bool doneViewing = false;
            while (!doneViewing)
            {
                ClearScreenWithWelcome();
                if (playerInventory.IsEmpty())
                {
                    Console.WriteLine("No items in inventory. Please purchase some items.");
                }
                else
                {
                    Console.WriteLine("Your Inventory:");
                    playerInventory.DisplayPlayerInventory();
                }
                Console.WriteLine(new string('=', 50));

                Console.WriteLine("\n1. Back to Main Menu");
                Console.Write("Choose an option: ");
                string inventoryChoice = Console.ReadLine();

                if (inventoryChoice == "1")
                {
                    doneViewing = true;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                    PromptToContinue();
                }
            }
        }

        static void RechargeBalance()
        {
            ClearScreenWithWelcome();
            Console.WriteLine($"Current balance: {balance} Gems");
            Console.Write("Enter amount to recharge: ");
            decimal rechargeAmount;
            while (!decimal.TryParse(Console.ReadLine(), out rechargeAmount) || rechargeAmount <= 0)
            {
                Console.Write("Please enter a valid amount: ");
            }
            balance += rechargeAmount;
            Console.WriteLine($"Balance recharged. New balance: {balance} Gems");
            PromptToGoBackOrExit();
        }

        static void PromptToGoBackOrExit()
        {
            Console.WriteLine("\nPress any key to go back to the main menu...");
            Console.ReadKey();
            ClearScreenWithWelcome();
        }

        static void PromptToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
