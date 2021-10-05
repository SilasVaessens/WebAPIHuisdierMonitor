﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;


namespace WebAPIHuisdierMonitor.DAL
{
    public static class ProductDAL
    {
        private readonly static string ConnString = ""; 
        private readonly static SqlConnection conn = new SqlConnection(ConnString);

        public static bool? ProductExists(int ProductID, int UserID)
        {
            Product product = new Product();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Products WHERE EXISTS (SELECT * FROM Products WHERE ProductID = @ProductID AND UserID = @UserID";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product.ProductID = (int)reader["ProductID"];
                    product.UserID = (int)reader["UserID"];
                }
                conn.Close();
                if (product.ProductID == ProductID && product.UserID == UserID)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                conn.Close();
                return null;
            }
        }

        public static bool? ProductExists(string UniqueIdentifier)
        {
            Product product = new Product();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Products WHERE EXISTS (SELECT * FROM Products WHERE UniqueIdentifier = @UniqueIdentifier";
            cmd.Parameters.AddWithValue("@UniqueIdentifier", UniqueIdentifier);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product.UniqueIdentifier = reader["UniqueIdentifier"].ToString();
                }
                if (product.UniqueIdentifier == UniqueIdentifier)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (SqlException)
            {
                conn.Close();
                return null;
            }

        }

        public static void DeleteProduct(int ProductID)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Products WHERE ProductID = @ProductID";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }
        
        public static List<Product> GetAllProducts(string UniqueIdentifier) //gaat niet werken, moet nog aanpassen
        {
            List<Product> Products = new List<Product>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Products WHERE UniqueIdentifier = @UniqueIdentifier";
            cmd.Parameters.AddWithValue("@UniqueIdentifier", UniqueIdentifier);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        UniqueIdentifier = (string)reader["UniqueIdentifier"],
                        Name = (string)reader["Name"]
                    };
                    Products.Add(product);
                }
                conn.Close();
                return Products;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static void UpdateProduct(int UserID, string Name, int ProductID)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Products SET UserID = @UserID, Name = @Name WHERE ProductID = @ProductID";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static Product GetProductIDAndUserID(string UniqueIdentifier)
        {
            Product product = new Product();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Products WHERE UniqueIdentifier = @UniqueIdentifier";
            cmd.Parameters.AddWithValue("@UniqueIdentifier", UniqueIdentifier);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product.ProductID = (int)reader["ProductID"];
                    product.UserID = (int)reader["UserID"];
                }
                conn.Close();
                return product;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new AccessViolationException();
            }
        }
    }
}
