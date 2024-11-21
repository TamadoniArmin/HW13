using HW13.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(10)]
        public string? FirstName { get; set; }
        [MaxLength(10)]
        public string? LastName { get; set; }
        public DateTime TimeOfRegister { get; set; }
        public RoleEnum Role { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

    }
}
