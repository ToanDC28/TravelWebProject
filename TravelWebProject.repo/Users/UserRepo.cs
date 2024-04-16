using BusinessObject.Models;
using DAO;

namespace TravelWebProject.repo.Users
{
    public class UserRepo : IUserRepo
    {
        public User? GetUser(string username) => UserDAO.Instance.GetUser(username);

        public bool RegisterUser(User user) => UserDAO.Instance.RegisterUser(user);
    }
}
