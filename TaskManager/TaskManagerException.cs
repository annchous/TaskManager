using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    namespace TaskManagerException
    {
        public class TaskNotFoundException : Exception
        {
            public TaskNotFoundException(string message)
                : base(message)
            { }
        }

        public class TaskAlreadyExistsException : Exception
        {
            public TaskAlreadyExistsException(string message)
                : base(message)
            { }
        }

        public class DeadlineNotSetException : Exception
        {
            public DeadlineNotSetException(string message)
                : base(message)
            { }
        }

        public class GroupAlreadyExistsException : Exception
        {
            public GroupAlreadyExistsException(string message)
                : base(message)
            { }
        }

        public class GroupNotFoundException : Exception
        {
            public GroupNotFoundException(string message)
                : base(message)
            { }
        }
    }
}
