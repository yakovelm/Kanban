using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    interface IPersistentObject<T> where T : DAL.DalObject<T>
    {
        T ToDalObject();
        void Save();
    }
}
