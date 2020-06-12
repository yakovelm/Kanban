﻿using KanbanUI.Model;
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
        }

        private void OnAddClick(object obj)
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
