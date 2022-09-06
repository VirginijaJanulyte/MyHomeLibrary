using Autofac.Extras.Moq;
using Library;
using Library.Data;
using Library.Models;
using Library.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace LibraryTests
{
    public class BookServiceTests
    {

        [Theory]
        [InlineData("Category1", "English", 1)]
        [InlineData("Category", "Englishn", 0)]
        [InlineData("", "English", 0)]
        [InlineData("History", "", 0)]
        [InlineData("History", "Norwegian", 1)]
        public void AddBookTest_ReturnsVoid(string c, string l, int r )
        {
            using (var mock = AutoMock.GetLoose())
            {
                var category = new Category() { Id = 1, Name = c };
                var language = new Language() { Id = 1, Name = l };
                var book = new Book()
                {
                    Name = "Name",
                    Author="Author",
                    Category = category,
                    Language = language,
                    PublicationDate = new DateOnly(2012, 01, 01)
                };
                
                mock.Mock<IAccessData<Book>>()
                    .Setup(x => x.UpdateFile());
                mock.Mock<IAccessData<Book>>()
                    .Setup(x => x.Data)
                    .Returns(new List<Book>());
                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleCategory);
                mock.Mock<IAccessData<Language>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleLanguage);
                
                var cls = mock.Create<BookService>();
                if (r == 0)
                {
                    Assert.Throws<Exception>(() => cls.Add(book));
                }
                else
                {
                    cls.Add(book);
                }
                

                mock.Mock<IAccessData<Book>>().Verify(x => x.UpdateFile(), Times.Exactly(r));

            }
        }
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(5, 0)]
        public void DeleteTest_ReturnsVoid(int id, int r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<Book>>()
                   .Setup(x => x.UpdateFile());
                mock.Mock<IAccessData<Book>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleBooks());

                var cls = mock.Create<BookService>();

                if (r == 0)
                {
                    Assert.Throws<Exception>(() => cls.Delete(id));
                }
                else
                {
                    cls.Delete(id);
                }
                mock.Mock<IAccessData<Book>>().Verify(x => x.UpdateFile(), Times.Exactly(r));
            }
        }

        [Theory]
        [InlineData(1, "History", "English", 1)]
        public void UpdateBookTest_ReturnsVoid(int id, string c, string l, int r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var category = new Category() { Id = 1, Name = c };
                var language = new Language() { Id = 1, Name = l };
                var book = new Book()
                {
                    Id = id,
                    Name = "Name**",
                    Author = "Author**",
                    Category = category,
                    Language = language,
                    PublicationDate = new DateOnly(2012, 01, 01)
                };
                
                mock.Mock<IAccessData<Book>>()
                    .Setup(x => x.UpdateFile());
                mock.Mock<IAccessData<Book>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleBooks());
                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleCategory);
                mock.Mock<IAccessData<Language>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleLanguage);
                
                var cls = mock.Create<BookService>();

                cls.Update(book);

                mock.Mock<IAccessData<Book>>().Verify(x => x.UpdateFile(), Times.Exactly(r));
            }
        }
        [Theory]
        [InlineData(3, "User2", true)]
        [InlineData(1, "User5", false)]
        [InlineData(1, "User2", true)]
        [InlineData(1, "User1", false)]
        [InlineData(10, "User2", false)]
        public void TakeBookTest_ReturnsBool(int bookId, string userName, bool r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var book = new Book() { Id=bookId};
                var user=new User() { Name = userName };
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleUser());
                mock.Mock<IAccessData<Book>>()
                    .Setup(x => x.UpdateFile());
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.UpdateFile());
                mock.Mock<IAccessData<Book>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleBooks());
                
                var cls = mock.Create<BookService>();
               
                if (r == false)
                {
                    Assert.Throws<Exception>(() => cls.TakeBook(bookId, userName));
                }
                else
                {
                    bool actual = cls.TakeBook(bookId, userName);
                    Assert.Equal(r, actual);
                }               
            }
        }
        [Theory]
        [InlineData(6, 1, true)]
        [InlineData(1, 1, true)]
        [InlineData(11, 1, false)]
        [InlineData(1, 12, false)]
        [InlineData(15, 1, false)]
        public void ReturnBookTest_ReturnsBool(int bookId, int userId, bool r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var book = new Book() { Id = bookId };
                var user = new User() { Id = userId };
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleUser());
                mock.Mock<IAccessData<Book>>()
                    .Setup(x => x.UpdateFile());
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.UpdateFile());
                mock.Mock<IAccessData<Book>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleBooks());

                var cls = mock.Create<BookService>();
                if (r == false)
                {
                    Assert.Throws<Exception>(() => cls.ReturnBook(bookId, userId));
                }
                else
                {
                    bool actual = cls.ReturnBook(bookId, userId);
                    Assert.Equal(r, actual);
                }
            }
        }
        private List<Category> GetSampleCategory()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name="History"
                },
                new Category()
                {
                    Id = 2,
                    Name="Category1"
                }
            };
        }
        private List<Language> GetSampleLanguage()
        {
            return new List<Language>()
            {
                new Language()
                {
                    Id = 1,
                    Name="English"
                },
                new Language()
                {
                    Id = 2,
                    Name="Norwegian"
                }
            };
        }
        private List<Book> GetSampleBooks()
        {
            return new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Name = "Name",
                    Author="Author",
                    Category = GetSampleCategory()[0],
                    Language = GetSampleLanguage()[0],
                    PublicationDate = new DateOnly(2012, 01, 01),
                    IsAvailable=true,
                },
                new Book()
                {   Id = 2,
                    Name = "Name1",
                    Author="Author",
                    Category = GetSampleCategory()[0],
                    Language = GetSampleLanguage()[1],
                    PublicationDate = new DateOnly(2012, 01, 01),
                    IsAvailable = false
                },
                 new Book()
                {   Id = 3,
                    Name = "Name2",
                    Author="Author",
                    Category = GetSampleCategory()[0],
                    Language = GetSampleLanguage()[1],
                    PublicationDate = new DateOnly(2012, 01, 01),
                    IsAvailable = true
                },
                  new Book()
                {   Id = 6,
                    Name = "Name2",
                    Author="Author",
                    Category = GetSampleCategory()[0],
                    Language = GetSampleLanguage()[1],
                    PublicationDate = new DateOnly(2012, 01, 01),
                    IsAvailable = false
                }
            };
        }
        private List<User> GetSampleUser()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name="User1",
                    Books=new List<BookBasic>(){new BookBasic(){Id=1, Name="Name", Author="Author"}, new BookBasic() { Id = 6, Name = "Name", Author = "Author" }, new BookBasic() { Id = 9, Name = "Name", Author = "Author" }, new BookBasic() { Id = 7, Name = "Name", Author = "Author" }, new BookBasic() { Id = 8, Name = "Name", Author = "Author" }, }
                 },
                new User()
                {
                    Id = 2,
                    Name="User2",
                    Books=new List<BookBasic>()
                }
            };
        }
    }
}