using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
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

        public void DeleteProduct(Product product)
        {
            bool Exists = ProductDAL.ProductExists(product.UserID, product.ProductID); //controleer of specifiek product bestaat
            if (Exists == true)
            {
                try
                {
                    ProductDAL.DeleteProduct(product.ProductID);
                }
                catch (SqlException) //als er iets misgaat in de database
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentNullException(); 
            }
        }

        public List<Product> GetAllProducts(int UserID)
        {
            bool Exists = ProductDAL.ProductExists(UserID); //controleer of user producten geregistreerd heeft staan
            if (Exists == true)
            {
                try
                {
                    return ProductDAL.GetAllProducts(UserID);
                }
                catch (SqlException) //als er iets misgaat in de database
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentNullException("Product bestaat niet"); // product niet in database
            }

        }

        public void UpdateProduct(Product product)
        {
            bool Exists = ProductDAL.ProductExists(product.UserID, product.ProductID); //controleer of specifiek product bestaat
            if (Exists == true)
            {
                try
                {
                    ProductDAL.UpdateProduct(product.UserID, product.Name, product.ProductID);
                }
                catch (SqlException) //als er iets misgaat in de database
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public int GetProductID(string UniqueIdentifier)
        {
            bool Exist = ProductDAL.ProductExists(UniqueIdentifier);
            if (Exist == true)
            {
                int FailureCount = 0;
                while (true)
                {
                    try
                    {
                        if (FailureCount >= 20)
                        {
                            throw new DivideByZeroException();
                        }
                        int ProductID = ProductDAL.GetProductID(UniqueIdentifier);
                        return ProductID;
                    }
                    catch (SqlException)
                    {
                        FailureCount++;
                        continue;
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
