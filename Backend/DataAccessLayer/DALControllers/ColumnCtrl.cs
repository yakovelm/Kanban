using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    class ColumnCtrl : DALCtrl
    {
        protected override ColumnDTO convert(SQLiteDataReader reader)
        {
            throw new NotImplementedException();
        }
        //--SELECT--
        //select will need to first pull the column data using:
        //SELECT * FROM columns WHERE {CID}={thisColumnID}
        //should be only 1 and return it
        //it will then need to pull the task data using:
        //SELECT * FROM tasks WHERE {CID}={thisColumnID}
        //should be a list and return it
        //issue is differentiatin between the 2 pulls
    }
}
