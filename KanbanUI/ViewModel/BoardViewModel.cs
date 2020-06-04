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

        private string _name;
        public string Name { get => _name;set {  _name = value; RaisePropertyChanged("Name"); } }

        

        public BoardViewModel(UserModel um)
        {
            UM = um;
            BM = new BoardModel(UM.Controller);
            Name = UM.email;
        }
    }
}
