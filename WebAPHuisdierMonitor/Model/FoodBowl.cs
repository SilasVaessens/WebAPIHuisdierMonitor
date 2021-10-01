using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Sql;
using WebAPIHuisdierMonitor.DAL;

namespace WebAPIHuisdierMonitor.Model
{
    public class FoodBowl : Product
    {
        public int RFID { get; set; }
        public float Weight { get; set; }

        public FoodBowl()
        {

        }

        public FoodBowl(int ProductID, int UserID, string Identifier, int MeasurementID, DateTime Time, int RFIDValue, float WeightValue) : base(ProductID, UserID, Identifier, MeasurementID, Time)
        {
            RFID = RFIDValue;
            Weight = WeightValue;
        }

        public FoodBowl GetMeasurement(FoodBowl foodBowl)
        {

        }

        public List<FoodBowl> GetAllMeasurements(FoodBowl foodBowl)
        {

        }

        public void AddMeasurement(FoodBowl foodBowl)
        {

        }

        public void DeleteAllMeasurement(FoodBowl foodBowl)
        {

        }
    }
}
