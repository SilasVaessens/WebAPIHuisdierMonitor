using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIHuisdierMonitor.Model
{
    public class WaterBowl
    {
        public int WaterBowlMeasurementID { get; set; }
        public float WeightWaterBowl { get; set; }
        public DateTime WeightTimeWater { get; set; }
        public bool UnderLimitWater { get; set; }
        public int RFIDValueWaterBowl { get; set; }
        public string WaterBowlName { get; set; }
        public string WaterBowlSSID { get; set; }
        public string WaterBowlMAC { get; set; }
        public int ProductID { get; set; }

        public WaterBowl(int WaterID, float Weight, DateTime Time, bool Limit, int RFID, string Name, string SSID, string MAC, int productID)
        {
            WaterBowlMeasurementID = WaterID;
            WeightWaterBowl = Weight;
            WeightTimeWater = Time;
            UnderLimitWater = Limit;
            RFIDValueWaterBowl = RFID;
            WaterBowlName = Name;
            WaterBowlSSID = SSID;
            WaterBowlMAC = MAC;
            ProductID = productID;
        }

        public bool AddWaterBowelMeasurement()
        {
            return false;
        }

        public WaterBowl GetWaterBowlMeasurement()
        {
            WaterBowl waterBowl = new WaterBowl(0, 0, DateTime.Now, false, 0, "Name", "SSID", "MAC", 0);
            return waterBowl;
        }
        public List<WaterBowl> GetAllWaterBowlMeasurements()
        {
            List<WaterBowl> WaterBowls = new List<WaterBowl>();
            return WaterBowls;
        }

        public bool DeleteAllWaterBowlMeasurements()
        {
            return false;
        }
    }
}
