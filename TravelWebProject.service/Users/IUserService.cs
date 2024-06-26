using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace TravelWebProject.service.Users
{
    public interface IUserService
    {
        public User? GetUser(string email);
        public bool CheckUserExists(string email);
        public bool RegisterUser(User user);
        public bool UpdateUser(string email, string password);
    }
}