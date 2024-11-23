using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Model
{
    internal class Clothing : Product
    {
      
        public string Size { get; set; }
        public string Color { get; set; }

        public Clothing()
        {
        }

        
        public Clothing(int productId, string productName, string description, double price, int quantityInStock, string productType, string size, string color)
            : base(productId, productName, description, price, quantityInStock, productType)
        {
            Size = size;
            Color = color;
        }

    
        //public new void DisplayProductDetails()
        //{ 
        //    base.DisplayProductDetails();
        //    Console.WriteLine($"Size: {Size}");
        //    Console.WriteLine($"Color: {Color}");
        //}
    }
}
