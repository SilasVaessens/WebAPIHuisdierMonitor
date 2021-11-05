using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.DAL;

namespace WebAPIHuisdierMonitor.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int MeasurementID { get; set; }
        public DateTime Time { get; set; }
        public string UniqueIdentifier { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }


        public Product() //makkelijke lege constructor
        {

        }

        public Product(int Product, int User, string Identifier, string NameProduct, string KindOf) //voor gebruik bij producten 
        {
            ProductID = Product;
            UserID = User;
            UniqueIdentifier = Identifier;
            Name = NameProduct;
            Type = KindOf;
        }

        public Product(int Product, int User, string Identifier, int Measurement, 
            DateTime MeasurementTime) // voor gebruik bij measurements
        {
            ProductID = Product;
            UserID = User;
            MeasurementID = Measurement;
            UniqueIdentifier = Identifier;
            Time = MeasurementTime;
        }

        public void AddProduct(string UniqueIdentider, string Type)
        {
            bool? Exists = ProductDAL.ProductExists(UniqueIdentider);
            if (Exists == false)
            {
                try
                {
                    ProductDAL.AddProduct(UniqueIdentider, Type);
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

        public void DeleteProduct(int ProductID, int UserID)
        {
            bool? Exists = ProductDAL.ProductExists(ProductID, UserID); //controleer of specifiek product bestaat
            if (Exists == true)
            {
                try
                {
                    ProductDAL.DeleteProduct(ProductID);
                }
                catch (DivideByZeroException) //als er iets misgaat in de database
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

        public Product GetProduct() //wordt alleen gebruikt binnen de unit tests
        {
            try
            {
                return ProductDAL.GetProduct();
            }
            catch (DivideByZeroException)
            {
                throw;
            }
        }

        public List<Product> GetAllProducts(int UserID)
        {
            bool? Exists = ProductDAL.ProductExists(UserID); //controleer of user producten geregistreerd heeft staan
            if (Exists == true)
            {
                try
                {
                    return ProductDAL.GetAllProducts(UserID);
                }
                catch (DivideByZeroException) //als er iets misgaat in de database
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
                throw new ArgumentNullException(); // product niet in database
            }
        }

        public void UpdateProduct(int ProductID, int UserID, string Name, string UniqueIdentifier)
        {
            bool? Exists = ProductDAL.ProductExists(UniqueIdentifier); //controleer of specifiek product bestaat
            if (Exists == true)
            {
                try
                {
                    if (ProductID == 0)
                    {
                        ProductDAL.UpdateProduct(UserID, Name, ProductDAL.GetProductID(UniqueIdentifier));
                    }
                    else
                    {
                        ProductDAL.UpdateProduct(UserID, Name, ProductID);
                    }
                    ProductDAL.UpdateProduct(UserID, Name, ProductID);
                }
                catch (DivideByZeroException) //als er iets misgaat in de database
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

        public Product GetProductIDAndUserID(string UniqueIdentifier)
        {
            bool? Exist = ProductDAL.ProductExists(UniqueIdentifier);
            if (Exist == true)
            {
                int FailureCount = 0;
                while (true)
                {
                    try
                    {
                        if (FailureCount >= 20) //gooit error bij 20 of meer pogingen
                        {
                            throw new DivideByZeroException();
                        }
                        return ProductDAL.GetProductIDAndUserID(UniqueIdentifier); ;
                    }
                    catch (AccessViolationException) //sql error bij het verkrijgen van user ID en product ID
                    {
                        FailureCount++;
                        continue;
                    }
                    catch (InvalidCastException)
                    {
                        throw;
                    }
                }
            }
            if (Exist == null) //sql error bij checken of product bestaat
            {
                throw new DivideByZeroException();
            }
            else //product staat niet in database
            {
                throw new ArgumentNullException();
            }

        }
    }
}
