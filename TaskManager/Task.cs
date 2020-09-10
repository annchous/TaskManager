using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager
{
    class Task
    {
        public string Id { get; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Deadline { get; set; }
        public List<Subtask> subtasks { get; set; }

        public Task(string id, string description)
        {
            Id = id;
            Description = description;
            IsCompleted = false;
            Deadline = DateTime.MaxValue;
            subtasks = new List<Subtask>();
        }

        public int CompletedSubtasksCount() => subtasks.Select(x => x).Where(t => t.IsCompleted == true).Count();
        public int AllSubtasksCount() => subtasks.Count;
        public string Progress() => CompletedSubtasksCount().ToString() + "/" + AllSubtasksCount().ToString();
        public void PrintTask(string progress = "") => Console.WriteLine(
            OutputText.OutputFormat,
            Id,
            GetType().Name + progress,
            Description,
            Deadline.ToShortDateString(),
            IsCompleted ? "Completed" : "In progress"
            );

        public void PrintSubtasks() => subtasks.ForEach(x => { PrintTask(); });
        public void PrintSubtasks(List<Subtask> _subtasks) => _subtasks.ForEach(x => { PrintTask(); });
        public bool NoSubtasks() => subtasks.Count == 0;
    }
}
