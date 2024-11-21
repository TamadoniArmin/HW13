using HW13.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Contracts.Repositories
{
    public interface IBookRepository
    {
        public List<Book> GetListOfAllBook();
        public List<Book> GetListOfAvailableBooks();
        List<Book> ShowListOfUserBooks(int ModelUserId);
        bool BorrowBook(int modelBookId);
        bool ReturnBook(int ModelBookId);
        void AddBook(Book book);
        bool FindBook(string Name);
        
    }
}
