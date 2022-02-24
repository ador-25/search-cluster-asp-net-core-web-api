using Microsoft.AspNetCore.Authorization;
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
    public class UrlController : Controller
    {
        IUrlData _urlData;
        IClusterData _clusterData;
        public UrlController(IUrlData urlData, IClusterData clusterData)
        {
            _urlData = urlData;
            _clusterData = clusterData;
        }

        // Get all urls in a cluster
        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{clustername}")]
        public IActionResult GetUrls(string clustername)  
        {
            var userName = User.Identity.Name;
            if (userName == null)
                return NotFound();
            Guid clusterId = _clusterData.GetClusterID(userName, clustername);
            return Ok(_urlData.GetUrls(clusterId));
        }



        /// <summary>
        ///                     GET ALL URL ID's with Cluster ID
        /// </summary>
        /// <param name="clusterid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]/urlid/{clusterid}")]
        public IActionResult GetUrlIds(Guid clusterid)
        {
            return Ok(_urlData.GetUrlIds(clusterid));
        }

        // get url string from id
        [HttpGet]
        [Route("api/[controller]/urlstring/{urlid}")]
        public IActionResult GetUrlString(Guid urlid)
        {
            return Ok(_urlData.GetUrlFromId(urlid));
        }
        //get url depth from id
        [HttpGet]
        [Route("api/[controller]/urldepth/{urlid}")]
        public IActionResult GetUrlDepth(Guid urlid)
        {
            return Ok(_urlData.GetDepthFromId(urlid));
        }






        [Authorize]
        [HttpPost]
        [Route("api/[controller]/{clustername}")]
        public IActionResult AddCluster(string clustername,[FromBody] UrlBody url)
        {
            var userName = User.Identity.Name;
            if (userName == null)
                return NotFound();
            Guid clusterId = _clusterData.GetClusterID(userName, clustername);
            return Ok(_urlData.AddUrl(clusterId, url.url, url.depth));
        }


        

    }
}
