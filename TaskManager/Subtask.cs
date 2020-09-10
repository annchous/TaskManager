using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class Subtask : Task
    {
        public Subtask(string id, string description)
                : base(id, description)
        { }
    }
}
