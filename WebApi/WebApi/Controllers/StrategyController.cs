using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;


namespace WebApi.Controllers
{
    [ApiController]
    public class StrategyController : Controller
    {
        IStrategyData _strategyData;

        public StrategyController(IStrategyData strategyData)
        {
            _strategyData = strategyData;
        }

        // get all strategies
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetStrategies()
        {
            return Ok(_strategyData.GetStrategies());
        }
        [HttpPost]
        [Route("api/[controller]/{strategyName}")]
        public IActionResult AddStrategy(string strategyName)
        {
            return Ok(_strategyData.AddUrlStrategy(strategyName));
        }
    }
}
