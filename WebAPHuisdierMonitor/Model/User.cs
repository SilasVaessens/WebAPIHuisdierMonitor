using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.DAL;

namespace WebAPIHuisdierMonitor.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PassWordHash { get; set; }

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

        public int ValidateLogIn(string UserName, string PassWordHash)
        {
            try
            {
                return UserDAL.ValidateLogIn(UserName, PassWordHash);
}
            catch (DivideByZeroException)
            {
                throw;
            }

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
