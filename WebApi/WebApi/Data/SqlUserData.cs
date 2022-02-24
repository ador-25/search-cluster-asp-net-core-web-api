using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataLayer;
using WebApi.Models;

namespace WebApi.Data
{
    public class SqlUserData : IUserData
    {
        private ApplicationDbContext _userDbContext;

        public SqlUserData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public User AddUser(User user)
        {
            if(user.Username!=null && user.Email!=null && user.Password != null)
            {
                _userDbContext.User.Add(user);
                _userDbContext.SaveChanges();
            }
            return user;
        }

        public User EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string Username)
        {
            var user = _userDbContext.User.Find(Username);
            if (user != null)
                return user;
            return user;
        }
       

        public List<User> GetUsers()
        {
            return _userDbContext.User.ToList();
        }
    }
}
