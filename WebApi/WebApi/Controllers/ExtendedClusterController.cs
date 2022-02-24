using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Body;
using WebApi.Data;

namespace WebApi.Controllers
{
    [ApiController]
    public class ExtendedClusterController : Controller
    {
        IExtendedClusterData _extendedClusterData;
        public ExtendedClusterController(IExtendedClusterData extendedClusterData)
        {
            _extendedClusterData = extendedClusterData;
        }

        [HttpPost]
        [Route("api/[controller]/{clusterId}")]
        public IActionResult AddUrl(Guid clusterId, [FromBody]TemplateString temp)
        {
            return Ok(_extendedClusterData.AddUrl(clusterId, temp.data));
        }

    }
}
