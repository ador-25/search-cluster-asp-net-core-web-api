using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Strategy
    {
        [Key]
        [Required]
        [MaxLength(10)]
        
        public string StrategyName { get; set; }


        [Required]
        public Guid StrategyId { get; set; }

        public ICollection<UrlStrategy> UrlStrategies { get; set; }

    }
}
