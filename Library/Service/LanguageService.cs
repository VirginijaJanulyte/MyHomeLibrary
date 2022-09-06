using Library.Data;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class LanguageService : BasicService<Language>
    {
        public LanguageService(IAccessData<Language> data) : base(data)
        {
        }
    }
}
