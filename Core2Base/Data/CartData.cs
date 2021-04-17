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
                string sql = @"Select * FROM CartDetails INNER JOIN PRODUCT on CartDetails.ProductID = Product.ProductID where UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("UserID", userid);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CartDetail CartItem = new CartDetail()
                    {
                        CartId = Convert.ToString(reader["CartId"]),
                        ProductId = Convert.ToString(reader["ProductID"]),
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

        public static int AddProductToCart(string userid, string productid)
        {
            int sucess = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cart = GetCartInfo(userid);
                var iter = from cartitem in cart where cartitem.ProductId == productid select cartitem;
                if (iter.Any() == false)
                {
                    // add count for new row
                    string sqladdrow = @"INSERT INTO CartDetails (UserId,ProductID,Qty) Values(@UserID, @ProductID, 1)";
                    SqlCommand cmd = new SqlCommand(sqladdrow, conn);
                    cmd.Parameters.AddWithValue("ProductID", productid);
                    cmd.Parameters.AddWithValue("UserID", userid);
                    sucess = cmd.ExecuteNonQuery();
                    return sucess;
                }
                else
                {
                    // +1 to table
                    string sqladd1 = @"UPDATE CartDetails SET Qty = Qty + 1 WHERE (ProductID = @ProductID AND UserID = @UserID)";
                    SqlCommand cmd = new SqlCommand(sqladd1, conn);
                    cmd.Parameters.AddWithValue("ProductID", productid);
                    cmd.Parameters.AddWithValue("UserID", userid);
                    sucess = cmd.ExecuteNonQuery();
                    return sucess;
                }
            }
        }
        public static int SubtractProductFromCart(string userid, string productid)
        {
            int sucess = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cart = GetCartInfo(userid);
                var iter = from cartitem in cart where cartitem.ProductId == productid select cartitem;
                if (iter.Any() == false)
                {
                    // add count for new row
                    string sqlsubrow = @"INSERT INTO CartDetails (UserId,ProductID,Qty) Values(@UserID, @ProductID, 1)";
                    SqlCommand cmd = new SqlCommand(sqlsubrow, conn);
                    cmd.Parameters.AddWithValue("ProductID", productid);
                    cmd.Parameters.AddWithValue("UserID", userid);
                    sucess = cmd.ExecuteNonQuery();
                    return sucess;
                }
                else
                {
                    // -1 to table
                    string sqlsub1 = @"UPDATE CartDetails SET Qty = Qty - 1 WHERE (ProductID = @ProductID AND UserID = @UserID)";
                    SqlCommand cmd = new SqlCommand(sqlsub1, conn);
                    cmd.Parameters.AddWithValue("ProductID", productid);
                    cmd.Parameters.AddWithValue("UserID", userid);
                    sucess = cmd.ExecuteNonQuery();
                    return sucess;
                }
            }
        }
        public static int NumberOfCartItems(string userID)
        {
            List<CartDetail> cartitemlist = new List<CartDetail>();
            cartitemlist = CartData.GetCartInfo(userID);
            int numberofitems = 0;
            foreach (CartDetail cartitem in cartitemlist)
            {
                numberofitems = numberofitems + cartitem.qty;
            }
            return numberofitems;
        }
    }
}

