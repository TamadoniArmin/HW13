using HW13.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Infrestructure
{
    public static class InMemoryDatabase
    {
        public static User OnlineUser { get; set; }
        static InMemoryDatabase()
        {
            OnlineUser=new User();
        }
        public static void CheckUserIsLogin()
        {
            if (OnlineUser is null)
            {
                throw new Exception("You dont have access for this action");
            }
        }
    }
}
