using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Core2Base.Models;
using Microsoft.AspNetCore.Http;

namespace Core2Base.Data
{
    public class CartData : Data
    {
        public static List<CartDetail> GetCartInfo(string userid)
        {
            
            List<CartDetail> CartItems = new List<CartDetail>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"Select * FROM Cart INNER JOIN CartDetail on Cart.CartID = CartDetail.CartID INNER JOIN PRODUCT on CartDetail.ProductID = Product.ProductID where UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID", userid);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CartDetail CartItem = new CartDetail()
                    {
                        CartId = (int)(reader["CartId"]),
                        ProductId = (Guid)reader["ProductID"],
                        qty = (int)reader["Qty"],
                        ProductName = (string)reader["ProductName"],
                        ProductDesc = (string)reader["ProductDesc"],
                        ProductCat = (string)reader["ProductCat"],
                        UnitPrice = (double)reader["Price"],
                        ProductImg = (string)reader["ProductImg"]
    };
                    CartItems.Add(CartItem);
                }
                return CartItems;
            }
        }
    }
}
