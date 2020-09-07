using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class Subtask : Task
    {
        public string TaskId { get; }
        public Subtask(string id, string description, string taskId)
                : base(id, description)
        {
            TaskId = taskId;
        }
    }
}
