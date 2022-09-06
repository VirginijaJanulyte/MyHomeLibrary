using Library.Data;
using Library.Models;
using System.Xml.Linq;


namespace Library.Service
{
    public class BookService : BasicService<Book>
    {
        private IAccessData<Book> _bookData;
        private BasicService<Category> _categoryService;
        private BasicService<Language> _languageService;
        private UserService _userService;

        public BookService(IAccessData<Book> data, BasicService<Category> categoryService,
            BasicService<Language> languageService, UserService userService) : base(data)
        {
            _bookData = data;
            _categoryService = categoryService;
            _languageService = languageService;
            _userService = userService;

        }
        public override void Add(Book book)
        {
            if (book == null)
            {
                throw new Exception("Can't add the book.");
            }
            var category = _categoryService.GetByName(book.Category.Name);
            var language = _languageService.GetByName(book.Language.Name);
            if (category == null || language == null||string.IsNullOrEmpty(book.Name)||book.Name.Count()>50)
            {
                throw new Exception($"Can't add a book to the library.");
            }
            book.Id = CreateId();
            _bookData.Data.Add(book);
            _bookData.UpdateFile();           
        }
        public override void Delete(int id)
        {
            var book = GetById(id);
            if (book == null)
            {
                throw new Exception($"Can not delete the book. The book Id {id} is not in the library.");
            }
            else if (!book.IsAvailable)
            {
                throw new Exception($"Can not delete the book. The book Id {id} is borrowed.");
            }
            else
            {
                _bookData.Data.Remove(book);
                _bookData.UpdateFile();
            }
        }
        public override void Update(Book book)
        {
            if (book == null)
            {
                throw new Exception("Can't update.");
            }
            var category = _categoryService.GetByName(book.Category.Name);
            var language = _languageService.GetByName(book.Language.Name);
            var bookToUpdate = GetById(book.Id);
            if (category == null || language == null || book.Id == 0||string.IsNullOrEmpty(book.Name)||book.Name.Count()>50
                ||book.Author.Count()>50)
            {
                throw new Exception("Can't update.");
            }
            bookToUpdate.Id = book.Id;
            bookToUpdate.Name = book.Name;
            bookToUpdate.Author = book.Author;
            bookToUpdate.PublicationDate = book.PublicationDate;
            bookToUpdate.Category = book.Category;
            bookToUpdate.Language = book.Language;
            bookToUpdate.IsAvailable = book.IsAvailable;
            _bookData.UpdateFile();            
        }    
        public bool TakeBook(int bookId, string userName)
        {
            var user = _userService.GetByName(userName);
            var book = GetById(bookId);
            if (user == null)
            {
                throw new Exception($"The user {userName} does not exist.");
                return false;
            }
            if (book == null)
            {
                throw new Exception($"Can not find the book in the library");
                return false;
            }
            if (book != null && user != null)
            {
                if (user.Books.Count >= 5)
                {
                    throw new Exception($"It is not allowd to borrow more than 5 books.");
                    return false;
                }
                else if (!book.IsAvailable)
                {
                    throw new Exception($"The book {bookId} is not available.");
                    return false;
                }
                book.IsAvailable = false;
                book.DateOfBorrowing = DateTime.Now;
                user.Books.Add(new BookBasic(book));
                _bookData.UpdateFile();
                _userService.UpdateUsersFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ReturnBook(int bookId, int userId)
        {
            var user = _userService.GetById(userId);
            var book = GetById(bookId);

            if (user == null)
            {
                throw new Exception($"The user {userId} does not exist.");
                return false;
            }
            if (book == null)
            {
                throw new Exception($"Can not find the book in the library");
                return false;
            }
            var bookInUsersBookList = user.Books.Where(b => b.Id == bookId).FirstOrDefault();
            if (bookInUsersBookList == null)
            {
                throw new Exception("Can't return the book");
                return false;
            }
            if (user.Books.Remove(bookInUsersBookList))
            {
                book.IsAvailable = true;
                _bookData.UpdateFile();
                _userService.UpdateUsersFile();
                return true;
            }
            return false;
        }
        public List<Book> GetBooksByAuthor(string author)
        {
            var length = author.Count();
            var books = _bookData.Data;
            return books
                .Where(b => b.Author.Count() >= length)
                .Where(b => b.Author.ToLower().Substring(0, length) == author.ToLower()).Select(b => b).ToList();
        }
        public List<Book> GetBooksByName(string name)
        {
            var length = name.Count();
            var books = _bookData.Data;
            return books
                .Where(b => b.Name.Count() >= length)
                .Where(b => b.Name.ToLower().Substring(0, length) == name.ToLower()).Select(b => b).ToList();
        }
        public List<Book> GetBooksByCategory(string category)
        {
            var books = _bookData.Data;
            return books.Where(b => b.Category.Name.ToLower() == category.ToLower()).OrderBy(b => b.Name).Select(b => b).ToList();
        }
        public List<Book> GetBooksByLanguage(string language)
        {
            var books = _bookData.Data;
            return books.Where(b => b.Language.Name.ToLower() == language.ToLower()).OrderBy(b => b.Name).Select(b => b).ToList();
        }
        public List<Book> GetTakenBooks()
        {
            var books = _bookData.Data;
            return books.Where(b => b.IsAvailable == false).OrderBy(b => b.Name).Select(b => b).ToList();
        }
        public List<Book> GetAvailableBooks()
        {
            var books = _bookData.Data;
            return books.Where(b => b.IsAvailable == true).OrderBy(b => b.Name).Select(b => b).ToList();
        }
    }
}
