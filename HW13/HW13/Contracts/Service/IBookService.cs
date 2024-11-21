using HW13.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Contracts.Service
{
    public interface IBookService
    {
        List<Book> ShowAllBooks();
        List<Book> ShowAllUsersBook();
        public List<Book> GetListOfAvailableBooks();
        bool BorrowBook(int modelBokkId);
        bool ReturnBook(int modelBokkId);
        void AddBook(string name);
    }
}
