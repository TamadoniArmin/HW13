using HW13.Entities;
using HW13.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Contracts.Service
{
    public interface IUserService
    {
        public void Register(string Userame, string Password,RoleEnum roleEnum);
        public bool Login(string Username, string Password);
        public RoleEnum Role();
        public User GetCurrentUser();
        public List<User> ShowAllUsers();
        public void RechargeCredit(int userId);

    }
}
