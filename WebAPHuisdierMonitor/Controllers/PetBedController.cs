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
    public class PetBedController : ControllerBase
    {
        private readonly static PetBed StaticPetBed = new PetBed();

        [HttpPost("Post")]
        public IActionResult AddPetBedMeasurement([FromBody] PetBed petBed)
        {
            try
            {
                StaticPetBed.AddMeasurement(petBed);
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
        public IActionResult GetPetBedMeasurement([FromBody] PetBed petBed)
        {
            try
            {
                return Ok(StaticPetBed.GetMeasurement(petBed));
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
        public IActionResult GetAllPetBedMeasurements([FromBody] PetBed petBed)
        {
            try
            {
                return Ok(StaticPetBed.GetAllMeasurements(petBed));
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
        public IActionResult DeleteAllPetBedMeasurements([FromBody] PetBed petBed)
        {
            try
            {
                StaticPetBed.DeleteAllMeasurements(petBed);
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
