using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    abstract class DalObject<T> where T:DalObject<T>
    {

        protected abstract string toJson();
        protected abstract T fromJson(string filename);


    }
}
