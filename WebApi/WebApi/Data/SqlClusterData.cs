using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataLayer;
using WebApi.Models;

namespace WebApi.Data
{
    public class SqlClusterData : IClusterData
    {
        private ApplicationDbContext _userDbContext;

        public SqlClusterData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        // create new clusters
        public Cluster AddCluster(Cluster cluster, string username)
        {
            List<string> temp = GetClusters(username);
            if (temp.Contains(cluster.ClusterName))
            {
                return null;
            }
            //dont update if user is null
            cluster.User = _userDbContext.User.Find(username);
            cluster.ClusterId = new Guid();
            if (cluster.ClusterName != null && cluster.Description != null && cluster.User != null)
            {
                // set cluster status 0 while creating cluster first time
                cluster.IsUpdated = 0;
                // set current time to cluster creation date
                cluster.ClusterCreated = DateTime.Now;
                _userDbContext.Cluster.Add(cluster);
                _userDbContext.SaveChanges();
            }
            return cluster;
        }





        // edit cluster
        public Cluster EditCluster(Cluster cluster)
        {

            var temp = _userDbContext.Cluster.Find(cluster.ClusterId);
            temp.ClusterName = cluster.ClusterName;
            temp.Description = cluster.Description;
            _userDbContext.Cluster.Update(temp);
            _userDbContext.SaveChanges();
            return temp;

        }







        //get cluster info with ID

        private Cluster GetCluster(Guid ClusterId)
        {
            var cluster = _userDbContext.Cluster.Find(ClusterId);
            return cluster != null ? cluster : null;

        }


        // get cluster details with a username and clustername
        public Cluster GetCluster(string username, string clusterName)
        {
            List<Guid> temp = GetClusterIds(username);
            foreach (var clusterId in temp)
            {
                Cluster c = GetCluster(clusterId);
                if (c.ClusterName == clusterName)
                    return c;
            }

            return null;

        }

        // get ID of all clusters for a user
        private List<Guid> GetClusterIds(string username)
        {
            return _userDbContext.Cluster
                .Where(u => u.User.Username == username)
                .Select(p => p.ClusterId).
                ToList();
        }

        //get clusters for a specific user
        public List<string> GetClusters(string Username)
        {
            return _userDbContext.Cluster
                .Where(u => u.User.Username == Username)
                .Select(p => p.ClusterName).
                ToList();

        }


        //get all clusters.only for test
        public List<Cluster> GetClusters()
        {
            return _userDbContext.Cluster.ToList();
        }



        // get Cluster Id with username and clustername

        public Guid GetClusterID( string username, string clustername)
        {
            List <Guid> temp = GetClusterIds(username);
            foreach (var clusterId in temp)
            {
                Cluster c = GetCluster(clusterId);
                if (c.ClusterName == clustername)
                    return c.ClusterId;
            }
            return new Guid();
        }

    }
}
