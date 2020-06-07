using KanbanUI.Model;
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
        public BoardModel BM;
        public UserModel UM;
        public ObservableCollection<ColumnModel> Columns { get; set; } 
        

        private string _name;
        public string Name { get => _name;set {  _name = value; RaisePropertyChanged("Name"); } }
        private string _host;
        public string Host { get => _host; set { _host = value; RaisePropertyChanged("Host"); } }
        

        public BoardViewModel(UserModel um)
        {
            UM = um;
            BM = new BoardModel(UM.Controller,um.email);
            Name = "Logged in as: "+UM.email;
            Host = "Board hosted by: " + ((BM.host==null) ? "you" : BM.host);
            Columns = new ObservableCollection<ColumnModel>();
            Columns.Clear();
        }

        internal void changeName(object text)
        {
            throw new NotImplementedException();
        }

        public void LoadColumns()
        {
            foreach (string s in BM.columnNames)
            {
                Columns.Add(new ColumnModel(UM.Controller, UM.email,s, _host));
            }
        }
   

    }
}
