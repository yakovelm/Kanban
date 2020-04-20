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
        private Dictionary<string,TC.Column> columns;
        private int ID=0;
        private string email;
        private TC.Column[] columnsInt;
        public Board(string email)
        {
            this.email = email;
            columns = new Dictionary<string, TC.Column>();
            columnsInt = new TC.Column[4];
            columns.Add("done", new TC.Column(email, "done"));
            columnsInt[3] = columns["done"];
            columns.Add("in progress", new TC.Column(email, "in progress"));
            columnsInt[2] = columns["in progress"];
            columns.Add("backlog", new TC.Column(email, "backlog"));
            columnsInt[1] = columns["backlog"];
            LoadData();
            log.Debug("a board for " + email + " has been made.");
        }
        public Board()
        {
            email = null;
            log.Debug("empty board created.");
        }

        public void LimitColumnTask(int ColumnOrdinal,int limit)
        {
            
            CheckColumnOrdinal(ColumnOrdinal);
            columnsInt[ColumnOrdinal].setLimit(limit);
        }

        public string GetEmail() { return email; }


        public TC.Task AddTask(string title,string desciption, DateTime dueTime)
        {
            ID++;
            TC.Task newTack = new TC.Task(ID, title,desciption,dueTime,this.email);
            columnsInt[1].addTask(newTack);
            return newTack;
        }
        public void UpdateTaskDueDate(int columnOrdinal, int taskID, DateTime Due)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            TC.Task updateTask=columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDue(Due);
            log.Debug("due date of task #" + taskID + "has been updated.");
            columnsInt[columnOrdinal].Save();
        }
        public void UpdateTaskTitle(int columnOrdinal, int taskID,string title)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            TC.Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editTitle(title);
            log.Debug("title of task #" + taskID + "has been updated.");
            columnsInt[columnOrdinal].Save();
        }
        public void UpdateTaskDescription( int columnOrdinal ,int taskID, string description)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            TC.Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDesc(description);
            log.Debug("description of task #" + taskID + "has been updated.");
            columnsInt[columnOrdinal].Save();
        }
        public TC.Task GetTask(int taskID)
        {
            if (this.email == null) {
                log.Warn("user not logged into system.");
                throw new Exception("you need to login to system"); }
            CheckTaskID(taskID);
            foreach (TC.Column a in columns.Values)
            {
                TC.Task checktask = a.getTask(taskID);
                if (checktask != null)
                    return checktask;
            }
            return null;//we must to return something (defult).
        }
        public void AdvanceTask(int columnOrdinal ,int taskId)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskId);
            TC.Task advTask = columnsInt[columnOrdinal].getTask(taskId);
            if (advTask == null)
            {
                log.Warn(email + "  tried to advance task #"+taskId+" that does not exist in "+ columnsInt[columnOrdinal].getName()+" column.");
                throw new Exception("task does not exist in this columm"); }
            columnsInt[columnOrdinal + 1].addTask(advTask);
            columnsInt[columnOrdinal].deleteTask(advTask);
            log.Debug("task " + taskId + " advanced successfully.");
        }
        public TC.Column GetColumn(string columnName)
        {

            CheckColumnName(columnName);
            return columns[columnName];
        }
        public TC.Column GetColumn(int columnOrdinal)
        { 
            CheckColumnOrdinal(columnOrdinal);
            return columnsInt[columnOrdinal];
        }
        public Dictionary<string, TC.Column> getColumns()
        {
            return columns;
        }

        private void CheckTaskID(int taskID)
        {
            if(taskID>this.ID | taskID < 1)
            {
                log.Warn(email+" has entered an invalid task ID.");
                throw new Exception("you entered an invalid ID");
            }
        }
        private void ColumnIsNotDoneColumn(int columnOrdinal)
        {
            if (columnOrdinal == 3)
            {
                log.Warn(email+" has attempted to change a completed task. no tasks were changed.");
                throw new Exception("Completed tasks cannot be changed");
            }
        }
        private void CheckColumnName(string name)
        {
            if(!name.Equals(columnsInt[1].getName()) & !name.Equals(columnsInt[2].getName()) & !name.Equals(columnsInt[3].getName()))
            {
                log.Warn(email + " has entered an invalid column name.");
                throw new Exception("The column name you searched for is invalid"); }
        }
        private void LoadData()
        {
            for(int i=1;i<4;i++)
            {
                columnsInt[i].Load();
                ID += columnsInt[i].getSize();
            }
        }

        private void CheckColumnOrdinal(int num)
        {
            if (num < 1 | num > 3)
            {
                log.Warn(email + " has entered an invalid column number.");
                throw new Exception("Invalid column number");
            }
        }
    }
}
