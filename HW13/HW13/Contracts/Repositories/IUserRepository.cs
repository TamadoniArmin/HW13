using HW13.Entities;
using HW13.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Contracts.Repositories
{
    public interface IUserRepository
    {
        public void AddUser(User user);
        public bool Login(string Username, string Password);
        public void AddBookToListOfUser(Book book);
        public List<User> ShowAllUsers();
        public User FindUser(int modelUserid);
        public bool FindUserByName(string Username);
        public RoleEnum Role(int userId);
        public void RechargeCredit(int userId);
    }
}
