using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WaterBowlController : ControllerBase
    {
        private readonly static WaterBowl StaticWaterBowl = new WaterBowl();

        [HttpPost]
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

        [HttpGet]
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

        [HttpGet("GetAll")]
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

        [HttpDelete]
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
