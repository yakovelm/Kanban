using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.TaskControl
{
    class Board
    {
        private Dictionary<string,Column> columns;
        private int ID;
        private string email;
        private Column[] columnsInt;
        public Board(string email)
        {
            this.email = email;
            //add the column of this email
            ID = 1;
            columnsInt = new Column[3];
            int i= 0;
            foreach (Column a in columns.Values) {
                ID += a.getSize();
                columnsInt[i] = a;
                i++;
            }
            
        }
        public void LimitColumnTask(int ColumnOrdinal,int limit)
        {
            columnsInt[ColumnOrdinal].setLimit(limit);
        }
        public bool CheckColumnOrdinal(int num)
        {
            if (num < 1 | num > 3)
            { return false; }
            return true;
        }
        public bool CheckEmail(string email)
        {
            if (!email.Equals(this.email)) { return false; }
            return true;
        }
        public void AddTask(string title,string desciption, DateTime dueTime)
        {
            ID++;
            Task newTack = new Task(ID, title,desciption,dueTime,this.email);
            columnsInt[0].addTask(newTack);
        }
        public void UpdateTaskDueDate(int columnOrdinal, int taskID, DateTime Due)
        {
            ColumnIsNotDoneColumn(columnOrdinal);
            Task updateTask=columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDue(Due);
        }
        public void UpdateTaskTitle(int columnOrdinal, int taskID,string title)
        {
            ColumnIsNotDoneColumn(columnOrdinal);
            Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editTitle(title);
        }
        public void UpdateTaskDescription(int columnOrdinal ,int taskID, string description)
        {
            ColumnIsNotDoneColumn(columnOrdinal);
            Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDesc(description);
        }
        public Task GetTask(int taskID)
        {
            foreach (Column a in columns.Values)
            {
                Task checktask = a.getTask(taskID);
                if (checktask != null)
                    return checktask;
            }
            return null;
        }
        public bool AdvanceTask(int columnOrdinal ,int taskId)
        {
            ColumnIsNotDoneColumn(columnOrdinal);
            Task advTask = columnsInt[columnOrdinal].getTask(taskId);
            if (advTask == null) { return false; }
            columnsInt[columnOrdinal].deleteTask(advTask);
            columnsInt[columnOrdinal+1].addTask(advTask);
            return true;
        }
        public Column GetColumn(string columnName)
        {
            return columns[columnName];
        }
        public Column GetColumn(int columnOrdinal)
        {
            return columnsInt[columnOrdinal];
        }

        public bool CheckTaskID(int taskID)
        {
            if(taskID>this.ID | taskID < 1)
            {
                return false;
            }
            return true;
        }
        void ColumnIsNotDoneColumn(int columnOrdinal)
        {
            if (columnOrdinal == 3)
                throw new Exception("Completed tasks cannot be changed");
        }
    }
}
