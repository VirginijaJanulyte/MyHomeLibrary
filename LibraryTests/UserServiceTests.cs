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
    public class UserServiceTests
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(15, 0)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        public void DeleteTest_ReturnsVoid(int id, int r)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var user = new User() { Id = id };
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.UpdateFile());

                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.Data)
                    .Returns(GetSampleUser());

                var cls = mock.Create<UserService>();
                if (r == 0)
                {
                    Assert.Throws<Exception>(() => cls.Delete(id));
                }
                else
                {
                    cls.Delete(id);
                }
               

                mock.Mock<IAccessData<User>>().Verify(x => x.UpdateFile(), Times.Exactly(r));
            }
        }
        [Fact]
        public void UpdateUsersFile_ReturnsVoid()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IAccessData<User>>()
                    .Setup(x => x.UpdateFile());

                var cls = mock.Create<UserService>();

                cls.UpdateUsersFile();
                mock.Mock<IAccessData<User>>()
                    .Verify(x => x.UpdateFile(),Times.Exactly(1));
            }
        }
        private List<User> GetSampleUser()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name="User1",
                    Books=new List<BookBasic>(){new BookBasic(){Id=1, Name="Name", Author="Author"} }
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
