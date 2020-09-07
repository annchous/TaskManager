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
            // TO DO: to think about optimization
            line = Console.ReadLine().Split(" ");
            string args = String.Join(' ', line, 1, line.Length - 1);
            switch (line[0])
            {
                case "/add":
                    commander.Add(args);
                    break;
                case "/all":
                    commander.All();
                    break;
                case "/delete":
                    commander.Delete(args);
                    break;
                case "/save":
                    commander.Save(args);
                    break;
                case "/load":
                    commander.Load(args);
                    break;
                case "/complete":
                    if (args.StartsWith('S'))
                        commander.CompleteSubtask(args);
                    else
                        commander.Complete(args);
                    break;
                case "/completed":
                    commander.Completed();
                    break;
                case "/setdeadline":
                    var deadlineArgs = args.Split(" ");
                    commander.SetDeadline(deadlineArgs[0], deadlineArgs[1]);
                    break;
                case "/today":
                    commander.Today();
                    break;
                case "/add-subtask":
                    var subtaskArgs = args.Split(" ");
                    var subtask = String.Join(' ', subtaskArgs, 1, subtaskArgs.Length - 1);
                    commander.AddSubtask(subtaskArgs[0], subtask);
                    break;
                default:
                    Console.WriteLine($"Invalid command: {line[0].ToString()}\n");
                    break;
            }
        }
    }
}
