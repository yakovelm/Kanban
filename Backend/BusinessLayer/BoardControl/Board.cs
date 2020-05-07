using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC = IntroSE.Kanban.Backend.BusinessLayer.TaskControl;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;
using System.IO;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardControl
{
    class Board : IPersistentObject<DAL.Board>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<TC.Column> columns;
        private const int minColumn = 2;
        private int size;
        private int IDtask;
        private int IDcolumn;
        private string email;
        public Board(string email)
        {
            this.email = email;
            Load();
            log.Debug("a board for " + email + " has been made.");        
        }

        public void LimitColumnTask(int ColumnOrdinal, int limit) // change the limit of a specific column
        {

            CheckColumnOrdinal(ColumnOrdinal);
            columns[ColumnOrdinal].setLimit(limit);
        }

        public string GetEmail() { return email; }

        public TC.Task AddTask(string title, string desciption, DateTime dueTime) // add a new task for this user
        {
            TC.Task newTack = new TC.Task(IDtask, title, desciption, dueTime, this.email);
            IDtask++;
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
                log.Warn(email + " has entered an invalid task ID.");
                throw new Exception("you entered an invalid ID.");
            }
        }
        private void ColumnIsNotDoneColumn(int columnOrdinal) // check if the given column is the 'done' column or not
        {
            if (columnOrdinal == size-1)
            {
                log.Warn(email + " has attempted to change a completed task. no tasks were changed.");
                throw new Exception("Completed tasks cannot be changed.");
            }
        }
        private bool CheckColumnName(string name) // check if given column name is a legal name (is either 'backlog', 'in progress' or 'done')
        {
            foreach(TC.Column c in columns)
            {
                if (name.Equals(c.getName()))
                {
                    return true;
                }
            }
            return false;
        }
        private void LoadData(List<string> names) // load column list from json
        {
            IDtask = 0;
            foreach (string s in names)
            {
                TC.Column temp = new TC.Column(email, s);
                temp.Load();
                columns.Add(new TC.Column(email, s));
                size++;
                IDtask += temp.getSize();
            }
        }

        private void CheckColumnOrdinal(int num) // check if the given column number is legal
        {
            if (num < 0 | num >= size)
            {
                log.Warn(email + " has entered an invalid column number.");
                throw new Exception("Invalid column number.");
            }
        }
        private int ChengeToInt(string s) // get the munber of the column with the given name
        {
            for (int i = 0; i < size; i++)
            {
                if (s.Equals(columns[i].getName()))
                {
                    return i;
                }
            }
            return -1;
        }

        public DAL.Board ToDalObject()
        {
            log.Debug("the board converting to DAL obj in " + email + ".");
            try
            {
                List<string> DColumns = new List<string>();
                return new DAL.Board(email, ChengeToString());
            }
            catch (Exception e)
            {
                log.Error("issue converting board BL object to column DAL object due to " + e.Message);
                throw e;
            }
        }

        public void FromDalObject(DAL.Board DalObj)
        {
            log.Debug("the board converting from DAL obj in " + email + ".");
            try
            {
                LoadData(DalObj.columns);
            }
            catch (Exception e)
            {
                log.Error("issue converting board DAL object to column BL object due to " + e.Message);
                throw e;
            }
        }

        public void Save()
        {
            try
            {
                log.Debug("the board saving to hard drive for " + email + ".");
                DAL.Board DB = ToDalObject();
                DB.Write("JSON\\" + email + "\\Board.json", DB.toJson());
            }
            catch (Exception e)
            {
                log.Error("failed to write to file due to " + e.Message);
                throw e;
            }
        }
        private List<string> ChengeToString()
        {
            List<string> output = new List<string>();
            foreach(TC.Column c in columns)
            {
                output.Add(c.getName());
            }
            return output;
        }

        public void Load()
        {
            ResetBoard();
            log.Debug("the board of " + email + "loading from hard drive.");
            DAL.Board DB = new DAL.Board(email);
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\JSON\\" + email + "\\Board.json"))
            {
                log.Info("no preexisting the board file for " + email + " initializing new empty file.");
                newBoard();
                Save();
            }
            else
            {
                try
                {
                    DB.fromJson("JSON\\" + email + "\\Board.json");
                }
                catch (Exception e)
                {
                    log.Error("failed to load board from file due to " + e.Message);
                    throw e;
                }
                FromDalObject(DB);
            }
        }
        private void ResetBoard()///////////////////////////////////////////////////////////////////////////////////////
        {
            IDtask = 0;
            IDcolumn = 0;//////////////////////////////////////////////////////////////////////////////////////////////
            size = 0;
            columns = new List<TC.Column>();
        }

        private void newBoard()
        {
            columns.Add(new TC.Column(email, "backlog",IDcolumn,0));
            IDcolumn++;
            columns.Add(new TC.Column(email, "in progress", IDcolumn, 1));
            IDcolumn++;
            columns.Add(new TC.Column(email, "done", IDcolumn, 2));
            IDcolumn++;
            size = 3;
            IDtask = 1;
            IDcolumn = 1;
            log.Debug("a new board for " + email + " has been made.");
        }

        public void DeleteData()
        {
            //foreach (TC.Column c in columns) { c.DeleteData(); }
            //try
            //{
            //    DAL.Board temp = new DAL.Board(email);
            //    temp.Delete("JSON\\" + email + "\\Board.json");
            //}
            //catch (Exception e) { throw new Exception("Could not delete the board"); }
        }

        public void RemoveColumn(int columnOrdinal)
        {
            CheckColumnOrdinal(columnOrdinal);
            checkSize();
            var c = columns[columnOrdinal].getAll();
            if (columnOrdinal == 0){columns[1].addTasks(c); }
            else { 
                columns[columnOrdinal - 1].addTasks(c);
                MoveColumns(columnOrdinal);
                setOrdColumns();
            }
            size--;
            log.Debug(email + " removed column number #" + columnOrdinal + " succses");
            Save();
        }


        public TC.Column AddColumn(int columnOrdinal, string Name)
        {
            checkrColumnNumber(columnOrdinal);
            if (CheckColumnName(Name))
            {
                log.Warn(email + " has entered exist column name.");
                throw new Exception("The column name you searched for is invalid.");
            }
            columns.Insert(columnOrdinal,new TC.Column(email, Name,IDcolumn,columnOrdinal));
            size++;
            IDcolumn++;
            setOrdColumns();
            log.Debug(email + " added column number #" + columnOrdinal + " succses");
            Save();
            return columns[columnOrdinal];
        }
        private void setOrdColumns()
        {
            for(int i=0;i<size;i++)
            {
                columns[i].setOrd(i);
            }
        }
        private void checkrColumnNumber(int ord)
        {
            if (ord < 0 | ord > size)
            {
                log.Warn(email + " has entered an invalid column number for the new column.");
                throw new Exception("Invalid column number.");
            }
        }

        public TC.Column MoveColumnRight(int columnOrdinal)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotDoneColumn(columnOrdinal);
            ExchangeColumns(columnOrdinal, columnOrdinal + 1);
            log.Debug(email + " Moved right column was number #" + columnOrdinal + " succses");
            Save();
            return columns[columnOrdinal + 1];
        }

        public TC.Column MoveColumnLeft(int columnOrdinal)
        {
            CheckColumnOrdinal(columnOrdinal);
            ColumnIsNotFirstColumn(columnOrdinal);
            ExchangeColumns(columnOrdinal, columnOrdinal -1);
            log.Debug(email + " Moved left column was number #" + columnOrdinal + " succses");
            Save();
            return columns[columnOrdinal - 1];
        }
        private void ExchangeColumns(int num1, int num2)
        {
            TC.Column temp = columns[num1];
            columns[num1] = columns[num2];
            columns[num2] = temp;
            log.Debug("the columns #" + num1 + " #" + num2 + " chenge place.");
            columns[num1].setOrd(num2);
            columns[num1].setOrd(num2);
        }
        private void MoveColumns(int num)
        {
            for(int i=num; i<size-1; i++)
            {
                columns[i] = columns[i+1];
            }
        }
        private void ColumnIsNotFirstColumn(int num)
        {
            if (num == 0)
            {
                log.Warn(email + " has attempted to Move left a first column.");
                throw new Exception("user try to Move left a first column.");
            }
        }
        private void checkSize()
        {
            if (size == minColumn)
            {
                log.Warn(email + " has attempted to remove column when the minmum columns is 2.");
                throw new Exception("user attempted to remove column when the minmum columns is 2.");
            }
        }
    }
}
