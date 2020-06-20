using IntroSE.Kanban.Backend.ServiceLayer;
using KanbanUI.Model;
using System;
using System.Collections.ObjectModel;

namespace KanbanUI
{
    public class BackendController
    {
        private Service s;
        public BackendController()
        {
            s = new Service();
        }

        public Tuple<string, int, ObservableCollection<TaskModel>> getColumn(string email, string name, int n)
        {
            Response<Column> res = s.GetColumn(email, name);
            isErr(res);
            ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
            foreach (IntroSE.Kanban.Backend.ServiceLayer.Task t in res.Value.Tasks)
            {
                tasks.Add(taskToModel(t, email, n));
            }
            return Tuple.Create(res.Value.Name, res.Value.Limit, tasks);
        }

        internal void DeleteColumn(string email, int index)
        {
            Response res = s.RemoveColumn(email, index);
            isErr(res);
        }

        internal void addTask(string email, string title, string desc, DateTime due)
        {
            Response res = s.AddTask(email, title, desc, due);
            isErr(res);
        }

        internal void Logout(string email)
        {
            Response res = s.Logout(email);
            isErr(res);
        }

        private TaskModel taskToModel(IntroSE.Kanban.Backend.ServiceLayer.Task t, string email, int n)
        {
            return new TaskModel(this, t.Id, t.emailAssignee, t.Title, t.Description, t.DueDate, t.CreationTime, n, email);
        }

        internal void editDue(string email, int columnIndex, int ID, DateTime due)
        {
            Response res = s.UpdateTaskDueDate(email, columnIndex, ID, due);
            isErr(res);
        }

        internal void editAssignee(string email, int columnIndex, int ID, string assignee)
        {
            Response res = s.AssignTask(email, columnIndex, ID, assignee);
            isErr(res);
        }

        internal void AdvanceTask(string email, int columnIndex, int ID)
        {
            Response res = s.AdvanceTask(email, columnIndex, ID);
            isErr(res);
        }

        internal void DeleteTask(string email, int columnIndex, int ID)
        {
            Response res = s.DeleteTask(email, columnIndex, ID);
            isErr(res);
        }

        internal void editDesc(string email, int columnIndex, int ID, string desc)
        {
            Response res = s.UpdateTaskDescription(email, columnIndex, ID, desc);
            isErr(res);
        }

        internal void editTitle(string email, int columnIndex, int ID, string title)
        {
            Response res = s.UpdateTaskTitle(email, columnIndex, ID, title);
            isErr(res);
        }

        internal ColumnModel AddColumn(string email, int index, string name)
        {
            Response<Column> res = s.AddColumn(email, index, name);
            isErr(res);
            return new ColumnModel(this, res.Value.Name, email, index);

        }

        public Tuple<string, ObservableCollection<ColumnModel>> getBoard(UserModel um)
        {
            Response<Board> res = s.GetBoard(um.email);
            isErr(res);
            ObservableCollection<ColumnModel> temp = new ObservableCollection<ColumnModel>();
            int i = 0;
            foreach (string s in res.Value.ColumnsNames)
            {
                temp.Add(ColumnToModel(s, um.email, i));
                i++;
            }
            return Tuple.Create(res.Value.emailCreator, temp);
        }
        private ColumnModel ColumnToModel(string s, string email, int n)
        {
            return new ColumnModel(this, s, email, n);
        }

        public UserModel Login(string email, string password)
        {
            Response<User> res = s.Login(email, password);
            isErr(res);
            return new UserModel(this, res.Value.Email);

        }
        public void register(string email, string password, string nickname, string host)
        {
            Response res;
            if (host == null || host == "")
            {
                res = s.Register(email, password, nickname);
            }
            else
            {
                res = s.Register(email, password, nickname, host);
            }
            if (res.ErrorOccured) throw new Exception(res.ErrorMessage);
        }
        public void Reset()
        {
            Response res = s.DeleteData();
            if (res.ErrorOccured) throw new Exception(res.ErrorMessage);
        }
        private void isErr(Response res)
        {
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }
        public void changeColumnName(int index, string newname, string email)
        {
            Response res = s.ChangeColumnName(email, index, newname);
            isErr(res);
        }
        public void changeColumnLimit(int index, int newLimit, string email)
        {
            Response res = s.LimitColumnTasks(email, index, newLimit);
            isErr(res);
        }
        public void MoveLeft(string email, int ind)
        {
            Response res = s.MoveColumnLeft(email, ind);
            isErr(res);
        }
        public void MoveRight(string email, int ind)
        {
            Response res = s.MoveColumnRight(email, ind);
            isErr(res);
        }
    }
}
