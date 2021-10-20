using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIHuisdierMonitor.Model;
using System.Linq;
using System.Collections.Generic;

namespace TestsWebAPIHuisdierMonitor
{
    [TestClass]
    public class TestWaterBowls
    {
        [TestMethod]
        public void AddMeasurementNoUserID()
        {
            Product product = new Product(1, 0, "WaterBowlNoUser", "Product", "WaterBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);

            WaterBowl waterBowl = new WaterBowl(1, 0, product.UniqueIdentifier, 0, DateTime.Now, "0", 0);
            try
            {
                waterBowl.AddMeasurement(waterBowl);
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
            WaterBowl waterBowl = new WaterBowl(1, 0, "WaterBowlNoProduct", 0, DateTime.Now, "0", 0);
            waterBowl.AddMeasurement(waterBowl);
        }

        [TestMethod]
        public void AddMeasurementSucces()
        {
            Product product = new Product(1, 0, "WaterBowlSuccesMeasurement", "Product", "WaterBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            WaterBowl waterBowl = new WaterBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "0", 0);
            waterBowl.AddMeasurement(waterBowl);
            WaterBowl AddedWaterBowl = waterBowl.GetMeasurement(waterBowl);

            Assert.AreEqual(waterBowl.ProductID, AddedWaterBowl.ProductID);
            Assert.AreEqual(waterBowl.UserID, AddedWaterBowl.UserID);
            Assert.AreEqual(waterBowl.RFID, AddedWaterBowl.RFID);
            Assert.AreEqual(waterBowl.Weight, AddedWaterBowl.Weight);

            AddedWaterBowl.DeleteAllMeasurements(AddedWaterBowl);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllMeasurementsNoMeasurement()
        {
            WaterBowl waterBowl = new WaterBowl(0, 0, "Identifier", 0, DateTime.Now, "0", 0);
            waterBowl.GetAllMeasurements(waterBowl);
        }

        [TestMethod]
        public void GetAllMeasurementsSucces()
        {
            Product product = new Product(1, 1, "WaterBowlSuccesGetAll", "Product", "WaterBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            WaterBowl waterBowl = new WaterBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "123456789", 10);
            waterBowl.AddMeasurement(waterBowl);
            waterBowl = waterBowl.GetMeasurement(waterBowl);

            Assert.AreEqual(1, waterBowl.GetAllMeasurements(waterBowl).Count);

            WaterBowl waterBowl1 = new WaterBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "987654321", 20);
            waterBowl1.AddMeasurement(waterBowl1);
            waterBowl1 = waterBowl1.GetMeasurement(waterBowl1);

            Assert.AreEqual(2, waterBowl1.GetAllMeasurements(waterBowl1).Count);

            Assert.AreNotEqual(waterBowl.MeasurementID, waterBowl1.MeasurementID);
            Assert.AreEqual(waterBowl.ProductID, waterBowl1.ProductID);
            Assert.AreEqual(waterBowl.UserID, waterBowl1.UserID);
            Assert.AreNotEqual(waterBowl.Time, waterBowl1.Time);
            Assert.AreNotEqual(waterBowl.RFID, waterBowl1.RFID);
            Assert.AreNotEqual(waterBowl.Weight, waterBowl1.Weight);

            waterBowl.DeleteAllMeasurements(waterBowl);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMeasurementWrongIDs()
        {
            WaterBowl waterBowl = new WaterBowl(0, 0, "GetMeasurementsWrongIDs", 0, DateTime.Now, "0", 0);
            waterBowl.GetMeasurement(waterBowl);
        }

        [TestMethod]
        public void GetMeasurementSucces()
        {
            Product product = new Product(1, 1, "WaterBowlGetAll", "Product", "WaterBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            WaterBowl waterBowl = new WaterBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "0", 0);
            waterBowl.AddMeasurement(waterBowl);

            waterBowl = waterBowl.GetMeasurement(waterBowl);
            WaterBowl Comparison = waterBowl.GetAllMeasurements(waterBowl).Last();

            Assert.AreEqual(waterBowl.MeasurementID, Comparison.MeasurementID);
            Assert.AreEqual(waterBowl.ProductID, Comparison.ProductID);
            Assert.AreEqual(waterBowl.UserID, Comparison.UserID);
            Assert.AreEqual(waterBowl.Time, Comparison.Time);
            Assert.AreEqual(waterBowl.RFID, Comparison.RFID);
            Assert.AreEqual(waterBowl.Weight, Comparison.Weight);

            waterBowl.DeleteAllMeasurements(waterBowl);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteAllMeasurementsWrongIDs()
        {
            WaterBowl waterBowl = new WaterBowl(0, 0, "Identifier", 0, DateTime.Now, "0", 0);
            waterBowl.DeleteAllMeasurements(waterBowl);
        }

        [TestMethod]
        public void DeleteAllMeasurementsSucces()
        {
            Product product = new Product(1, 1, "WaterBowlDeleteAll", "Product", "WaterBowl");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            WaterBowl waterBowl = new WaterBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "123456789", 10);
            waterBowl.AddMeasurement(waterBowl);

            WaterBowl waterBowl1 = new WaterBowl(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "987654321", 20);
            waterBowl1.AddMeasurement(waterBowl1);

            Assert.AreEqual(2, waterBowl1.GetAllMeasurements(waterBowl1).Count);
            waterBowl1.DeleteAllMeasurements(waterBowl1);

            try
            {
                int AmountMeasurements = waterBowl1.GetAllMeasurements(waterBowl1).Count;
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
