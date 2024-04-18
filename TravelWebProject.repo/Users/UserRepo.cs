﻿using BusinessObject.Models;
using DAO;

namespace TravelWebProject.repo.Users
{
    public class UserRepo : IUserRepo
    {
        public User? GetUser(string email) => UserDAO.Instance.GetUser(email);

        public bool RegisterUser(User user) => UserDAO.Instance.RegisterUser(user);

        User? IUserRepo.Authenticate(string email, string password) => UserDAO.Instance.Authenticate(email, password);
    }
}
