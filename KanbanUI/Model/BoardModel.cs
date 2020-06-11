using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    public class BoardModel: NotifiableModelObject
    {
        public UserModel UM;
        public string host;
        public ObservableCollection<ColumnModel> columns { get; set; }
        public BoardModel(UserModel um): base(um.Controller)
        {
            UM = um;
            Tuple<string, ObservableCollection<ColumnModel>> board=Controller.getBoard(um);
            host = board.Item1;
            columns = board.Item2;
        }

        internal void ReLoad()
        {
            columns = Controller.getBoard(UM).Item2;
            RaisePropertyChanged("columns");
        }

        internal void Delete(ColumnModel p)
        {
            Controller.DeleteColumn(UM.email, p.Index);
            columns.Remove(p);
            int i = 0;
            foreach (ColumnModel cm in columns)
            {
                cm.Index = i;
                i++;
            }
            RaisePropertyChanged("columns");
        }

        internal void MoveRight(ColumnModel p)
        {
            Controller.MoveRight(UM.email, p.Index);
            ReLoad();
        }
        internal void MoveLeft(ColumnModel p)
        {
            Controller.MoveLeft(UM.email, p.Index);
            ReLoad();
        }

        internal void Add(string Index, string Name)
        {
            ColumnModel col=Controller.AddColumn(UM.email, Int32.Parse(Index), Name);
            columns.Insert(col.Index, col);
            int i = 0;
            foreach(ColumnModel cm in columns){
                cm.Index = i;
                i++;
            }
            RaisePropertyChanged("columns");
        }
       
    }
}
