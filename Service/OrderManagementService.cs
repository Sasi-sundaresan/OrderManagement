using OrderManagementSystem.Dao;
using OrderManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Service
{
    internal class OrderManagementService
    {
        private IOrderManagementRepository orderRepository;

        public OrderManagementService()
        {
            orderRepository = new OrderProcess();
        }
        public bool CreateUser(User user)
        {
            return orderRepository.CreateUser(user);
        }

        public bool CreateProduct(Product product) 
        {
            return orderRepository.CreateProduct(product);
        }

        public bool CreateOrder(int userId, List<Product> products)
        {
            return orderRepository.CreateOrder(userId, products);
        }

        public bool CancelOrder(int userId, int orderId)
        {
            return orderRepository.CancelOrder(userId, orderId);
        }

        public List<Product> GetAllProducts()
        {
            return orderRepository.GetAllProducts();
        }

        public List<Product> GetOrdersByUser(int userId) 
        {
            return orderRepository.GetOrdersByUser(userId);
        }

    }
}
