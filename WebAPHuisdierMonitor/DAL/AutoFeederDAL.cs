using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor.DAL
{
    public static class AutoFeederDAL
    {
        private readonly static string ConnString = "";
        private readonly static SqlConnection conn = new SqlConnection(ConnString);

        public static bool? MeasurementsExists(int ProductID, int UserID)
        {
            List<int> ExistingMeasurements = new List<int>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT FROM AutoFeeders WHERE EXISTS (SELECT * FROM AutoFeeders WHERE ProductID = @ProductID AND UserID = @UserID)";
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
                    return false;
                }
            }
            catch (SqlException)
            {
                conn.Close();
                return null;
            }
        }

        public static AutoFeeder GetMeasurement(int ProductID, int UserID)
        {
            AutoFeeder autoFeeder = new AutoFeeder();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM AutoFeeders WHERE MeasurementID=(SELECT max(MeasurementID) FROM AutoFeeders WHERE ProductID == @ProductID AND UserID == @UserID";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    autoFeeder = new AutoFeeder
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        MeasurementID = (int)reader["MeasurementID"],
                        Time = (DateTime)reader["Time"],
                        UnderLimit = (bool)reader["UnderLimit"]
                    };
                }
                conn.Close();
                return autoFeeder;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static List<AutoFeeder> GetAllMeasurement(int ProductID, int UserID)
        {
            List<AutoFeeder> AutoFeeders = new List<AutoFeeder>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM AutoFeeders WHERE ProductID = @ProductID AND UserID = @UserID";
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AutoFeeder autoFeeder = new AutoFeeder
                    {
                        MeasurementID = (int)reader["MeasurementID"],
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        Time = (DateTime)reader["Time"],
                        UnderLimit = (bool)reader["UnderLimit"]
                    };
                    AutoFeeders.Add(autoFeeder);
                }
                conn.Close();
                return AutoFeeders;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static void AddMeasurement(AutoFeeder autoFeeder)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO AutoFeeders VALUES (@ProductID, @UserID, @Time, @UnderLimit)";
            cmd.Parameters.AddWithValue("@ProductID", autoFeeder.ProductID);
            cmd.Parameters.AddWithValue("@UserID", autoFeeder.UserID);
            cmd.Parameters.AddWithValue("@Time", autoFeeder.Time);
            cmd.Parameters.AddWithValue("@UnderLimit", autoFeeder.UnderLimit);
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
            cmd.CommandText = "DELETE FROM AutoFeeders WHERE ProductID = @ProductID AND UserID = @UserID";
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
