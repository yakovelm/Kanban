using System;
using System.Collections.ObjectModel;

namespace KanbanUI.Model
{
    public class BoardModel : NotifiableModelObject
    {
        public bool isSorted { get; set; }
        public UserModel UM;
        public string host;
        public ObservableCollection<ColumnModel> Columns { get; set; }
        public BoardModel(UserModel um) : base(um.Controller)
        {
            UM = um;
            Tuple<string, ObservableCollection<ColumnModel>> board = Controller.getBoard(um);
            host = board.Item1;
            Columns = board.Item2;
            isSorted = false;
        }

        internal void ReLoad()
        {
            Columns = Controller.getBoard(UM).Item2;
            Sort();
            RaisePropertyChanged("columns");
        }
        internal void Sort()
        {
            if (isSorted)
            {
                foreach (ColumnModel cm in Columns)
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
            ColumnModel col = Controller.AddColumn(UM.email, Int32.Parse(Index), Name);
            Columns.Insert(col.Index, col);
            int i = 0;
            foreach (ColumnModel cm in Columns)
            {
                cm.Index = i;
                i++;
            }
            RaisePropertyChanged("columns");
        }


        internal void AdvanceTask(int columnIndex, int ID, TaskModel T)
        {
            Controller.AdvanceTask(UM.email, columnIndex, ID);
            Columns[columnIndex].tasks.Remove(T);
            Columns[columnIndex].Reload();
            Columns[columnIndex + 1].tasks.Add(T);
            Columns[columnIndex + 1].Reload();
            Sort();
            RaisePropertyChanged("columns");
        }

        internal void Filter(string filter)
        {
            foreach (ColumnModel c in Columns)
            {
                c.Filter(filter);
            }
        }

        internal void Unsort()
        {
            foreach (ColumnModel c in Columns)
            {
                c.isSorted = false;
            }
            ReLoad();
        }
    }
}
