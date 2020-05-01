using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Board : DalObject<Board>
    {
        public string email { get; set; }
        public List<string> colums { get; set; }

        public Board(string email) { this.email = email; }
        public Board(string email, List<string> columns)
        {
            this.email = email;
            this.colums = colums;
        }
        public override Board fromJson(string filename)
        {
            throw new NotImplementedException();
        }

        public override string toJson()
        {
            throw new NotImplementedException();
        }
    }
}
