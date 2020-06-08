using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    public class BoardModel: NotifiableModelObject
    {
        public UserModel UM;
        public string host;
        public ObservableCollection<ColumnModel> columns;
        public BoardModel(UserModel um): base(um.Controller)
        {
            UM = um;
            Tuple<string, ObservableCollection<ColumnModel>> board=Controller.getBoard(um);
            host = board.Item1;
            columns = board.Item2;
        }
    }
}
