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
    { // this class holds the majority of constants and creates the initial DB
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public const string FirstTableName = "users";
        public const string SecondTableName = "columns";
        public const string ThirdTableName = "tasks";
        public const string UserDBName1 = "email";
        public const string UserDBName2 = "pw";
        public const string UserDBName3 = "nickname";
        public const string UserDBName4 = "emailHost";
        public const string UserDBName5 = "UID";
        public const string TaskDBName1 = "boardHost";
        public const string TaskDBName2 = "TID";
        public const string TaskDBName3 = "assignee";
        public const string TaskDBName4 = "Cname";
        public const string TaskDBName5 = "title";
        public const string TaskDBName6 = "description";
        public const string TaskDBName7 = "dueDate";
        public const string TaskDBName8 = "creationDate";
        public const string ColumnDBName1 = "Host";
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
        public void Build() // build a new DB
        {
            bool ex = false;
            SQLiteConnection connection;
            using (connection = new SQLiteConnection(connection_string))
            {
                try
                {
                    connection.Open();
                    createUserTable(connection);
                    log.Debug("created UserTable");
                    createColumnTable(connection);
                    log.Debug("created ColumnTable");
                    createTaskTable(connection);
                    log.Debug("created TaskTable");
                    connection.Close();
                }
                catch (Exception)
                {
                    log.Error("Failed to create new DataBase");
                    ex = true;
                }
                finally 
                { 
                    if(ex) { throw new Exception("Failed to create new DataBase"); }
                    connection.Close();
                }
            }
        }

        
        private void createUserTable(SQLiteConnection connection) // build user table
        {
            string createTableQuery = $@"CREATE TABLE [{FirstTableName}]([{UserDBName5}] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [{UserDBName1}] TEXT NOT NULL UNIQUE, [{UserDBName2}] TEXT NOT NULL,[{UserDBName3}] TEXT NOT NULL,[{UserDBName4}] INTEGER NOT NULL)";
            SQLiteCommand c = new SQLiteCommand(connection);
            c.CommandText = createTableQuery;
            c.ExecuteNonQuery();
        }
        private void createColumnTable(SQLiteConnection connection) // build column table
        {
            string createTableQuery = $@"CREATE TABLE [{SecondTableName}]([{ColumnDBName1}] INTEGER NOT NULL,[{ColumnDBName2}] TEXT NOT NULL,[{ColumnDBName3}] INTEGER NOT NULL,[{ColumnDBName4}] INTEGER NOT NULL,PRIMARY KEY(`{ColumnDBName1}`,`{ColumnDBName2}`), FOREIGN KEY('{ColumnDBName1}') REFERENCES '{FirstTableName}'('{UserDBName4}'))";
            SQLiteCommand c = new SQLiteCommand(connection);
            c.CommandText = createTableQuery;
            c.ExecuteNonQuery();
        }
        private void createTaskTable(SQLiteConnection connection) // build task table
        {
            string createTableQuery = $@"CREATE TABLE [{ThirdTableName}]([{TaskDBName1}] INTEGER NOT NULL ,[{TaskDBName2}] INTEGER NOT NULL,[{TaskDBName3}] TEXT NOT NULL,[{TaskDBName4}] TEXT NOT NULL,[{TaskDBName5}] TEXT NOT NULL,[{TaskDBName6}] TEXT,[{TaskDBName7}] INTEGER NOT NULL,[{TaskDBName8}] INTEGER NOT NULL,
            PRIMARY KEY(`{TaskDBName1}`,`{TaskDBName2}`),FOREIGN KEY('{TaskDBName3}') REFERENCES '{FirstTableName}'('{UserDBName1}'),FOREIGN KEY('{TaskDBName1}') REFERENCES '{SecondTableName}'('{ColumnDBName1}') )";
            SQLiteCommand c = new SQLiteCommand(connection);
            c.CommandText = createTableQuery;
            c.ExecuteNonQuery();
        }	
        private void drop(string s) // drop all tables and rebuild DataBase from nothing
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
                catch (Exception)
                {
                    log.Error("fail to delete from " + s);
                    ex = true;                    
                }
                finally
                {
                    if (ex) { throw new Exception("fail to delete from " + s); }
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
            catch(Exception) { }

            try { drop(SecondTableName);

            }
            catch (Exception) { }
            try { drop(ThirdTableName);
            }
            catch (Exception) { }
            Build();
        }

    }
}
