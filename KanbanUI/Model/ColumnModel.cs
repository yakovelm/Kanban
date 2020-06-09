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
        public Boolean isHost { get => _ishost; set { _ishost = value; RaisePropertyChanged("isHost"); } }
        private Boolean _ishost;
        public int Index { get => _index; set { _index = value; RaisePropertyChanged("Index"); } }
        private int _index;
        private string _name;
        public string ColumnName { get=>_name; set {
                if (changename(value)){
                    _name = value; }
                RaisePropertyChanged("ColumnName");
            }  }
        private int _limit;
        public int ColumnLimit { get => _limit; set { _limit = value; RaisePropertyChanged("ColumnLimit"); } }
        public UserModel UM;
        public ObservableCollection<TaskModel> tasks;
       
        public ColumnModel(BackendController c,string Name,UserModel um,int n,string host): base(c)
        {
            UM = um;
            Tuple<string,int, ObservableCollection<TaskModel>> col = Controller.getColumn(UM.email,Name);
            ColumnName = col.Item1;
            ColumnLimit = col.Item2;
            tasks = col.Item3;
            Index = n;
            isHost = UM.email == host;
        }
        private Boolean changename(string newname) 
        {
            try
            {
                Controller.changeColumnName(Index, newname, UM.email);
                return true;
            }
            catch { return false; }
        }
    }
}
