using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebAPIHuisdierMonitor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodBowlController : ControllerBase
    {
        private readonly static FoodBowl StaticFoodBowl = new FoodBowl();

        [HttpPost("Post")]
        public IActionResult AddFoodBowlMeasurement([FromBody] FoodBowl foodBowl)
        {
            try
            {
                StaticFoodBowl.AddMeasurement(foodBowl);
                return Ok();
            }
            catch (DivideByZeroException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (InvalidCastException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpPost("Get")]
        public IActionResult GetFoodBowlMeasurements([FromBody] FoodBowl foodBowl)
        {
            try
            {
                return Ok(StaticFoodBowl.GetMeasurement(foodBowl));
            }
            catch (DivideByZeroException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("GetAll")]
        public IActionResult GetAllFoodBowlMeasurements([FromBody] FoodBowl foodBowl)
        {
            try
            {
                return Ok(StaticFoodBowl.GetAllMeasurements(foodBowl));
            }
            catch (DivideByZeroException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("GetFood")]
        public IActionResult GetAmountFoodPets([FromBody] FoodBowl foodBowl)
        {
            try
            {
                return Ok(StaticFoodBowl.GetFoodPet(foodBowl));
            }
            catch (DivideByZeroException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (InvalidCastException) // product nog niet geregistreerd
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpPost("Delete")]
        public IActionResult DeleteAllFoodBowlMeasurements([FromBody] FoodBowl foodBowl)
        {
            try
            {
                StaticFoodBowl.DeleteAllMeasurements(foodBowl);
                return Ok();
            }
            catch (DivideByZeroException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}
