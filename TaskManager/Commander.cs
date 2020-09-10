using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskManager.TaskManagerException;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskManager
{
    class Commander
    {
        public List<Task> tasks;
        public List<Group> groups;
        private int identificator;

        

        public Commander()
        {
            tasks = new List<Task>();
            groups = new List<Group>();
            identificator = 1;
        }

        public void Add(string task)
        {
            if (tasks.Contains(tasks.FirstOrDefault(x => x.Description == task)))
                throw new TaskAlreadyExistsException(OutputText.TaskAlreadyExists);
            else
            {
                Task newTask = new Task('T' + identificator.ToString(), task);
                tasks.Add(newTask);
                identificator += 1;
            }
        }

        public void AddSubtask(string id, string subtask)
        {
            if (!tasks.Contains(tasks.FirstOrDefault(x => x.Id == id)))
                throw new TaskNotFoundException(OutputText.TaskNotFound);

            var selectedTask = tasks.First(x => x.Id == id);
            Subtask newSubtask = new Subtask('S' + identificator.ToString(), subtask);

            if (selectedTask.subtasks.Contains(newSubtask))
                throw new TaskAlreadyExistsException(OutputText.SubtaskAlreadyExists);
            else
            {
                selectedTask.subtasks.Add(newSubtask);
                identificator += 1;
            }
        }

        public void Delete(string id)
        {
            if (tasks.Contains(tasks.First(x => x.Id == id)))
                tasks.Remove(tasks.First(x => x.Id == id));
            else
                throw new TaskNotFoundException(OutputText.TaskNotFound);
        }

        public void Complete(string id)
        {
            if (tasks.Contains(tasks.First(x => x.Id == id)))
                tasks.First(x => x.Id == id).IsCompleted = true;
            else
                throw new TaskNotFoundException(OutputText.TaskNotFound);
        }

        public void CompleteSubtask(string id)
        {
            if (!tasks.Select(x => x.ContainsSubtask(id)).First())
                throw new TaskNotFoundException(OutputText.SubtaskNotFound);

            var toCompleteSubtask = tasks.Select(x => x.subtasks.Where(x => x.Id == id).First()).First();
            toCompleteSubtask.IsCompleted = true;
        }

        public void CreateGroup(string name)
        {
            if (groups.Contains(groups.FirstOrDefault(x => x.Name == name)))
                throw new GroupAlreadyExistsException(OutputText.GroupAlreadyExists);
            groups.Add(new Group(name));
        }

        public void DeleteGroup(string name)
        {
            if (groups.Contains(groups.First(x => x.Name == name)))
                groups.Remove(groups.First(x => x.Name == name));
            else
                throw new GroupNotFoundException(OutputText.GroupNotFound);
        }

        public void AddToGroup(string id, string name)
        {
            if (!groups.Contains(groups.FirstOrDefault(x => x.Name == name)))
                throw new GroupNotFoundException(OutputText.GroupNotFound);

            if (groups.First(x => x.Name == name).groupTasks.Contains(id))
                throw new TaskAlreadyExistsException(OutputText.TaskAlreadyExists);

            groups.First(x => x.Name == name).groupTasks.Add(id);
        }

        public void DeleteFromGroup(string id, string name)
        {
            if (!groups.Contains(groups.First(x => x.Name == name)))
                throw new GroupNotFoundException(OutputText.GroupNotFound);

            if (groups.First(x => x.Name == name).groupTasks.Contains(id))
                groups.First(x => x.Name == name).groupTasks.Remove(id);
            else
                throw new TaskNotFoundException(OutputText.TaskNotFound);
        }

        public void All()
        {
            OutputText.OutputHeader();

            groups.ForEach(x => { DisplayGroup(x); });

            foreach (var task in tasks.Select(x => x).Where(t => !HasGroup(t.Id)).OrderBy(t => t.IsCompleted))
            {
                if (task.NoSubtasks())
                    task.PrintTask();
                else
                {
                    task.PrintTask(task.Progress());
                    task.PrintSubtasks();
                }
            }
            Console.WriteLine();
        }

        public void Completed()
        {
            OutputText.OutputHeader();
            foreach (var task in tasks)
            {
                if (task.IsCompleted && task.NoSubtasks())
                    task.PrintTask();
                else if (!task.NoSubtasks())
                {
                    if (task.IsCompleted)
                        task.PrintTask(task.Progress());
                    task.PrintSubtasks(task.subtasks.Select(x => x).Where(x => x.IsCompleted).ToList());
                }
            }
            Console.WriteLine();
        }

        public void CompletedInGroup(string name)
        {
            if (!groups.Contains(groups.First(x => x.Name == name)))
                throw new GroupNotFoundException(OutputText.GroupNotFound);

            Console.WriteLine("{0, 40}\n", $"Completed tasks in group {name}");
            OutputText.OutputHeader();

            foreach (var task in groups.First(x => x.Name == name).groupTasks)
            {
                var currentTask = tasks.First(x => x.Id == task);
                if (currentTask.IsCompleted && currentTask.NoSubtasks())
                    currentTask.PrintTask();
                else if (!currentTask.NoSubtasks())
                {
                    if (currentTask.IsCompleted)
                        currentTask.PrintTask(currentTask.Progress());
                    currentTask.PrintSubtasks(currentTask.subtasks
                        .Select(x => x).Where(x => x.IsCompleted).ToList());
                }
            }
            Console.WriteLine();
        }

        public bool HasGroup(string id) => groups.Where(x => x.ContainsTask(id)).Any();

        public void DisplayGroup(Group g)
        {
            g.PrintGroupHeader();

            foreach (string id in g.groupTasks)
            {
                var task = tasks.First(x => x.Id == id);

                if (task.subtasks.Count == 0)
                    task.PrintTask();
                else
                {
                    task.PrintTask(task.Progress());
                    task.PrintSubtasks();
                }
            }
        }

        public void Today()
        {
            Console.WriteLine("{0, 40}\n", "Today's deadline tasks");
            OutputText.OutputHeader();
            tasks.ForEach(x => { if (x.Deadline.Date == DateTime.Now.Date) { x.PrintTask(); } });
            Console.WriteLine();
        }

        public void Save(string fileName)
        {
            string path = Environment.GetFolderPath
                (Environment.SpecialFolder.MyDocuments) 
                + @$"\{fileName}";

            if (!File.Exists(path))
                throw new FileNotFoundException(OutputText.FileNotFound);

            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(fileName, json);
        }

        public void Load(string fileName)
        {
            string path = Environment.GetFolderPath
                (Environment.SpecialFolder.MyDocuments)
                + @$"\{fileName}";

            if (!File.Exists(path))
                throw new FileNotFoundException(OutputText.FileNotFound);

            var f = File.ReadAllText(path);
            tasks = JsonSerializer.Deserialize<List<Task>>(f);
        }

        public void SetDeadline(string id, string date)
        {
            if (tasks.Contains(tasks.First(x => x.Id == id)))
            {
                var selectedTask = tasks.First(x => x.Id == id).Deadline;
                if (DateTime.TryParse(date, out selectedTask)) { }
                else
                    throw new DeadlineNotSetException(OutputText.DeadlineNotSet);
            }
            else
                throw new TaskNotFoundException(OutputText.TaskNotFound);
        }
    }
}
