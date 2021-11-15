using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.DAL;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace WebAPIHuisdierMonitor.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PassWordHash { get; set; }
        public string Salt { get; set; }

        public User()
        {

        }

        public User(int ID, string Name, string Password)
        {
            UserID = ID;
            UserName = Name;
            PassWordHash = Password;
        }

        public void AddUser(User user)
        {
            bool? Exists = UserDAL.UserExists(user.UserName);
            if (Exists == false)
            {
                try
                {
                    byte[] Salt = CreateSalt(); //create salt
                    byte[] Hash = HashPassword(user.PassWordHash, Salt); //hash password + salt
                    user.Salt = Convert.ToBase64String(Salt);
                    user.PassWordHash = Convert.ToBase64String(Hash);
                    UserDAL.AddUser(user);
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

        public void DeleteUser(User user)
        {
            bool? Exists = UserDAL.UserExists(user.UserID); // check of ID bestaat
            if (Exists == true)
            {
                try
                {
                    if (ValidateLogIn(user.UserName, user.PassWordHash) == user.UserID) // check of username en password bij ID horen
                    {
                        UserDAL.DeleteUser(user.UserID);
                    }
                    else
                    {
                        throw new ArgumentException(); // username en password horen niet bij id / verkeerde username en password
                    }
                }
                catch (DivideByZeroException) // sql error
                {
                    throw;
                }
            }
            if (Exists == null)
            {
                throw new DivideByZeroException(); //sql error
            }
            if (Exists == false)
            {
                throw new ArgumentNullException(); // ID staat niet in database
            }
        }

        public void UpdateUserName(User user)
        {
            bool? Exists = UserDAL.UserExists(user.UserName);
            if (Exists == false)
            {
                try
                {
                    UserDAL.UpdateUsername(user);
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

        public void UpdatePassword(User user)
        {
            bool? Exists = UserDAL.UserExists(user.UserID);
            if (Exists == true)
            {
                try
                {
                    byte[] Salt = CreateSalt(); //create salt
                    byte[] Hash = HashPassword(user.PassWordHash, Salt); //hash password + salt
                    user.Salt = Convert.ToBase64String(Salt);
                    user.PassWordHash = Convert.ToBase64String(Hash);
                    UserDAL.UpdatePassword(user);
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

        public int ValidateLogIn(string UserName, string Password)
        {
            try
            {
                User ToValidate = UserDAL.ValidateLogin(UserName);
                byte[] SaltArray = Convert.FromBase64String(ToValidate.Salt); //convert to byte array
                byte[] HashArray = Convert.FromBase64String(ToValidate.PassWordHash); 

                bool Succes = VerifyHash(Password, SaltArray, HashArray);
                if (Succes)
                {
                    return ToValidate.UserID;
                }
                else
                {
                    return 0;
                }

            }
            catch (DivideByZeroException)
            {
                throw;
            }
        }

        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8, // four cores
                Iterations = 4,
                MemorySize = 32 * 32
            };

            return argon2.GetBytes(16);
        }

        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }



        public User GetUser()
        {
            try
            {
                return UserDAL.GetUser();
            }
            catch (DivideByZeroException)
            {
                throw;
            }
        }
    }
}
