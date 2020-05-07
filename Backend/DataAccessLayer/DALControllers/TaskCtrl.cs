using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
     class TaskCtrl : DALCtrl<Task>
    {
        private const string TaskTableName = "tasks";
        public TaskCtrl(): base(TaskTableName)
        {

        }
        //public List<Task> SelectAllTasks(string Filter)
        //{
        //    List<Task> result = Select(Filter).Cast<Task>().ToList();
        //    return result;
        //}


        public override bool Insert(Task obj)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({Task.EmailAtt} ,{Task.IDAtt}," +
                        $"{Task.ColumnAtt},{Task.TitleAtt},{Task.DescAtt},{Task.DueAtt},{Task.createAtt}) " +
                        $"VALUES (@emailVal,@IDVal,@CnameVal,@TitleVal,@DescVal,@DueVal,@CreVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", obj.Email);
                    SQLiteParameter IDParam = new SQLiteParameter(@"IDVal", obj.ID);
                    SQLiteParameter CnameParam = new SQLiteParameter(@"CnameVal", obj.Cname);
                    SQLiteParameter TitleParam = new SQLiteParameter(@"TitleVal", obj.Title);
                    SQLiteParameter DescParam = new SQLiteParameter(@"DescVal", obj.Desc);
                    SQLiteParameter DueParam = new SQLiteParameter(@"DueVal", obj.Due);
                    SQLiteParameter CreParam = new SQLiteParameter(@"CreVal", obj.Create);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(IDParam);
                    command.Parameters.Add(CnameParam);
                    command.Parameters.Add(TitleParam);
                    command.Parameters.Add(DescParam);
                    command.Parameters.Add(DueParam);
                    command.Parameters.Add(CreParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
                return res > 0;
            }
        }



        protected override Task ConvertReaderToObject(SQLiteDataReader reader)
        {
            Task result = new Task(reader.GetString(0), reader.GetInt64(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt64(5), reader.GetInt64(6));
            return result;
        }

        //--SELECT--
        //select will need to pull the task data using:
        //SELECT * FROM tasks WHERE {CID}={thisColumnID}
        //should be only 1 and return it

    }
}
