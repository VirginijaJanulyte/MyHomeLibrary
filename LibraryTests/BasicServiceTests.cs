using Autofac.Core;
using Autofac.Extras.Moq;
using Library.Data;
using Library.Models;
using Library.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryTests
{
    public class BasicServiceTests
    {
        [Fact]        
        public void GetTest_ReturnsCategories()
        {
            
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleCategory());

                var cls = mock.Create<BasicService<Category>>();
                
                var expected = GetSampleCategory();
                var actual=cls.Get();

                Assert.Equal(expected[1].Name, actual[0].Name);
                Assert.Equal(expected.Count, actual.Count);
            }
        }
        public void GetTest_ReturnsLanguages()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<Language>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleLanguage());

                var cls = mock.Create<BasicService<Language>>();

                var expected = GetSampleLanguage();
                var actual = cls.Get();

                Assert.Equal(expected[0].Name, actual[0].Name);
                Assert.Equal(expected.Count, actual.Count);
            }
        }

        [Fact]
        public void GetByIdTest_ReturnsCategory()
        {
            using(var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleCategory());

                var cls = mock.Create<BasicService<Category>>();

                int id1 = 2;
                var expected = GetSampleCategory()[1];
                int id2= 0;

                Assert.Equal(expected.Name, cls.GetById(id1).Name);
                Assert.Equal(null, cls.GetById(id2));
            }
        }
        [Fact]
        public void GetByIdTest_ReturnsLanguage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<Language>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleLanguage());

                var cls = mock.Create<BasicService<Language>>();

                int id1 = 2;
                var expected = GetSampleLanguage()[1];
                int id2 = 0;

                Assert.Equal(expected.Name, cls.GetById(id1).Name);
                Assert.Equal(null, cls.GetById(id2));
            }
        }
        [Fact]
        public void GetByIdTest_ReturnsUser()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleUser());

                var cls = mock.Create<BasicService<User>>();

                int id1 = 2;
                var expected = GetSampleUser()[1];
                int id2 = 0;

                Assert.Equal(expected.Name, cls.GetById(id1).Name);
                Assert.Equal(null, cls.GetById(id2));
            }
        }
        [Fact]
        public void GetByNameTest_ReturnsCategory()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleCategory());

                var cls = mock.Create<BasicService<Category>>();

                string name1 = "History";
                var expected1 = GetSampleCategory()[0];
                string name2= "Cat";

                Assert.Equal(expected1.Name, cls.GetByName(name1).Name);
                Assert.Equal(null, cls.GetByName(name2));
            }
        }
        [Fact]
        public void GetByNameTest_ReturnsLanguage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<Language>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleLanguage());

                var cls = mock.Create<BasicService<Language>>();

                string name1 = "English";
                var expected1 = GetSampleLanguage()[0];
                string name2 = "Cat";

                Assert.Equal(expected1.Name, cls.GetByName(name1).Name);
                Assert.Equal(null, cls.GetByName(name2));
            }
        }
        [Fact]
        public void GetByNameTest_ReturnsUser()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleUser());

                var cls = mock.Create<BasicService<User>>();

                string name1 = "User1";
                var expected1 = GetSampleUser()[0];
                string name2 = "Cat";

                Assert.Equal(expected1.Name, cls.GetByName(name1).Name);
                Assert.Equal(null, cls.GetByName(name2));
            }
        }
        [Theory]
        [InlineData("Category3", 1)]
        [InlineData("History", 0)]
        [InlineData("", 0)]
        public void AddTest_CategoryReturnsVoid(string cat, int r )
        {
            using (var mock = AutoMock.GetLoose())
            {
                var category = new Category() { Name=cat};

                mock.Mock<IAccessData<Category>>()
                  .Setup(x => x.UpdateFile());
                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleCategory());               
                var cls = mock.Create<BasicService<Category>>();
                if (r == 0)
                {
                    Assert.Throws<Exception>(() => cls.Add(category));
                }
                else
                {
                    cls.Add(category);
                }               
                mock.Mock<IAccessData<Category>>().Verify(x => x.UpdateFile(), Times.Exactly(r));                           
            }
        }
        [Theory]
        [InlineData("User3", 1)]
        [InlineData("User1", 0)]
        [InlineData("", 0)]
        public void AddTest_UserReturnsVoid(string u, int r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var user = new User() { Name = u };
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.UpdateFile());

                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleUser());

                var cls = mock.Create<BasicService<User>>();
                if (r == 0)
                {
                    Assert.Throws<Exception>(() => cls.Add(user));
                }
                else
                {
                    cls.Add(user);
                }
                
                mock.Mock<IAccessData<User>>().Verify(x => x.UpdateFile(), Times.Exactly(r));
            }
        }
        [Theory]
        [InlineData(1, "category", 1)]
        [InlineData(1, "", 0)]
        [InlineData(15, "category", 0)]
        public void UpdateTest_CategoryReturnsVoid(int id, string cat, int r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var category = new Category() { Id = id, Name = cat };
                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.UpdateFile());

                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleCategory());

                var cls = mock.Create<BasicService<Category>>();
                if (r == 0)
                {
                    Assert.Throws<Exception>(() => cls.Update(new Category() { Id = id, Name = cat }));
                }
                else
                {
                    cls.Update(new Category() { Id = id, Name = cat });
                }
                mock.Mock<IAccessData<Category>>().Verify(x => x.UpdateFile(), Times.Exactly(r));
            }
        }
        [Theory]
        [InlineData(1, "user***", 1)]
        [InlineData(1, "", 0)]
        [InlineData(15, "user***", 0)]
        public void UpdateTest_UserReturnsVoid(int id, string u, int r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var user = new User() { Id = id, Name = u };
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.UpdateFile());

                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleUser());

                var cls = mock.Create<BasicService<User>>();
                if (r == 0)
                {
                    Assert.Throws<Exception>(() => cls.Update(new User() { Id = id, Name = u }));
                }
                else
                {
                    cls.Update(new User() { Id = id, Name = u });
                }
                

                mock.Mock<IAccessData<User>>().Verify(x => x.UpdateFile(), Times.Exactly(r));
            }
        }
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(15, 0)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        public void DeleteTest_CategoryReturnsVoid(int id, int r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var category = new Category() { Id = id };
                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.UpdateFile());

                mock.Mock<IAccessData<Category>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleCategory());

                var cls = mock.Create<BasicService<Category>>();
                if (r == 0)
                {
                    Assert.Throws<Exception>(() => cls.Delete(id));
                }
                else
                {
                    cls.Delete(id);
                }
                

                mock.Mock<IAccessData<Category>>().Verify(x => x.UpdateFile(), Times.Exactly(r));
            }
        }
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(15, 0)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        public void DeleteTest_LanguageReturnsVoid(int id, int r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var l = new Language() { Id = id };
                mock.Mock<IAccessData<Language>>()
                    .Setup(x => x.UpdateFile());

                mock.Mock<IAccessData<Language>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleLanguage());

                var cls = mock.Create<BasicService<Language>>();

                if (r == 0)
                {
                    Assert.Throws<Exception>(() => cls.Delete(id));
                }
                else
                {
                    cls.Delete(id);
                }

                mock.Mock<IAccessData<Language>>().Verify(x => x.UpdateFile(), Times.Exactly(r));
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
                    Name="Category2"
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
        private List<User> GetSampleUser()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name="User1"
                },
                new User()
                {
                    Id = 2,
                    Name="User2"
                }
            };
        }
    }
}
