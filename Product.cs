using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagmentSystem
{
    public class Product
    {
        private string name = string.Empty;
        private decimal price;
        private int quantity;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public string GetDetails()
        {
            string formattedPrice = Price.ToString("F2");
            return $"  Name     : {Name}{Environment.NewLine}" +
                   $"  Price    : {formattedPrice}{Environment.NewLine}" +
                   $"  Quantity : {Quantity}";
        }
    }


}
