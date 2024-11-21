using HW13.Contracts.Service;
using HW13.Entities;
using HW13.Enum;
using HW13.Infrestructure;
using HW13.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Service
{
    public class UserService : IUserService
    {
        UserRepository UserRepository = new UserRepository();

        public User GetCurrentUser()
        {
            return UserRepository.FindUser(InMemoryDatabase.OnlineUser.Id);
        }

        public bool Login(string Username, string Password)
        {
            return UserRepository.Login(Username, Password);
        }

        public void Register(string Userame, string Password, RoleEnum roleEnum)
        {
            bool Result = UserRepository.FindUserByName(Userame);
            if (!Result)
            {
                User user = new User()
                {
                    Username = Userame,
                    Password = Password,
                    Role = roleEnum,
                    TimeOfRegister = DateTime.Now  
                };
                UserRepository.AddUser(user);
            }
            else if (Result)
            {
                throw new Exception("This Username is already taken!");
            }
        }

        public RoleEnum Role()
        {
            return UserRepository.Role(InMemoryDatabase.OnlineUser.Id);
        }

        public List<User> ShowAllUsers()
        {
            return UserRepository.ShowAllUsers();
        }
        public void RechargeCredit(int userId)
        {
            UserRepository.RechargeCredit(userId);
        }
    }
}
