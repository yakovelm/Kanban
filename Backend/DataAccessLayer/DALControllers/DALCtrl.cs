using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    abstract class DALCtrl<T> where T:DalObject<T>
    {
        public bool Delete(DalObject<T> obj)
        {
            return false;
        }
        public List <DalObject<T>> Select()
        {
            return null;
        }
        public bool Update(int ID)
        {
            return false;
        }
        public bool Update(string val)
        {
            return false;
        }
        protected abstract DalObject<T> convert(SQLiteDataReader reader);
    }
}
