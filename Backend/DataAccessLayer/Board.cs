using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Board // mainly just a small transitional class to load the tasks an columns
    {
        public List<Column> columns { get; set; }
        public int Host { get; set; }
        public Board(int Host)
        {
            this.Host = Host;
        }

        public void LoadData()
        {
            Column temp = new Column();
            columns = temp.GetAllColumns(Host);
        }

    }
}
