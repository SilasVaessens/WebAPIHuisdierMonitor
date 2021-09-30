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


        [HttpDelete]
        public IActionResult DeleteProduct([FromBody] Product product)
        {
            try
            {
                StaticProduct.DeleteProduct(product);
                return Ok();
            }
            catch (SqlException) //ging iets mis in de database
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //product bestaat niet in database
            {
                return StatusCode(StatusCodes.Status409Conflict);
            } 
        }

        [HttpGet("{UserID}")]
        public IActionResult GetAllProducts(int UserID)
        {
            try
            {
                List<Product> AllProducts =  StaticProduct.GetAllProducts(UserID);
                return Ok(AllProducts);
            }
            catch (SqlException) //ging iets mis in de database
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //geen geregistreerde producten bij user
            {
                return StatusCode(StatusCodes.Status409Conflict); 
            }

        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                StaticProduct.UpdateProduct(product);
                return Ok();
            }
            catch (SqlException) //ging iets mis in de database
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //Product bestaat niet in database
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

        }

    }
}
