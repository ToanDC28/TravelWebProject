using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using TravelWebProject.repo.Users;

namespace TravelWebProject.service.Authentication
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepo userRepo;
        public CustomAuthenticationService(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public User? Authenticate(string email, string password)
        {
            try {
                return userRepo.Authenticate(email, password);
            } catch(Exception e) {
                throw new Exception("Error in CustomAuthenticationService.Authenticate", e);
            }
        }
    }
}