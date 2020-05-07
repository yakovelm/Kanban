using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    class BoardCtrl
    {
        protected readonly string connectionString;
        protected readonly string tableName="users";
        protected readonly string EmailAtt = "email";
        public BoardCtrl()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "KanbanDB.db"));
            connectionString = $"Data Source={path}; Version=3;";
        }

        public List<string> LoadData()
        {
            List<string> results = new List<string>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT {EmailAtt} FROM {tableName}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(dataReader.GetString(0));
                    }
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }
    }
}
