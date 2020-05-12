using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    public abstract class DALCtrl<T> where T : DalObject<T>
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly string connectionString;
        protected readonly string tableName;
        protected readonly string DB= "KanbanDB.db";

        public DALCtrl(string tableName)
        {
            this.tableName = tableName;
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), DB));
            this.connectionString = $"Data Source={path}; Version=3;";
        }


        public bool Delete(string Filter)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {tableName} {Filter}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    /////////////////////////////////////////////////////////////////////////
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }

        public bool Update(string Filter, string attributeName, string attributeValue)
        {
            log.Debug("in update.");
            int res = -1;
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {tableName} SET [{attributeName}]=@{attributeName} {Filter}"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    ///////////////////////////////////////////////////////////////////////////
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public bool Update(string Filter, string attributeName, long attributeValue)
        {
            //log.Debug("update ord with: " +Filter+" "+attributeName+" "+attributeValue);
            int res = -1;
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {tableName} SET [{attributeName}]=@{attributeName} {Filter}"
                };
                log.Debug(command.CommandText);
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res=command.ExecuteNonQuery();
                }
                catch (Exception e){
                    /////////////////////////////////////////////////////////////////////////
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }

        public List<T> Select(string Filter)
        {
            List<T> results = new List<T>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {tableName} {Filter}";
                log.Debug(command.CommandText);
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));

                    }
                }
                catch (Exception e){/////////////////////////////////////////////////////////
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
            log.Debug("results len: " + results.Count());
            return results;
        }
        protected abstract T ConvertReaderToObject(SQLiteDataReader reader);
        public abstract bool Insert(T obj);

    }
}
