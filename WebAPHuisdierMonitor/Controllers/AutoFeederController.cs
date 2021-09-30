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
    public class AutoFeederController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddAutoFeederMeasurement()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAutoFeederMeasurements()
        {
            return Ok();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllAutoFeederMeasurements()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult DeleteAllAutoFeederMeasurements()
        {
            return Ok();
        }
    }
}
