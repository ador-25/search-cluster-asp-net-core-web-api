using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IUrlStrategyData
    {
        UrlStrategy AddUrlHasStrategy(Guid urlString, string strategyName);
        List<UrlStrategy> GetStrategies(Guid urlString);
        List<string> GetStrategyNames(Guid urlString);
        //for test
        List<string> GetAllStrategies();
    }
}
