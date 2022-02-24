using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Body
{
    public class ExternalAuthDto
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
    }
}
