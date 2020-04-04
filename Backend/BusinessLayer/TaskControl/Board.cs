﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.TaskControl
{
    public class Board
    {
        private Dictionary<string,Column> columns;
        private int ID;
        private string email;
        private Column[] columnsInt;
        public Board(string email)
        {
            this.email = email;
            //add the column of this email
            columns = new Dictionary<string, Column>();
            ID = 0;
            columnsInt = new Column[3];
            int i= 0;
            foreach (Column a in columns.Values) {
                ID += a.getSize();
                columnsInt[i] = a;
                i++;
            }
            
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
            if (!email.Equals(this.email)) { throw new Exception("The email you entered does not match the email of the party");}
        }
        public void AddTask(string email, string title,string desciption, DateTime dueTime)
        {
            CheckEmail(email);
            ID++;
            Task newTack = new Task(ID, title,desciption,dueTime,this.email);
            columnsInt[1].addTask(newTack);
        }
        public void UpdateTaskDueDate(int columnOrdinal, int taskID, DateTime Due)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            Task updateTask=columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDue(Due);
        }
        public void UpdateTaskTitle(int columnOrdinal, int taskID,string title)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editTitle(title);
        }
        public void UpdateTaskDescription(int columnOrdinal ,int taskID, string description)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskID);
            Task updateTask = columnsInt[columnOrdinal].getTask(taskID);
            updateTask.editDesc(description);
        }
        public Task GetTask(int taskID)
        {
            CheckTaskID(taskID);
            foreach (Column a in columns.Values)
            {
                Task checktask = a.getTask(taskID);
                if (checktask != null)
                    return checktask;
            }
            return null;
        }
        public void AdvanceTask(int columnOrdinal ,int taskId)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            CheckTaskID(taskId);
            Task advTask = columnsInt[columnOrdinal].getTask(taskId);
            if (advTask == null)
            { throw new Exception("task does not exist in this columm"); }
            columnsInt[columnOrdinal].deleteTask(advTask);
            columnsInt[columnOrdinal+1].addTask(advTask);
        }
        public Column GetColumn(string columnName)
        {
            if (columns[columnName] == null)
            { throw new Exception("The column name you searched for is invalid"); }
            return columns[columnName];
        }
        public Column GetColumn(int columnOrdinal)
        {
            CheckColumnOrdinal(columnOrdinal);
            return columnsInt[columnOrdinal];
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
    }
}
