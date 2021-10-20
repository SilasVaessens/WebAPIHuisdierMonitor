using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;


namespace WebAPIHuisdierMonitor.DAL
{
    public static class FoodBowlDAL
    {
        private readonly static string ConnString = "Server=tcp:petmonitor.database.windows.net,1433;Initial Catalog=PetMonitorDB;Persist Security Info=False;User ID=PetMonitor;Password=99Siva'02;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly static SqlConnection conn = new SqlConnection(ConnString);

        public static bool? MeasurementsExists(int ProductID, int UserID)
        {
            List<int> ExistingMeasurements = new List<int>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM FoodBowls WHERE ProductID = @ProductID AND UserID = @UserID";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int MeasurementID = (int)reader["MeasurementID"];
                    ExistingMeasurements.Add(MeasurementID);
                }
                if (ExistingMeasurements.Count > 0)
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

        public static FoodBowl GetMeasurement(int ProductID, int UserID)
        {
            FoodBowl foodBowl = new FoodBowl();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM FoodBowls WHERE MeasurementID=(SELECT max(MeasurementID) FROM FoodBowls WHERE ProductID = @ProductID AND UserID = @UserID)";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foodBowl = new FoodBowl
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        MeasurementID = (int)reader["MeasurementID"],
                        Time = (DateTime)reader["Time"],
                        RFID = (string)reader["RFID"],
                        Weight = (float)reader["Weight"]
                    };
                }
                conn.Close();
                return foodBowl;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static List<FoodBowl> GetAllMeasurement(int ProductID, int UserID)
        {
            List<FoodBowl> FoodBowls = new List<FoodBowl>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM FoodBowls WHERE ProductID = @ProductID AND UserID = @UserID";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FoodBowl foodBowl = new FoodBowl
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        MeasurementID = (int)reader["MeasurementID"],
                        Time = (DateTime)reader["Time"],
                        RFID = (string)reader["RFID"],
                        Weight = (float)reader["Weight"]
                    };
                    FoodBowls.Add(foodBowl);
                }
                conn.Close();
                return FoodBowls;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static void AddMeasurement(FoodBowl foodBowl)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO FoodBowls VALUES (@ProductID, @UserID, @Time, @RFID, @Weight)";
            cmd.Parameters.AddWithValue("@ProductID", foodBowl.ProductID);
            cmd.Parameters.AddWithValue("@UserID", foodBowl.UserID);
            cmd.Parameters.AddWithValue("@Time", foodBowl.Time);
            cmd.Parameters.AddWithValue("@RFID", foodBowl.RFID);
            cmd.Parameters.AddWithValue("@Weight", foodBowl.Weight);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException)
            {
                conn.Close();
                throw new AccessViolationException();
            }
        }

        public static void DeleteAllMeasurements(int ProductID, int UserID)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM FoodBowls WHERE ProductID = @ProductID AND UserID = @UserID";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
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

    }
}
