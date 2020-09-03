using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class CommandParser
    {
        public Commander commander = new Commander();
        string[] line;

        public void Run()
        {
            line = Console.ReadLine().Split(" ");
            string args = String.Join(' ', line, 1, line.Length - 1);
            switch (line[0].ToString())
            {
                case "/add":
                    commander.Add(args);
                    break;
                case "/all":
                    commander.All();
                    break;
                case "/delete":
                    commander.Delete(Convert.ToInt32(args));
                    break;
                case "/save":
                    commander.Save(args);
                    break;
                case "/load":
                    commander.Load(args);
                    break;
                case "/complete":
                    commander.Complete(Convert.ToInt32(args));
                    break;
                case "/completed":
                    commander.Completed();
                    break;
                case "/setdeadline":
                    var splittedArgs = args.Split(" ");
                    commander.SetDeadline(Convert.ToInt32(splittedArgs[0]), splittedArgs[1]);
                    break;
                case "/today":
                    commander.Today();
                    break;
                default:
                    Console.WriteLine($"Invalid command: {line[0].ToString()}\n");
                    break;
            }
        }
    }
}
