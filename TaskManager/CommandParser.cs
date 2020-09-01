using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class CommandParser
    {
        public Commander commander = new Commander();
        public string command;
        string[] line;

        public void ReadCommand()
        {
            this.line = Console.ReadLine().Split(" ");
        }

        public void GetCommand()
        {
            if (line[0].ToString() == "/add")
                command = "/add";
            else if (line[0].ToString() == "/all")
                command = "/all";
            else if (line[0].ToString() == "/delete")
                command = "/delete";
            else if (line[0].ToString() == "/save")
                command = "/save";
            else if (line[0].ToString() == "/load")
                command = "/load";
            else if (line[0].ToString() == "/complete")
                command = "/complete";
            else if (line[0].ToString() == "/completed")
                command = "/completed";
        }

        public void СommandProcessing()
        {
            string args = String.Join(' ', line, 1, line.Length - 1);
            switch (command)
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
            }
        }

        public void Run()
        {
            this.ReadCommand();
            this.GetCommand();
            this.СommandProcessing();
        }
    }
}
