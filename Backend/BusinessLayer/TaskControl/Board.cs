﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.TaskControl
{
    class Board
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Dictionary<string,Column> columns;
        private int ID=0;
        private string email;
        private Column[] columnsInt;
        public Board(string email)
        {
            this.email = email;
            columns = new Dictionary<string, Column>();
            columnsInt = new Column[4];
            columns.Add("done", new Column(email, "done"));
            columnsInt[3] = columns["done"];
            columns.Add("in progress", new Column(email, "in progress"));
            columnsInt[2] = columns["in progress"];
            columns.Add("backlog", new Column(email, "backlog"));
            columnsInt[1] = columns["backlog"];
            LoadData();
        }
        public Board()
        {
            email = null;
        }

        public void LimitColumnTask(int ColumnOrdinal,int limit)
        {
            
            CheckColumnOrdinal(ColumnOrdinal);
            columnsInt[ColumnOrdinal].setLimit(limit);
        }

        public string GetEmail() { return email; }


        public Task AddTask(string title,string desciption, DateTime dueTime)
        {
            ID++;
            Task newTack = new Task(ID, title,desciption,dueTime,this.email);
            columnsInt[1].addTask(newTack);
            return newTack;
        }
        public void UpdateTaskDueDate(int columnOrdinal, int taskID, DateTime Due)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            Task updateTask=columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDue(Due);
            columnsInt[columnOrdinal].Save();
        }
        public void UpdateTaskTitle(int columnOrdinal, int taskID,string title)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editTitle(title);
            columnsInt[columnOrdinal].Save();
        }
        public void UpdateTaskDescription( int columnOrdinal ,int taskID, string description)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDesc(description);
            columnsInt[columnOrdinal].Save();
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
            return null;//we must to return something (defult).
        }
        public void AdvanceTask(int columnOrdinal ,int taskId)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskId);
            Task advTask = columnsInt[columnOrdinal].getTask(taskId);
            if (advTask == null)
            {
                log.Warn(email + "  tried to advande the task "+taskId+" that does not exist in "+ columnOrdinal+" column." );
                throw new Exception("task does not exist in this columm"); }
            columnsInt[columnOrdinal + 1].addTask(advTask);
            columnsInt[columnOrdinal].deleteTask(advTask);
        }
        public Column GetColumn(string columnName)
        {

            CheckColumnName(columnName);
            return columns[columnName];
        }
        public Column GetColumn(int columnOrdinal)
        { 
            CheckColumnOrdinal(columnOrdinal);
            return columnsInt[columnOrdinal];
        }
        public Dictionary<string,Column> getColumns()
        {
            return columns;
        }

        private void CheckTaskID(int taskID)
        {
            if(taskID>this.ID | taskID < 1)
            {
                log.Warn(email + "  entered an invalid identity number");
                throw new Exception("you entered an illegal ID");
            }
        }
        private void ColumnIsNotDoneColumn(int columnOrdinal)
        {
            if (columnOrdinal == 3)
            {
                log.Warn(email+" Attempt has attempted to try status / details in the task found in column DONE");
                throw new Exception("Completed tasks cannot be changed");
            }
        }
        private void CheckColumnName(string name)
        {
            if(!name.Equals(columnsInt[1].getName()) & !name.Equals(columnsInt[2].getName()) & !name.Equals(columnsInt[3].getName()))
            {
                log.Warn(email + "  enter an incorrect column name.");
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
                log.Warn(email + "  enter an incorrect column number.");
                throw new Exception("Invalid column number");
            }
        }
    }
}
