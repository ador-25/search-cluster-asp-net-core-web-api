using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IClusterInfo
    {
        List<ClusterData> Search(string keyword);
        List<ClusterData> GetData(Guid cluserId);
    }
}
