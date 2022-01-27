using System;
using System.Collections.Generic;
using WebAPIHuisdierMonitor.DAL;

namespace WebAPIHuisdierMonitor.Model
{
    public class WaterBowl : Product
    {
        public string RFID { get; set; }
        public float Weight { get; set; }

        public WaterBowl()
        {

        }

        public WaterBowl(int ProductID, int UserID, string Identifier, int MeasurementID, DateTime Time, string RFIDValue, float WeightValue) : base(ProductID, UserID, Identifier, MeasurementID, Time)
        {
            RFID = RFIDValue;
            Weight = WeightValue;
        }

        public WaterBowl GetMeasurement(WaterBowl waterBowl)
        {
            bool? Exists = WaterBowlDAL.MeasurementsExists(waterBowl.ProductID, waterBowl.UserID);
            if (Exists == true)
            {
                try
                {
                    return WaterBowlDAL.GetMeasurement(waterBowl.ProductID, waterBowl.UserID);
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

        public List<WaterBowl> GetAllMeasurements(WaterBowl waterBowl)
        {
            bool? Exists = WaterBowlDAL.MeasurementsExists(waterBowl.ProductID, waterBowl.UserID);
            if (Exists == true)
            {
                try
                {
                    return WaterBowlDAL.GetAllMeasurement(waterBowl.ProductID, waterBowl.UserID);
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

        public void AddMeasurement(WaterBowl waterBowl)
        {
            try
            {
                Product Feeder = GetProductIDAndUserID(waterBowl.UniqueIdentifier);
                waterBowl.ProductID = Feeder.ProductID;
                waterBowl.UserID = Feeder.UserID;
                waterBowl.Time = DateTime.Now;

                int FailureCount = 0;
                while (true)
                {
                    try
                    {
                        if (FailureCount >= 20)
                        {
                            throw new DivideByZeroException();
                        }
                        WaterBowlDAL.AddMeasurement(waterBowl);
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

        public void DeleteAllMeasurements(WaterBowl waterBowl)
        {
            bool? Exists = WaterBowlDAL.MeasurementsExists(waterBowl.ProductID, waterBowl.UserID);
            if (Exists == true)
            {
                try
                {
                    WaterBowlDAL.DeleteAllMeasurements(waterBowl.ProductID, waterBowl.UserID);
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
    }
}
