using System;
using System.Collections.Generic;
using WebAPIHuisdierMonitor.DAL;

namespace WebAPIHuisdierMonitor.Model
{
    public class FoodBowl : Product
    {
        public string RFID { get; set; }
        public int Weight { get; set; }

        public FoodBowl()
        {

        }

        public FoodBowl(int ProductID, int UserID, string Identifier, int MeasurementID, DateTime Time, string RFIDValue, int WeightValue) : base(ProductID, UserID, Identifier, MeasurementID, Time)
        {
            RFID = RFIDValue;
            Weight = WeightValue;
        }

        public FoodBowl GetMeasurement(FoodBowl foodBowl)
        {
            bool? Exists = FoodBowlDAL.MeasurementsExists(foodBowl.ProductID, foodBowl.UserID);
            if (Exists == true)
            {
                try
                {
                    return FoodBowlDAL.GetMeasurement(foodBowl.ProductID, foodBowl.UserID);
                }
                catch (DivideByZeroException) //sql error bij verkrijgen van measurement
                {
                    throw;
                }
            }
            if (Exists == null) // sql error bij het kijken of de het product bestaat
            {
                throw new DivideByZeroException();
            }
            else  // er staan geen measurements voor het specifieke product in de database
            {
                throw new ArgumentNullException();
            }
        }

        public List<FoodBowl> GetAllMeasurements(FoodBowl foodBowl)
        {
            bool? Exists = FoodBowlDAL.MeasurementsExists(foodBowl.ProductID, foodBowl.UserID);
            if (Exists == true)
            {
                try
                {
                    return FoodBowlDAL.GetAllMeasurement(foodBowl.ProductID, foodBowl.UserID);
                }
                catch (DivideByZeroException) //sql error bij verkrijgen alle measurements
                {
                    throw;
                }
            }
            if (Exists == null) // sql error bij het kijken of de het product bestaat
            {
                throw new DivideByZeroException();
            }
            else // er staan geen measurements voor het specifieke product in de database
            {
                throw new ArgumentNullException();
            }
        }

        public void AddMeasurement(FoodBowl foodBowl)
        {
            try
            {
                Product Feeder = GetProductIDAndUserID(foodBowl.UniqueIdentifier);
                foodBowl.ProductID = Feeder.ProductID;
                foodBowl.UserID = Feeder.UserID;
                foodBowl.Time = DateTime.Now;

                int FailureCount = 0;
                while (true)
                {
                    try
                    {
                        if (FailureCount >= 20)
                        {
                            throw new DivideByZeroException();
                        }
                        FoodBowlDAL.AddMeasurement(foodBowl);
                        break;
                    }
                    catch (AccessViolationException)
                    {
                        FailureCount++;
                        continue;
                    }
                    catch (InvalidCastException)
                    {
                        throw;
                    }
                }
            }
            catch (DivideByZeroException) //sql error, komt van verkrijgen user ID en product ID of van toevoegen measurement aan database
            {
                throw;
            }
            catch (ArgumentNullException) //product niet in database bij verkrijgen user id en product id
            {
                throw;
            }
        }

        public void DeleteAllMeasurements(FoodBowl foodBowl)
        {
            bool? Exists = FoodBowlDAL.MeasurementsExists(foodBowl.ProductID, foodBowl.UserID);
            if (Exists == true)
            {
                try
                {
                    FoodBowlDAL.DeleteAllMeasurements(foodBowl.ProductID, foodBowl.UserID);
                }
                catch (DivideByZeroException) // sql error bij het verwijderen van alle measurements
                {
                    throw;
                }
            }
            if (Exists == null) // sql error bij het kijken of de het product bestaat
            {
                throw new DivideByZeroException();
            }
            if (Exists == false) // er staan geen measurements voor het specifieke product in de database
            {
                throw new ArgumentNullException();
            }
        }

        public List<Pet> GetFoodPet(FoodBowl foodBowl)
        {
            try
            {
                Product Feeder = GetProductIDAndUserID(foodBowl.UniqueIdentifier); //verkrijg Product en UserID
                Pet pet = new Pet();
                List<Pet> PetData = pet.GetAllPets(Feeder.UserID);
                return PetData;
            }
            catch (DivideByZeroException) // sql error
            {
                throw;
            }
            catch (ArgumentNullException) //pet of product bestaat niet / niet geregistreerd
            {
                throw;
            }
            catch (InvalidCastException) // product nog niet geregistreerd
            {
                throw;
            }

        }
    }
}
