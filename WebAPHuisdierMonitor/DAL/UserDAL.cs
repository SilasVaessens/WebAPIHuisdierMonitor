using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor.DAL
{
    public static class UserDAL
    {
        private readonly static string ConnString = "Data Source=LAPTOP-4NFCKE65;Initial Catalog=PetMonitorDB;Integrated Security=True";
        private readonly static SqlConnection conn = new SqlConnection(ConnString);


        public static bool? UserExists(int UserID)
        {
            int ID = new int();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Users WHERE EXISTS (SELECT * FROM Users WHERE UserID = @UserID)";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ID = (int)reader["UserID"];
                }
                conn.Close();
                if (ID == UserID)
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
        public static bool? UserExists(string UserName)
        {
            string userName = "";
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Users WHERE EXISTS (SELECT * FROM Users WHERE UserName = @UserName)";
            cmd.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userName = (string)reader["UserID"];
                }
                conn.Close();
                if (userName == UserName)
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
        public static void AddUser (User user)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Users VALUES (@UserName, @PassWordHash)";
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@PassWordHash", user.PassWordHash);
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

        public static void DeleteUser(int UserID)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Users WHERE UserID = @UserID";
            cmd.Parameters.AddWithValue("@UserID", UserID);
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

        public static int ValidateLogIn(string UserName, string PassWordHash)
        {
            int UserID = 0;
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Users WHERE UserName = @UserName AND PassWordHash = @PassWordHash";
            cmd.Parameters.AddWithValue("@Username", UserName);
            cmd.Parameters.AddWithValue("@PassWordHash", PassWordHash);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                }
                conn.Close();
                return UserID;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }
    }
}
