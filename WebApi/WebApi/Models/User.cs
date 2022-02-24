using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    // Model for User table in database
    public class User
    {
        [Key]
        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Email { get; set; }


        [Required]
        [MaxLength(100)]
        [MinLength(4)]
        public string Fullname { get; set; }


        [Required]
        [MaxLength(30)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
