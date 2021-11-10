using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor.DAL
{
    public static class UserDAL
    {
        private readonly static string ConnString = "Server=tcp:petmonitor.database.windows.net,1433;Initial Catalog=PetMonitorDB;Persist Security Info=False;User ID=PetMonitor;Password=99Siva'02;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly static SqlConnection conn = new SqlConnection(ConnString);


        public static bool? UserExists(int UserID)
        {
            int ID = new int();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Users WHERE UserID = @UserID";
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
            cmd.CommandText = "SELECT * FROM Users WHERE UserName = @UserName";
            cmd.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userName = (string)reader["UserName"];
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
            catch (SqlException e)
            {
                Debug.WriteLine(e);
                conn.Close();
                return null;
            }

        }
        public static void AddUser (User user)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Users VALUES (@UserName, @PassWordHash, @Salt)";
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@PassWordHash", user.PassWordHash);
            cmd.Parameters.AddWithValue("@Salt", user.Salt);
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

        public static User GetUser()
        {
            User user = new User();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Users WHERE UserID = (SELECT max(UserID) from Users)";
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.UserID = (int)reader["UserID"];
                    user.UserName = (string)reader["UserName"];
                    user.PassWordHash = (string)reader["PassWordHash"];
                }
                conn.Close();
                return user;
            }
            catch (SqlException)
            {
                conn.Close();
                throw;
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

        public static User ValidateLogin(string UserName)
        {
            User ToValidate = new User();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Users WHERE UserName = @UserName";
            cmd.Parameters.AddWithValue("@Username", UserName);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ToValidate.UserID = (int)reader["UserID"];
                    ToValidate.PassWordHash = (string)reader["PassWordHash"];
                    ToValidate.Salt = (string)reader["Salt"];
                }
                conn.Close();
                return ToValidate;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

    }
}
