using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{ 
    //  User Interface to interact with database
    //  SQL or MockData class will implement the interface
    public interface IUserData
    {
        // Get all users for test
        // Will return all user objects of the system
        List<User> GetUsers();

        // Get individual user data with Username. Username will come from routing info
        // Will return user object 
        User GetUser(string Username);
        
        // For sign up.Controller will be query type for this
        // Will return the User object that is added 
        User AddUser(User user);

        // To edit user info.Controller will be query type for this
        // Updated user object will be returned
        User EditUser(User user);
    }
}
