using HW13.Contracts.Service;
using HW13.Enum;
using HW13.Infrestructure;
using HW13.Service;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
bool Exit = false;
do
{
    Console.WriteLine("***** Wellcome *****");
    Console.WriteLine("1.Login");
    Console.WriteLine("2.Register");
    Console.WriteLine("3.Exit");
    var answer1 = int.Parse(Console.ReadLine()!);
    UserService userService = new UserService();
    if (answer1 == 1)
    {
        Console.WriteLine("***** Login *****");
        Console.Write("Please enter your username: ");
        string EnteredUsername = Console.ReadLine()!;
        Console.WriteLine("Please enter your password: ");
        string EnteredPassword = Console.ReadLine()!;
        bool ResultOfLogin = userService.Login(EnteredUsername, EnteredPassword);
        if (!ResultOfLogin)
        {
            throw new Exception("There is no user with this information! Please try again");
        }
        else
        {
            var CurrentUserRole = userService.Role();
            if (CurrentUserRole == RoleEnum.Normluser)
            {
                if ((DateTime.Now - InMemoryDatabase.OnlineUser.TimeOfRegister).TotalDays > 30)
                {
                    Console.WriteLine("Your you don't have credit by now!!");
                    break;
                }
                else
                {
                    UserPage();
                }
            }
            else if (CurrentUserRole == RoleEnum.Admin)
            {
                AdminPage();
            }
            else
            {
                throw new Exception("You don't set any role for this user yet!");
            }
        }
    }
    else if (answer1 == 2)
    {
        Console.WriteLine("***** Register *****");
        Console.Write("Please enter your Username: ");
        string RegisterName = Console.ReadLine()!;
        Console.Write("Please enter you password: ");
        string RegisterPassword = Console.ReadLine()!;
        Console.WriteLine("Select role (1. Admin 2. Normaluser)");
        int role = int.Parse(Console.ReadLine()!);
        userService.Register(RegisterName, RegisterPassword, (RoleEnum)role);
        Console.WriteLine("Done. Now please login");
    }
    else if (answer1 == 3)
    {
        Exit = true;
    }
    else
    {
        throw new Exception("Action is invalid!");
    }

} while (!Exit);


void UserPage()
{
    bool Logout = false;
    do
    {
        Console.WriteLine("***** User *****");
        Console.WriteLine("Please select your action: ");
        Console.WriteLine("1.Borrow book");
        Console.WriteLine("2.Return book");
        Console.WriteLine("3.See you borrowed books");
        Console.WriteLine("4.See all books in library");
        Console.WriteLine("5.Logout");
        //UserService userService = new UserService();
        BookService bookService = new BookService();
        int UserAnswer = int.Parse(Console.ReadLine()!);
        switch (UserAnswer)
        {
            case 1:
                var books = bookService.GetListOfAvailableBooks();
                foreach (var book in books)
                {
                    Console.WriteLine($"Name:{book.NameOfBook} | Id= {book.Id}");
                };
                Console.WriteLine("************************************************");
                Console.Write("Please enter your wanted book's id: ");
                int WantedBookForBorrow = int.Parse(Console.ReadLine()!);
                bool ResultOfBorrow = bookService.BorrowBook(WantedBookForBorrow);
                if (ResultOfBorrow)
                {
                    Console.WriteLine("Done.");
                }
                else if (!ResultOfBorrow)
                {
                    throw new Exception("You can not borrow this book at the time! Please Check the information again.");
                }
                break;
            case 2:
                var UserBooks = bookService.ShowAllUsersBook();
                foreach (var book in UserBooks)
                {
                    Console.WriteLine($"Name:{book.NameOfBook} | ID: {book.Id}");
                }
                Console.WriteLine("************************************************");
                Console.Write("Please enter the id of book that you want to return: ");
                int WantedIdForRetern = int.Parse(Console.ReadLine()!);
                bool ResultOfReturn = bookService.ReturnBook(WantedIdForRetern);
                if (!ResultOfReturn)
                {
                    throw new Exception("Oops.... Something goes wrong! Please try it later.");
                }
                else
                {
                    Console.WriteLine("Done.");
                };
                break;
            case 3:
                var ListOfBorrowedBooks = bookService.ShowAllUsersBook();
                foreach (var book in ListOfBorrowedBooks)
                {
                    Console.WriteLine($"Name:{book.NameOfBook} | Id= {book.Id}");
                };
                Console.WriteLine("************************************************");
                Console.WriteLine("Press any key to countinue.");
                Console.ReadKey();
                break;
            case 4:
                var ListOfAllBooks = bookService.GetListOfAvailableBooks();
                foreach (var book in ListOfAllBooks)
                {
                    Console.WriteLine($"Name:{book.NameOfBook} | Id= {book.Id}");
                };
                Console.WriteLine("************************************************");
                Console.WriteLine("Press any key to countinue.");
                Console.ReadKey();
                break;
            case 5:
                InMemoryDatabase.OnlineUser = null;
                Logout = true;
                break;
            default:
                break;
        }
    } while (!Logout);
}
void AdminPage()
{
    bool Logout = false;
    do
    {
        Console.WriteLine("***** Admin *****");
        Console.WriteLine("Please select you action.");
        Console.WriteLine("1. Show all books in library.");
        Console.WriteLine("2. Show all ussers");
        Console.WriteLine("3. Add book");
        Console.WriteLine("4. Recharge credit");
        Console.WriteLine("5. Logout");
        UserService userService = new();
        BookService bookService = new();
        int AdminAnswer = int.Parse(Console.ReadLine()!);
        switch (AdminAnswer)
        {
            case 1:
                var books = bookService.GetListOfAvailableBooks();
                foreach (var book in books)
                {
                    if (book.Status == BookStatusEnum.Borrowed)
                    {
                        Console.WriteLine($"Name:{book.NameOfBook} | Id= {book.Id}");
                        Console.WriteLine($"Status: Borrowed by user {book.UserId}");
                    }
                    else
                    {
                        Console.WriteLine($"Name:{book.NameOfBook} | Id= {book.Id}");
                        Console.WriteLine("Status: This books is not borrowed by any user.");
                    }
                    Console.WriteLine("************************************************");
                };
                break;
            case 2:
                var users = userService.ShowAllUsers();
                foreach (var user in users)
                {
                    Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                    Console.WriteLine($"Username: {user.Username}");
                    Console.WriteLine($"Id: {user.Id}");
                    Console.WriteLine("************************************************");
                };
                break;
            case 3:
                Console.Write("Please entre the name of book:");
                string BookName = Console.ReadLine()!;
                bookService.AddBook(BookName);
                break;
            case 4:
                var ListOfUsers = userService.ShowAllUsers();
                foreach (var user in ListOfUsers)
                {
                    Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                    Console.WriteLine($"Username: {user.Username}");
                    Console.WriteLine($"Id: {user.Id}");
                    Console.WriteLine("************************************************");
                }
                Console.Write("Please enter your wanted userid:");
                int RechargeId = int.Parse(Console.ReadLine()!);
                userService.RechargeCredit(RechargeId);
                Console.WriteLine("Done.");
                break;
            case 5:
                InMemoryDatabase.OnlineUser = null;
                Logout = true;
                break;
            default:
                break;
        }
    } while (!Logout);

}