using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class Task
    {
        private int id;
        private string task;
        private bool completed;

        public Task(int id, string task)
        {
            this.id = id;
            this.task = task;
        }

        public int getId()
        {
            return id;
        }

        public string getTask()
        {
            return task;
        }

        public bool isComplited()
        {
            return completed;
        }

        public void setCompleted()
        {
            this.completed = true;
        }
    }
}
