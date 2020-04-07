using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class User : DalObject<User>
    {
        protected override User fromJson(string filename)
        {
            throw new NotImplementedException();
        }

        protected override string toJson()
        {
            throw new NotImplementedException();
        }
    }
}
