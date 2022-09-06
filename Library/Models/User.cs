using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class User:BasicModel
    {
        public List<BookBasic> Books { get; set; } = new List<BookBasic>();        
    }
}
