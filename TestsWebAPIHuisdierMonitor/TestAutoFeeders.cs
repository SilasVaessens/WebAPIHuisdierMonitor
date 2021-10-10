using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIHuisdierMonitor.Model;
using System.Linq;
using System.Collections.Generic;

namespace TestsWebAPIHuisdierMonitor
{
    [TestClass]
    public class TestAutoFeeders
    {
        [TestMethod]
        public void AddMeasurementNoUserID()
        {
            Product product = new Product(1, 0, "AutoFeederAddNoUser", "Product", "AutoFeeder");
            product.AddProduct(product.UniqueIdentifier, product.Type);

            AutoFeeder autoFeeder = new AutoFeeder(1, 0, product.UniqueIdentifier, 0, DateTime.Now, false);
            try
            {
                autoFeeder.AddMeasurement(autoFeeder);
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
            AutoFeeder autoFeeder = new AutoFeeder(1, 0, "AutoFeederNoProduct", 0, DateTime.Now, false);
            autoFeeder.AddMeasurement(autoFeeder);
        }

        [TestMethod]
        public void AddMeasurementSucces()
        {
            Product product = new Product(1, 0, "AutoFeederSuccesMeasurement", "Product", "AutoFeeder");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            AutoFeeder autoFeeder = new AutoFeeder(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, false);
            autoFeeder.AddMeasurement(autoFeeder);
            AutoFeeder AddedAutoFeeder = autoFeeder.GetMeasurement(autoFeeder);

            Assert.AreEqual(autoFeeder.ProductID, AddedAutoFeeder.ProductID);
            Assert.AreEqual(autoFeeder.UserID, AddedAutoFeeder.UserID);
            Assert.AreEqual(autoFeeder.UnderLimit, AddedAutoFeeder.UnderLimit);

            AddedAutoFeeder.DeleteAllMeasurements(AddedAutoFeeder);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllMeasurementsNoMeasurements()
        {
            AutoFeeder autoFeeder = new AutoFeeder(0, 0, "Identifier", 0, DateTime.Now, false);
            autoFeeder.GetAllMeasurements(autoFeeder);
        }

        [TestMethod]
        public void GetAllMeasurementsSucces()
        {
            Product product = new Product(1, 1, "AutoFeederSuccesGetAll", "Product", "AutoFeeder");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            AutoFeeder autoFeeder = new AutoFeeder(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, false);
            autoFeeder.AddMeasurement(autoFeeder);
            autoFeeder = autoFeeder.GetMeasurement(autoFeeder);

            Assert.AreEqual(1, autoFeeder.GetAllMeasurements(autoFeeder).Count);

            AutoFeeder autoFeeder2 = new AutoFeeder(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, true);
            autoFeeder2.AddMeasurement(autoFeeder2);
            autoFeeder2 = autoFeeder2.GetMeasurement(autoFeeder2);

            Assert.AreEqual(2, autoFeeder2.GetAllMeasurements(autoFeeder2).Count);

            Assert.AreNotEqual(autoFeeder.MeasurementID, autoFeeder2.MeasurementID);
            Assert.AreEqual(autoFeeder.ProductID, autoFeeder2.ProductID);
            Assert.AreEqual(autoFeeder.UserID, autoFeeder2.UserID);
            Assert.AreNotEqual(autoFeeder.Time, autoFeeder2.Time);
            Assert.AreNotEqual(autoFeeder.UnderLimit, autoFeeder2.UnderLimit);

            autoFeeder.DeleteAllMeasurements(autoFeeder);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMeasurementWrongIDs()
        {
            AutoFeeder autoFeeder = new AutoFeeder(0, 0, "GetMeasurementsWrongIDs", 0, DateTime.Now, false);
            autoFeeder.GetMeasurement(autoFeeder);
        }

        [TestMethod]
        public void GetMeasurementsSucces()
        {
            Product product = new Product(1, 1, "AutoFeederSuccesGet", "Product", "AutoFeeder");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            AutoFeeder autoFeeder = new AutoFeeder(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, false);
            autoFeeder.AddMeasurement(autoFeeder);

            autoFeeder = autoFeeder.GetMeasurement(autoFeeder);
            AutoFeeder Comparison = autoFeeder.GetAllMeasurements(autoFeeder).Last();

            Assert.AreEqual(autoFeeder.MeasurementID, Comparison.MeasurementID);
            Assert.AreEqual(autoFeeder.ProductID, Comparison.ProductID);
            Assert.AreEqual(autoFeeder.UserID, Comparison.UserID);
            Assert.AreEqual(autoFeeder.Time, Comparison.Time);
            Assert.AreEqual(autoFeeder.UnderLimit, Comparison.UnderLimit);

            autoFeeder.DeleteAllMeasurements(autoFeeder);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteAllMeasurementsWrongIDs()
        {
            AutoFeeder autoFeeder = new AutoFeeder(0, 0, "Identifier", 0, DateTime.Now, false);
            autoFeeder.DeleteAllMeasurements(autoFeeder);
        }

        [TestMethod]
        public void DeleteAllMeasurementsSucces()
        {
            Product product = new Product(1, 1, "AutoFeederSuccesDeleteAll", "Product", "AutoFeeder");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            AutoFeeder autoFeeder = new AutoFeeder(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, false);
            autoFeeder.AddMeasurement(autoFeeder);

            AutoFeeder autoFeeder1 = new AutoFeeder(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, true);
            autoFeeder1.AddMeasurement(autoFeeder1);

            Assert.AreEqual(2, autoFeeder1.GetAllMeasurements(autoFeeder1).Count);
            autoFeeder1.DeleteAllMeasurements(autoFeeder1);

            try
            {
                int AmountMeasurements = autoFeeder1.GetAllMeasurements(autoFeeder1).Count;
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
