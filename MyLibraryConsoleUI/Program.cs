using Library;
using Library.Data;
using Library.Models;
using Library.Service;
using MyLibraryConsoleUI;

Console.WriteLine("My Library");
Console.WriteLine("===========================");

IAccessData<Category> categories = new AccessData<Category>("categories");
IAccessData<Language> languages = new AccessData<Language>("languages");
IAccessData<User> users = new AccessData<User>("users");
IAccessData<Book> books = new AccessData<Book>("books");
BasicService<Category> categoryService = new BasicService<Category>(categories);
BasicService<Language> languageService = new BasicService<Language>(languages);
UserService userService = new UserService(users);
BookService bookService = new BookService(books, categoryService, languageService,userService );


//Console.Write("Add a new category: ");
//string category = Console.ReadLine();
//categoryService.Add(new Category { Name = category });

//Console.Write("Delete the category Id: ");
//string id = Console.ReadLine();
//categoryService.Delete(int.Parse(id));

Console.WriteLine("Enter Id if you will to update the category: ");
var categoriesList = categoryService.Get();
foreach(var c in categoriesList)
{
    Console.Write($"Id: {c.Id} Name: {c.Name} ");
    var categoryToUpdate = Console.ReadLine();
    if(int.TryParse(categoryToUpdate, out int id))
    {
        if (id == c.Id)
        {
            Console.Write($"Enter a new name of category {c.Name}: ");
            var name = Console.ReadLine();
            categoryService.Update(new Category { Id = c.Id, Name = name });
        }
    }
}
Console.WriteLine("The updated list of categories:");
Console.WriteLine("-------------------------------");
categoriesList = categoryService.Get();
foreach (var c in categoriesList)
{
    Console.WriteLine($"Id: {c.Id} Name: {c.Name} ");
}
Console.Write("Get a category by Id: ");
var idString=Console.ReadLine();
if(int.TryParse(idString, out int i))
{
    var c=categoryService.GetById(i);
    Console.WriteLine($"Id: {c.Id} Name: {c.Name} ");
}

//categoryService.Add(new Category() { Name="History"});
//categoryService.Add(new Category() { Name= "Biography" });
//languageService.Add(new Language() { Name= "English"});
//userService.Add(new User() {Name= "User1" });
//bookService.Add(
//    new Book()
//    {       
//        Name = "English for Everyone",
//        Author = "****",
//        Category = "Study",
//        Language = "English",
//        PublicationDate = new DateTime(2012, 01, 01)
//    }
//    );


//bookService.Get().ForEach(book =>Console.WriteLine($"{book.Id} {book.Author} {book.Name}"));
//Console.WriteLine("===========================");
//bookService.Delete(4);
//Console.WriteLine("_________________________________");
//bookService.Get().ForEach(book => Console.WriteLine($"{book.Id} {book.Author} {book.Name}"));
//Console.WriteLine("_________________________________");
//bookService.Update(new BookViewModel()
//{
//    Id = 5,
//    Name = "English for Everyone",
//    Author = "Gill Johnson",
//    Category = "Study",
//    Language = "English",
//    PublicationDate = new DateTime(2001, 01, 01)
////});
//bookService.Get().ForEach(book => Console.WriteLine($"{book.Id} {book.Author} {book.Name}"));
//Console.WriteLine("_________________________________");
//bookService.TakeBook(6, "User");
//bookService.GetTakenBooks().ForEach(book => Console.WriteLine($"{book.Id} {book.Author} {book.Name}"));
//var u=userService.GetById(1);
//Console.WriteLine($"{u.Id} {u.Name} {u.Books.Count()}");
//var c=categoryService.GetByName("History");
//Console.WriteLine("_________________________________");
//Console.WriteLine($"{c.Name}");