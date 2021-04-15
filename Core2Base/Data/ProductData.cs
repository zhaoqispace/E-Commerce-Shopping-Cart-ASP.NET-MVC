using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Core2Base.Models;

namespace Core2Base.Data
{
    public class ProductData : Data
    {
        public static List<Product> GetProductInfo()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"Select * FROM Product";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = (Guid)(reader["ProductId"]),
                        Name = (string)reader["ProductName"],
                        Description = (string)reader["ProductDesc"],
                        Category = (string)reader["ProductCat"],
                        UnitPrice = (double)reader["Price"],
                        Image = (string)reader["ProductImg"]
                    };
                    products.Add(product);
                }
                return products;
            }
        }

        public static List<Product> SearchProducts(string searchTerm)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"Select * FROM Product where ProductName like @ProductName OR Price like @Price OR ProductDesc like @ProductDesc";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductName", '%' + searchTerm + '%');
                cmd.Parameters.AddWithValue("@Price", '%' + searchTerm + '%');
                cmd.Parameters.AddWithValue("@ProductDesc", '%' + searchTerm + '%');
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = (Guid)reader["ProductId"],
                        Name = (string)reader["ProductName"],
                        Description = (string)reader["ProductDesc"],
                        Category = (string)reader["ProductCat"],
                        UnitPrice = (double)reader["Price"],
                        Image = (string)reader["ProductImg"],
                    };
                    products.Add(product);
                }


                return products;
            }
        }

    }
}
