using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookBasic
    {
        public BookBasic() { }
        public BookBasic(Book book)
        {
            Id = book.Id;
            Name = book.Name;
            Author = book.Author;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }

}
