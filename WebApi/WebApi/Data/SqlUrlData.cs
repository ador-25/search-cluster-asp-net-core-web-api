using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataLayer;
using WebApi.Models;

namespace WebApi.Data
{
    public class SqlUrlData : IUrlData
    {
        private ApplicationDbContext _userDbContext;

        public SqlUrlData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public URL AddUrl(Guid clusterId, string url, int depth)
        {
            URL tempUrl = new URL();
            tempUrl.UrlId = new Guid();
            tempUrl.Url = url;
            tempUrl.Depth = depth;
            tempUrl.Cluster = _userDbContext.Cluster.Find(clusterId);
            _userDbContext.URL.Add(tempUrl);
            _userDbContext.SaveChanges();
            return tempUrl;
        }

        public List<URL> GetUrls(Guid clusterId)
        {
            return _userDbContext.URL
                .Where(c => c.Cluster.ClusterId == clusterId)
                .ToList();
        }
        public List<string> GetUrlIds(Guid clusterId)
        {
            return _userDbContext.URL
                .Where(c => c.Cluster.ClusterId == clusterId)
                .Select(u => u.UrlId.ToString())
                .ToList();
        }
        public string GetUrlFromId(Guid urlId)
        {
            var temp = _userDbContext.URL.Find(urlId);
            if (temp == null)
                return null;
            return temp.Url.ToString();


        }

        public int GetDepthFromId(Guid urlId)
        {
            var temp = _userDbContext.URL.Find(urlId);
            if (temp == null)
                return 1;
            return temp.Depth;
        }
    }
}
