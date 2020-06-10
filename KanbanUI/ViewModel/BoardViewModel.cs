using KanbanUI.Model;
using KanbanUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.ViewModel
{
    public class BoardViewModel: NotifiableObject
    {
        public BoardModel BM { get; set; }
        public UserModel UM;
        

        private string _name;
        public string Name { get => _name;set {  _name = value; RaisePropertyChanged("Name"); } }
        private string _host;
        public bool IsHost { get; set; }
        public string Host { get => _host; set { _host = value; RaisePropertyChanged("Host"); } }
        public ColumnModel SelectedColumn { get=>_selectedColumn; 
            set { _selectedColumn = value;
                LeftClick.RaiseCanExecuteChanged();
                RightClick.RaiseCanExecuteChanged();
            }
        }

        internal void logout()
        {
            UM.logout();
        }

        private ColumnModel _selectedColumn;
        public MyICommand LeftClick { get; set; }
        public MyICommand RightClick { get; set; }


        public BoardViewModel(UserModel um)
        {
            UM = um;
            BM = new BoardModel(um);
            Name = "Logged in as: "+UM.email;
            Host = "Board hosted by: " + ((BM.host==UM.email) ? "you" : BM.host);
            IsHost = UM.email == BM.host;
            LeftClick = new MyICommand(OnLeftClick, CanMove);
            RightClick = new MyICommand(OnRightClick, CanMove);
        }

        private void OnLeftClick()
        {
            SelectedColumn.moveLeft();
            BM.ReLoad();

        }
        private void OnRightClick()
        {
            SelectedColumn.moveRight();
            BM.ReLoad();
        }

        private bool CanMove()
        {
            return SelectedColumn != null;
        }

    }
}
