using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataLayer;
using WebApi.Models;

namespace WebApi.Data
{
    public class SqlExtendedClusterData:IExtendedClusterData
    {
        private ApplicationDbContext _userDbContext;

        public SqlExtendedClusterData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public string AddUrl(Guid clusterId, string urlString)
        {
            try
            {
                ExtendedCluster temp = new ExtendedCluster();
                temp.id = new Guid();
                temp.Cluster = _userDbContext.Cluster.Find(clusterId);
                temp.urlString = urlString;
                _userDbContext.ExtendedCluster.Add(temp);
                _userDbContext.SaveChanges();
                return temp.urlString+" Added to db";
            }
            catch (Exception e)
            {
                return "could not add url:"+ urlString;
            }
            
        }
    }
}
