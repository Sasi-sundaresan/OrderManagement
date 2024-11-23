using OrderManagementSystem.Model;
using OrderManagementSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Main
{
    internal class OrderManagemet
    {
        private  OrderManagementService orderService;

        public OrderManagemet()
        {
            orderService = new OrderManagementService();
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nOrder Management System");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("7. Create Order");
                Console.WriteLine("3. Cancel Order");
                Console.WriteLine("4. Get All Products");
                Console.WriteLine("5. Get Orders by User");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateUser();
                        break;
                    case "2":
                        CreateProduct();
                        break;
                    case "3":
                        CancelOrder();
                        break;
                    case "4":
                        GetAllProducts();
                        break;
                    case "5":
                        GetOrdersByUser();
                        break;

                    case "7":
                        CreateOrder();
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void CreateUser()
        {
            Console.WriteLine("\n--- Create User ---");
            Console.Write("Enter User Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter User Password: ");
            string password = Console.ReadLine();

            Console.Write("Enter User Role (Admin/User): ");
            string role = Console.ReadLine();

            var user = new User(name, password, role);
            orderService.CreateUser(user);

            Console.WriteLine("User created successfully.");
        }

        private void CreateProduct()
        {
            Console.WriteLine("\n--- Create Product ---");

            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter Product Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Product Price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Product Quantity in Stock: ");
            int quantityInStock = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Product Type (Electronics/Clothing): ");
            string productType = Console.ReadLine();

            var product = new Product
            {
                ProductName = productName,
                Description = description,
                Price = price,
                QuantityInStock = quantityInStock,
                ProductType = productType
            };

            bool isProductCreated = orderService.CreateProduct(product);

            if (isProductCreated)
            {
                Console.WriteLine("Product created successfully.");
            }
            else
            {
                Console.WriteLine("Failed to create product.");
            }
        }

        private void CreateOrder()
        {
            Console.WriteLine("\n--- Create Order ---");

            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            List<Product> products = new List<Product>();

            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            products.Add(new Product { ProductId = productId });

            bool orderCreated = orderService.CreateOrder(userId, products);

            if (orderCreated)
            {
                Console.WriteLine("Order created successfully.");
            }
            else
            {
                Console.WriteLine("Failed to create the order.");
            }
        }

        private void CancelOrder()
        {
            Console.WriteLine("\n--- Cancel Order ---");

            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            Console.Write("Enter Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            bool isOrderCancelled = orderService.CancelOrder(userId, orderId);

            if (isOrderCancelled)
            {
                Console.WriteLine("Order cancelled successfully.");
            }
            else
            {
                Console.WriteLine("Failed to cancel the order.");
            }
        }

        private void GetAllProducts()
        {
            Console.WriteLine("\n--- All Products ---");

            var products = orderService.GetAllProducts();

            if (products != null && products.Count > 0)
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}");
                    Console.WriteLine($"Product Name: {product.ProductName}");
                    Console.WriteLine($"Description: {product.Description}");
                    Console.WriteLine($"Price: {product.Price}");
                    Console.WriteLine($"Quantity in Stock: {product.QuantityInStock}");
                    Console.WriteLine($"Product Type: {product.ProductType}");
                    Console.WriteLine("-----------------------------");
                }
            }
            else
            {
                Console.WriteLine("No products found.");
            }
        }

        private void GetOrdersByUser()
        {
            Console.WriteLine("\n--- Orders by User ---");

            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            var products = orderService.GetOrdersByUser(userId);

            if (products != null && products.Count > 0)
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}");
                    Console.WriteLine($"Product Name: {product.ProductName}");
                    Console.WriteLine($"Description: {product.Description}");
                    Console.WriteLine($"Price: {product.Price}");
                    Console.WriteLine($"Quantity in Stock: {product.QuantityInStock}");
                    Console.WriteLine($"Product Type: {product.ProductType}");
                    Console.WriteLine("-----------------------------");
                }
            }
            else
            {
                Console.WriteLine("No orders found for this user.");
            }
        }

    }
}
