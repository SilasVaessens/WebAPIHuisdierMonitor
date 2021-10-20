using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.DAL;
using System.Data.SqlClient;

namespace WebAPIHuisdierMonitor.Model
{
    public class PetBed : Product
    {
        public string RFID { get; set; }
        public float Weight { get; set; }

        public PetBed()
        {

        }

        public PetBed(int ProductID, int UserID, string Identifier, int MeasurementID, DateTime Time, string RFIDValue, float WeightValue) : base(ProductID, UserID, Identifier, MeasurementID, Time)
        {
            RFID = RFIDValue;
            Weight = WeightValue;
        }

        public PetBed GetMeasurement(PetBed petBed)
        {
            bool? Exists = PetBedDAL.MeasurementsExists(petBed.ProductID, petBed.UserID);
            if (Exists == true)
            {
                try
                {
                    return PetBedDAL.GetMeasurement(petBed.ProductID, petBed.UserID);
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

        public List<PetBed> GetAllMeasurements(PetBed petBed)
        {
            bool? Exists = PetBedDAL.MeasurementsExists(petBed.ProductID, petBed.UserID);
            if (Exists == true)
            {
                try
                {
                    return PetBedDAL.GetAllMeasurement(petBed.ProductID, petBed.UserID);
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

        public void AddMeasurement(PetBed petBed)
        {
            try
            {
                Product Feeder = GetProductIDAndUserID(petBed.UniqueIdentifier);
                petBed.ProductID = Feeder.ProductID;
                petBed.UserID = Feeder.UserID;
                petBed.Time = DateTime.Now;

                int FailureCount = 0;
                while (true)
                {
                    try
                    {
                        if (FailureCount >= 20)
                        {
                            throw new DivideByZeroException();
                        }
                        PetBedDAL.AddMeasurement(petBed);
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

        public void DeleteAllMeasurements(PetBed petBed)
        {
            bool? Exists = PetBedDAL.MeasurementsExists(petBed.ProductID, petBed.UserID);
            if (Exists == true)
            {
                try
                {
                    PetBedDAL.DeleteAllMeasurements(petBed.ProductID, petBed.UserID);
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
