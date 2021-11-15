using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIHuisdierMonitor.Model
{
    public class Pet
    {
        public int PetID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string RFID { get; set; }

        public Pet()
        {

        }

        public Pet(int petID, int userID, string name, string rfid)
        {
            PetID = petID;
            UserID = userID;
            Name = name;
            RFID = rfid;
        }
    }
}
