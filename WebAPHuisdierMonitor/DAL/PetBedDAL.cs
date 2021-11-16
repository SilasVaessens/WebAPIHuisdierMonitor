using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor.DAL
{
    public static class PetBedDAL
    {
        private readonly static string ConnString = "Data Source=192.168.96.3,1913;Initial Catalog=PetMonitorDB;Persist Security Info=True;User ID=sa;Password=DZ2IUDeSqvum";
        private readonly static SqlConnection conn = new SqlConnection(ConnString);

        public static bool? MeasurementsExists(int ProductID, int UserID)
        {
            List<int> ExistingMeasurements = new List<int>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM PetBeds WHERE ProductID = @ProductID AND UserID = @UserID";
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

        public static PetBed GetMeasurement(int ProductID, int UserID)
        {
            PetBed petBed = new PetBed();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM PetBeds WHERE MeasurementID=(SELECT max(MeasurementID) FROM PetBeds WHERE ProductID = @ProductID AND UserID = @UserID)";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    petBed = new PetBed
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
                return petBed;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static List<PetBed> GetAllMeasurement(int ProductID, int UserID)
        {
            List<PetBed> PetBeds = new List<PetBed>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM PetBeds WHERE ProductID = @ProductID AND UserID = @UserID";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PetBed petBed = new PetBed
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        MeasurementID = (int)reader["MeasurementID"],
                        Time = (DateTime)reader["Time"],
                        RFID = (string)reader["RFID"],
                        Weight = (float)reader["Weight"]
                    };
                    PetBeds.Add(petBed);
                }
                conn.Close();
                return PetBeds;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static void AddMeasurement(PetBed petBed)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO PetBeds VALUES (@ProductID, @UserID, @Time, @RFID, @Weight)";
            cmd.Parameters.AddWithValue("@ProductID", petBed.ProductID);
            cmd.Parameters.AddWithValue("@UserID", petBed.UserID);
            cmd.Parameters.AddWithValue("@Time", petBed.Time);
            cmd.Parameters.AddWithValue("@RFID", petBed.RFID);
            cmd.Parameters.AddWithValue("@Weight", petBed.Weight);
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
            cmd.CommandText = "DELETE FROM PetBeds WHERE ProductID = @ProductID AND UserID = @UserID";
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
