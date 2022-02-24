using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Auth;
using WebApi.Body;
using WebApi.Data;
using WebApi.Models;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        IUserData _userData;
        



        public AuthenticateController(IUserData userData,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _userData = userData;

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            Console.WriteLine(model);
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            _userData.AddUser(model);
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });

        }





        // Google login

        [HttpPost("google-login")]
        [ProducesDefaultResponseType]
        public async Task<JsonResult> GoogleLogin(ExternalAuthDto request)
        {
            // ...
            Payload payload=null;
            try
            {
                payload = await ValidateAsync(request.IdToken, new ValidationSettings
                {
                    Audience = new[] { "818495489978-3p0bs1ocr5k76vrr98orb1ojcd7ks0vt.apps.googleusercontent.com" }
                });
                // It is important to add your ClientId as an audience in order to make sure
                // that the token is for your application!
            }
            catch
            {
                // Invalid token
            }

            var user = await GetOrCreateExternalLoginUser("google", payload.Subject, payload.Email, payload.GivenName, payload.FamilyName);
            LoginModel t = new LoginModel
            {
                Username = user.Email,
                Password = "Password@123!"
            };
            return (JsonResult)await Login(t);

        }

        public async Task<ApplicationUser> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName)
        {
            User temp = null;
            // Login already linked to a user
            var user = await userManager.FindByLoginAsync(provider, key);
            if (user != null)
                return user;

            user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // No user exists with this email address, we create a new one
                user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                temp = new User
                {
                    Username = email,
                    Email = email,
                    Fullname = firstName + " " + lastName,
                    Password = "Password@123!"

                };

            }

            // Link the user to this login
            var info = new UserLoginInfo(provider, key, provider.ToUpperInvariant());
            var result = await userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
            {
                var res = await userManager.CreateAsync(user, "Password@123!");
                _userData.AddUser(temp);

                return user;
            }
            return null;
        }





    }
}
