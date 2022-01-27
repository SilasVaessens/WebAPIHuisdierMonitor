using System;
using System.Collections.Generic;
using WebAPIHuisdierMonitor.DAL;

namespace WebAPIHuisdierMonitor.Model
{
    public class Pet
    {
        public int PetID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string RFID { get; set; }
        public int AmountFood { get; set; }

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

        public void AddPet(Pet pet)
        {
            bool? Exists = PetDAL.PetExists(pet.RFID);
            if (Exists == false)
            {
                try
                {
                    PetDAL.AddPet(pet);
                }
                catch (DivideByZeroException)
                {
                    throw;
                }
            }
            if (Exists == null)
            {
                throw new DivideByZeroException();
            }
            if (Exists == true)
            {
                throw new ArgumentNullException();
            }
        }

        public void DeletePet(Pet pet)
        {
            bool? Exists = PetDAL.PetExists(pet.RFID);
            if (Exists == true)
            {
                try
                {
                    PetDAL.DeletePet(pet.PetID);
                }
                catch (DivideByZeroException)
                {
                    throw;
                }
            }
            if (Exists == null)
            {
                throw new DivideByZeroException();
            }
            if (Exists == false)
            {
                throw new ArgumentNullException();
            }
        }

        public List<Pet> GetAllPets(int UserID)
        {
            bool? Exists = PetDAL.PetExists(UserID);
            if (Exists == true)
            {
                try
                {
                    return PetDAL.GetAllPets(UserID);
                }
                catch (DivideByZeroException)
                {
                    throw;
                }
            }
            if (Exists == null)
            {
                throw new DivideByZeroException();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public List<FoodBowl> GetDataFoodbowl(Pet pet)
        {
            bool? Exists = PetDAL.PetExists(pet.RFID);
            if (Exists == true)
            {
                try
                {
                    return PetDAL.GetDataPetFoodbowl(pet.RFID);
                }
                catch (DivideByZeroException)
                {
                    throw;
                }
            }
            if (Exists == null)
            {
                throw new DivideByZeroException();
            }
            else
            {
                throw new ArgumentNullException();
            }

        }

        public List<WaterBowl> GetDataWaterbowl(Pet pet)
        {
            bool? Exists = PetDAL.PetExists(pet.RFID);
            if (Exists == true)
            {
                try
                {
                    return PetDAL.GetDataPetWaterBowl(pet.RFID);
                }
                catch (DivideByZeroException)
                {
                    throw;
                }
            }
            if (Exists == null)
            {
                throw new DivideByZeroException();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public List<PetBed> GetDataPetBed(Pet pet)
        {
            bool? Exists = PetDAL.PetExists(pet.RFID);
            if (Exists == true)
            {
                try
                {
                    return PetDAL.GetDataPetPetBed(pet.RFID);
                }
                catch (DivideByZeroException)
                {
                    throw;
                }
            }
            if (Exists == null)
            {
                throw new DivideByZeroException();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public Analyse Analyze(Pet pet)
        {
            try
            {
                return new Analyse(GetDataFoodbowl(pet), GetDataWaterbowl(pet), GetDataPetBed(pet));
            }
            catch (DivideByZeroException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }


        public void UpdatePet(Pet pet)
        {
            bool? Exists = PetDAL.PetExists(pet.RFID);
            if (Exists == true)
            {
                try
                {
                    PetDAL.UpdatePet(pet.PetID, pet.Name, pet.AmountFood);
                }
                catch (DivideByZeroException)
                {
                    throw;
                }
            }
            if (Exists == null)
            {
                throw new DivideByZeroException();
            }
            if (Exists == false)
            {
                throw new ArgumentNullException();
            }
        }



    }
}
