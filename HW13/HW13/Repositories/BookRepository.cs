using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW13.Contracts.Repositories;
using HW13.Entities;
using HW13.Infrestructure;
using HW13.Infrestructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HW13.Repositories
{
    public class BookRepository : IBookRepository
    {
        AppDbContext _context = new AppDbContext();
        UserRepository UserRepository = new();

        public void AddBook(Book book)
        {
            var book2 = _context.books.FirstOrDefault(p => p.NameOfBook == book.NameOfBook);
            if (book2 is null)
            {
                _context.books.Add(book);
                _context.SaveChanges();
            }
        }

        public bool BorrowBook(int modelId)
        {
            var book1 = _context.books.FirstOrDefault(p => p.Id == modelId);
            if (book1 is not null)
            {
                book1.Status = Enum.BookStatusEnum.Borrowed;
                book1.User = InMemoryDatabase.OnlineUser;
                book1.UserId = InMemoryDatabase.OnlineUser.Id;
                _context.SaveChanges();
                UserRepository.AddBookToListOfUser(book1);
                return true;
            }
            return false;
        }

        public bool FindBook(string Name)
        {
            return _context.books.Any(p => p.NameOfBook == Name);
        }

        public List<Book> GetListOfAllBook()
        {
            return _context.books.AsNoTracking().ToList();
        }

        public List<Book> GetListOfAvailableBooks()
        {
            return _context.books.AsNoTracking().Where(p => p.Status == Enum.BookStatusEnum.NotBorrowed).ToList();
        }

        public bool ReturnBook(int ModelId)
        {
            var book2 = _context.books.FirstOrDefault(p => p.Id == ModelId);
            var Admin = _context.users.FirstOrDefault(u => u.Id == 1);
            if (book2 is not null)
            {
                book2.Status = Enum.BookStatusEnum.NotBorrowed;
                book2.User = Admin;
                book2.UserId = Admin.Id;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Book> ShowListOfUserBooks(int ModelUserId)
        {
            var user= _context.users.AsNoTracking().FirstOrDefault(p => p.Id == ModelUserId);
            return user.Books;
        }
    }
}
