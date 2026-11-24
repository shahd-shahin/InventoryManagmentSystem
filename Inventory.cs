using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagmentSystem
{
    public class Inventory
    {
        private readonly List<Product> products = new();

        public Product? FindProduct(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            string trimmed = name.Trim();

            return products.Where(p => string.Equals(p.Name, trimmed, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
        }

        public bool AddProduct(Product product)
        {
            if (FindProduct(product.Name) != null)
            {
                return false;
            }

            products.Add(product);
            return true;
        }

        public IReadOnlyList<Product> GetAllProducts()
        {
            return products.AsReadOnly();
        }


    }



}
