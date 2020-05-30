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
        private Dictionary<string, int> hosts;
        private Dictionary<int, string> IdToEmail;
        private Board Cur;
        private string CurEmail;
        private DC.BoardCtrl DBC;

        public BoardController()
        {
            BC = new Dictionary<string, Board>();
            hosts = new Dictionary<string, int>();
            IdToEmail = new Dictionary<int, string>();
            Cur = null;
            CurEmail = null;
            log.Debug("BoardController created.");
        }

        public void LoadData() // load board dictionary (boards keyd by email) of all saved boards
        {
            DBC = new DC.BoardCtrl();
            List<Tuple<string, long, long>> temp = DBC.LoadData();
            temp.ForEach(x => { IdToEmail.Add((int)x.Item3, x.Item1); });
            temp.ForEach(x => { hosts.Add(x.Item1, (int)x.Item3); });
            temp.Where(x => x.Item2 == x.Item3).ToList().ForEach(x => { BC.Add(x.Item1, new Board(x.Item1,(int)x.Item3)); BC[x.Item1].LoadData(); });
            temp.Where(x => x.Item2 != x.Item3).ToList().ForEach(x => { BC[IdToEmail[(int)x.Item2]].Join((int)x.Item3); });
            log.Debug("board list has been loaded.");
        }
        public void Register(string email, string emailhost)
        {
            email = email.ToLower();
            emailhost = emailhost.ToLower();
            int ID = FindID(email);// we can ussome that the length of IDtoEmail +1 is the ID...
            IdToEmail.Add(ID, email);
            if (email.Equals(emailhost))
            {
                hosts.Add(email, ID);
                BC.Add(email, new Board(email, ID));
                BC[email].Register();
            }
            else
            {
                int IDhost= CheckHost(emailhost);
                hosts.Add(email, IDhost);
                BC.Add(email, BC[emailhost]);
                BC[emailhost].Join(ID);
            }

            log.Debug($"the Board of {email} is ready and his Host is {emailhost}.");
        }
        private int CheckHost(string email)
        {
            int check = FindID(email);
            if (check == -1)
            {
                log.Warn($"the User try to join to {email} board but is email is illegal.");
                throw new Exception($"the User try to join to {email} board but is email is illegal.");
            }
            return check;
        }
        private int EmailToId(string email)
        {
            return IdToEmail.FirstOrDefault(x => x.Value.Equals(email)).Key;
        }

        public void Login(string email) // log in currend board holder
        {
            IsActive();
            email = email.ToLower();
            if (!BC.ContainsKey(email))
            {
                log.Warn($"{email} try to login to system but he is not register to the system.");
                throw new Exception($"{email} try to login to system but he is not register to the system.");
            }
            log.Debug("successfully opened board for " + email + ".");
            Cur = BC[email];
            CurEmail = email;
            BC[email].Login(EmailToId(email));
        }
        private int FindID(string email)
        {
            int temp = DBC.FindBoard(email);
            if (temp ==-1)
            {
                log.Warn($"{email} try to do action  with email Illegal");
                throw new Exception($"{email} try to do action  with email Illegal");
            }
            return temp;
        }
        public void Logout(string email) // log out current board holder
        {
            CheckEmail(email);
            Cur.Logout();
            Cur = null;
            CurEmail = null;
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
            if (Cur==null || !s.Equals(CurEmail))
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
        public void DeleteTask(string email, int columnOrdinal, int taskId)
        {
            CheckEmail(email);
            Cur.DeleteTask(columnOrdinal, taskId);
        }
        public void AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee)
        {
            CheckEmail(email);
            int IdAssignee = FindID(emailAssignee);
            Cur.AssignTask(columnOrdinal,taskId,IdAssignee);
        }

        }
}
