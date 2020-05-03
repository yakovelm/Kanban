using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    class BoardCtrl : DALCtrl<Board>
    {
        protected override DalObject<Board> convert(SQLiteDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
