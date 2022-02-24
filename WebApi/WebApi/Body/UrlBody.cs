using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Body
{
    public class UrlBody
    {
        [Required]
        public string url { get; set; }
        [Required]
        public int depth { get; set; }
    }
}
