using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            bool Exists = AutoFeederDAL.MeasurementsExists(autoFeeder.ProductID, autoFeeder.UserID);
            if (Exists == true)
            {
                try
                {
                    return AutoFeederDAL.GetMeasurement(autoFeeder.ProductID, autoFeeder.UserID);
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public List<AutoFeeder> GetAllMeasurements(AutoFeeder autoFeeder)
        {
            bool Exists = AutoFeederDAL.MeasurementsExists(autoFeeder.ProductID, autoFeeder.UserID);
            if (Exists == true)
            {
                try
                {
                    return AutoFeederDAL.GetAllMeasurement(autoFeeder.ProductID, autoFeeder.UserID);
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

        }

        public void AddMeasurement(AutoFeeder autoFeeder)
        {

        }

        public void DeleteAllMeasurements(AutoFeeder autoFeeder)
        {

        }
    }
}
