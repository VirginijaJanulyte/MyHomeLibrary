using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book:BasicModel
    {
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public Category Category { get; set; }
        public Language Language { get; set; }
        public DateTime DateOfBorrowing { get; set; }
        public bool IsAvailable { get; set; } = true;
    }   
}
