using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataLayer;
using WebApi.Models;

namespace WebApi.Data
{
    public class SqlStrategyData : IStrategyData
    {
        private ApplicationDbContext _userDbContext;

        public SqlStrategyData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public Strategy AddUrlStrategy(string name)
        {
            Strategy str = new Strategy();
            str.StrategyId = new Guid();
            str.StrategyName = name;
            _userDbContext.Add(str);
            _userDbContext.SaveChanges();
            return str;
        }

        public List<Strategy> GetStrategies()
        {
            
            return _userDbContext.Strategy.ToList();
        }
    }
}
