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
            Task newTask = new Task('T' + identificator.ToString(), task);
            tasks.Add(newTask);
            identificator += 1;
        }

        public void All()
        {
            var sortedTasks = from task in tasks
                              orderby task.Status
                              select task;

            Console.WriteLine("{0, -20} {1, -20} {2, -50} {3, 20}\n", "Task ID", "Type", "Description", "Deadline");
            foreach (var task in sortedTasks)
            {
                if (task.subtasks.Count == 0)
                    Console.WriteLine("{0, -20} {1, -20} {2, -50} {3, 20}",
                        task.Id, task.GetType().Name, task.Description,
                        task.Deadline.ToShortDateString());
                else
                {
                    Console.WriteLine("{0, -20} {1, -20} {2, -50} {3, 20}",
                        task.Id, task.GetType().Name + " " + Convert.ToString(task.CompletedSubtasksCount()) 
                        + "/" + Convert.ToString(task.AllSubtasksCount()), task.Description,
                        task.Deadline.ToShortDateString());

                    foreach (var subtask in task.subtasks)
                    {
                        Console.WriteLine("{0, -20} {1, -20} {2, -50} {3, 20}",
                        subtask.Id, subtask.GetType().Name, subtask.Description,
                        subtask.Deadline.ToShortDateString());
                    }
                }
            }
            Console.WriteLine();
        }

        public void Delete(string id)
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

        public void Complete(string id)
        {
            var toCompleteTask = (from task in tasks
                                  where task.Id == id
                                  select task).First();
            if (toCompleteTask.subtasks.Count() == 0)
                toCompleteTask.Status = true;
            else
            {
                foreach (var subtask in toCompleteTask.subtasks)
                {
                    subtask.Status = true;
                }
                toCompleteTask.Status = true;
            }
            //var toCompleteTask = tasks.First(x => x.Id == id);
            //toCompleteTask.Status = true;
        }

        public void Completed()
        {
            Console.WriteLine("{0, 40}\n", "Completed tasks");
            Console.WriteLine("{0, -20} {1, -50}\n", "Task ID", "Description");
            foreach (var task in tasks)
            {
                if (task.subtasks.Count() == 0 && task.Status)
                    Console.WriteLine("{0, -20} {1, -50}", task.Id, task.Description);
                else
                {
                    if (task.Status)
                        Console.WriteLine("{0, -20} {1, -50}", task.Id, task.Description);
                    foreach (var subtask in task.subtasks)
                    {
                        if (subtask.Status)
                            Console.WriteLine("{0, -20} {1, -50}", subtask.Id, subtask.Description);
                    }    
                }
            }
            Console.WriteLine();
        }

        public void SetDeadline(string id, string date)
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

        public void AddSubtask(string id, string subtask)
        {
            var selectedTask = tasks.First(x => x.Id == id);
            Subtask newSubtask = new Subtask('S' + identificator.ToString(), subtask, id);
            selectedTask.subtasks.Add(newSubtask);
            identificator += 1;
        }

        public void CompleteSubtask(string id)
        {
            var toCompleteSubtask = from task in tasks
                                    from subtask in task.subtasks
                                    where subtask.Id == id
                                    select subtask;
            
            toCompleteSubtask.First().Status = true;
            tasks.First(x => x.Id == toCompleteSubtask.First().TaskId).CheckStatus();
        }
    }
}
