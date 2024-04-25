using BusinessObject.Models;
using TravelWebProject.repo.Users;

namespace TravelWebProject.service.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepo userRepo;
        public UserService(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }



        public User? GetUser(string email)
        {
            try
            {
                return userRepo.GetUser(email);
            }
            catch(Exception e)
            {
                throw new Exception("Error in UserService.GetUser", e);
            }
        }

        public bool RegisterUser(User user)
        {
            try
            {
                return userRepo.RegisterUser(user);
            }
            catch(Exception e)
            {
                throw new Exception("Error in UserService.RegisterUser", e);
            }
        }

        public bool CheckUserExists(string email)
        {
            try
            {
                return userRepo.CheckUserExists(email);
            }
            catch(Exception e)
            {
                throw new Exception("Error in UserService.CheckUserExists", e);
            }
        }

        public bool UpdateUser(string email, string password)
        {
            try
            {
                return userRepo.UpdateUser(email, password);
            }
            catch(Exception e)
            {
                throw new Exception("Error in UserService.UpdateUser", e);
            }
        }
    }
}
