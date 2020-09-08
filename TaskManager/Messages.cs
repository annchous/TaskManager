using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class Messages
    {
        public string WrongAccess { get; } = "You are trying to access to a non-existing element!\n";
        public string WrongAdding { get; } = "An error occurred while adding an item!\n";
        public string AccessingError { get; } = "Some error occurred while accessing the element!\n";
        public string SavingError { get; } = "An error occurred while saving an item!\n";
    }
}
