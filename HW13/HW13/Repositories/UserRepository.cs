using HW13.Contracts.Repositories;
using HW13.Entities;
using HW13.Enum;
using HW13.Infrestructure;
using HW13.Infrestructure.Configuration;
using HW13.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Repositories
{
    public class UserRepository : IUserRepository
    {
        AppDbContext _context = new AppDbContext();
        public void AddUser(User user)
        {
            var user2 = _context.users.FirstOrDefault(p => p.Username == user.Username);
            if (user2 is null)
            {
                _context.users.Add(user);
                _context.SaveChanges();
            }
            //else
            //{
            //    throw new Exception("This Username is already taken");
            //}
        }

        public User FindUser(int modelUserid)
        {
            return _context.users.AsNoTracking().FirstOrDefault(p => p.Id == modelUserid);
            //if (user is null)
            //{
            //    throw new Exception("User not found!");
            //}
        }

        public bool FindUserByName(string Username)
        {
            return _context.users.Any(p => p.Username == Username);
        }

        public bool Login(string Username, string Password)
        {
            var ResultOfLogin = _context.users.FirstOrDefault(p => p.Username == Username && p.Password == Password);
            //if (!ResultOfLogin)
            //{
            //    return false;
            //}

            //return true;
            if (ResultOfLogin is not null)
            {
                InMemoryDatabase.OnlineUser = ResultOfLogin;
                return true;
            }
            return false;
            
        }

        public List<User> ShowAllUsers()
        {
            return _context.users.AsNoTracking().Where(p=>p.Role==RoleEnum.Normluser).ToList();
        }
        public RoleEnum Role(int userId)
        {
            var user = _context.users.AsNoTracking().FirstOrDefault(p => p.Id == userId);
            return user.Role;
        }

        public void RechargeCredit(int userId)
        {
            var user = _context.users.Where(p => p.Id == userId).ExecuteUpdate(b => b.SetProperty(u => u.TimeOfRegister, DateTime.Now));
            _context.SaveChanges();
        }

        public void AddBookToListOfUser(Book book)
        {
            var user = _context.users.FirstOrDefault(p => p.Id == InMemoryDatabase.OnlineUser.Id);
            if (user is not null)
            {
                user.Books.Add(book);
                _context.SaveChanges();
            }
        }
    }
}
