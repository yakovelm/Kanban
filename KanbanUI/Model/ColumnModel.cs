using KanbanUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    public class ColumnModel : NotifiableModelObject
    {
        public int Index { get => _index; set { _index = value; RaisePropertyChanged("Index"); } }
        private int _index;
        private string _name;
        private string email;
        public MyICommand DeleteTaskClick { get; set; }
       
        public bool isSorted {get;set ;}
        public string ColumnName { get=>_name; set {
                if (changename(value)){
                    _name = value; }
                RaisePropertyChanged("ColumnName");
            }  }
        private int _limit;
        public string ColumnLimit
        {
            get => _limit.ToString(); set
            {
                if (changeLimit(value))
                {
                    _limit = Int32.Parse(value);
                    RaisePropertyChanged("ColumnLimit");
                }
            }
        }
        private string _message;
        public string Message
        {
            get => _message; set
            {
                _message = value; RaisePropertyChanged("Message");
            }
        }

        internal void Reload()
        {
            tasks=Controller.getColumn(email,ColumnName,Index).Item3;
            Sort(); 
            RaisePropertyChanged("tasks");
        }

        internal void Sort()
        {
            if (isSorted)
            {
                ObservableCollection<TaskModel> temp = new ObservableCollection<TaskModel>();
                for (int i = 0; i < tasks.Count; i++)
                {
                    var a = tasks[i];
                    for (int j = i + 1; j < tasks.Count; j++)
                    {
                        if (DateTime.Parse(a.Due) > DateTime.Parse(tasks[j].Due))
                        {
                            tasks[i] = tasks[j];
                            tasks[j] = a;
                            a = tasks[i];
                        }
                    }
                }
                isSorted = true;
                RaisePropertyChanged("tasks");
            }
        }


        public ObservableCollection<TaskModel> tasks { get; set; }

        
        public ColumnModel(BackendController c,string Name,string email,int n): base(c)
        {
            Tuple<string,int, ObservableCollection<TaskModel>> col = Controller.getColumn(email,Name,n);
            DeleteTaskClick = new MyICommand(OnDeleteTaskClick);
            _name = col.Item1;
            _limit = col.Item2;
            tasks = col.Item3;
            this.email = email;
            Index = n;
            isSorted = false;
        }
        private void OnDeleteTaskClick(object p)
        {
            TaskModel T = (TaskModel)p;
            Message = "";
            try
            {
                Controller.DeleteTask(email, Index, T.ID);
                tasks.Remove(T);
                RaisePropertyChanged("tasks");
                Message = "task: " + T.Title + " was deleted.";
            }
            catch (Exception e)
            {
                Message = "task: " + T.Title + " was not deleted due to: " + e.Message;
            }
        }

        private Boolean changename(string newname) 
        {
            Message = "";
            try
            {
                Controller.changeColumnName(Index, newname, email);
                Message = "Namecolumn: "+newname+" update";
                return true;
            }
            catch(Exception e) 
            {
                Message = e.Message;
                return false; 
            }
        }

        internal void Delete()
        {
            throw new NotImplementedException();
        }

        private Boolean changeLimit(string newLimit)
        {
            try
            {
                Controller.changeColumnLimit(Index,Int32.Parse(newLimit), email);
                return true;
            }
            catch { return false; }
        }
    }
}
