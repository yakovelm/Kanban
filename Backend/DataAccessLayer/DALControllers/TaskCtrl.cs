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
        private const string TaskTableName = "tasks";
        public TaskCtrl(): base(TaskTableName)
        {

        }
        

        public override bool Insert(Task obj)
        {
            throw new NotImplementedException();
        }



        protected override Task ConvertReaderToObject(SQLiteDataReader reader)
        {
            throw new NotImplementedException();
        }

        //--SELECT--
        //select will need to pull the task data using:
        //SELECT * FROM tasks WHERE {CID}={thisColumnID}
        //should be only 1 and return it

    }
}
