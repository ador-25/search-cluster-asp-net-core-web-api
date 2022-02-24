using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.net_Web_Api_Crud.Models
{
    public class Employee
    {   
        [Key]
        public Guid Id { get; set; }
        public string Name{ get; set; }
    }
}
