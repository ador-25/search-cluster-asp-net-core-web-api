using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IClusterData
    {

        List<string> GetClusters(string Username);
        List<Cluster> GetClusters();
        Cluster GetCluster(string username,string clusterName);
        Guid GetClusterID(string username, string clustername);
        Cluster AddCluster(Cluster cluster,string username);
        Cluster EditCluster(Cluster cluster);
        
    }
}
