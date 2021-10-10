using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIHuisdierMonitor.Model;

namespace TestsWebAPIHuisdierMonitor
{
    [TestClass]
    public class TestUsers
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddExisingUserTest()
        {
            User ExistingUser = new User(1, "TestUser", "esrjhgfRahstuw6hg6152gveger11651");
            ExistingUser.AddUser(ExistingUser);
        }

        [TestMethod]
        public void AddNewUser()
        {
            User NewUser = new User(100, "UnitTestUser", "ThisShouldBeHashed");
            NewUser.AddUser(NewUser);
            User LastUser = NewUser.GetUser();
            Assert.AreEqual(NewUser.UserName, LastUser.UserName);
            Assert.AreEqual(NewUser.PassWordHash, LastUser.PassWordHash);
            LastUser.DeleteUser(LastUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteUserNotExistingID()
        {
            User WrongID = new User(-1, "UnitTestUser", "ThisShouldBeHashed");
            WrongID.DeleteUser(WrongID);
        }

        [TestMethod]
        [DataRow(1, "NotExisting", "esrjhgfRahstuw6hg6152gveger11651")]
        [DataRow(1, "TestUser", "ThisShouldBeHashed")]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteUserWrongUserNamePassWord(int ID, string Name, string PassWord)
        {
            User WrongName = new User(ID, Name, PassWord);
            WrongName.DeleteUser(WrongName);
        }

        [TestMethod]
        public void DeleteExistingUser()
        {
            User user = new User(0, "ToBeDeleted", "PassWord");
            User LastInDB = user.GetUser();
            user.AddUser(user);
            User AddedInDB = user.GetUser();
            Assert.AreNotEqual(LastInDB.PassWordHash, AddedInDB.PassWordHash);
            Assert.AreNotEqual(LastInDB.UserName, AddedInDB.UserName);
            AddedInDB.DeleteUser(AddedInDB);
            User AfterDeletion = user.GetUser();
            Assert.AreEqual(LastInDB.PassWordHash, AfterDeletion.PassWordHash);
            Assert.AreEqual(LastInDB.UserName, AfterDeletion.UserName);
        }

        [TestMethod]
        public void ValidateLoginFail()
        {
            User user = new User(0, "NotExistingUserName", "NotExistingPassWord");
            int UserID = user.ValidateLogIn(user.UserName, user.PassWordHash);
            Assert.AreEqual(UserID, 0);
        }

        [TestMethod]
        public void ValidateLogInSucces()
        {
            User user = new User(1, "TestUser", "esrjhgfRahstuw6hg6152gveger11651");
            int UserID = user.ValidateLogIn(user.UserName, user.PassWordHash);
            Assert.AreEqual(UserID, user.UserID);
        }
    }
}
