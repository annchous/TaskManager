using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class Group
    {
        public string Name { get; set; }
        public List<string> groupTasks;

        public Group(string name)
        {
            Name = name;
            groupTasks = new List<string>();
        }

        public bool ContainsTask(string id) => groupTasks.Contains(id);
        public bool ContainsAnyTask() => groupTasks.Count != 0;
    }
}
