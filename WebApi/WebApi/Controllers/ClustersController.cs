using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    
    public class ClustersController : ControllerBase
    {
        IClusterData _clusterData;
 
        public ClustersController(IClusterData clusterData)
        {
            _clusterData = clusterData;
        }


        // return all clusters
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetClusters()
        {
            return Ok(_clusterData.GetClusters());
        }



        //return clusters for one username
        [Authorize]
        [HttpGet]
        [Route("api/[controller]/myclusters")]
        public IActionResult GetCluster()
        {
            var userName = User.Identity.Name;
            if (userName == null)
                return NotFound();
            return Ok(_clusterData.GetClusters(userName));
        }

        // get cluster details with clustername
        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{clustername}")]
        public IActionResult GetCluster(string clustername)
        {
            var userName = User.Identity.Name;
            if (userName == null)
                return NotFound();


            var temp = _clusterData.GetCluster(userName,clustername);
            if (temp != null)
                return Ok(temp);
            else
                return NotFound();
        }

        //edit existing clusters(name,descriptions only)



        //edit later
        /*
        [HttpPatch]
        [Route("api/[controller]/{ClusterId}")]
        private IActionResult EditCluster(Guid ClusterId)
        {
            var temp=_clusterData.GetCluster(ClusterId);
            if (temp != null)
            {
                return Ok(_clusterData.EditCluster(temp));
            }
            return null;
            //return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + user.Username, user);
        }
        */





        [Authorize]
        [HttpPost]
        [Route("api/[controller]/{clustername}/{description}/")]
        public IActionResult AddCluster(string clustername, string description)
        {
            var userName = User.Identity.Name;
            if (userName == null)
                return NotFound();




            Cluster cluster = new Cluster();
            cluster.ClusterName = clustername;
            cluster.Description = description;
      
            var temp= _clusterData.AddCluster(cluster,userName);
            if (temp == null)
            {
                Response response = new Response()
                {
                    Message = "Cluster already exists",
                    Status = "Error",
                };
                return BadRequest("Cluster with same name already exists");
            }

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + cluster.ClusterName, cluster); 
        }
    }
}
