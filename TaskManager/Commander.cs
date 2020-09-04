using System;
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
                              orderby task.Status
                              select task;

            Console.WriteLine("{0, -20} {1, -50} {2, 30}\n", "Task ID", "Description", "Deadline");
            foreach (var task in sortedTasks)
            {
                Console.WriteLine("{0, -20} {1, -50} {2, 30}", task.Id, task.Description, task.Deadline.ToShortDateString());
            }
            Console.WriteLine();
        }

        public void Delete(int id)
        {
            try
            {
                var toDeleteTask = tasks.First(x => x.Id == id);
                tasks.Remove(toDeleteTask);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Save(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path += @$"\{fileName}";
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                foreach (var task in tasks)
                {
                    outputFile.Write(task.Id);
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
            var toCompleteTask = tasks.First(x => x.Id == id);
            toCompleteTask.Status = true;
        }

        public void Completed()
        {
            Console.WriteLine("{0, 40}\n", "Completed tasks");
            Console.WriteLine("{0, -20} {1, -50}\n", "Task ID", "Description");
            foreach (var task in tasks)
            {
                if (task.Status)
                    Console.WriteLine("{0, -20} {1, -50}", task.Id, task.Description);
            }
            Console.WriteLine();
        }

        public void SetDeadline(int id, string date)
        {
            var selectedTask = tasks.First(x => x.Id == id);
            selectedTask.Deadline = DateTime.Parse(date);
        }

        public void Today()
        {
            Console.WriteLine("{0, 40}\n", "Today's deadline tasks");
            Console.WriteLine("{0, -20} {1, -50}\n", "Task ID", "Description");
            foreach (var task in tasks)
            {
                if (task.Deadline.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Console.WriteLine("{0, -20} {1, -50}", task.Id, task.Description);
            }
            Console.WriteLine();
        }
    }
}
