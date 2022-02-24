using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IUrlData
    {
        List<URL> GetUrls(Guid clusterId);
        URL AddUrl(Guid clusterId,string url, int depth);
        string GetUrlFromId(Guid urlId);
        List<string> GetUrlIds(Guid clusterId);
        int GetDepthFromId(Guid urlId);



    }
}
