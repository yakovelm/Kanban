using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    internal class ColumnCtrl : DALCtrl<Column>
    {
        private const string ColumnTableName = "columns";

        public ColumnCtrl() : base(ColumnTableName)
        {
        }
        public List<Column> SelectAllColumn(string Filter)
        {
            List<Column> result = Select(Filter).Cast<Column>().ToList();
            return result;
        }


        protected override Column ConvertReaderToObject(SQLiteDataReader reader)
        {
            Column result = new Column(reader.GetString(0), reader.GetString(1), reader.GetInt64(2), reader.GetInt64(3));
            return result;
        }
        public override bool Insert(Column c)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({Column.EmailAtt} ,{Column.NameAtt}," +
                        $"{Column.OrdAtt},{Column.LimitAtt}) " +
                        $"VALUES (@emailVal,@nameVal,@ordVal,@limitVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", c.Email);
                    SQLiteParameter CnameParam = new SQLiteParameter(@"nameVal", c.Cname);
                    SQLiteParameter OrdParam = new SQLiteParameter(@"ordVal", c.Ord);
                    SQLiteParameter LimitParam = new SQLiteParameter(@"limitVal", c.Limit);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(CnameParam);
                    command.Parameters.Add(OrdParam);
                    command.Parameters.Add(LimitParam);
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


        //--SELECT--
        //select will need to first pull the column data using:
        //SELECT * FROM columns WHERE {CID}={thisColumnID}
        //should be only 1 and return it
        //it will then need to pull the task data using:
        //SELECT * FROM tasks WHERE {CID}={thisColumnID}
        //should be a list and return it
        //issue is differentiatin between the 2 pulls
    }
}
