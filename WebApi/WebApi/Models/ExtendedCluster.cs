using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ExtendedCluster
    {
        [Key]
        public Guid id { get; set; }
        public ClusterData ClusterData { get; set; }
        public Cluster Cluster { get; set; }
        public string urlString { get; set; }
    }
}
