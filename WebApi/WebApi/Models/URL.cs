using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class URL
    {
        [Required]
        [Key]
        public Guid UrlId { get; set; }

        [Required]
        [MinLength(4)]
        public string Url { get; set; }

        [Required]
        public int Depth { get; set; }
       
        [Required]
        public Cluster Cluster { get; set; }
        public ICollection<UrlStrategy> UrlStrategies { get; set; }
    }
}
