using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Controllers
{
    [ApiController]
    public class UrlStrategyController : Controller
    {
        IUrlStrategyData _urlStrategy;

        public UrlStrategyController(IUrlStrategyData urlStrategy)
        {
            _urlStrategy = urlStrategy;
        }
        // add strategy to a url
        [HttpPost]
        [Route("api/[controller]/{urlId}/{strategyName}")]
        public IActionResult AddStrategies(Guid urlId, string strategyName)
        {
            return Ok(_urlStrategy.AddUrlHasStrategy(urlId, strategyName));
        }
        // get all startegies for one url
        [HttpGet]
        [Route("api/[controller]/{urlId}")]
        public IActionResult GetStrategies(Guid urlId)
        {
            return Ok(_urlStrategy.GetStrategies(urlId));
        }
        [HttpGet]
        [Route("api/[controller]/strategynames/{urlId}")]
        public IActionResult GetStrategiesName(Guid urlId)
        {
            return Ok(_urlStrategy.GetStrategyNames(urlId));
        }


        // FOR TEST GET ALL EXISTING STRATEGIES

        [HttpGet]
        [Route("api/[controller]/test")]
        public IActionResult GetAllStrategynames()
        {
            return Ok(_urlStrategy.GetAllStrategies());
        }


    }
}
