using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WaterBowlController : ControllerBase
    {
        private readonly static WaterBowl StaticWaterBowl = new WaterBowl();

        [HttpPost("Post")]
        public IActionResult AddWaterBowlMeasurement([FromBody] WaterBowl waterBowl)
        {
            try
            {
                StaticWaterBowl.AddMeasurement(waterBowl);
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
        public IActionResult GetWaterBowlMeasurement([FromBody] WaterBowl waterBowl)
        {
            try
            {
                return Ok(StaticWaterBowl.GetMeasurement(waterBowl));
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
        public IActionResult GetAllWaterBowlMeasurements([FromBody] WaterBowl waterBowl)
        {
            try
            {
                return Ok(StaticWaterBowl.GetAllMeasurements(waterBowl));
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

        [HttpPost("Delete")]
        public IActionResult DeleteAllWaterBowlMeasurements([FromBody] WaterBowl waterBowl)
        {
            try
            {
                StaticWaterBowl.DeleteAllMeasurements(waterBowl);
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
