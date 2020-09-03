# TaskManager
### Task description
Implement an application that handles commands:
#### Newcomer Level
- [X] **/add** *task-info* - creates new task
- [X] **/all** - displays all tasks
- [X] **/delete** *id* - deletes the task by id (which should appear in /all)
- [X] **/save** *file-name.txt* - saves all current tasks to the specified file
- [X] **/load** *file-name.txt* - loads tasks from file
- [X] **/complete** *id* - indicates that the task is completed. *Completed tasks are displayed at the end of the list and are marked as completed.*
- [X] **/completed** - displays all completed tasks
#### Sectarian Level
*The ability to specify the due date:*
- [ ] Info about **deadlines** in /all
- [ ] **/today** - displays tasks with the deadline for the current day

*Grouping tasks:*
- [ ] **/create-group** *group-name* - creates a group for tasks
- [ ] **/delete-group** *group-name* - removes the group with the specified name
- [ ] **/add-to-group** *id group-name* - adds a task with the specified id to the specified group
- [ ] **/delete-from-group** *id group-name* - removes a task with the specified id from the specified group
- [ ] Tasks that are in the group should be displayed in a **nested list** when /all is executed
- [ ] **/completed** *group-name* - displays all completed tasks in the group
