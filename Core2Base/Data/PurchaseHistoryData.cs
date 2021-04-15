using Core2Base.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Core2Base.Data
{
    public class PurchaseHistoryData : Data
    {
        public static List<PurchaseHistory> GetPurchaseHistory()
        {
            //declare purchaseList
            List<PurchaseHistory> purchaseHistories = new List<PurchaseHistory>();
            //declare connection, sqlcommand, reader
            SqlConnection sqlConn = null;
            SqlCommand cmd;
            SqlDataReader reader;

            using (sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                //query for product
                string sql = @"Select distinct(p.ProductName),p.ProductID,p.ProductDesc, p.Price  from Product p, OrderDetails od , [dbo].[Order] o where p.ProductID=od.ProductID and od.OrderID=o.OrderID";

                cmd = new SqlCommand(sql, sqlConn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //count for Quantity
                    int count = 0;
                    //declare list of activation and date
                    List<string> activationStatus = new List<string> { };
                    List<string> dateOfPurchase = new List<string> { };

                    using (SqlConnection conn2 = new SqlConnection(connectionString))
                    {
                        //getting purchase date and activation status
                        string orderSql = "Select CONVERT(varchar(10),o.DateOfPurchase,101) as DateOfPurchase, od.ActivationStatus,p.Price from Product p, OrderDetails od , [dbo].[Order] o where p.ProductID=od.ProductID and od.OrderID=o.OrderID and p.ProductID=@ProductID";
                        using (SqlCommand cmd_order = new SqlCommand(orderSql, conn2))
                        {
                            SqlParameter param = new SqlParameter();
                            param.ParameterName = "@ProductID";
                            param.Value = reader["ProductID"];

                            // add new parameter to command object
                            cmd_order.Parameters.Add(param);
                            //open connection
                            conn2.Open();
                            using (SqlDataReader dr_order = cmd_order.ExecuteReader())
                            {
                                while (dr_order.Read())
                                {
                                    //adding object into list
                                    Debug.WriteLine(dr_order["DateOfPurchase"].ToString());
                                    dateOfPurchase.Add(dr_order["DateOfPurchase"].ToString());
                                    activationStatus.Add(dr_order["ActivationStatus"].ToString());
                                    count += 1;
                                }
                            }

                        }

                    }
                    //adding data to purchaseHistory model
                    PurchaseHistory product = new PurchaseHistory()
                    {
                        ProductID = (System.Guid)reader["ProductID"],
                        ProductName = (string)reader["ProductName"],
                        ProductDescription = (string)reader["ProductDesc"],
                        ActivationStatus = activationStatus,
                        Quantity = count,
                        Price = (double)reader["Price"],
                        DateOfPurchase = dateOfPurchase
                    };
                    purchaseHistories.Add(product);
                }
                reader.Close(); // <- too easy to forget
                reader.Dispose(); // <- too easy to forget
                sqlConn.Close(); // <- too easy to forget

                return purchaseHistories;
            }
        }

        public static string GetDateAndActivation(string date, string productID)
        {
            SqlConnection sqlConn = null;
            SqlCommand cmd;
            SqlDataReader reader;
            string activationCode = "";
            using (sqlConn = new SqlConnection(connectionString))
            {
                //query for date and activation
                string sql = @"select * from OrderDetails od, [dbo].[Order] o where o.OrderID=od.OrderID and o.DateOfPurchase=@Date and od.ProductID=@ProductID";

                cmd = new SqlCommand(sql, sqlConn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Date";
                param.Value = date;

                // add new parameter to command object
                cmd.Parameters.Add(param);

                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@ProductID";
                param2.Value = productID;
                cmd.Parameters.Add(param2);

                sqlConn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    activationCode = reader["ActivationStatus"].ToString();
                }
            }

            return activationCode;
        }
    }
}
