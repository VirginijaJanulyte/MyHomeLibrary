using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeLibraryConsoleUI
{
    public enum Command
    {
        add,
        update,
        delete,
        list,
        take,
        returnbook,
        getbyname,
        getbyauthor,
        getbycategory,
        getbylanguage,
        getavailable,
        gettaken,       
        quit
    }
}
