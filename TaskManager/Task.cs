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
        public bool Status { get; set; }
        public DateTime Deadline { get; set; }
        public List<Subtask> subtasks { get; set; }

        public Task(string id, string description)
        {
            Id = id;
            Description = description;
            Deadline = DateTime.MaxValue;
            Status = false;
            subtasks = new List<Subtask>();
        }

        public int CompletedSubtasksCount()
        {
            // return subtasks.Select(x => x.Status == true).Count();
            return (from subtask in subtasks
                   where subtask.Status == true
                   select subtask).Count();
        }

        public int AllSubtasksCount()
        {
            return subtasks.Count;
        }

        public void CheckStatus()
        {
            if (AllSubtasksCount() == CompletedSubtasksCount())
                Status = true;
        }
    }
}
