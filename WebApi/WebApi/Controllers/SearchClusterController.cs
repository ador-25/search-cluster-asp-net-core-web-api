using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Controllers
{
    [ApiController]
    public class SearchClusterController : Controller
    {
        IClusterInfo _clusterInfo;
        public SearchClusterController(IClusterInfo clusterInfo)
        {
            _clusterInfo = clusterInfo;
        }
        [HttpGet]
        [Route("api/[controller]/{keyword}")]
        public IActionResult GetClusters(string keyword)
        {
   
            try
            {
                return Ok(_clusterInfo.Search(keyword));
            }
            catch(Exception e)
            {
                return Ok(e);
            }
            
        }
        [HttpGet]
        [Route("api/[controller]/get/{clusterid}")]
        public IActionResult GetData(Guid clusterid)
        {

            try
            {
                return Ok(_clusterInfo.GetData(clusterid));
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }
    }
}
