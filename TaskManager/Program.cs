using System;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandParser commandParser = new CommandParser();
            while (true)
            {
                try
                {
                    commandParser.Run();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
