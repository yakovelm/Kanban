using KanbanUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.ViewModel
{
    class BoardViewModel: NotifiableObject
    {
        BoardModel BM;
        UserModel UM;
        BoardViewModel(UserModel um)
        {
            UM = um;
            BM = new BoardModel(UM.Controller);
        }
    }
}
