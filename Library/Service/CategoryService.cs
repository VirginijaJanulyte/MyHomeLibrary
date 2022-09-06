using Library.Data;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class CategoryService : BasicService<Category>
    {
        public CategoryService(IAccessData<Category> data) : base(data)
        {
        }
    }
}
