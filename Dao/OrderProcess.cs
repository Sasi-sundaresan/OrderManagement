//using OrderManagementSystem.Exception;
using OrderManagementSystem.Model;
using OrderManagementSystem.Util;
using System.Data.SqlClient;


namespace OrderManagementSystem.Dao
{
    internal class OrderProcess : IOrderManagementRepository
    {
        private string connectionString;
       

        public OrderProcess()
        {
            connectionString = DbConnUtil.GetConnectionString();
        }

        public bool CreateUser(User user)
        {

            string insertQuery = @"INSERT INTO Users (username, [password], [role]) 
                                   VALUES (@username, @password, @role)";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@username", user.Username);
                    insertCommand.Parameters.AddWithValue("@password", user.Password);
                    insertCommand.Parameters.AddWithValue("@role", user.Role);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }



        public bool CreateOrder(int userId, List<Product> products)
        {
            string checkUserQuery = "SELECT COUNT(1) FROM Users WHERE userId = @userId";
            string insertOrderQuery = @"INSERT INTO Orders (productId, orderDate, orderstatus) 
                                VALUES (@productId, @orderDate, @orderStatus)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

               
                SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection);
                checkUserCommand.Parameters.AddWithValue("@userId", userId);
                int userExists = (int)checkUserCommand.ExecuteScalar();

                if (userExists == 0)
                {
                    throw new Exception("User not found in the database.");
                }



                foreach (var product in products)
                {
                    SqlCommand insertOrderCommand = new SqlCommand(insertOrderQuery, connection);
                    insertOrderCommand.Parameters.AddWithValue("@productId", product.ProductId);
                    insertOrderCommand.Parameters.AddWithValue("@orderDate", DateTime.Now);
                    insertOrderCommand.Parameters.AddWithValue("@orderStatus", "Pending");
                    insertOrderCommand.ExecuteNonQuery();
                }

                return true;


            }

       
        }

        
        public bool CancelOrder(int userId, int orderId)
        {
            string checkOrderQuery = @"SELECT COUNT(1) 
                               FROM Orders 
                               WHERE orderId = @orderId
                                ";

            string updateOrderQuery = @"UPDATE Orders 
                                SET orderstatus = 'Cancelled' 
                                WHERE orderId = @orderId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

            
                SqlCommand checkOrderCommand = new SqlCommand(checkOrderQuery, connection);
                checkOrderCommand.Parameters.AddWithValue("@orderId", orderId);

                var result = checkOrderCommand.ExecuteScalar();

            
                if (result == null || Convert.ToInt32(result) == 0)
                {
                    throw new Exception("Order not found or cannot be cancelled (either not yours or already processed).");
                }

                SqlCommand updateOrderCommand = new SqlCommand(updateOrderQuery, connection);
                updateOrderCommand.Parameters.AddWithValue("@orderId", orderId);
                int rowsAffected = updateOrderCommand.ExecuteNonQuery();

              
                return rowsAffected > 0;
            }
        }

       
        public bool CreateProduct(Product product)
        {
            string insertProductQuery = @"INSERT INTO Products (productName, [description], price, quantityInStock, productType) 
                                  VALUES (@productName, @description, @price, @quantityInStock, @productType)";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand insertProductCommand = new SqlCommand(insertProductQuery, connection))
                    {

                        insertProductCommand.Parameters.AddWithValue("@productName", product.ProductName);
                        insertProductCommand.Parameters.AddWithValue("@description", product.Description);
                        insertProductCommand.Parameters.AddWithValue("@price", product.Price);
                        insertProductCommand.Parameters.AddWithValue("@quantityInStock", product.QuantityInStock);
                        insertProductCommand.Parameters.AddWithValue("@productType", product.ProductType);

                        int rowsAffected = insertProductCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            //Console.WriteLine("Product created successfully.");
                            return true;
                        }
                        else
                        {
                            //Console.WriteLine("Error: Failed to create product.");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }
        
        public List<Product> GetAllProducts()
        {
            string query = @"SELECT productId, productName, [description], price, quantityInStock, productType FROM Products";
            var products = new List<Product>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                            ProductName = reader.GetString(reader.GetOrdinal("productName")),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description")),
                            Price = Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("price"))),
                            QuantityInStock = reader.GetInt32(reader.GetOrdinal("quantityInStock")),
                            ProductType = reader.GetString(reader.GetOrdinal("productType"))
                        });
                    }
                }

                reader.Close();
            }

            return products;
        }

      
        public List<Product> GetOrdersByUser(int userId)
        {
            string query = @"SELECT p.productId, p.productName, p.description, p.price, p.quantityInStock, p.productType 
                     FROM Products p 
                     JOIN Orders o ON  o.productId = p.productId";
                   

            var products = new List<Product>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product
                            {
                                ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                                ProductName = reader.GetString(reader.GetOrdinal("productName")),
                                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description")),
                                Price = Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("price"))),
                                QuantityInStock = reader.GetInt32(reader.GetOrdinal("quantityInStock")),
                                ProductType = reader.GetString(reader.GetOrdinal("productType"))
                            };
                            products.Add(product);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
              
                Console.WriteLine($"SQL Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"General Exception: {ex.Message}");
            }

            return products;
        }
        }
}
