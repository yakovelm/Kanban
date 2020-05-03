using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    class UserCtrl : DALCtrl<User>
    {
        protected override DalObject<User> convert(SQLiteDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
