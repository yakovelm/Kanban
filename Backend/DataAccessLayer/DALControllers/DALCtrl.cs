using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using DAL= IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    public abstract class DALCtrl<T> where T : DalObject<T>
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly string connectionString;
        protected readonly string tableName;
        protected readonly string DB= DAL.DB.database_name;

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
                bool fail = false;
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
                    fail = true;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    if (fail)
                    {
                        log.Error("fail to delete from " + tableName);
                        throw new Exception("fail to delete from " + tableName);
                    }
                }
            }
            return res > 0;
        }


        public bool Update(string Filter, string attributeName, string attributeValue)
        {
            bool ex = false;
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
                    log.Error("fail to Update from " + tableName);
                    ex = true;
                    
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    if(ex) throw new Exception("fail to Update from " + tableName);
                }

            }
            return res > 0;
        }

        public bool Update(string Filter, string attributeName, long attributeValue)
        {
            bool ex = false;
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
                    res=command.ExecuteNonQuery();
                }
                catch (Exception e){
                    log.Error("fail to Update from " + tableName);
                    ex = true;
                    
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    if(ex) throw new Exception("fail to Update from " + tableName);
                }

            }
            return res > 0;
        }

        public List<T> Select(string Filter)
        {
            bool ex = false;
            List<T> results = new List<T>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {tableName} {Filter}";
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
                catch (Exception e)
                {
                    log.Error("fail to Select from " + tableName);
                    ex = true;
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                    if(ex) throw new Exception("fail to Select from " + tableName);
                }

            }
            return results;
        }
        protected abstract T ConvertReaderToObject(SQLiteDataReader reader);
        public abstract bool Insert(T obj);

    }
}
