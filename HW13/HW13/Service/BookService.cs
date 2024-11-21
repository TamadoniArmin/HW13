using HW13.Contracts.Service;
using HW13.Entities;
using HW13.Infrestructure;
using HW13.Infrestructure.Configuration;
using HW13.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Service
{
    public class BookService : IBookService
    {
        UserRepository UserRepository = new UserRepository();
        BookRepository BookRepository = new BookRepository();
        public bool BorrowBook(int modelBokkId)
        {
            var user = UserRepository.FindUser(InMemoryDatabase.OnlineUser.Id);
            if (user.Books is not null)
            {
                foreach (var book in user.Books)
                {
                    if (book.Id == modelBokkId)
                    {
                        return false;
                    }
                }
            }
            bool result = BookRepository.BorrowBook(modelBokkId);
            if (!result)
            {
                return false;
            }
            return true;
        }

        public bool ReturnBook(int modelBokkId)
        {
            bool Existing = false;
            var user = UserRepository.FindUser(InMemoryDatabase.OnlineUser.Id);

            foreach (var book in user.Books)
            {
                if (book.Id == modelBokkId)
                {
                    Existing = true;
                }
            }
            if (!Existing)
            {
                return false;
            }
            else
            {
                bool result = BookRepository.ReturnBook(modelBokkId);
                if (!result)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public List<Book> ShowAllBooks()
        {
            return BookRepository.GetListOfAllBook();
        }

        public List<Book> ShowAllUsersBook()
        {
            return BookRepository.ShowListOfUserBooks(InMemoryDatabase.OnlineUser.Id);
        }

        public List<Book> GetListOfAvailableBooks()
        {
            return BookRepository.GetListOfAvailableBooks();
        }

        public void AddBook(string name)
        {
            bool FindBook = BookRepository.FindBook(name);
            if (!FindBook)
            {
                Book book = new()
                {
                    NameOfBook = name,
                    UserId = InMemoryDatabase.OnlineUser.Id
                };
                BookRepository.AddBook(book);
            }
            else
            {
                throw new Exception("This book is already exist!");
            }
        }
    }
}
