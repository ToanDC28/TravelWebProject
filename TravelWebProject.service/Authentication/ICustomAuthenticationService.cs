using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace TravelWebProject.service.Authentication
{
    public interface ICustomAuthenticationService
    {
        User? Authenticate(string email, string password);
    }
}