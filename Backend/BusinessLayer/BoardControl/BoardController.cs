using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TC = IntroSE.Kanban.Backend.BusinessLayer.TaskControl;
using DC = IntroSE.Kanban.Backend.DataAccessLayer.DALControllers;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardControl
{
    class BoardController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Dictionary<string, Board> BC;
        private Board Cur;

        public BoardController()
        {
            BC = new Dictionary<string, Board>();
            Cur = null;
            log.Debug("BoardController created.");
        }

        public void LoadData() // load board dictionary (boards keyd by email) of all saved boards
        {
            DC.BoardCtrl DBC = new DC.BoardCtrl();
            List<string> temp = DBC.LoadData();
            foreach(string b in temp)
            {
                BC.Add(b, new Board(b));
            }
            log.Debug("board list has been loaded.");
        }

        public void Login(string email) // log in currend board holder
        {
            IsActive();
            email = email.ToLower();
            if (BC.ContainsKey(email))
            {
                Cur = BC[email];
                log.Debug("successfully opened board for " + email + ".");
            }
            else
            {
                log.Info(email + " does not have a board yet, creating new empty board.");
                BC.Add(email, new Board(email));
                Cur = BC[email];
            }
        }
        public void Logout(string email) // log out current board holder
        {
            CheckEmail(email);
            Cur = null;
            log.Debug(email + " has logged out.");
        }

        public void LimitColumnTask(string email, int ColumnOrdinal, int limit) // change the limit of a specific column
        {
            CheckEmail(email);
            Cur.LimitColumnTask(ColumnOrdinal, limit);
        }

        private void CheckEmail(string email) // checks that the email given in the request matches to the email of the currently logged in board holder
        {
            if (email == null)
            {
                log.Error("An offline user tried to take action.");
                throw new Exception("you need to login to system.");
            }
            string s = email.ToLower();
            if (Cur==null || !s.Equals(Cur.GetEmail()))
            {
                log.Warn(email + " does not match the email connected to the system.");
                throw new Exception("The email you entered does not match the email connected to the system.");
            }
        }
        public TC.Task AddTask(string email, string title, string desciption, DateTime dueTime) // add a new task for this user
        {
            CheckEmail(email);
            return Cur.AddTask(title, desciption, dueTime);
        }
        public void UpdateTaskDueDate(string email, int columnOrdinal, int taskID, DateTime Due) // update due date of this task
        {
            CheckEmail(email);
            Cur.UpdateTaskDueDate(columnOrdinal, taskID, Due);
        }
        public void UpdateTaskTitle(string email, int columnOrdinal, int taskID, string title) // update title of this task
        {
            CheckEmail(email);
            Cur.UpdateTaskTitle(columnOrdinal, taskID, title);
        }
        public void UpdateTaskDescription(string email, int columnOrdinal, int taskID, string description) // update description of this task
        {
            CheckEmail(email);
            Cur.UpdateTaskDescription(columnOrdinal, taskID, description);
        }
        public void AdvanceTask(string email, int columnOrdinal, int taskId) // advance this task to the next column
        {
            CheckEmail(email);
            Cur.AdvanceTask(columnOrdinal, taskId);
        }
        public TC.Column GetColumn(string email, string columnName) // get column data of a specific column (by name)
        {
            CheckEmail(email);
            return Cur.GetColumn(columnName);
        }
        public TC.Column GetColumn(string email, int columnOrdinal) // get column data of a specific column (by ID)
        {
            CheckEmail(email);
            return Cur.GetColumn(columnOrdinal);
        }
        public List<TC.Column> getColumns(string email) // get all columns of current board holder
        {
            CheckEmail(email);
            return Cur.getColumns();
        }
        private void IsActive()
        {
            if (Cur != null)
            {
                log.Error("tried to login or drop data while a user is already logged in.");
                throw new Exception("tried to login or drop data while a user is already logged in.");
            }
        }

        public void RemoveColumn(string email, int columnOrdinal)
        {
            CheckEmail(email);
            Cur.RemoveColumn(columnOrdinal);
        }

        public TC.Column AddColumn(string email, int columnOrdinal, string Name)
        {
            CheckEmail(email);
            return Cur.AddColumn(columnOrdinal, Name);
        }

        public TC.Column MoveColumnRight(string email, int columnOrdinal)
        {
            CheckEmail(email);
            return Cur.MoveColumnRight(columnOrdinal);
        }

        public TC.Column MoveColumnLeft(string email, int columnOrdinal)
        {
            CheckEmail(email);
            return Cur.MoveColumnLeft(columnOrdinal);
        }
        public void Drop()
        {
            IsActive();
            BC = new Dictionary<string, Board>();
        }

    }
}
