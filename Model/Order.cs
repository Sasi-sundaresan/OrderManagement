using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Model
{
    internal class Order
    {
        public int OrderId { get; set; } 
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }

       
        public Order(int userId, int productId, int productId1, DateTime orderDate, string orderStatus)
        {
            UserId = userId;
            ProductId = productId;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
        }


        public Order() { }
    }
}

