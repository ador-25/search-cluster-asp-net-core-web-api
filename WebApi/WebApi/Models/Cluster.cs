using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Cluster
    {
        [Key]
        [Required]
        public Guid ClusterId { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]

        public string ClusterName { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime ClusterCreated { get; set; }
        public int IsUpdated { get; set; }
        public User User { get; set; }



    }
}