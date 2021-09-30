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
    public class FoodBowlController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddFoodBowlMeasurement()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetFoodBowlMeasurements()
        {
            return Ok();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllFoodBowlMeasurements()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAllFoodBowlMeasurements()
        {
            return Ok();
        }
    }
}
