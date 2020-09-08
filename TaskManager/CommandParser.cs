using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class CommandParser
    {
        public Commander commander;
        private string[] line;
        private bool run;

        public CommandParser()
        {
            commander = new Commander();
            run = true;
        }

        public void CommandProcessing()
        {
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
                    if (args.Length == 0)
                        commander.Completed();
                    else
                        commander.CompletedInGroup(args);
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
                case "/create-group":
                    commander.CreateGroup(args);
                    break;
                case "/delete-group":
                    commander.DeleteGroup(args);
                    break;
                case "/add-to-group":
                    var groupArgs = args.Split(" ");
                    var groupName = String.Join(' ', groupArgs, 1, groupArgs.Length - 1);
                    commander.AddToGroup(groupArgs[0], groupName);
                    break;
                case "/delete-from-group":
                    var delGroupArgs = args.Split(" ");
                    var delGroupName = String.Join(' ', delGroupArgs, 1, delGroupArgs.Length - 1);
                    commander.DeleteFromGroup(delGroupArgs[0], delGroupName);
                    break;
                case "/exit":
                    run = false;
                    break;
                default:
                    Console.WriteLine($"Invalid command: {line[0].ToString()}\n");
                    break;
            }
        }

        public void Run()
        {
            while (run)
            {
                try
                {
                    CommandProcessing();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
