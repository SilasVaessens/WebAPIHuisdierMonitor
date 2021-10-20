using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor.DAL
{
    public static class WaterBowlDAL
    {
        private readonly static string ConnString = "Server=tcp:petmonitor.database.windows.net,1433;Initial Catalog=PetMonitorDB;Persist Security Info=False;User ID=PetMonitor;Password=99Siva'02;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly static SqlConnection conn = new SqlConnection(ConnString);

        public static bool? MeasurementsExists(int ProductID, int UserID)
        {
            List<int> ExistingMeasurements = new List<int>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM WaterBowls WHERE ProductID = @ProductID AND UserID = @UserID";
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

        public static WaterBowl GetMeasurement(int ProductID, int UserID)
        {
            WaterBowl waterBowl = new WaterBowl();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM WaterBowls WHERE MeasurementID=(SELECT max(MeasurementID) FROM WaterBowls WHERE ProductID = @ProductID AND UserID = @UserID)";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    waterBowl = new WaterBowl
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
                return waterBowl;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static List<WaterBowl> GetAllMeasurement(int ProductID, int UserID)
        {
            List<WaterBowl> WaterBowls = new List<WaterBowl>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM WaterBowls WHERE ProductID = @ProductID AND UserID = @UserID";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WaterBowl waterBowl = new WaterBowl
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        MeasurementID = (int)reader["MeasurementID"],
                        Time = (DateTime)reader["Time"],
                        RFID = (string)reader["RFID"],
                        Weight = (float)reader["Weight"]
                    };
                    WaterBowls.Add(waterBowl);
                }
                conn.Close();
                return WaterBowls;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static void AddMeasurement(WaterBowl waterBowl)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO WaterBowls VALUES (@ProductID, @UserID, @Time, @RFID, @Weight)";
            cmd.Parameters.AddWithValue("@ProductID", waterBowl.ProductID);
            cmd.Parameters.AddWithValue("@UserID", waterBowl.UserID);
            cmd.Parameters.AddWithValue("@Time", waterBowl.Time);
            cmd.Parameters.AddWithValue("@RFID", waterBowl.RFID);
            cmd.Parameters.AddWithValue("@Weight", waterBowl.Weight);
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
            cmd.CommandText = "DELETE FROM WaterBowls WHERE ProductID = @ProductID AND UserID = @UserID";
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
