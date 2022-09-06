using Library.Data;
using Library.Models;
using Library.Service;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeLibraryConsoleUI
{
    public static class ParseCommand
    {
        public static string Parse(string command)
        {
            IAccessData<Category> categories = new AccessData<Category>("categories");
            IAccessData<Language> languages = new AccessData<Language>("languages");
            IAccessData<User> users = new AccessData<User>("users");
            IAccessData<Book> books = new AccessData<Book>("books");
            BasicService<Category> categoryService = new BasicService<Category>(categories);
            BasicService<Language> languageService = new BasicService<Language>(languages);
            UserService userService = new UserService(users);
            BookService bookService = new BookService(books, categoryService, languageService, userService);

            string msg =string.Empty;
            Command c;
            if (!Enum.TryParse(command, out c))
            {
                return "Please enter a valid command.";
            }
            
            switch (c)
            {
                case Command.add:
                    var newBook = new CreateBook().Create(categoryService, languageService);
                    try
                    {
                        bookService.Add(newBook);
                        msg = "The book is added to the library.";
                    }
                    catch(Exception ex)
                    {
                        msg=ex.Message;
                    }
                    break;
                   
                case Command.delete:
                    GetAllBooks(bookService);
                    Console.Write("Enter books to delete id: ");
                    if(int.TryParse(Console.ReadLine(), out int id))
                    {
                        try
                        {
                            bookService.Delete(id);
                            msg = "The book is deleted.";
                        }
                        catch(Exception ex)
                        {
                            msg=ex.Message;
                        }
                        break;
                    }
                    msg = "Can't delete the book.";
                    break;
                case Command.update:
                    GetAllBooks(bookService);
                    Console.Write(" Enter the books to edit id: ");
                    if(int.TryParse(Console.ReadLine(), out id))
                    {
                        var bookToEdit = bookService.GetById(id);
                        if ( bookToEdit!= null)
                        {
                            var book = new EditBook().Edit(bookToEdit, categoryService, languageService);
                            try
                            {
                                bookService.Update(book);
                                msg = "The book is updated.";
                            }
                            catch (Exception ex)
                            {
                                msg= ex.Message;
                            }
                        }                        
                    }                   
                    break;
                case Command.list:
                    GetAllBooks(bookService);
                    break;
                case Command.take:
                    GetAllBooks(bookService);
                    Console.Write("Enter books id: ");
                    var bookId = Console.ReadLine();
                    Console.Write("Enter users name: ");
                    var usersName = Console.ReadLine();
                    if (int.TryParse(bookId, out id))
                    {
                        try
                        {
                            bookService.TakeBook(id, usersName);
                            msg = "You have borrowed the book.";
                        }
                        catch(Exception ex)
                        {
                            msg = ex.Message;
                        }
                    }                      
                    break;
                case Command.returnbook:
                    GetAllBooks(bookService);
                    Console.Write("Enter books id: ");
                    var bId = Console.ReadLine();
                    Console.Write("Enter users id: ");
                    var uId = Console.ReadLine();
                    if (int.TryParse(bId, out int b)&&int.TryParse(uId, out int u))
                    {
                        try
                        {
                            bookService.ReturnBook(b, u);
                            msg = "You have returned the book.";
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                        }
                    }
                    break;
                case Command.quit:
                    msg = "Good by!";
                    break;
                case Command.getbyname:
                    Console.Write("Name: ");
                    var name=Console.ReadLine();
                    var booksByName=bookService.GetBooksByName(name);
                    WriteToConsole(booksByName);
                    
                    break;
                case Command.getbyauthor:
                    Console.Write("Author: ");
                    var author = Console.ReadLine();
                    var booksByAuthor = bookService.GetBooksByAuthor(author);
                    WriteToConsole(booksByAuthor);
                    break;
                case Command.gettaken:                   
                    var bookstaken = bookService.GetTakenBooks();
                    WriteToConsole(bookstaken);
                    break;
                case Command.getavailable:
                    var booksavailable = bookService.GetAvailableBooks();
                    WriteToConsole(booksavailable);
                    break;
                case Command.getbycategory:
                    Console.Write("Category: ");
                    var cat = Console.ReadLine();
                    var booksByCategory = bookService.GetBooksByCategory(cat);
                    WriteToConsole(booksByCategory);
                    break;
                case Command.getbylanguage:
                    Console.Write("Language: ");
                    var lang = Console.ReadLine();
                    var booksByLanguage = bookService.GetBooksByLanguage(lang);
                    WriteToConsole(booksByLanguage);
                    break;
                default:                   
                    break;
            }
            return msg; ;
        }       
        private static void GetAllBooks(BookService bookService)
        {
            Console.WriteLine("Books in the library:");
            var booksList = bookService.Get();
            WriteToConsole(booksList);
        }
        private static void WriteToConsole(List<Book> books)
        {
            foreach (var b in books)
            {
                Console.WriteLine($"{b.Id.ToString().PadRight(5)}  {b.Name.PadRight(10)} {b.Author.PadRight(10)} {b.PublicationDate.ToString("yyyy-MM-dd").PadRight(10)} {b.Language.Name.PadRight(10)} {b.Category.Name.PadRight(10)} {b.IsAvailable.ToString().PadRight(5)}");
            }
        }
    }
}
