using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Board // mainly just a small transitional class to load the tasks an columns
    {
        public List<Column> columns { get; set; }
        public string Email { get; set; }
        public Board(string email)
        {
            Email = email;
        }

        public void LoadData()
        {
            Column temp = new Column();
            columns = temp.GetAllColumns(Email);
        }

    }
}
