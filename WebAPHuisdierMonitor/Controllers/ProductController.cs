using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace WebAPIHuisdierMonitor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly static Product StaticProduct = new Product();

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                StaticProduct.AddProduct(product.UniqueIdentifier, product.Type);
                return Ok();
            }
            catch (DivideByZeroException) //sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //product staat al in database
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct([FromBody] Product product)
        {
            try
            {
                StaticProduct.DeleteProduct(product.ProductID, product.UserID);
                return Ok();
            }
            catch (SqlException) //sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //product bestaat niet in database
            {
                return StatusCode(StatusCodes.Status404NotFound);
            } 
        }

        [HttpGet]
        public IActionResult GetAllProducts([FromBody] Product product)
        {
            try
            {
                return Ok(StaticProduct.GetAllProducts(product.UserID));
            }
            catch (SqlException) //ging iets mis in de database
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //geen geregistreerde producten bij user
            {
                return StatusCode(StatusCodes.Status404NotFound); 
            }

        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                StaticProduct.UpdateProduct(product.ProductID, product.UserID, product.Name, product.UniqueIdentifier);
                return Ok();
            }
            catch (SqlException) //ging iets mis in de database
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //Product bestaat niet in database
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

        }

    }
}
