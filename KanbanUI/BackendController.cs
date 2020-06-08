using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using KanbanUI.Model;

namespace KanbanUI
{
    public class BackendController
    {
        private Service s; 
        public BackendController() {
            s = new Service();
        }

        public Tuple<string,int, ObservableCollection<TaskModel>> getColumn(string email,string name)
        {
            Response<Column> res = s.GetColumn(email,name);
            isErr(res);
            ObservableCollection<TaskModel> tasks= new ObservableCollection<TaskModel>();
            foreach(IntroSE.Kanban.Backend.ServiceLayer.Task t in res.Value.Tasks){
                tasks.Add(taskToModel(t,email));
            }
            return Tuple.Create(res.Value.Name, res.Value.Limit,tasks);
        }
        private TaskModel taskToModel(IntroSE.Kanban.Backend.ServiceLayer.Task t,string email)
        {
            return new TaskModel(this,email,t.Title,t.Description,t.DueDate,t.CreationTime);
        }
        public Tuple<string, ObservableCollection<ColumnModel>> getBoard(UserModel um)
        {
            Response<Board> res = s.GetBoard(um.email);
            isErr(res);
            ObservableCollection<ColumnModel> temp = new ObservableCollection<ColumnModel>();
            foreach (string s in res.Value.ColumnsNames) 
            {
                temp.Add(ColumnToModel(s,um));
            }
            return Tuple.Create(res.Value.emailCreator, temp);
        } 
        private ColumnModel ColumnToModel(string s,UserModel um) 
        {
            return new ColumnModel(this,s, um);
        }

        public UserModel Login(string email, string password)
        {
            Response<User> res = s.Login(email,password) ;
            isErr(res);
            return new UserModel(this, res.Value.Email);
            
        }
        public void register(string email,string password,string nickname,string host)
        {
            Response res;
            if (host == null||host=="")
            {
                res=s.Register(email, password, nickname);
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
    }
}
