using BusinessObject.Models;

namespace DAO
{
    public class UserDAO
    {
        private static UserDAO instance = null!;
        private readonly TravelWebContext context = null!;

        private UserDAO()
        {
            context = new TravelWebContext();
        }

        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDAO();
                }
                return instance;
            }
        }

        public User? GetUser(string username)
        {
            try {
                return context.Users.SingleOrDefault(u => u.Username == username);
            } catch(Exception e) {
                throw new Exception("Error in UserDAO.GetUser", e);
            }
        }

        public bool RegisterUser(User user)
        {
            try {
                context.Users.Add(user);
                context.SaveChanges();
                return true;
            } catch(Exception e) {
                throw new Exception("Error in UserDAO.RegisterUser", e);
            }
        }

        public User? Authenticate(string email, string password)
        {
            try {
                return context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
            } catch(Exception e) {
                throw new Exception("Error in UserDAO.Authenticate", e);
            }
        }
    }
}
