using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    class BoardModel: NotifiableModelObject
    {
        public string host;
        IReadOnlyCollection<string> columnNames;
        public BoardModel(BackendController c,string email): base(c)
        {
            Tuple<string, IReadOnlyCollection<string>> board=Controller.getBoard(email);
            host = board.Item1;
            columnNames = board.Item2;
        }
    }
}
