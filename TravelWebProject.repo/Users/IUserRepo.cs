using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace TravelWebProject.repo.Users
{
    public interface IUserRepo
    {
        public User? GetUser(string username);
        public bool RegisterUser(User user);
    }
}