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

        private string _message;
        public string Message
        {
            get => _message; set
            {
                _message = value; RaisePropertyChanged("Message");
            }
        }

        private string _newColumnName;
        public string NewColumnName { get => _newColumnName; set { _newColumnName = value; RaisePropertyChanged("NewColumnName"); } }
        private string _newColumnIndex;
        public string NewColumnIndex { get => _newColumnIndex; set { _newColumnIndex = value; RaisePropertyChanged("NewColumnIndex"); } }
        private string _name;
        public string Name { get => _name;set {  _name = value; RaisePropertyChanged("Name"); } }
        private string _host;
        public bool IsHost { get; set; }
        public string Host { get => _host; set { _host = value; RaisePropertyChanged("Host"); } }

        internal void AddTask()
        {
            throw new NotImplementedException();
        }

        public TaskModel SelectedTask { get => _selectedTask;
            set { 
                _selectedTask = value;
                RaisePropertyChanged("SelectedTask");
            }
        }
        private TaskModel _selectedTask;

        internal void Reload()
        {
            BM.ReLoad();
        }

        public ColumnModel SelectedColumn { get=>_selectedColumn; 
            set { _selectedColumn = value;
                LeftClick.RaiseCanExecuteChanged();
                RightClick.RaiseCanExecuteChanged();
                DeleteClick.RaiseCanExecuteChanged();
            }
        }

        internal void logout()
        {
            UM.logout();
        }

        private ColumnModel _selectedColumn;
        public ColumnCommand LeftClick { get; set; }
        public ColumnCommand RightClick { get; set; }
        public ColumnCommand DeleteClick { get; set; }
        public EmptyCommand AddColumn { get; set; }
        public TaskCommand AdvanceClick { get; set; }
        public EmptyCommand SortClick { get; set; }


        public BoardViewModel(UserModel um)
        {
            UM = um;
            BM = new BoardModel(um);
            Name = "Logged in as: "+UM.email;
            Host = "Board hosted by: " + ((BM.host==UM.email) ? "you" : BM.host);
            IsHost = UM.email == BM.host;
            NewColumnName = "name";
            NewColumnIndex = "index";
            LeftClick = new ColumnCommand(OnLeftClick);
            RightClick = new ColumnCommand(OnRightClick);
            DeleteClick = new ColumnCommand(OnDeleteClick);
            AddColumn = new EmptyCommand(OnAddClick);
            AdvanceClick = new TaskCommand(onAdvanceClick);
            SortClick = new EmptyCommand(onSortClick);
        }
        private void onSortClick()
        {
            BM.isSorted = true;
            BM.Sort();
        }
        private void onAdvanceClick(TaskModel p)
        {
            Message = "";
            try
            {

                BM.AdvanceTask(p.ColumnIndex, p.ID,p);
                Message = "task: " + p.Title + " was advanced.";
            }
            catch (Exception e)
            {
                Message = "task: " + p.Title + " was not advanced due to: " + e.Message;
            }
        }

        private void OnAddClick()
        {
            Message = "";
            try
            {
                BM.Add(NewColumnIndex,NewColumnName);
                Message = "column: " + NewColumnName + " added at "+NewColumnIndex+".";
            }
            catch (Exception e)
            {
                Message = "column: " + NewColumnName + " added due to: " + e.Message;
            }
        }
        private void OnLeftClick(ColumnModel p)
        {
            Message = "";
            try
            {
                BM.MoveLeft(p);
                Message = "column: " + p.ColumnName + " moved left.";
            }
            catch (Exception e)
            {
                Message = "column: " + p.ColumnName + " not moved due to: " + e.Message;
            }
        }
        private void OnRightClick(ColumnModel p)
        {
            Message = "";
            try
            {
                BM.MoveRight(p);
                Message = "column: " + p.ColumnName + " moved right.";
            }
            catch (Exception e)
            {
                Message = "column: " + p.ColumnName + " not moved due to: " + e.Message;
            }
        }
        private void OnDeleteClick(ColumnModel p)
        {
            Message = "";
            try
            {
                BM.Delete(p);
                Message = "column: " + p.ColumnName + " deleted.";
            }
            catch (Exception e)
            {
                Message = "column: " + p.ColumnName + " not deleted due to: " + e.Message;
            }
        }
    }
}
