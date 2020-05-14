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


        public bool Delete(List<Tuple<string,string>> Filter)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(connectionString))
            {
                string s = $"DELETE FROM {tableName} ";
                if (Filter.Count() > 0)
                {
                    s += "WHERE ";
                    foreach (Tuple<string, string> t in Filter)
                    {
                        s += $"{t.Item1}=@{t.Item1} AND ";
                    }
                    s = s.Substring(0, s.Length - 4);
                }
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = s
                };
                try
                {
                    connection.Open();
                    foreach (Tuple<string, string> t in Filter)
                    {
                        command.Parameters.Add(new SQLiteParameter(t.Item1,t.Item2));
                    }
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("fail to delete from " + tableName);
                    throw new Exception("fail to delete from " + tableName);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }

        public bool Update(List<Tuple<string, string>> Filter, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(connectionString))
            {
                string s = $"UPDATE {tableName} SET [{attributeName}]=@{attributeName} ";
                if (Filter.Count() > 0)
                {
                    s += "WHERE ";
                    foreach (Tuple<string, string> t in Filter)
                    {
                        s += $"{t.Item1}=@{t.Item1} AND ";
                    }
                    s = s.Substring(0, s.Length - 4);
                }

                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = s
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    foreach (Tuple<string, string> t in Filter)
                    {
                        command.Parameters.Add(new SQLiteParameter(t.Item1, t.Item2));
                    }
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    log.Error("fail to Update from " + tableName);
                    throw new Exception("fail to Update from " + tableName);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public bool Update(List<Tuple<string, string>> Filter, string attributeName, long attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(connectionString))
            {
                string s = $"UPDATE {tableName} SET [{attributeName}]=@{attributeName} ";
                if (Filter.Count() > 0)
                {
                    s += "WHERE ";
                    foreach (Tuple<string, string> t in Filter)
                    {
                        s += $"{t.Item1}=@{t.Item1} AND ";
                    }
                    s = s.Substring(0, s.Length - 4);
                }
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = s
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    foreach (Tuple<string, string> t in Filter)
                    {
                        command.Parameters.Add(new SQLiteParameter(t.Item1, t.Item2));
                    }
                    res =command.ExecuteNonQuery();
                }
                catch (Exception e){
                    log.Error("fail to Update from " + tableName);
                    throw new Exception("fail to Update from " + tableName);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }

        public List<T> Select(List<Tuple<string, string>> Filter)
        {
            List<T> results = new List<T>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                string s = $"SELECT * FROM {tableName} " ;
                if (Filter.Count() > 0)
                {
                    s += "WHERE ";
                    foreach (Tuple<string, string> t in Filter)
                    {
                        s += $"{t.Item1}=@{t.Item1} AND ";
                    }
                    s = s.Substring(0, s.Length - 4);
                }
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = s;
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    foreach (Tuple<string, string> t in Filter)
                    {
                        command.Parameters.Add(new SQLiteParameter(t.Item1, t.Item2));
                    }
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));

                    }
                }
                catch (Exception e){
                    log.Error("fail to Select from " + tableName);
                    throw new Exception("fail to Select from " + tableName);
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
        protected abstract T ConvertReaderToObject(SQLiteDataReader reader);
        public abstract bool Insert(T obj);

    }
}
