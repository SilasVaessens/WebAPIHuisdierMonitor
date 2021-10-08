using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;
using Microsoft.AspNetCore.Http;

namespace WebAPIHuisdierMonitor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodBowlController : ControllerBase
    {
        private readonly static FoodBowl StaticFoodBowl = new FoodBowl();

        [HttpPost]
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

        [HttpGet]
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

        [HttpGet("GetAll")]
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

        [HttpDelete]
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
