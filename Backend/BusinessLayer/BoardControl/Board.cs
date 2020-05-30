﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC = IntroSE.Kanban.Backend.BusinessLayer.TaskControl;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;
using System.IO;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardControl
{
    class Board
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<TC.Column> columns;
        private const int minColumn = 2;
        private int size;
        private int IDtask;
        private string email;
        private int host;
        private List<int> Licensed;
        private int cur;
        public Board(string email,int id)
        {
            this.email = email;
            size = 0;
            IDtask = 1;
            host = id;
            cur = -1;
            Licensed = new List<int>();
            Licensed.Add(cur);
            log.Debug("a board for " + email + " has been made.");
            
        }
        public void LoadData()
        {
            DAL.Board b = new DAL.Board(email);
            b.LoadData();
            OrdinaltheList(ColumnsToBT(b.columns));
            UpdateTheIdTask();
            UpdateTheSize();
            log.Debug("LoadData Board of email: " + email + " seccess. IDTask is: " + IDtask);
        }
        public void Register()
        {
            columns.Add(new TC.Column(email, "backlog", 0));
            columns.Add(new TC.Column(email, "in progress", 1));
            columns.Add(new TC.Column(email, "done", 2));
            size = 3;
            log.Debug("a new board for " + email + " has been made.");
        }
        public void Join(int id)
        {
            Licensed.Add(id);
        }
        public void Login(int Id)
        {
            Contains(Id);
            cur = Id;
        }
        public void Logout()
        {
            cur= - 1;
        }
        public void Contains(int Id)
        {
            if (!Licensed.Contains(Id))
            {
                log.Warn($"Id #{Id} deos not exist in Licensed of  user Id{host}");
                throw new Exception($"Id #{Id} deos not exist in Licensed of  user Id{host}");
            }
        }
        private void UpdateTheIdTask() // make sure that his board has the correct next task ID
        {
            columns.ForEach(x => { IDtask += x.getSize(); });
        }
        private void UpdateTheSize() // make sure this board has the correct size (number of columns)
        {
            columns.ForEach(x => { size += 1; });
        }
        private void OrdinaltheList(List<TC.Column> list) // make sure all columns are syncronized with their place in the column list
        {
            log.Debug("Order the columns of email: " + email);
            List<TC.Column> output = new List<TC.Column>();
            for (int i = 0; i < list.Count(); i++)
            {
                for (int j = 0; j < list.Count(); j++)
                {
                    if (list[j].getOrd() == i)
                    {
                        output.Add(list[j]);
                    }
                }
                if (output.Count() != i + 1)
                {
                    log.Warn("there is no column with ord " + i);
                    throw new Exception("there is no column with ord " + i);
                }
            }
            columns = output;
        }
        private List<TC.Column> ColumnsToBT(List<DAL.Column> list) // convert DAL columns to BL columns
        {
            List<TC.Column> output = new List<TC.Column>();
            foreach (DAL.Column a in list)
            {
                TC.Column temp = new TC.Column();
                temp.FromDalObject(a);
                output.Add(temp);
            }
            return output;
        }
        public void LimitColumnTask(int ColumnOrdinal, int limit) // change the limit of a specific column
        {
            CheckHost();
            CheckColumnOrdinal(ColumnOrdinal);
            columns[ColumnOrdinal].setLimit(limit);
        }
        private void CheckColumnOrdinal(int num) // check if the given column number is legal
        {
            if (num < 0 | num >= size)
            {
                log.Warn(email + " has entered an invalid column number.");
                throw new Exception("Invalid column number.");
            }
        }
        public string GetEmail() { return email; }

        public TC.Task AddTask(string title, string desciption, DateTime dueTime) // add a new task for this user
        {
            TC.Task output = new TC.Task(IDtask, columns[0].getName(), title, desciption, dueTime, this.email);
            columns[0].addTask(output);
            IDtask++;
            return output;
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
            if (!CheckColumnName(columnName))
            {
                log.Warn(email + " has entered an invalid column name.");
                throw new Exception("The column name you searched for is invalid.");
            }
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
            if (taskID > this.IDtask | taskID < 1)
            {
                log.Warn(email + " has entered an invalid task ID. max ID is: " + IDtask);
                throw new Exception("you entered an invalid ID.");
            }
        }
        private void ColumnIsNotDoneColumn(int columnOrdinal) // check if the given column is the 'done' column or not
        {
            if (columnOrdinal == size - 1)
            {
                log.Warn(email + " has attempted to change a completed task. no tasks were changed.");
                throw new Exception("Completed tasks cannot be changed.");
            }
        }
        private bool CheckColumnName(string name) // check if given column name is a legal name (is either 'backlog', 'in progress' or 'done')
        {
            if (name == null) return true;
            foreach (TC.Column c in columns)
            {
                if (name.Equals(c.getName()))
                {
                    return true;
                }
            }
            return false;
        }

        private int ChengeToInt(string s) // get the number of the column with the given name
        {
            for (int i = 0; i < size; i++)
            {
                if (s.Equals(columns[i].getName())) { return i; }
            }
            log.Debug(s + " does not exist in columns.");
            throw new Exception(s + " does not exist in columns.");
        }

        public void RemoveColumn(int columnOrdinal) // remove column in the given position from column list
        {
            CheckHost();
            CheckColumnOrdinal(columnOrdinal);
            checkSize();
            var c = columns[columnOrdinal].getAll();
            if (columnOrdinal == 0)
            {
                columns[1].addTasks(c);
            }
            else { columns[columnOrdinal - 1].addTasks(c); }
            columns[columnOrdinal].delete();
            MoveColumns(columnOrdinal);
            setOrdColumns();
            log.Debug(email + " removed column number #" + columnOrdinal + " succses");
        }

        public TC.Column AddColumn(int columnOrdinal, string Name)// add column in the given position of column list
        {
            CheckHost();
            checkrColumnNumber(columnOrdinal);
            if (CheckColumnName(Name))
            {
                log.Warn(email + " has entered exist column name.");
                throw new Exception("The column name you searched for is invalid.");
            }
            columns.Insert(columnOrdinal, new TC.Column(email, Name, columnOrdinal));
            size++;
            setOrdColumns();
            log.Debug(email + " added column number #" + columnOrdinal + " succses");
            return columns[columnOrdinal];
        }
        private void setOrdColumns() // set each column to hold his ordinal position
        {
            for (int i = 0; i < size; i++) { columns[i].setOrd(i); }
        }
        private void checkrColumnNumber(int ord) // check if column number is legal
        {
            if (ord < 0 | ord > size)
            {
                log.Warn(email + " has entered an invalid column number for the new column.");
                throw new Exception("Invalid column number.");
            }
        }

        public TC.Column MoveColumnRight(int columnOrdinal)
        {
            CheckHost();
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            ExchangeColumns(columnOrdinal, columnOrdinal + 1);
            log.Debug(email + " Moved right column was number #" + columnOrdinal + " succses");
            return columns[columnOrdinal + 1];
        }

        public TC.Column MoveColumnLeft(int columnOrdinal)
        {
            CheckHost();
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotFirstColumn(columnOrdinal);
            ExchangeColumns(columnOrdinal, columnOrdinal - 1);
            log.Debug(email + " Moved left column was number #" + columnOrdinal + " succses");
            return columns[columnOrdinal - 1];
        }
        private void ExchangeColumns(int num1, int num2) // change the position of 2 columns in the list
        {
            TC.Column temp = columns[num1];
            columns[num1] = columns[num2];
            columns[num2] = temp;
            columns[num1].setOrd(num1);
            columns[num2].setOrd(num2);
            log.Debug("the columns #" + num1 + " #" + num2 + " chenge place.");
        }
        private void MoveColumns(int num) // cascade down all columns in the list starting at num by 1
        {
            for (int i = num; i < size - 1; i++) { columns[i] = columns[i + 1]; }
            if (columns[size - 1] != null) { columns.Remove(columns[size - 1]); }
            size--;
        }
        private void ColumnIsNotFirstColumn(int num)
        {
            if (num == 0)
            {
                log.Warn(email + " has attempted to Move left a first column.");
                throw new Exception("user try to Move left a first column.");
            }
        }
        private void checkSize() // check if minimum board size reached
        {
            if (size == minColumn)
            {
                log.Warn(email + " has attempted to remove column when the minmum columns is 2.");
                throw new Exception("user attempted to remove column when the minmum columns is 2.");
            }
        }
        public void DeleteTask(int columnOrdinal, int taskId)
        {
            checkrColumnNumber(columnOrdinal);
            CheckTaskID(taskId);
            columns[columnOrdinal].deleteTask(columns[columnOrdinal].getTask(taskId));
            log.Debug($"{email} accsses to Delete Task #{taskId}");
        }
        private void CheckHost()
        {
            if (cur != host)
            {
                log.Warn($"ID #{cur} is not host and can not do this action.");
                throw new Exception($"ID #{cur} is not host and can not do this action.");
            }
        }
        public void AssignTask(int columnOrdinal, int taskId, int IDAssignee)
        {
            CheckLisence(IDAssignee);
            CheckColumnOrdinal(columnOrdinal);
            CheckTaskID(taskId);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        private void CheckLisence(int ID)
        {
            if (!Licensed.Contains(ID))
            {
                log.Warn($"this ID {ID} dont have License in Board of {email}.");
                throw new Exception($"this ID {ID} dont have License in Board of {email}.");
            }
        }
    }
}