using Library.Models;
using Library.Service;

namespace MyHomeLibraryConsoleUI
{
    public class EditBook
    {
        public Book Edit(Book book, BasicService<Category> categoryService, BasicService<Language> languageService)
        {
            GetCategories.Get(categoryService);
            Console.Write($"Edit category {book.Category.Name}: ");
            var category= Console.ReadLine();
            if (!String.IsNullOrEmpty(category))
            {
                book.Category.Name = category;
            }
            GetLanguages.Get(languageService);
            Console.Write($"Edit language {book.Language.Name}: ");
            var language = Console.ReadLine();
            if (!String.IsNullOrEmpty(language))
            {
                book.Language.Name = language;
            }
            Console.Write($"Edit name {book.Name}: ");
            var newName=Console.ReadLine();
            if (!String.IsNullOrEmpty(newName))
            {
                book.Name = newName;
            }
            Console.Write($"Edit author {book.Author}: ");
            var newAuthor=Console.ReadLine();
            if (!String.IsNullOrEmpty(newAuthor))
            {
                book.Author = newAuthor;
            }
            Console.Write($"Edit publications date {book.PublicationDate}: ");
            var newPublicationsDate = Console.ReadLine();
            if (!String.IsNullOrEmpty(newPublicationsDate))
            {
                if (DateTime.TryParse(newPublicationsDate, out DateTime result))
                {
                    book.PublicationDate = result;
                }               
            }
            return book;
        }
    }
}
