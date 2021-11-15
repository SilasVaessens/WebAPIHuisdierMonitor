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
    [Route("controlller")]
    public class PetController : ControllerBase
    {
        private readonly Pet StaticPet = new Pet();

        [HttpPost("Post")]
        public IActionResult AddPet([FromBody] Pet pet)
        {
            try
            {
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost("Delete")]
        public IActionResult DeletePet([FromBody] Pet pet)
        {
            try
            {
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Getall")]
        public IActionResult GetAllPets()
        {
            try
            {
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetPet")]
        public IActionResult GetDataPet(int ID)
        {
            try
            {
                return Ok(ID);
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpPut]
        public IActionResult UpdatePet([FromBody] Pet pet)
        {
            try
            {
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
