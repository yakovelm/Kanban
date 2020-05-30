using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    class BoardCtrl // this calss is used to load the initial board list upon startup
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly string connectionString;
        protected readonly string tableName="users";
        protected readonly string EmailAtt = "email";
        public BoardCtrl()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "KanbanDB.db"));
            connectionString = $"Data Source={path}; Version=3;";
        }
        public int FindBoard(string s)
        {
            bool fail = false;
            int result=-1;
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT {EmailAtt} FROM {tableName} WHERE { EmailAtt}= '{s}'";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        result =(int) dataReader.GetInt64(4);
                    }
                }
                catch (Exception e)
                {
                    fail = true;
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                    if (fail)
                    {
                        log.Error("fail to delete from " + tableName);
                        throw new Exception("fail to delete from " + tableName);
                    }
                }

            }
            return result;
        }

        public List<Tuple<string,long,long>> LoadData()
        {
            bool fail = false;
            List<Tuple<string, long, long>> results = new List<Tuple<string, long, long>>();
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
                        results.Add(Tuple.Create(dataReader.GetString(0), dataReader.GetInt64(3), dataReader.GetInt64(4)));
                    }
                }
                catch(Exception e)
                {
                    fail = true;
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                    if (fail)
                    {
                        log.Error("fail to delete from " + tableName);
                        throw new Exception("fail to delete from " + tableName);
                    }
                }

            }
            return results;
        }
    }
}
