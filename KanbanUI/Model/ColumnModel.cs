using KanbanUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    public class ColumnModel: NotifiableModelObject
    {
        public int Index { get => _index; set { _index = value; RaisePropertyChanged("Index"); } }
        private int _index;
        private string _name;
        private string email;
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


        public ObservableCollection<TaskModel> tasks;
       
        public ColumnModel(BackendController c,string Name,string email,int n): base(c)
        {
            Tuple<string,int, ObservableCollection<TaskModel>> col = Controller.getColumn(email,Name);
            _name = col.Item1;
            _limit = col.Item2;
            tasks = col.Item3;
            this.email = email;
            Index = n;
        }

        private Boolean changename(string newname) 
        {
            try
            {
                Controller.changeColumnName(Index, newname, email);
                return true;
            }
            catch { return false; }
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
