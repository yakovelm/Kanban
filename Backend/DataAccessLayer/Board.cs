using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Board : DalObject<Board>
    {
        public string email { get; set; }
        public List <string> columns { get; set; }

        public Board() { } // json package requires an empty constructor
        public Board(string email,List<string> columns)
        {
            this.email = email;
            this.columns = columns;
        }
        public Board(string email)
        {
            this.email = email;
            this.columns = columns;
        }
        public override Board fromJson(string filename)
        {
            string objectAsJson = read(filename);
            Board temp = JsonSerializer.Deserialize<Board>(objectAsJson);
            this.columns = temp.columns;
            return this;
        }
        public override string toJson()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }
    }
}
