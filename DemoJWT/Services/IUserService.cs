using DemoJWT.Entities;
using System.Collections.Generic;

namespace DemoJWT.Services
{
    public interface IUserService
    {
        User Login(string email, string password);
        List<User> GetAll();
    }
}