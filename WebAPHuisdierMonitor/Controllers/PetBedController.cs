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
        [HttpPost]
        public IActionResult AddPetBedMeasurement()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPetBedMeasurement()
        {
            return Ok();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllPetBedMeasurements()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAllPetBedMeasurements()
        {
            return Ok();
        }
    }
}
