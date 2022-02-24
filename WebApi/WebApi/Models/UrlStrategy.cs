using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UrlStrategy
    {
      
        public Guid UrlId { get; set; }
        public URL URL { get; set; }
       
        public string StrategyName { get; set; }
        public Strategy Strategy { get; set; }
    }
}
