using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaskManager
{
    class Commander
    {
        public List<Task> tasks;
        public List<Group> groups;
        private int identificator = 1;
        Messages messages;

        public Commander()
        {
            tasks = new List<Task>();
            groups = new List<Group>();
            messages = new Messages();
        }

        public void Add(string task)
        {
            try
            {
                Task newTask = new Task('T' + identificator.ToString(), task);
                tasks.Add(newTask);
                identificator += 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(messages.WrongAdding);
                Console.WriteLine(e.Message);
            }
        }

        public void All()
        {
            var sortedTasks = from task in tasks
                              orderby task.Status
                              select task;

            Console.WriteLine("{0, -20} {1, -20} {2, -40} {3, 10} {4, 20}\n", "Task ID", "Type", "Description", "Deadline", "Status");
            foreach (var task in sortedTasks)
            {
                if (task.subtasks.Count == 0)
                    Console.WriteLine(
                        "{0, -20} {1, -20} {2, -40} {3, 10} {4, 20}",
                        task.Id, 
                        task.GetType().Name, 
                        task.Description,
                        task.Deadline.ToShortDateString(), 
                        task.Status ? "Completed" : "In progress"
                        );
                else
                {
                    Console.WriteLine(
                        "{0, -20} {1, -20} {2, -40} {3, 10} {4, 20}",
                        task.Id, 
                        task.GetType().Name + " " 
                        + Convert.ToString(task.CompletedSubtasksCount()) 
                        + "/" + Convert.ToString(task.AllSubtasksCount()), 
                        task.Description,
                        task.Deadline.ToShortDateString(), 
                        task.Status ? "Completed" : "In progress"
                        );

                    foreach (var subtask in task.subtasks)
                    {
                        Console.WriteLine(
                            "{0, -20} {1, -20} {2, -40} {3, 10} {4, 20}",
                            subtask.Id, 
                            subtask.GetType().Name, 
                            subtask.Description,
                            subtask.Deadline.ToShortDateString(),
                            subtask.Status ? "Completed" : "In progress"
                            );
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
                if (outputFile.Equals(null))
                    throw new FileNotFoundException("File was not found!\n");

                foreach (var task in tasks)
                {
                    try
                    {
                        outputFile.Write(task.Id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(messages.SavingError);
                        Console.WriteLine($"Error message: {e.Message}");
                    }
                }
                
            }
        }

        public void Load(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path += @$"\{fileName}";
            using (StreamReader inputFile = new StreamReader(path))
            {
                if (inputFile.Equals(null))
                    throw new FileLoadException("File was not found!\n");

                while (!inputFile.EndOfStream)
                {
                    try
                    {
                        this.Add(inputFile.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(messages.WrongAdding);
                        Console.WriteLine($"Error message: {e.Message}");
                    }
                }
            }
        }

        public void Complete(string id)
        {
            var toCompleteTask = (from task in tasks
                                  where task.Id == id
                                  select task).First();

            if (!tasks.Contains(toCompleteTask))
                throw new NullReferenceException(messages.WrongAccess);

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
        }

        public void Completed()
        {
            Console.WriteLine("{0, 40}\n", "Completed tasks");
            Console.WriteLine("{0, -20} {1, -50}\n", "Task ID", "Description");
            foreach (var task in tasks)
            {
                if (task.subtasks.Count() == 0 && task.Status)
                    Console.WriteLine(
                        "{0, -20} {1, -50}", 
                        task.Id, 
                        task.Description
                        );
                else
                {
                    if (task.Status)
                        Console.WriteLine(
                            "{0, -20} {1, -50}", 
                            task.Id, 
                            task.Description
                            );

                    foreach (var subtask in task.subtasks)
                    {
                        if (subtask.Status)
                            Console.WriteLine(
                                "{0, -20} {1, -50}", 
                                subtask.Id, 
                                subtask.Description
                                );
                    }    
                }
            }
            Console.WriteLine();
        }

        public void SetDeadline(string id, string date)
        {
            try
            {
                var selectedTask = tasks.First(x => x.Id == id);
                selectedTask.Deadline = DateTime.Parse(date);
            }
            catch (Exception e)
            {
                Console.WriteLine(messages.WrongAdding);
                Console.WriteLine($"Error message: {e.Message}");
            }
        }

        public void Today()
        {
            Console.WriteLine("{0, 40}\n", "Today's deadline tasks");
            Console.WriteLine("{0, -20} {1, -50}\n", "Task ID", "Description");
            foreach (var task in tasks)
            {
                if (task.Deadline.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Console.WriteLine(
                        "{0, -20} {1, -50}", 
                        task.Id, 
                        task.Description
                        );
            }
            Console.WriteLine();
        }

        public void AddSubtask(string id, string subtask)
        {
            try
            {
                var selectedTask = tasks.First(x => x.Id == id);
                Subtask newSubtask = new Subtask('S' + identificator.ToString(), subtask, id);
                selectedTask.subtasks.Add(newSubtask);
                identificator += 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(messages.WrongAdding);
                Console.WriteLine($"Error message: {e.Message}");
            }
        }

        public void CompleteSubtask(string id)
        {
            var toCompleteSubtask = from task in tasks
                                    from subtask in task.subtasks
                                    where subtask.Id == id
                                    select subtask;

            if (toCompleteSubtask.Any())
                toCompleteSubtask.First().Status = true;
            else
                throw new NullReferenceException(messages.WrongAccess);
        }

        public void CreateGroup(string name)
        {
            try
            {
                groups.Add(new Group(name));
            }
            catch (Exception e)
            {
                Console.WriteLine(messages.WrongAdding);
                Console.WriteLine($"Error message: {e.Message}");
            }
        }

        public void DeleteGroup(string name)
        {
            if (groups.Contains(groups.First(x => x.Name == name)))
                groups.Remove(groups.First(x => x.Name == name));
            else
                throw new NullReferenceException(messages.WrongAccess);
        }

        public void AddToGroup(string id, string name)
        {
            try
            {
                groups.First(x => x.Name == name).groupTasks.Add(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(messages.WrongAdding);
                Console.WriteLine($"Error message: {e.Message}");
            }
        }

        public void DeleteFromGroup(string id, string name)
        {
            if (groups.First(x => x.Name == name).groupTasks.Contains(id))
                groups.First(x => x.Name == name).groupTasks.Remove(id);
            else
                throw new NullReferenceException(messages.WrongAccess);
        }

        public void CompletedInGroup(string name)
        {
            var groupTasks = (from g in groups
                             where g.Name == name
                             select g.groupTasks).First();

            Console.WriteLine("{0, 40}\n", $"Completed tasks in group {name}");
            Console.WriteLine("{0, -20} {1, -50}\n", "Task ID", "Description");

            foreach (var task in groupTasks)
            {
                var currentTask = tasks.First(x => x.Id == task);
                if (currentTask.subtasks.Count() == 0 && currentTask.Status)
                    Console.WriteLine(
                        "{0, -20} {1, -50}", 
                        currentTask.Id, 
                        currentTask.Description
                        );
                else
                {
                    if (currentTask.Status)
                        Console.WriteLine(
                            "{0, -20} {1, -50}", 
                            currentTask.Id, 
                            currentTask.Description
                            );

                    foreach (var subtask in currentTask.subtasks)
                    {
                        if (subtask.Status)
                            Console.WriteLine(
                                "{0, -20} {1, -50}", 
                                subtask.Id, 
                                subtask.Description
                                );
                    }
                }
            }

            Console.WriteLine();
        }
    }
}
