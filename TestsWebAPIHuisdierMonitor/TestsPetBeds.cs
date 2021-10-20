using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIHuisdierMonitor.Model;
using System.Linq;
using System.Collections.Generic;

namespace TestsWebAPIHuisdierMonitor
{
    [TestClass]
    public class TestsPetBeds
    {
        [TestMethod]
        public void AddMeasurementNoUserID()
        {
            Product product = new Product(1, 0, "PetBedAddNoUser", "Product", "PetBed");
            product.AddProduct(product.UniqueIdentifier, product.Type);

            PetBed petBed = new PetBed(1, 0, product.UniqueIdentifier, 0, DateTime.Now, "0", 0);
            try
            {
                petBed.AddMeasurement(petBed);
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
            PetBed petBed = new PetBed(1, 0, "FoodBowlNoProduct", 0, DateTime.Now, "0", 0);
            petBed.AddMeasurement(petBed);
        }

        [TestMethod]
        public void AddMeasurementSucces()
        {
            Product product = new Product(1, 0, "PetBedSuccesMeasurement", "Product", "PetBed");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            PetBed petBed = new PetBed(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "0", 0);
            petBed.AddMeasurement(petBed);
            PetBed AddedPetBed = petBed.GetMeasurement(petBed);

            Assert.AreEqual(petBed.ProductID, AddedPetBed.ProductID);
            Assert.AreEqual(petBed.UserID, AddedPetBed.UserID);
            Assert.AreEqual(petBed.RFID, AddedPetBed.RFID);
            Assert.AreEqual(petBed.Weight, AddedPetBed.Weight);

            AddedPetBed.DeleteAllMeasurements(petBed);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllMeasurementsNoMeasurements()
        {
            PetBed petBed = new PetBed(0, 0, "Identifier", 0, DateTime.Now, "0", 0);
            petBed.GetAllMeasurements(petBed);
        }

        [TestMethod]
        public void GetAllMeasurementsSucces()
        {
            Product product = new Product(1, 1, "PetBedSuccesGetAll", "Product", "PetBed");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            PetBed petBed = new PetBed(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "123456789", 10);
            petBed.AddMeasurement(petBed);
            petBed = petBed.GetMeasurement(petBed);

            Assert.AreEqual(1, petBed.GetAllMeasurements(petBed).Count);

            PetBed petBed2 = new PetBed(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "987654321", 20);
            petBed2.AddMeasurement(petBed2);
            petBed2 = petBed2.GetMeasurement(petBed2);

            Assert.AreEqual(2, petBed.GetAllMeasurements(petBed2).Count);

            Assert.AreNotEqual(petBed.MeasurementID, petBed2.MeasurementID);
            Assert.AreEqual(petBed.ProductID, petBed2.ProductID);
            Assert.AreEqual(petBed.UserID, petBed2.UserID);
            Assert.AreNotEqual(petBed.Time, petBed2.Time);
            Assert.AreNotEqual(petBed.RFID, petBed2.RFID);
            Assert.AreNotEqual(petBed.Weight, petBed2.Weight);

            petBed.DeleteAllMeasurements(petBed);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMeasurementWrongIDs()
        {
            PetBed petBed = new PetBed(0, 0, "GetMeasurementsWrongIDs", 0, DateTime.Now, "0", 0);
            petBed.GetMeasurement(petBed);
        }

        [TestMethod]
        public void GetMeasurementSucces()
        {
            Product product = new Product(1, 1, "PetBedGetAll", "Product", "PetBed");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            PetBed petBed = new PetBed(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "0", 0);
            petBed.AddMeasurement(petBed);

            petBed = petBed.GetMeasurement(petBed);
            PetBed Comparison = petBed.GetAllMeasurements(petBed).Last();

            Assert.AreEqual(petBed.MeasurementID, Comparison.MeasurementID);
            Assert.AreEqual(petBed.ProductID, Comparison.ProductID);
            Assert.AreEqual(petBed.UserID, Comparison.UserID);
            Assert.AreEqual(petBed.Time, Comparison.Time);
            Assert.AreEqual(petBed.RFID, Comparison.RFID);
            Assert.AreEqual(petBed.Weight, Comparison.Weight);

            petBed.DeleteAllMeasurements(petBed);
            product.DeleteProduct(product.ProductID, product.UserID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteAllMeasurementsWrongIDs()
        {
            PetBed petBed = new PetBed(0, 0, "Identifier", 0, DateTime.Now, "0", 0);
            petBed.DeleteAllMeasurements(petBed);
        }

        [TestMethod]
        public void DeleteAllMeasurementsSucces()
        {
            Product product = new Product(1, 1, "PetBedSuccesDeleteAll", "Product", "PetBed");
            product.AddProduct(product.UniqueIdentifier, product.Type);
            product = product.GetProduct();
            product.UserID = 1;
            product.UpdateProduct(product.ProductID, product.UserID, "Product", product.UniqueIdentifier);

            PetBed petBed = new PetBed(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "123456789", 10);
            petBed.AddMeasurement(petBed);

            PetBed petBed1 = new PetBed(product.ProductID, product.UserID, product.UniqueIdentifier, 0, DateTime.Now, "987654321", 20);
            petBed1.AddMeasurement(petBed1);

            Assert.AreEqual(2, petBed1.GetAllMeasurements(petBed1).Count);
            petBed1.DeleteAllMeasurements(petBed1);

            try
            {
                int AmountMeasurements = petBed1.GetAllMeasurements(petBed1).Count;
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
