using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Model
{
    internal class Electronics : Product
    {
        public string Brand { get; set; }
        public int WarrantyPeriod { get; set; } 

        // Default Constructor
        public Electronics()
        {
        }

        
        public Electronics(int productId, string productName, string description, double price, int quantityInStock, string productType, string brand, int warrantyPeriod)
            : base(productId, productName, description, price, quantityInStock, productType)
        {
            Brand = brand;
            WarrantyPeriod = warrantyPeriod;
        }

     
        //public new void DisplayProductDetails()
        //{
            
        //    base.DisplayProductDetails();
        //    Console.WriteLine($"Brand: {Brand}");
        //    Console.WriteLine($"Warranty Period: {WarrantyPeriod} months");
        //}
    }
}
