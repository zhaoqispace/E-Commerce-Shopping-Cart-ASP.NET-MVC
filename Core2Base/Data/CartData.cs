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
                string sqlsub1 = @"UPDATE CartDetails SET Qty = Qty - 1 WHERE (ProductID = @ProductID AND UserID = @UserID)";
                SqlCommand cmd = new SqlCommand(sqlsub1, conn);
                cmd.Parameters.AddWithValue("ProductID", productid);
                cmd.Parameters.AddWithValue("UserID", userid);
                sucess = cmd.ExecuteNonQuery();
                return sucess;
            }
        }
        public static int RemoveProductFromCart(string userid, string productid)
        {
            int sucess = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // delete row from table
                string sqlremove = @"DELETE FROM CartDetails WHERE (ProductID = @ProductID AND UserID = @UserID)";
                SqlCommand cmd = new SqlCommand(sqlremove, conn);
                cmd.Parameters.AddWithValue("ProductID", productid);
                cmd.Parameters.AddWithValue("UserID", userid);
                sucess = cmd.ExecuteNonQuery();
                return sucess;
            }
        }


        public static int EditProductQuantity(string userid, string productid, int quantity)
        {
            int sucess = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqledit = @"UPDATE CartDetails SET Qty = @quantity where (ProductID = @ProductID AND UserID = @UserID)";
                SqlCommand cmd = new SqlCommand(sqledit, conn);
                cmd.Parameters.AddWithValue("ProductID", productid);
                cmd.Parameters.AddWithValue("UserID", userid);
                cmd.Parameters.AddWithValue("quantity", quantity);
                sucess = cmd.ExecuteNonQuery();
                return sucess;
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

        //Same functions for tempDB instead
        public static List<CartDetail> GetCartInfoTemp(string session)
        {

            List<CartDetail> CartItems = new List<CartDetail>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"Select * FROM TempCartDetail INNER JOIN PRODUCT on TempCartDetail.ProductID = Product.ProductID where SessionID = @SessionID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("SessionID", session);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CartDetail CartItem = new CartDetail()
                    {
                        CartId = Convert.ToString(reader["TempCartId"]),
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
        public static int AddProductToCartTemp(string session, string productid)
        {
            int sucess = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cart = GetCartInfoTemp(session);
                var iter = from cartitem in cart where cartitem.ProductId == productid select cartitem;
                if (iter.Any() == false)
                {
                    // add count for new row
                    string sqladdrow = @"INSERT INTO TempCartDetail (SessionID,ProductID,QTY) Values(@SessionID, @ProductID, 1)";
                    SqlCommand cmd = new SqlCommand(sqladdrow, conn);
                    cmd.Parameters.AddWithValue("ProductID", productid);
                    cmd.Parameters.AddWithValue("SessionID", session);
                    sucess = cmd.ExecuteNonQuery();
                    return sucess;
                }
                else
                {
                    // +1 to table
                    string sqladd1 = @"UPDATE TempCartDetail SET QTY = QTY + 1 WHERE (ProductID = @ProductID AND SessionID = @SessionID)";
                    SqlCommand cmd = new SqlCommand(sqladd1, conn);
                    cmd.Parameters.AddWithValue("ProductID", productid);
                    cmd.Parameters.AddWithValue("SessionID", session);
                    sucess = cmd.ExecuteNonQuery();
                    return sucess;
                }
            }
        }
        public static int SubtractProductFromCartTemp(string session, string productid)
        {
            int sucess = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlsub1 = @"UPDATE TempCartDetail SET Qty = Qty - 1 WHERE (ProductID = @ProductID AND SessionID = @SessionID)";
                SqlCommand cmd = new SqlCommand(sqlsub1, conn);
                cmd.Parameters.AddWithValue("ProductID", productid);
                cmd.Parameters.AddWithValue("SessionID", session);
                sucess = cmd.ExecuteNonQuery();
                return sucess;
            }
        }
        public static int RemoveProductFromCartTemp(string session, string productid)
        {
            int sucess = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // delete row from table
                string sqlremove = @"DELETE FROM TempCartDetail WHERE (ProductID = @ProductID AND SessionID = @SessionID)";
                SqlCommand cmd = new SqlCommand(sqlremove, conn);
                cmd.Parameters.AddWithValue("ProductID", productid);
                cmd.Parameters.AddWithValue("SessionID", session);
                sucess = cmd.ExecuteNonQuery();
                return sucess;
            }
        }


        public static int EditProductQuantityTemp(string session, string productid, int quantity)
        {
            int sucess = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqledit = @"UPDATE TempCartDetail SET Qty = @quantity where (ProductID = @ProductID AND SessionID = @SessionID)";
                SqlCommand cmd = new SqlCommand(sqledit, conn);
                cmd.Parameters.AddWithValue("ProductID", productid);
                cmd.Parameters.AddWithValue("SessionID", session);
                cmd.Parameters.AddWithValue("quantity", quantity);
                sucess = cmd.ExecuteNonQuery();
                return sucess;
            }
        }
        public static int NumberOfCartItemsTemp(string session)
        {
            List<CartDetail> cartitemlist = new List<CartDetail>();
            cartitemlist = CartData.GetCartInfoTemp(session);
            int numberofitems = 0;
            foreach (CartDetail cartitem in cartitemlist)
            {
                numberofitems = numberofitems + cartitem.qty;
            }
            return numberofitems;
        }


        //Misc/Merge/Delete Date
        public static int MergeTempCartAndDelete(string UserID, string SessionID)
        {
            int sucess = 0;
            List<CartDetail> TempCartItems = new List<CartDetail>();
            TempCartItems = GetCartInfoTemp(SessionID);
            if (TempCartItems != null)
            {
                foreach (CartDetail cartData in TempCartItems)
                {
                    sucess = Mergecart(cartData.ProductId, cartData.qty, UserID);
                    if (sucess == 0) break;
                    sucess = RemoveProductFromCartTemp(SessionID, cartData.ProductId);
                    if (sucess == 0) break;
                }
            }

            return sucess;
        }

        public static int Mergecart(string productid, int qty, string userid)
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
                    string sqladdrow = @"INSERT INTO CartDetails (UserId,ProductID,Qty) Values(@UserID, @ProductID, @qty)";
                    SqlCommand cmd = new SqlCommand(sqladdrow, conn);
                    cmd.Parameters.AddWithValue("ProductID", productid);
                    cmd.Parameters.AddWithValue("UserID", userid);
                    cmd.Parameters.AddWithValue("qty", qty);
                    sucess = cmd.ExecuteNonQuery();
                    return sucess;
                }
                else
                {
                    // +1 to table
                    string sqladd1 = @"UPDATE CartDetails SET Qty = Qty + @qty WHERE (ProductID = @ProductID AND UserID = @UserID)";
                    SqlCommand cmd = new SqlCommand(sqladd1, conn);
                    cmd.Parameters.AddWithValue("ProductID", productid);
                    cmd.Parameters.AddWithValue("UserID", userid);
                    cmd.Parameters.AddWithValue("qty", qty);
                    sucess = cmd.ExecuteNonQuery();
                    return sucess;
                }
            }
        }
    }
}

