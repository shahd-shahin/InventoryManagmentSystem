using System.Globalization;

namespace InventoryManagmentSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new();
            bool running = true;

            while (running)
            {
                ShowMenu();
                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice?.Trim())
                {
                    case "1":
                        AddProduct(inventory);
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        ShowError("Invalid option. Please choose a number from 1 to 6.");
                        break;
                }

                if (running)
                {
                    PauseBeforeMenu();
                }
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            ShowTitle("==================================");
            ShowTitle("   INVENTORY MANAGEMENT SYSTEM");
            ShowTitle("==================================");
            Console.WriteLine("1. Add a product");
            Console.WriteLine("2. View all products");
            Console.WriteLine("3. Edit a product");
            Console.WriteLine("4. Delete a product");
            Console.WriteLine("5. Search for a product");
            Console.WriteLine("6. Exit");
            ShowTitle("==================================");
            Console.Write("Enter your choice (1-6): ");
        }


        private static void AddProduct(Inventory inventory)
        {
            ShowTitle("--- Add a product ---");

            string name = ReadRequiredText("Product name: ");

            if (inventory.FindProduct(name) != null)
            {
                ShowError($"A product named '{name}' already exists. Nothing was added.");
                return;
            }

            decimal price = ReadPrice("Price: ");
            int quantity = ReadQuantity("Quantity in stock: ");

            Product product = new(name, price, quantity);

            if (inventory.AddProduct(product))
            {
                ShowSuccess($"Product '{product.Name}' was added successfully.");
            }
            else
            {
                ShowError("The product could not be added.");
            }
        }

/// //////////////////////validations////////////////////////////////////////////////

        private static string ReadRequiredText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                ShowError("Input cannot be empty. Please try again.");
            }
        }

        private static decimal ReadPrice(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value) && value >= 0)
                {
                    return value;
                }

                ShowError("Invalid price. Enter a number that is 0 or greater (example: 12.50).");
            }
        }

        private static int ReadQuantity(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value) && value >= 0)
                {
                    return value;
                }

                ShowError("Invalid quantity. Enter a whole number that is 0 or greater.");
            }
        }

/// //////////////////////////////////////////////////////////

        private static void ShowTitle(string message)
        {
            WriteColored(message, ConsoleColor.Cyan);
        }

        private static void ShowSuccess(string message)
        {
            WriteColored(message, ConsoleColor.Green);
        }

        private static void ShowError(string message)
        {
            WriteColored(message, ConsoleColor.Red);
        }

        private static void ShowInfo(string message)
        {
            WriteColored(message, ConsoleColor.Yellow);
        }

        private static void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void PauseBeforeMenu()
        {
            Console.WriteLine();
            Console.Write("Press Enter to return to the menu...");
            Console.ReadLine();
        }
    }







}
