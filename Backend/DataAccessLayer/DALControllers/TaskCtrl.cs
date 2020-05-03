using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    class TaskCtrl : DALCtrl<Task>
    {
        protected override DalObject<Task> convert(SQLiteDataReader reader)
        {
            throw new NotImplementedException();
        }

        //--SELECT--
        //select will need to pull the task data using:
        //SELECT * FROM tasks WHERE {CID}={thisColumnID}
        //should be only 1 and return it

    }
}
