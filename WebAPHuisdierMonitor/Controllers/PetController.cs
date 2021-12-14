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
    public class PetController : ControllerBase
    {
        private readonly Pet StaticPet = new Pet();

        [HttpPost("Add")]
        public IActionResult AddPet([FromBody] Pet pet)
        {
            try
            {
                StaticPet.AddPet(pet);
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        [HttpPost("Delete")]
        public IActionResult DeletePet([FromBody] Pet pet)
        {

            try
            {
                StaticPet.DeletePet(pet);
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("Getall")]
        public IActionResult GetAllPets([FromBody] Pet pet)
        {
            try
            {
                return Ok(StaticPet.GetAllPets(pet.UserID));
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("GetPetData")]
        public IActionResult GetDataPet([FromBody] Pet pet)
        {
            try
            {
                switch (pet.Name)
                {
                    case "Foodbowl":
                        return Ok(StaticPet.GetDataFoodbowl(pet));
                    case "Waterbowl":
                        return Ok(StaticPet.GetDataWaterbowl(pet));
                    case "PetBed":
                        return Ok(StaticPet.GetDataPetBed(pet));
                    default:
                        return NotFound();
                }
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }


        [HttpPut]
        public IActionResult UpdatePet([FromBody] Pet pet)
        {
            try
            {
                StaticPet.UpdatePet(pet);
                return Ok();
            }
            catch (DivideByZeroException) // sql error
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
