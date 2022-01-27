using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly static Product StaticProduct = new Product();

        [HttpPost("Post")]
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

        [HttpPost("Delete")]
        public IActionResult DeleteProduct([FromBody] Product product)
        {
            try
            {
                StaticProduct.DeleteProduct(product.ProductID, product.UserID);
                return Ok();
            }
            catch (DivideByZeroException) //sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //product bestaat niet in database
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("GetAll")]
        public IActionResult GetAllProducts([FromBody] Product product)
        {
            try
            {
                return Ok(StaticProduct.GetAllProducts(product.UserID));
            }
            catch (DivideByZeroException) //ging iets mis in de database
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //geen geregistreerde producten bij user
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                StaticProduct.UpdateProduct(product);
                return Ok();
            }
            catch (DivideByZeroException) //ging iets mis in de database
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) //Product bestaat niet in database
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (AccessViolationException) // Product is al geregistreerd
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

        }

    }
}
