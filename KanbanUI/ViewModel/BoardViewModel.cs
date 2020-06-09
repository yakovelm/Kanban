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
        public BoardModel BM { get; set; }
        public UserModel UM;
        

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
        }

    }
}
