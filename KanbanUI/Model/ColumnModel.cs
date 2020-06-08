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
        private string _name;
        public string ColumnName { get=>_name; set {  _name = value; RaisePropertyChanged("ColumnName"); }  }
        private int _limit;
        public int ColumnLimit { get => _limit; set { _limit = value; RaisePropertyChanged("ColumnLimit"); } }
        public UserModel UM;
        public ObservableCollection<TaskModel> tasks;
        public ColumnModel(BackendController c,string Name,UserModel um): base(c)
        {
            UM = um;
            Tuple<string,int, ObservableCollection<TaskModel>> col = Controller.getColumn(UM.email,Name);
            ColumnName = col.Item1;
            ColumnLimit = col.Item2;
            tasks = col.Item3;
        }
    }
}
