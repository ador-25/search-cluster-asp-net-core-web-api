using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserData _userData;
        public UsersController(IUserData userdata)
        {
            _userData = userdata;
        }
       
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetUsers()
        {
            return Ok(_userData.GetUsers());
        }


        [Authorize]
        [HttpGet]
        [Route("api/[controller]/user")]
        public IActionResult GetUser()
        {
            var userName = User.Identity.Name;
            if (userName == null)
                return NotFound();
            return Ok(_userData.GetUser(userName));
        }


        // will be implemented later
        [HttpPatch]
        [Route("api/[controller]/user")]
        private IActionResult EditUser([FromQuery]User user)
        {
            _userData.AddUser(user);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + user.Username, user);
        }



        // Route => 
        // https://localhost:44398/api/users/user?username=ador&password=adadadad&email=someemail&fullname=abtaheefarhanador


        [HttpPost]
        [Route("api/[controller]/user")]
        public IActionResult AddUser([FromQuery] User user)
        {
            _userData.AddUser(user);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + user.Username, user);
        }




    }
}
