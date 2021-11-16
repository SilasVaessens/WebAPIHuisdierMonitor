using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;


namespace WebAPIHuisdierMonitor.DAL
{
    public static class PetDAL
    {
        private readonly static string ConnString = "Data Source=192.168.96.3,1913;Initial Catalog=PetMonitorDB;Persist Security Info=True;User ID=user;Password=sv22010899v";
        private readonly static SqlConnection conn = new SqlConnection(ConnString);
        
        public static bool? PetExists(string RFID)
        {
            string rfid = null;
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Pets WHERE RFID = @RFID";
            cmd.Parameters.AddWithValue("@RFID", RFID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rfid = (string)reader["RFID"];
                }
                conn.Close();
                if (rfid == RFID)
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

        public static bool? PetExists(int UserID)
        {
            int ID = 0;
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Pets WHERE UserID = @UserID";
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

        public static void AddPet (Pet pet)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Pets VALUES (@UserID, @Name, @RFID)";
            cmd.Parameters.AddWithValue("@UserID", pet.UserID);
            cmd.Parameters.AddWithValue("@Name", pet.Name);
            cmd.Parameters.AddWithValue("@RFID", pet.RFID);
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

        public static void DeletePet (int PetID)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Pets WHERE PetID = @PetID";
            cmd.Parameters.AddWithValue("@PetID", PetID);
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

        public static void UpdatePet (int PetID, string Name)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Pets SET Name = @Name WHERE PetID = @PetID";
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@PetID", PetID);
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

        public static List<Pet> GetAllPets(int UserID)
        {
            List<Pet> Pets = new List<Pet>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Pets WHERE UserID = @UserID";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    Pet pet = new Pet
                    {
                        PetID = (int)reader["PetID"],
                        UserID = (int)reader["UserID"],
                        Name = (string)reader["Name"],
                        RFID = (string)reader["RFID"]
                    };
                    Pets.Add(pet);
                }
                conn.Close();
                return Pets;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static List<FoodBowl> GetDataPetFoodbowl(string RFID)
        {
            List<FoodBowl> foodBowls = new List<FoodBowl>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM FoodBowls WHERE RFID = @RFID";
            cmd.Parameters.AddWithValue("@RIFD", RFID);
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
                        Weight = (float)reader["Weight"]
                    };
                    foodBowls.Add(foodBowl);
                }
                conn.Close();
                return foodBowls;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static List<AutoFeeder> GetDataPetAutoFeeder(string RFID)
        {
            List<AutoFeeder> autoFeeders = new List<AutoFeeder>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM FoodBowls WHERE RFID = @RFID";
            cmd.Parameters.AddWithValue("@RIFD", RFID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AutoFeeder autoFeeder = new AutoFeeder
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        MeasurementID = (int)reader["MeasurementID"],
                        Time = (DateTime)reader["Time"],
                        UnderLimit = (bool)reader["UnderLimit"]
                    };
                    autoFeeders.Add(autoFeeder);
                }
                conn.Close();
                return autoFeeders;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static List<PetBed> GetDataPetPetBed(string RFID)
        {
            List<PetBed> petBeds = new List<PetBed>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM PetBeds WHERE RFID = @RFID";
            cmd.Parameters.AddWithValue("@RIFD", RFID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PetBed petBed = new PetBed()
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        MeasurementID = (int)reader["MeasurementID"],
                        Time = (DateTime)reader["Time"],
                        RFID = (string)reader["RFID"],
                        Weight = (float)reader["Weight"]
                    };
                    petBeds.Add(petBed);
                }
                conn.Close();
                return petBeds;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static List<WaterBowl> GetDataPetWaterBowl(string RFID)
        {
            List<WaterBowl> waterBowls = new List<WaterBowl>();
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM WaterBowls WHERE RFID = @RFID";
            cmd.Parameters.AddWithValue("@RIFD", RFID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WaterBowl waterBowl = new WaterBowl()
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        MeasurementID = (int)reader["MeasurementID"],
                        Time = (DateTime)reader["Time"],
                        RFID = (string)reader["RFID"],
                        Weight = (float)reader["Weight"]
                    };
                    waterBowls.Add(waterBowl);
                }
                conn.Close();
                return waterBowls;
            }
            catch (SqlException)
            {
                conn.Close();
                throw new DivideByZeroException();
            }
        }

    }
}
