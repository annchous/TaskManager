﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaskManager
{
    class Commander
    {
        public List<Task> tasks;
        private int identificator = 1;

        public Commander()
        {
            tasks = new List<Task>();
        }

        public void Add(string task)
        {
            Task newTask = new Task(identificator, task);
            tasks.Add(newTask);
            identificator += 1;
        }

        public void All()
        {
            var sortedTasks = from task in tasks
                              orderby task.isComplited()
                              select task;

            Console.WriteLine("Task ID\t\tTask info\n");
            foreach (var task in sortedTasks)
            {
                Console.WriteLine($"{task.getId()}\t\t{task.getTask()}");
            }
            Console.WriteLine();
        }

        public void Delete(int id)
        {
            var toDeleteTask = tasks.Where(x => x.getId() == id).Select(x => x).First();
            tasks.Remove(toDeleteTask);
        }

        public void Save(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path += @$"\{fileName}";
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                foreach (var task in tasks)
                {
                    outputFile.Write(task.getTask());
                }
                
            }
        }

        public void Load(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path += @$"\{fileName}";
            using (StreamReader inputFile = new StreamReader(path))
            {
                while (!inputFile.EndOfStream)
                {
                    this.Add(inputFile.ReadLine());
                }
            }
        }

        public void Complete(int id)
        {
            var toCompleteTask = tasks.Where(x => x.getId() == id).Select(x => x).First();
            toCompleteTask.setCompleted();
        }

        public void Completed()
        {
            Console.WriteLine("\tCompleted tasks\n");
            foreach (var task in tasks)
            {
                if (task.isComplited())
                    Console.WriteLine($"{task.getTask()}");
            }
            Console.WriteLine();
        }
    }
}