using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIHuisdierMonitor.Model;
using System.Linq;
using System.Collections.Generic;

namespace TestsWebAPIHuisdierMonitor
{
    [TestClass]
    public class TestFoodBowls
    {
        [TestMethod]
        public void AddMeasurementNoUserID()
        {
            Product product = new Product(1, 0, "FoodBowlNoUser", "Product", "FoodBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);

            FoodBowl foodBowl = new FoodBowl(1, 0, product.UniqueIdentifier, 0, DateTime.Now, 0, 0);
            try
            {
                foodBowl.AddMeasurement(foodBowl);
                Assert.IsTrue(false);
            }
            catch (InvalidCastException)
            {
                Product AddedProduct = product.GetProduct();
                AddedProduct.UserID = 1;
                AddedProduct.UpdateProduct(AddedProduct.ProductID, AddedProduct.UserID, product.Name, AddedProduct.UniqueIdentifier);
                AddedProduct.DeleteProduct(AddedProduct.ProductID, AddedProduct.UserID);
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddMeasurementNoProduct()
        {
            FoodBowl foodBowl = new FoodBowl(1, 0, "FoodBowlNoProduct", 0, DateTime.Now, 0, 0);
            foodBowl.AddMeasurement(foodBowl);
        }

        [TestMethod]
        public void AddMeasurementSucces()
        {
            Product product = new Product(1, 0, "FoodBowlSuccesMeasurement", "Product", "FoodBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            FoodBowl foodBowl = new FoodBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, 0, 0);
            foodBowl.AddMeasurement(foodBowl);
            FoodBowl AddedFoodBowl = foodBowl.GetMeasurement(foodBowl);

            Assert.AreEqual(foodBowl.ProductID, AddedFoodBowl.ProductID);
            Assert.AreEqual(foodBowl.UserID, AddedFoodBowl.UserID);
            Assert.AreEqual(foodBowl.RFID, AddedFoodBowl.RFID);
            Assert.AreEqual(foodBowl.Weight, AddedFoodBowl.Weight);

            AddedFoodBowl.DeleteAllMeasurements(AddedFoodBowl);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllMeasurementsNoMeasurement()
        {
            FoodBowl foodBowl = new FoodBowl(0, 0, "Identifier", 0, DateTime.Now, 0, 0);
            foodBowl.GetAllMeasurements(foodBowl);
        }

        [TestMethod]
        public void GetAllMeasurementsSucces()
        {
            Product product = new Product(1, 1, "FoodBowlSuccesGetAll", "Product", "FoodBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            FoodBowl foodBowl = new FoodBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, 123456789, 10);
            foodBowl.AddMeasurement(foodBowl);
            foodBowl = foodBowl.GetMeasurement(foodBowl);

            Assert.AreEqual(1, foodBowl.GetAllMeasurements(foodBowl).Count);

            FoodBowl foodBowl1 = new FoodBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, 987654321, 20);
            foodBowl1.AddMeasurement(foodBowl1);
            foodBowl1 = foodBowl1.GetMeasurement(foodBowl1);

            Assert.AreEqual(2, foodBowl1.GetAllMeasurements(foodBowl1).Count);

            Assert.AreNotEqual(foodBowl.MeasurementID, foodBowl1.MeasurementID);
            Assert.AreEqual(foodBowl.ProductID, foodBowl1.ProductID);
            Assert.AreEqual(foodBowl.UserID, foodBowl1.UserID);
            Assert.AreNotEqual(foodBowl.Time, foodBowl1.Time);
            Assert.AreNotEqual(foodBowl.RFID, foodBowl1.RFID);
            Assert.AreNotEqual(foodBowl.Weight, foodBowl1.Weight);

            foodBowl.DeleteAllMeasurements(foodBowl);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMeasurementWrongIDs()
        {
            FoodBowl foodBowl = new FoodBowl(0, 0, "GetMeasurementsWrongIDs", 0, DateTime.Now, 0, 0);
            foodBowl.GetMeasurement(foodBowl);
        }

        [TestMethod]
        public void GetMeasurementSucces()
        {
            Product product = new Product(1, 1, "FoodBowlGetAll", "Product", "FoodBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            FoodBowl foodBowl = new FoodBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, 0, 0);
            foodBowl.AddMeasurement(foodBowl);

            foodBowl = foodBowl.GetMeasurement(foodBowl);
            FoodBowl Comparison = foodBowl.GetAllMeasurements(foodBowl).Last();

            Assert.AreEqual(foodBowl.MeasurementID, Comparison.MeasurementID);
            Assert.AreEqual(foodBowl.ProductID, Comparison.ProductID);
            Assert.AreEqual(foodBowl.UserID, Comparison.UserID);
            Assert.AreEqual(foodBowl.Time, Comparison.Time);
            Assert.AreEqual(foodBowl.RFID, Comparison.RFID);
            Assert.AreEqual(foodBowl.Weight, Comparison.Weight);

            foodBowl.DeleteAllMeasurements(foodBowl);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteAllMeasurementsWrongIDs()
        {
            FoodBowl foodBowl = new FoodBowl(0, 0, "Identifier", 0, DateTime.Now, 0, 0);
            foodBowl.DeleteAllMeasurements(foodBowl);
        }

        [TestMethod]
        public void DeleteAllMeasurementsSucces()
        {
            Product product = new Product(1, 1, "FoodBowlDeleteAll", "Product", "FoodBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            FoodBowl foodBowl = new FoodBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, 123456789, 10);
            foodBowl.AddMeasurement(foodBowl);

            FoodBowl foodBowl1 = new FoodBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, 987654321, 20);
            foodBowl1.AddMeasurement(foodBowl1);

            Assert.AreEqual(2, foodBowl1.GetAllMeasurements(foodBowl1).Count);
            foodBowl1.DeleteAllMeasurements(foodBowl1);
            
            try
            {
                int AmountMeasurements = foodBowl1.GetAllMeasurements(foodBowl1).Count;
                Assert.IsTrue(false);
            }
            catch (ArgumentNullException)
            {
                product.DeleteProduct(product.ProductID, product.UserID);
                Assert.IsTrue(true);
            }
        }
    }
}
