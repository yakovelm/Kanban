using KanbanUI.Utils;
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
        public bool isSorted { get  ; set; }
        public UserModel UM;
        public string host;
        public ObservableCollection<ColumnModel> columns { get; set; }
        public BoardModel(UserModel um): base(um.Controller)
        {
            UM = um;
            Tuple<string, ObservableCollection<ColumnModel>> board=Controller.getBoard(um);
            host = board.Item1;
            columns = board.Item2;
            isSorted = false;
        }

        internal void ReLoad()
        {
            columns = Controller.getBoard(UM).Item2;
            Sort();
            RaisePropertyChanged("columns");
        }
        internal void Sort()
        {
            if (isSorted)
            {
                foreach (ColumnModel cm in columns)
                {
                    cm.isSorted = true;
                    cm.Sort();
                }
                RaisePropertyChanged("columns");
                isSorted = true;
            }
        }
        internal void Delete(ColumnModel p)
        {
            Controller.DeleteColumn(UM.email, p.Index);
            ReLoad();
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

        internal void DeleteTask(int columnIndex, int ID,TaskModel T)
        {
            Controller.DeleteTask(UM.email, columnIndex, ID);
            columns[columnIndex].tasks.Remove(T);
            RaisePropertyChanged("columns");
        }

        internal void AdvanceTask(int columnIndex, int ID,TaskModel T)
        {
            Controller.AdvanceTask(UM.email, columnIndex, ID);
            columns[columnIndex].tasks.Remove(T);
            columns[columnIndex].Reload();
            columns[columnIndex + 1].tasks.Add(T);
            columns[columnIndex+1].Reload();
            Sort();
            RaisePropertyChanged("columns");
        }
    }
}
