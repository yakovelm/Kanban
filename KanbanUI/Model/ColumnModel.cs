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
       // public Boolean isHost { get => _ishost; set { _ishost = value; RaisePropertyChanged("isHost"); } }
     //   private Boolean _ishost;
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
       // public UserModel UM;
        public ObservableCollection<TaskModel> tasks;
       
        public ColumnModel(BackendController c,string Name,string email,int n): base(c)
        {
       //     UM = um;
            Tuple<string,int, ObservableCollection<TaskModel>> col = Controller.getColumn(email,Name);
            _name = col.Item1;
            _limit = col.Item2;
            tasks = col.Item3;
            this.email = email;
            Index = n;
          //  isHost = UM.email == host;
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
        private Boolean changeLimit(string newLimit)
        {
            try
            {
                Controller.changeColumnLimit(Index,Int32.Parse(newLimit), email);
                return true;
            }
            catch { return false; }
        }

        public void moveLeft()
        {
            Console.WriteLine("in moveLeft");
            try
            {
                Controller.MoveLeft(email,Index);
                Index = Index - 1;
            }
            catch {  }
        }
        public void moveRight()
        {
            try
            {
                Controller.MoveRight(email, Index);
                Index = Index + 1;
            }
            catch { }
        }
    }
}
