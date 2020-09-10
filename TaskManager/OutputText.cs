using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    static class OutputText
    {
        public static string TaskNotFound = "Task with specified id wasn't found!";
        public static string TaskAlreadyExists = "This task already exists!";

        public static string SubtaskNotFound = "Subtask with specified id wasn't found!";
        public static string SubtaskAlreadyExists = "This subtask already exists!";

        public static string GroupNotFound = "Group with this name wasn't found!";
        public static string GroupAlreadyExists = "A group with the same name already exists!";

        public static string DeadlineNotSet = "Deadline wasn't set!";

        public static string FileNotFound = "File was not found!";

        public static string SaveError = "Some error while saving occurred!";

        public static string OutputFormat = "{0, -20} {1, -20} {2, -40} {3, 10} {4, 20}";
        public static string OutputHeaderFormat = "{0, -20} {1, -20} {2, -40} {3, 10} {4, 20}\n";

        public static void OutputHeader() =>
            Console.WriteLine(OutputHeaderFormat, "Task ID", "Type", "Description", "Deadline", "Status");
    }
}
