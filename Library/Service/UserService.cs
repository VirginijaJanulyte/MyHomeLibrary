using Library.Data;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class UserService : BasicService<User>
    {
        private IAccessData<User> _userData;
        public UserService(IAccessData<User> data) : base(data)
        {
            _userData = data;
        }
        public void UpdateUsersFile()
        {
            _userData.UpdateFile();

        }
        public override void Delete(int id)
        {
            var userToDelete = _userData.Data.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
            if (userToDelete == null)
            {
                throw new Exception("Can't find the user.");
            }
            if (userToDelete.Books.Count() != 0)
            {
                throw new Exception($"Can't delete user {userToDelete.Name}. User had borrowed books.");
            }
            _userData.Data.Remove(userToDelete);
            _userData.UpdateFile();
        }
    }
}
