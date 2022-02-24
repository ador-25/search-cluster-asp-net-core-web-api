using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ClusterData
    {
        [Key]
        public string UrlId { get; set; }
        public string Data { get; set; }
    }
}
