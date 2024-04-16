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

        public User? GetUser(string username)
        {
            try
            {
                return userRepo.GetUser(username);
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
    }
}
