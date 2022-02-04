using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using WebAPIHuisdierMonitor.Model;


namespace WebAPIHuisdierMonitor.DAL
{
    public static class PetDAL
    {
        private readonly static string ConnString = "Server=tcp:petmonitor.database.windows.net,1433;Initial Catalog=PetMonitorDB;Persist Security Info=False;User ID=PetMonitor;Password={wachtwoord};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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
            List<int> ExistingPets = new List<int>();
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
                    int ID = (int)reader["UserID"];
                    ExistingPets.Add(ID);
                }
                conn.Close();
                if (ExistingPets.Count > 0)
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

        public static void AddPet(Pet pet)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Pets VALUES (@UserID, @Name, @RFID, @AmountFood)";
            cmd.Parameters.AddWithValue("@UserID", pet.UserID);
            cmd.Parameters.AddWithValue("@Name", pet.Name);
            cmd.Parameters.AddWithValue("@RFID", pet.RFID);
            cmd.Parameters.AddWithValue("AmountFood", pet.AmountFood);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
                throw new DivideByZeroException();
            }
        }

        public static void DeletePet(int PetID)
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

        public static void UpdatePet(int PetID, string Name, int AmountFood)
        {
            using SqlCommand cmd = new SqlCommand(ConnString);
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Pets SET Name = @Name, AmountFood = @AmountFood WHERE PetID = @PetID";
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@PetID", PetID);
            cmd.Parameters.AddWithValue("@AmountFood", AmountFood);
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
                        RFID = (string)reader["RFID"],
                        AmountFood = (int)reader["AmountFood"]
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
            cmd.Parameters.AddWithValue("@RFID", RFID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                Debug.WriteLine(cmd.CommandText);
                while (reader.Read())
                {
                    FoodBowl foodBowl = new FoodBowl
                    {
                        ProductID = (int)reader["ProductID"],
                        UserID = (int)reader["UserID"],
                        MeasurementID = (int)reader["MeasurementID"],
                        Time = (DateTime)reader["Time"],
                        RFID = (string)reader["RFID"],
                        Weight = (int)reader["Weight"],
                    };
                    foodBowls.Add(foodBowl);
                }
                conn.Close();
                return foodBowls;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
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
            cmd.Parameters.AddWithValue("@RFID", RFID);
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
            cmd.CommandText = $"SELECT * FROM WaterBowls WHERE RFID = @RFID";
            cmd.Parameters.AddWithValue("@RFID", RFID);
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
