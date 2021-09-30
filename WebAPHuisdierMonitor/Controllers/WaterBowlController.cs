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
        [HttpPost]
        public IActionResult AddWaterBowlMeasurement()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetWaterBowlMeasurement()
        {
            return Ok();
        }

        [HttpGet("/GetAll")]
        public IActionResult GetAllWaterBowlMeasurements()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAllWaterBowlMeasurements()
        {
            return Ok();
        }
    }
}
