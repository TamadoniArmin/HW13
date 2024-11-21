using HW13.Enum;
using HW13.Infrestructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string NameOfBook { get; set; }
        [MaxLength(20)]
        public string? NameOfWriter { get; set; }
        public DateTime? YearOfPublish { get; set; }
        public BookStatusEnum? Status { get; set; } = BookStatusEnum.NotBorrowed;
        public User? User { get; set; }
        public int UserId { get; set; } = 1;
    }
}
