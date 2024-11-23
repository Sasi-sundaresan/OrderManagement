using OrderManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Dao
{
    internal interface IOrderManagementRepository
    {
        bool CancelOrder(int userId, int orderId);
        bool CreateOrder(int userId, List<Product> products);
        bool CreateProduct(Product product);
        bool CreateUser(User user);
        List<Product> GetAllProducts();
        List<Product> GetOrdersByUser(int userId);
    }
}
