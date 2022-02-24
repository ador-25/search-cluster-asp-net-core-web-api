using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Body;
using WebApi.DataLayer;
using WebApi.Models;

namespace WebApi.Data
{
    public class SqlUrlStrategyData:IUrlStrategyData
    {
        private ApplicationDbContext _userDbContext;

        public SqlUrlStrategyData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public UrlStrategy AddUrlHasStrategy(Guid urlString, string strategyName)
        {
            UrlStrategy u = new UrlStrategy();
            u.UrlId = urlString;
            u.StrategyName = strategyName;
            _userDbContext.UrlStrategies.Add(u);
            _userDbContext.SaveChanges();
            return u;
        }

        public List<string> GetAllStrategies()
        {
            return _userDbContext.UrlStrategies
                .Select(u=> u.StrategyName.ToString())
                .ToList();
        }

        public List<UrlStrategy> GetStrategies(Guid urlString)
        {
            return _userDbContext.UrlStrategies.Where(u => u.URL.UrlId == urlString).ToList();
        }

        public List<string> GetStrategyNames(Guid urlString)
        {
            return _userDbContext.UrlStrategies
                .Where(u => u.URL.UrlId == urlString)
                .Select(u=> u.StrategyName.ToString())
                .ToList();
        }
    }
}
