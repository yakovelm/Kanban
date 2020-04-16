using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroSE.Kanban.Backend.BusinessLayer.TaskControl
{
    class BoardController
    {
        private Dictionary<string,Board> BC;
        private Board Cur;

        public BoardController() { 
            BC = new Dictionary<string, Board>();
            Cur = new Board(); }

        public void LoadData()
        {
            string[] users = Directory.GetDirectories(Directory.GetCurrentDirectory()+"\\JSON");
            foreach(string email in users)
            {
                BC.Add(email, new Board(email));
            }
        }

        public void Login(string email) {
            Cur = BC[email];
            if (Cur == null)
            {
                BC.Add(email,new Board(email));
                Cur = BC[email];
            }
        }
        public void Logout()
        {
            Cur = new Board();
        }

        public void LimitColumnTask(string email, int ColumnOrdinal, int limit)
        {
            CheckEmail(email);
            Cur.LimitColumnTask(ColumnOrdinal, limit);
        }

        private void CheckEmail(string email)
        {
            if (Cur.GetEmail() == null) { throw new Exception("you need to login to system"); }
            if (!email.Equals(Cur.GetEmail())) { throw new Exception("The email you entered does not match the email of the party"); }
        }
        public Task AddTask(string email, string title, string desciption, DateTime dueTime)
        {
            CheckEmail(email);
            return Cur.AddTask(title, desciption, dueTime);
        }
        public void UpdateTaskDueDate(string email, int columnOrdinal, int taskID, DateTime Due)
        {
            CheckEmail(email);
            Cur.UpdateTaskDueDate(columnOrdinal, taskID, Due);
        }
        public void UpdateTaskTitle(string email, int columnOrdinal, int taskID, string title)
        {
            CheckEmail(email);
            Cur.UpdateTaskDescription(columnOrdinal, taskID, title);
        }
        public void UpdateTaskDescription(string email, int columnOrdinal, int taskID, string description)
        {
            CheckEmail(email);
            Cur.UpdateTaskDescription(columnOrdinal, taskID, description);
        }
        public Task GetTask(int taskID)
        {
            return Cur.GetTask(taskID);
        }
        public void AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            CheckEmail(email);
            Cur.AdvanceTask(columnOrdinal, taskId);
        }
        public Column GetColumn(string email, string columnName)
        {
            CheckEmail(email);
            return Cur.GetColumn(columnName);
        }
        public Column GetColumn(string email, int columnOrdinal)
        {
            CheckEmail(email);
            return Cur.GetColumn(columnOrdinal);
        }
        public Dictionary<string, Column> getColumns(string email)
        {
            CheckEmail(email);
            return Cur.getColumns();
        }

 



    }
}
