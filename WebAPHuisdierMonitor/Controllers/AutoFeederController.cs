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
    public class AutoFeederController : ControllerBase
    {
        private readonly static AutoFeeder StaticAutoFeeder = new AutoFeeder();

        [HttpGet]
        public IActionResult GetAutoFeederMeasurements([FromBody] AutoFeeder autoFeeder)
        {
            try
            {
                return Ok(StaticAutoFeeder.GetMeasurement(autoFeeder));
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
        public IActionResult GetAllAutoFeederMeasurements([FromBody] AutoFeeder autoFeeder)
        {
            try
            {
                return Ok(StaticAutoFeeder.GetAllMeasurements(autoFeeder));
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
        public IActionResult DeleteAllAutoFeederMeasurements([FromBody] AutoFeeder autoFeeder)
        {
            try
            {
                StaticAutoFeeder.DeleteAllMeasurements(autoFeeder);
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

        [HttpPost]
        public IActionResult AddAutoFeederMeasurement([FromBody] AutoFeeder autoFeeder)
        {
            try
            {
                StaticAutoFeeder.AddMeasurement(autoFeeder);
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
