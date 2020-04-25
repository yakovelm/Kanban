using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC = IntroSE.Kanban.Backend.BusinessLayer.TaskControl;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardControl
{
    class Board
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<TC.Column> columns;
        private int ID = 1;
        private string email;
        public Board(string email)
        {
            this.email = email;
            columns = new List<TC.Column>();
            columns.Add(new TC.Column(email, "backlog"));
            columns.Add(new TC.Column(email, "in progress"));
            columns.Add(new TC.Column(email, "done"));

            LoadData();
            log.Debug("a board for " + email + " has been made.");
        }
        public Board() // default board constructor in case no user in connected
        {
            email = null;
            log.Debug("empty board created.");
        }

        public void LimitColumnTask(int ColumnOrdinal, int limit) // change the limit of a specific column
        {

            CheckColumnOrdinal(ColumnOrdinal);
            columns[ColumnOrdinal].setLimit(limit);
        }

        public string GetEmail() { return email; }


        public TC.Task AddTask(string title, string desciption, DateTime dueTime) // add a new task for this user
        {
            TC.Task newTack = new TC.Task(ID, title, desciption, dueTime, this.email);
            ID++;
            columns[0].addTask(newTack);
            return newTack;
        }
        public void UpdateTaskDueDate(int columnOrdinal, int taskID, DateTime Due) // update due date of this task
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            columns[columnOrdinal].editDue(taskID, Due);
            log.Debug("due date of task #" + taskID + " has been updated.");
        }
        public void UpdateTaskTitle(int columnOrdinal, int taskID, string title) // update title of this task
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            columns[columnOrdinal].editTitle(taskID, title);
            log.Debug("title of task #" + taskID + " has been updated.");
        }
        public void UpdateTaskDescription(int columnOrdinal, int taskID, string description) // update description of this task
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            columns[columnOrdinal].editDesc(taskID, description);
            log.Debug("description of task #" + taskID + " has been updated.");
        }
        public void AdvanceTask(int columnOrdinal, int taskId) // advance this task to the next column
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskId);
            TC.Task advTask = columns[columnOrdinal].getTask(taskId); // board handles a task because it is transfered between 2 columns and columns are not aware of each other
            if (advTask == null) // case when the task in not in the given column
            {
                log.Warn(email + "  tried to advance task #" + taskId + " that does not exist in " + columns[columnOrdinal].getName() + " column.");
                throw new Exception("task does not exist in this columm.");
            }
            columns[columnOrdinal + 1].addTask(advTask);
            columns[columnOrdinal].deleteTask(advTask);
            log.Debug("task #" + taskId + " advanced successfully.");
        }
        public TC.Column GetColumn(string columnName) // get column data of a specific column (by name)
        {

            CheckColumnName(columnName);
            return columns[ChengeToInt(columnName)];
        }
        public TC.Column GetColumn(int columnOrdinal) // get column data of a specific column (by ID)
        {
            CheckColumnOrdinal(columnOrdinal);
            return columns[columnOrdinal];
        }
        public List<TC.Column> getColumns() // gets all columns of current board holder
        {
            return columns;
        }

        private void CheckTaskID(int taskID) // check if given task id is legal
        {
            if (taskID > this.ID | taskID < 1)
            {
                log.Warn(email + " has entered an invalid task ID.");
                throw new Exception("you entered an invalid ID.");
            }
        }
        private void ColumnIsNotDoneColumn(int columnOrdinal) // check if the given column is the 'done' column or not
        {
            if (columnOrdinal == 2)
            {
                log.Warn(email + " has attempted to change a completed task. no tasks were changed.");
                throw new Exception("Completed tasks cannot be changed.");
            }
        }
        private void CheckColumnName(string name) // check if given column name is a legal name (is either 'backlog', 'in progress' or 'done')
        {
            if (!name.Equals(columns[1].getName()) & !name.Equals(columns[2].getName()) & !name.Equals(columns[0].getName()))
            {
                log.Warn(email + " has entered an invalid column name.");
                throw new Exception("The column name you searched for is invalid.");
            }
        }
        private void LoadData() // load column list from json
        {
            foreach (TC.Column c in columns)
            {
                c.Load();
                ID += c.getSize();
            }
        }

        private void CheckColumnOrdinal(int num) // check if the given column number is legal
        {
            if (num < 0 | num > 2)
            {
                log.Warn(email + " has entered an invalid column number.");
                throw new Exception("Invalid column number.");
            }
        }
        private int ChengeToInt(string s) // get the munber of the column with the given name
        {
            for (int i = 0; i < 3; i++)
            {
                if (s.Equals(columns[i].getName()))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
