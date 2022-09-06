using Library.Models;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeLibraryConsoleUI
{
    public class CreateBook
    {
        public Book Create(BasicService<Category> categoryService, BasicService<Language> languageService)
        {
            Book book = new Book();

            GetCategories.Get(categoryService);
            Console.Write("Category: ");
            var category = new Category() { Name = Console.ReadLine() };
            book.Category = category;
            GetLanguages.Get(languageService);
            Console.Write("Language: ");
            var language = new Language() { Name = Console.ReadLine() };
            book.Language = language;
            Console.Write("Name: ");
            book.Name=Console.ReadLine();
            Console.Write("Author: ");
            book.Author = Console.ReadLine();
            while (true)
            {
                Console.Write("Publications date: ");
                Console.Write("2022/01/01: ");
                var d = Console.ReadLine();
                if (DateTime.TryParse(d, out DateTime result))
                {
                    book.PublicationDate = result;
                    break;
                }
            }                                  
            return book;
        }
    }
}
