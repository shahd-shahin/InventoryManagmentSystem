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

                    case "2":
                        ViewAllProducts(inventory);
                        break;
                    case "3":
                        EditProduct(inventory);
                        break;
                    case "4":
                        DeleteProduct(inventory);
                        break;
                    case "5":
                        SearchProduct(inventory);
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
            ShowSuccess("Thank you for using the Inventory Management System. Goodbye!");

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
        private static void ViewAllProducts(Inventory inventory)
        {
            ShowTitle("--- All products ---");

            IReadOnlyList<Product> products = inventory.GetAllProducts();

            if (products.Count == 0)
            {
                ShowInfo("The inventory is empty.");
                return;
            }

            int nameWidth = Math.Max("Name".Length, products.Max(p => p.Name.Length));
            const int priceWidth = 12;
            const int quantityWidth = 10;

            string header = "#".PadRight(4)
                + "Name".PadRight(nameWidth) + "  "
                + "Price".PadLeft(priceWidth) + "  "
                + "Quantity".PadLeft(quantityWidth);

            Console.WriteLine(header);
            Console.WriteLine(new string('-', header.Length));

            for (int i = 0; i < products.Count; i++)
            {
                Product product = products[i];
                string price = product.Price.ToString("F2", CultureInfo.InvariantCulture);

                Console.WriteLine(
                    (i + 1).ToString().PadRight(4)
                    + product.Name.PadRight(nameWidth) + "  "
                    + price.PadLeft(priceWidth) + "  "
                    + product.Quantity.ToString().PadLeft(quantityWidth));
            }

            Console.WriteLine(new string('-', header.Length));
            ShowInfo($"Total products: {products.Count}");
        }
        private static void EditProduct(Inventory inventory)
        {
            ShowTitle("--- Edit a product ---");

            string name = ReadRequiredText("Enter the name of the product to edit: ");
            Product? product = inventory.FindProduct(name);

            if (product == null)
            {
                ShowError($"Product '{name}' was not found.");
                return;
            }

            ShowInfo("Current details:");
            Console.WriteLine(product.GetDetails());
            Console.WriteLine();
            ShowInfo("Press Enter to keep a current value.");

            Console.Write("New name: ");
            string? newName = Console.ReadLine();
            decimal? newPrice = ReadOptionalPrice("New price: ");
            int? newQuantity = ReadOptionalQuantity("New quantity: ");

            if (inventory.EditProduct(name, newName, newPrice, newQuantity))
            {
                ShowSuccess("Product updated successfully. New details:");
                Console.WriteLine(product.GetDetails());
            }
            else
            {
                ShowError("Could not update: another product already uses that name. No changes were made.");
            }
        }
        private static decimal? ReadOptionalPrice(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value) && value >= 0)
                {
                    return value;
                }

                ShowError("Invalid price. Enter a number that is 0 or greater, or press Enter to keep the current one.");
            }
        }

        private static int? ReadOptionalQuantity(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value) && value >= 0)
                {
                    return value;
                }

                ShowError("Invalid quantity. Enter a whole number that is 0 or greater, or press Enter to keep the current one.");
            }
        }


        private static void DeleteProduct(Inventory inventory)
        {
            ShowTitle("--- Delete a product ---");

            string name = ReadRequiredText("Enter the name of the product to delete: ");

            if (inventory.DeleteProduct(name))
            {
                ShowSuccess($"Product '{name}' was deleted.");
            }
            else
            {
                ShowError($"Product '{name}' was not found.");
            }
        }

        private static void SearchProduct(Inventory inventory)
        {
            ShowTitle("--- Search for a product ---");

            string name = ReadRequiredText("Enter the product name to search for: ");
            Product? product = inventory.FindProduct(name);

            if (product == null)
            {
                ShowError($"Product '{name}' was not found.");
                return;
            }

            ShowSuccess("Product found:");
            Console.WriteLine(product.GetDetails());
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
