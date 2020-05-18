using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class DB
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public const string FirstTableName = "users";
        public const string SecondTableName = "columns";
        public const string ThirdTableName = "tasks";
        public const string UserDBName1 = "email";
        public const string UserDBName2 = "password";
        public const string UserDBName3 = "nickname";
        public const string TaskDBName1 = "email";
        public const string TaskDBName2 = "TID";
        public const string TaskDBName3 = "Cname";
        public const string TaskDBName4 = "title";
        public const string TaskDBName5 = "description";
        public const string TaskDBName6 = "dueDate";
        public const string TaskDBName7 = "creationDate";
        public const string ColumnDBName1 = "email";
        public const string ColumnDBName2 = "Cname";
        public const string ColumnDBName3 = "Ord";
        public const string ColumnDBName4 = "lim";
        public const string database_name ="KanbanDB.db";
        private string connection_string;

        public DB() 
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), database_name));
            connection_string = $"Data Source={path};Version=3;";
            
        }
        public void Build()
        {
            SQLiteConnection connection;
            using (connection = new SQLiteConnection(connection_string))
            {
                try
                {
                    connection.Open();
                    createUserTable(connection);
                    createColumnTable(connection);
                    createTaskTable(connection);
                    connection.Close();
                }
                catch (Exception e)
                {
                    log.Error("Failed to retrieve Data from the DataBase");
                    throw new Exception(e.Message);
                }
                finally { connection.Close(); }
            }
        }
        private void createUserTable(SQLiteConnection connection)
        {
            string createTableQuery = $@"CREATE TABLE [{FirstTableName}]([{UserDBName1}] TEXT NOT NULL ,[{UserDBName2}] TEXT NOT NULL,[{UserDBName3}] TEXT NOT NULL, PRIMARY KEY(`{UserDBName1}`))";
            SQLiteCommand c = new SQLiteCommand(connection);
            c.CommandText = createTableQuery;
            c.ExecuteNonQuery();
        }
        private void createColumnTable(SQLiteConnection connection)
        {
            string createTableQuery = $@"CREATE TABLE [{SecondTableName}]([{ColumnDBName1}] TEXT NOT NULL,[{ColumnDBName2}] TEXT NOT NULl,[{ColumnDBName3}] INTEGER NOT NULL,[{ColumnDBName4}] INTEGER NOT NULL,PRIMARY KEY(`{ColumnDBName1}`,`{ColumnDBName2}`))";
            SQLiteCommand c = new SQLiteCommand(connection);
            c.CommandText = createTableQuery;
            c.ExecuteNonQuery();
        }
        private void createTaskTable(SQLiteConnection connection)
        {
            string createTableQuery = $@"CREATE TABLE [{ThirdTableName}]([{TaskDBName1}] TEXT NOT NULL ,[{TaskDBName2}] INTEGER NOT NULL,[{TaskDBName3}] TEXT NOT NULL,[{TaskDBName4}] TEXT NOT NULL,[{TaskDBName5}] TEXT,[{TaskDBName6}] INTEGER NOT NULL,[{TaskDBName7}] INTEGER NOT NULL, PRIMARY KEY(`{TaskDBName1}`,`{TaskDBName2}`))";
            SQLiteCommand c = new SQLiteCommand(connection);
            c.CommandText = createTableQuery;
            c.ExecuteNonQuery();
        }
        private void drop(string s)
        {

            using (var connection = new SQLiteConnection(connection_string))
            {
                bool ex = false;
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DROP TABLE {s}"
                };

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("fail to delete from " + s);
                    ex = true;
                    throw new Exception("fail to delete from " + s);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
        public void DropAll()
        {
            log.Debug("DB DAL");
            try { drop(FirstTableName);
            }
            catch(Exception e) { }

            try { drop(SecondTableName);

            }
            catch (Exception e) { }
            try { drop(ThirdTableName);
            }
            catch (Exception e) { }
            Build();
        }

    }
}
