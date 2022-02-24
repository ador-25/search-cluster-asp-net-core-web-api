using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataLayer;
using WebApi.Models;

namespace WebApi.Data
{
    public class ClusterDataSql : IClusterInfo
    {
        private ApplicationDbContext _userDbContext;

        public ClusterDataSql(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public List<ClusterData> GetData(Guid cluserId)
        {
            String temp = "select dbo.ClusterData.UrlId,dbo.ClusterData.Data from dbo.ClusterData join dbo.ExtendedCluster on dbo.ExtendedCluster.urlstring=dbo.ClusterData.urlid where dbo.ExtendedCluster.clusterid='";
            temp = temp + cluserId.ToString()+"'";
            return _userDbContext.ClusterData
             .FromSqlRaw(temp)
             .ToList();
        }

        public List<ClusterData> Search(string keyword)
        {
            string temp = " SELECT * FROM dbo.ClusterData Where Data like '%";
            temp = temp + keyword + "%'";

            return _userDbContext.ClusterData
             .FromSqlRaw(temp)
             .ToList();

            
        }

    }
}
