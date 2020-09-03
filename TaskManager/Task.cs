using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class Task
    {
        public int Id { get; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Deadline { get; set; }

        public Task(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
