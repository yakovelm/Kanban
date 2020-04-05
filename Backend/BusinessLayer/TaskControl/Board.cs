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
            //need to check if have a columnws user in data acseses
            columns = new Dictionary<string, Column>();
            columnsInt = new Column[4];
            columns.Add("done", new Column(email, "done"));
            columnsInt[3] = columns["done"];
            columns.Add("in progress", new Column(email, "in progress"));
            columnsInt[2] = columns["in progress"];
            columns.Add("backlog", new Column(email, "backlog"));
            columnsInt[1] = columns["backlog"];
            ID =1;
        }
        public Board()
        {
            email = null;
        }
        public Board(string email,Column column1, Column column2, Column column3)
        {
            //texter Board
            this.email = email;
            //add the column of this email
            columns = new Dictionary<string, Column>();
            columns.Add("a", column1);
            columns.Add("b", column2);
            columns.Add("c", column3);
            ID = 0;
            columnsInt = new Column[4];
            int i = 1;
            foreach (Column a in columns.Values)
            {
                ID += a.getSize();
                columnsInt[i] = a;
                i++;
            }

        }
        public void LimitColumnTask(string email,int ColumnOrdinal,int limit)
        {
            CheckEmail(email);
            CheckColumnOrdinal(ColumnOrdinal);
            columnsInt[ColumnOrdinal].setLimit(limit);
        }
        private void CheckColumnOrdinal(int num)
        {
            if (num < 1 | num > 3)
            { throw new Exception("Invalid column numbe"); }
        }
        private void CheckEmail(string email)
        {
            if (this.email == null ) { throw new Exception("you need to login to system"); }
            if (!email.Equals(this.email)) { throw new Exception("The email you entered does not match the email of the party");}
        }
        public Task AddTask(string email, string title,string desciption, DateTime dueTime)
        {
            CheckEmail(email);
            ID++;
            Task newTack = new Task(ID, title,desciption,dueTime,this.email);
            columnsInt[1].addTask(newTack);
            return newTack;
        }
        public void UpdateTaskDueDate(string email,int columnOrdinal, int taskID, DateTime Due)
        {
            CheckEmail(email);
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            Task updateTask=columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDue(Due);
        }
        public void UpdateTaskTitle(string email, int columnOrdinal, int taskID,string title)
        {
            CheckEmail(email);
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editTitle(title);
        }
        public void UpdateTaskDescription(string email, int columnOrdinal ,int taskID, string description)
        {
            CheckEmail(email);
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDesc(description);
        }
        public Task GetTask(int taskID)
        {
            if (this.email == null) { throw new Exception("you need to login to system"); }
            CheckTaskID(taskID);
            foreach (Column a in columns.Values)
            {
                Task checktask = a.getTask(taskID);
                if (checktask != null)
                    return checktask;
            }
            return null;
        }
        public void AdvanceTask(string email,int columnOrdinal ,int taskId)
        {
            CheckEmail(email);
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskId);
            Task advTask = columnsInt[columnOrdinal].getTask(taskId);
            if (advTask == null)
            { throw new Exception("task does not exist in this columm"); }
            columnsInt[columnOrdinal].deleteTask(advTask);
            columnsInt[columnOrdinal+1].addTask(advTask);
        }
        public Column GetColumn(string email,string columnName)
        {
            CheckEmail(email);
            CheckColumnName(columnName);
            return columns[columnName];
        }
        public Column GetColumn(string email,int columnOrdinal)
        {
            CheckEmail(email);
            CheckColumnOrdinal(columnOrdinal);
            return columnsInt[columnOrdinal];
        }
        public Dictionary<string,Column> getColumns(string email)
        {
            CheckEmail(email);
            return columns;
        }

        private void CheckTaskID(int taskID)
        {
            if(taskID>this.ID | taskID < 1)
            {
                throw new Exception("you enterd illlegal ID");
            }
        }
        private void ColumnIsNotDoneColumn(int columnOrdinal)
        {
            if (columnOrdinal == 3)
                throw new Exception("Completed tasks cannot be changed");
        }
        private void CheckColumnName(string name)
        {
            if(!name.Equals(columnsInt[1].getName()) & !name.Equals(columnsInt[2].getName()) & !name.Equals(columnsInt[3].getName()))
            { throw new Exception("The column name you searched for is invalid"); }
        }
    }
}
