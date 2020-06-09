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
        //public Boolean isHost { get=> _ishost; set { _ishost = value; RaisePropertyChanged("isHost"); } }
        //private Boolean _ishost;
        public BoardModel BM;
        public UserModel UM;
        public ObservableCollection<ColumnModel> Columns { get => BM.columns; set { BM.columns = value; } } 
        

        private string _name;
        public string Name { get => _name;set {  _name = value; RaisePropertyChanged("Name"); } }
        private string _host;
        public string Host { get => _host; set { _host = value; RaisePropertyChanged("Host"); } }
        

        public BoardViewModel(UserModel um)
        {
            UM = um;
            BM = new BoardModel(um);
            Name = "Logged in as: "+UM.email;
            Host = "Board hosted by: " + ((BM.host==UM.email) ? "you" : BM.host);
            //Columns = new ObservableCollection<ColumnModel>();
            //Columns.Clear();
            //isHost = UM.email == BM.host;
            //Console.WriteLine(isHost);
        }

        internal void changeName(string text)
        {
            //if (UM.email.Equals(Host))
            //{ 
            //     = text;

            //}
        }

        //public void LoadColumns()
        //{
        //    foreach (string s in BM.columnNames)
        //    {
        //        Columns.Add(new ColumnModel(UM.Controller, UM.email,s, _host));
        //    }
        //}
   
    }
}
