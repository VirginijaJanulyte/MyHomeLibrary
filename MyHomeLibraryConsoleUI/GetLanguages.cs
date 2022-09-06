using Library.Models;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeLibraryConsoleUI
{
    public static class GetLanguages
    {
        public static void Get(BasicService<Language> languages)
        {
            var l = languages.Get();
            if (l != null)
            {
                Console.WriteLine("Languages: ");
                foreach (var item in l)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(item.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
