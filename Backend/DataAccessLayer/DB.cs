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
        private const string FirstTableName = "users";
        private const string SecondTableName = "columns";
        private const string ThirdTableName = "tasks";
        private const string UserDBName1 = "email";
        private const string UserDBName2 = "password";
        private const string UserDBName3 = "nickname";
        private const string TaskDBName1 = "email";
        private const string TaskDBName2 = "TID";
        private const string TaskDBName3 = "Cname";
        private const string TaskDBName4 = "title";
        private const string TaskDBName5 = "description";
        private const string TaskDBName6 = "dueDate";
        private const string TaskDBName7 = "creationDate";
        private const string ColumnDBName1 = "email";
        private const string ColumnDBName2 = "Cname";
        private const string ColumnDBName3 = "Ord";
        private const string ColumnDBName4 = "lim";

        public DB(string database_name) 
        {
            SQLiteConnection connection;
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), database_name));
            string connetion_string = $"Data Source={path};Version=3;";
            using (connection = new SQLiteConnection(connetion_string))
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
            string createTableQuery = $@"CREATE TABLE [{ThirdTableName}]([{TaskDBName1}] TEXT NOT NULL ,[{TaskDBName2}] INTEGER NOT NULL,[{TaskDBName3}] TEXT NOT NULL,[{TaskDBName4}] TEXT NOT NULL,[{TaskDBName5}] TEXT NOT NULL,[{TaskDBName6}] INTEGER NOT NULL,[{TaskDBName7}] INTEGER NOT NULL, PRIMARY KEY(`TID`,`email`))";
            SQLiteCommand c = new SQLiteCommand(connection);
            c.CommandText = createTableQuery;
            c.ExecuteNonQuery();
        }

    }
}
