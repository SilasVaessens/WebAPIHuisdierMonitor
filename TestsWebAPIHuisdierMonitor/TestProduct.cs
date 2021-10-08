using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIHuisdierMonitor.Model;
using System.Linq;
using System.Collections.Generic;

namespace TestsWebAPIHuisdierMonitor
{
    [TestClass]
    public class TestProduct
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddExistingProduct()
        {
            Product ExistingProduct = new Product(1, 1, "UniqueTest", "VoerbakSilas", "FoodBowl");
            ExistingProduct.AddProduct(ExistingProduct.UniqueIdentifier, ExistingProduct.Type);
        }

        [TestMethod]
        public void AddNewProduct()
        {
            Product NewProduct = new Product(100, 1, "AddProductIdentifier", "AddProductTest", "FoodBowl");
            NewProduct.AddProduct(NewProduct.UniqueIdentifier, NewProduct.Type);
            Product LastProduct = NewProduct.GetProduct();
            LastProduct.UpdateProduct(LastProduct.ProductID, 1, "ToBeDeleted", LastProduct.UniqueIdentifier); //zodat het verwijdert kan worden
            Assert.AreEqual(NewProduct.UniqueIdentifier, LastProduct.UniqueIdentifier);
            Assert.AreEqual(NewProduct.Type, LastProduct.Type);
            LastProduct.DeleteProduct(LastProduct.ProductID, 1);
        }

        [TestMethod]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteProductWrongProductIDUserID(int ProductID, int UserID)
        {
            Product WrongParameters = new Product();
            WrongParameters.DeleteProduct(ProductID, UserID);
        }

        [TestMethod]
        public void DeleteExistingProduct()
        {
            Product product = new Product(0, 1, "HasToBeDeleted", "DeleteProductTest", "AutoFeeder");
            Product LastInDB = product.GetProduct();
            product.AddProduct(product.UniqueIdentifier, product.Type);
            Product AddedInDB = product.GetProduct();
            AddedInDB.UserID = 1;
            AddedInDB.UpdateProduct(AddedInDB.ProductID, AddedInDB.UserID, product.Name, AddedInDB.UniqueIdentifier);
            Assert.AreNotEqual(LastInDB.UniqueIdentifier, AddedInDB.UniqueIdentifier);
            Assert.AreNotEqual(LastInDB.Type, AddedInDB.Type);
            AddedInDB.DeleteProduct(AddedInDB.ProductID, AddedInDB.UserID);
            Product AfterDeletion = product.GetProduct();
            Assert.AreEqual(LastInDB.UniqueIdentifier, AfterDeletion.UniqueIdentifier);
            Assert.AreEqual(LastInDB.Type, AfterDeletion.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateNotExistingProduct()
        {
            Product product = new Product(1, 1, "DoesNotExist", "Name", "WaterBowl");
            product.UpdateProduct(product.ProductID, product.UserID, product.Name, product.UniqueIdentifier);
        }

        [TestMethod]
        public void UpdateExistingProduct()
        {
            Product product = new Product(1, 1, "ShouldBeUnique", "MyProduct", "PetBed");
            product.AddProduct(product.UniqueIdentifier, product.Type);

            int FirstProductID = product.GetProduct().ProductID;
            product.UpdateProduct(FirstProductID, product.UserID, product.Name, product.UniqueIdentifier);
            
            User user = new User(1, "TransferUser", "TransferUserPassWord");
            user.AddUser(user);
            user = user.GetUser();

            Product BeforeUpdate = product.GetAllProducts(product.UserID).Last();
            product.UpdateProduct(BeforeUpdate.ProductID, user.UserID, "TransferUserProduct", BeforeUpdate.UniqueIdentifier);
            Product AfterUpdate = product.GetAllProducts(user.UserID).Last();

            Assert.AreNotEqual(BeforeUpdate.UserID, AfterUpdate.UserID);
            Assert.AreNotEqual(BeforeUpdate.Name, AfterUpdate.Name);

            product.DeleteProduct(AfterUpdate.ProductID, AfterUpdate.UserID);
            user.DeleteUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void UpdateNotExistingUser()
        {
            Product product = new Product(); 
            product.GetProduct();
            product.UpdateProduct(product.ProductID, 0, product.Name, product.UniqueIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllProductNotExistingUserID()
        {
            Product product = new Product();
            product.GetAllProducts(0);
        }

        [TestMethod]
        public void GetAllProductExistingID()
        {
            User user = new User(1, "ProductsOwner", "ProductsOwnerPassWord");
            user.AddUser(user);
            user = user.GetUser();

            Product product1 = new Product(1, 1, "ProductsOwner1", "Product1", "FoodBowl");
            product1.AddProduct(product1.UniqueIdentifier, product1.Type);
            product1 = product1.GetProduct();
            product1.UpdateProduct(product1.ProductID, user.UserID, "Product1", product1.UniqueIdentifier);

            int AmountProductsBefore = product1.GetAllProducts(user.UserID).Count;
            Assert.AreEqual(1, AmountProductsBefore);

            Product product2 = new Product(1, 1, "ProductsOwner2", "Product2", "AutoFeeder");
            product2.AddProduct(product2.UniqueIdentifier, product2.Type);
            product2 = product2.GetProduct();
            product2.UpdateProduct(product2.ProductID, user.UserID, "Product2", product2.UniqueIdentifier);

            int AmountProductsAfter = product2.GetAllProducts(user.UserID).Count;
            Assert.AreEqual(2, AmountProductsAfter);

            product2.DeleteProduct(product2.ProductID, user.UserID);
            product1.DeleteProduct(product1.ProductID, user.UserID);
            user.DeleteUser(user);


        }
    }
}
