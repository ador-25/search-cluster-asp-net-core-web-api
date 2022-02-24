using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IStrategyData
    {
        List<Strategy> GetStrategies();
        Strategy AddUrlStrategy(string name);
    }
}
