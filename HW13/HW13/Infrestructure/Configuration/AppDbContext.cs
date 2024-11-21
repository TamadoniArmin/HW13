using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW13.Configuration;
using HW13.Entities;
using Microsoft.EntityFrameworkCore;

namespace HW13.Infrestructure.Configuration
{
    public class AppDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-6RE5DJR\SQLEXPRESS;Initial Catalog=Library;User Id=sa; password=arminpooma00;Integrated Security=SSPI;TrustServerCertificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> users { get; set; }
        public DbSet<Book> books { get; set; }
    }
}
