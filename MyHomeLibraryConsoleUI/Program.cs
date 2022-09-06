using MyHomeLibraryConsoleUI;

string HeadlineCommands = "".PadRight(103, '-') + "\n" + "add".PadRight(15, ' ') + "update".PadRight(15, ' ') + "delete".PadRight(15, ' ') + "take".PadRight(15, ' ') + "returnbook".PadRight(15, ' ') + "list".PadRight(15, ' ') + "quit".PadRight(0, ' ') + "\n".PadRight(104, '-') + "\n" +
    "take".PadRight(15, ' ')+"getbyname".PadRight(15, ' ')+"getbyauthor".PadRight(15, ' ')+"getavailable".PadRight(15, ' ')+"gettaken".PadRight(15, ' ')+"getbylanguage".PadRight(15, ' ')+"getbycategory".PadRight(15, ' ') + "\n".PadRight(104, '-') + "\n";
Console.WriteLine("My Library");
Console.WriteLine("=".PadRight(103, '='));

bool end=true;
while (end)
{
    Console.WriteLine("Commands:");
    Console.WriteLine(HeadlineCommands);
    Console.Write("Please enter a command: ");
    string command = Console.ReadLine().ToLower(); 
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine(ParseCommand.Parse(command));
    Console.ForegroundColor = ConsoleColor.White;
    if (command == "quit")
    {
        end= false;
    }
}



