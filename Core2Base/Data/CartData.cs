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
                        CartId = (string)(reader["CartId"]),
                        ProductId = (string)reader["ProductID"],
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
                        string sqladdrow = @"INSERT INTO CartDetails (UserId,ProductID,Qty)
                                             Values("+userid+","+productid+",1)";
                        SqlCommand cmd = new SqlCommand(sqladdrow, conn);
                        sucess = cmd.ExecuteNonQuery();
                        return sucess;
                    }
                    else
                    {
                        // +1 to table
                        string sqladd1 = @"UPDATE CartDetails 
                                            SET Qty = Qty + 1 
                                            WHERE ( 
                                            ProductID = " + productid + 
                                            "AND UserID = " + userid + ")";
                        SqlCommand cmd = new SqlCommand(sqladd1, conn);
                        sucess = cmd.ExecuteNonQuery();
                        return sucess;
                    }

            }
        }
    }
}

