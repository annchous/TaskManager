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
    }
}
