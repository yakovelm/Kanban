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
        public MyICommand LeftClick { get; set; }
        public MyICommand RightClick { get; set; }
        public MyICommand DeleteClick { get; set; }
        public MyICommand AddColumn { get; set; }
        public MyICommand DeleteTaskClick { get; set; }
        public MyICommand AdvanceClick { get; set; }
        public MyICommand SortClick { get; set; }


        public BoardViewModel(UserModel um)
        {
            UM = um;
            BM = new BoardModel(um);
            Name = "Logged in as: "+UM.email;
            Host = "Board hosted by: " + ((BM.host==UM.email) ? "you" : BM.host);
            IsHost = UM.email == BM.host;
            NewColumnName = "name";
            NewColumnIndex = "index";
            LeftClick = new MyICommand(OnLeftClick);
            RightClick = new MyICommand(OnRightClick);
            DeleteClick = new MyICommand(OnDeleteClick);
            AddColumn = new MyICommand(OnAddClick);
            DeleteTaskClick = new MyICommand(OnDeleteTaskClick);
            AdvanceClick = new MyICommand(onAdvanceClick);
            SortClick = new MyICommand(onSortClick);
        }
        private void onSortClick(object p)
        {
            BM.isSorted = true;
            BM.Sort();
        }
        private void onAdvanceClick(object p)
        {
            TaskModel T = (TaskModel)p;
            Message = "";
            try
            {

                BM.AdvanceTask(T.ColumnIndex, T.ID,T);
                Message = "task: " + T.Title + " was advanced.";
            }
            catch (Exception e)
            {
                Message = "task: " + T.Title + " was not advanced due to: " + e.Message;
            }
        }

        private void OnAddClick(object p)
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
        private void OnDeleteTaskClick(object p)
        { 
            TaskModel T = (TaskModel)p;
            Message = "";
            try
            {
                
                BM.DeleteTask(T.ColumnIndex,T.ID,T);
                Message = "task: " +T.Title+ " was deleted.";
            }
            catch (Exception e)
            {
                Message = "task: " + T.Title + "was not deleted due to: " + e.Message;
            }
        }
        private void OnLeftClick(object p)
        {
            Message = "";
            try
            {
                BM.MoveLeft((ColumnModel)p);
                Message = "column: " + ((ColumnModel)p).ColumnName + " moved left.";
            }
            catch (Exception e)
            {
                Message = "column: " + ((ColumnModel)p).ColumnName + " not moved due to: " + e.Message;
            }
        }
        private void OnRightClick(object p)
        {
            Message = "";
            try
            {
                BM.MoveRight((ColumnModel)p);
                Message = "column: " + ((ColumnModel)p).ColumnName + " moved right.";
            }
            catch (Exception e)
            {
                Message = "column: " + ((ColumnModel)p).ColumnName + " not moved due to: " + e.Message;
            }
        }
        private void OnDeleteClick(object p)
        {
            Message = "";
            try
            {
                BM.Delete((ColumnModel)p);
                Message = "column: " + ((ColumnModel)p).ColumnName + " deleted.";
            }
            catch (Exception e)
            {
                Message = "column: " + ((ColumnModel)p).ColumnName + " not deleted due to: " + e.Message;
            }
        }
    }
}
