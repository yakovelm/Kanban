using KanbanUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.ViewModel
{
    public class BoardViewModel: NotifiableObject
    {
        BoardModel BM;
        UserModel UM;
        ColumnModel current;

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
        }

        public void LoadColumn(string columnName)
        {
            current = new ColumnModel(UM.Controller,UM.email,columnName);
        }
    }
}
