using DemoJWT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoJWT.Services
{
    public class UserService : IUserService
    {
        private List<User> Users { get; set; }

        public UserService()
        {
            Users = new List<User>();
            Users.Add(new User { Id = 1, Email = "steve@test.com", Password = "test1234", PhoneNumber = "0147/25.21.36", IsAdmin = true });
            Users.Add(new User { Id = 2, Email = "khun@test.com", Password = "test1234", PhoneNumber = "0147/25.21.36", IsAdmin = false });

        }

        public User Login(string email, string password)
        {
            User currentUser = null;
            foreach (User u in Users)
            {
                if (email == u.Email && password == u.Password)
                {
                    currentUser = u;
                }
            }

            return currentUser;
        }

        public List<User> GetAll()
        {
            return Users;
        }
    }
}
