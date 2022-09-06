using Library.Models;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeLibraryConsoleUI
{
    public static class GetCategories
    {
        public static void  Get(BasicService<Category> categories)
        {
            var c = categories.Get();
            if (c != null)
            {
                Console.WriteLine("Categories: ");
                foreach (var item in c)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(item.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
