using System;
using System.Collections.Generic;

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
        public bool EditProduct(string currentName, string? newName, decimal? newPrice, int? newQuantity)
        {
            Product? product = FindProduct(currentName);

            if (product == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(newName))
            {
                Product? other = FindProduct(newName);

                if (other != null && other != product)
                {
                    return false;
                }

                product.Name = newName.Trim();
            }

            if (newPrice.HasValue)
            {
                product.Price = newPrice.Value;
            }

            if (newQuantity.HasValue)
            {
                product.Quantity = newQuantity.Value;
            }

            return true;
        }

        public bool DeleteProduct(string name)
        {
            Product? product = FindProduct(name);

            if (product == null)
            {
                return false;
            }

            return products.Remove(product);
        }




    }



}
