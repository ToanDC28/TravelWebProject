using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace TravelWebProject.repo.Users
{
    public interface IUserRepo
    {
        public User? GetUser(string email);
        public bool RegisterUser(User user);
        User? Authenticate(string email, string password);
        public bool CheckUserExists(string email);
        public bool UpdateUser(string email, string password);
    }
}