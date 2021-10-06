using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.DAL;

namespace WebAPIHuisdierMonitor.Model
{
    public class AutoFeeder : Product
    {
        public bool UnderLimit { get; set; }

        public AutoFeeder()
        {

        }

        public AutoFeeder(int ProductID, int UserID, string Identifier, int MeasurementID, DateTime Time, bool Limit) : base(ProductID, UserID, Identifier, MeasurementID, Time)
        {
            UnderLimit = Limit;
        }

        public AutoFeeder GetMeasurement(AutoFeeder autoFeeder)
        {
            bool? Exists = AutoFeederDAL.MeasurementsExists(autoFeeder.ProductID, autoFeeder.UserID);
            if (Exists == true)
            {
                try
                {
                    return AutoFeederDAL.GetMeasurement(autoFeeder.ProductID, autoFeeder.UserID);
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
            else
            {
                throw new ArgumentNullException();
            }
        }

        public List<AutoFeeder> GetAllMeasurements(AutoFeeder autoFeeder)
        {
            bool? Exists = AutoFeederDAL.MeasurementsExists(autoFeeder.ProductID, autoFeeder.UserID);
            if (Exists == true)
            {
                try
                {
                    return AutoFeederDAL.GetAllMeasurement(autoFeeder.ProductID, autoFeeder.UserID);
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

        public void AddMeasurement(AutoFeeder autoFeeder)
        {
            try
            {
                Product Feeder = GetProductIDAndUserID(autoFeeder.UniqueIdentifier);
                autoFeeder.ProductID = Feeder.ProductID;
                autoFeeder.UserID = Feeder.UserID;
                autoFeeder.Time = DateTime.Now;

                int FailureCount = 0;
                while (true)
                {
                    try
                    {
                        if (FailureCount >= 20)
                        {
                            throw new DivideByZeroException();
                        }
                        AutoFeederDAL.AddMeasurement(autoFeeder);
                        break;
                    }
                    catch (AccessViolationException)
                    {
                        FailureCount++;
                        continue;
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

        public void DeleteAllMeasurements(AutoFeeder autoFeeder)
        {
            bool? Exists = AutoFeederDAL.MeasurementsExists(autoFeeder.ProductID, autoFeeder.UserID);
            if (Exists == true)
            {
                try
                {
                    AutoFeederDAL.DeleteAllMeasurements(autoFeeder.ProductID, autoFeeder.UserID);
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
