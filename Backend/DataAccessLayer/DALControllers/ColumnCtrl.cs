using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    internal class ColumnCtrl : DALCtrl<Column>
    {
        private const string ColumnTableName = DAL.DB.SecondTableName;
        public ColumnCtrl() : base(ColumnTableName) { }
        protected override Column ConvertReaderToObject(SQLiteDataReader reader)
        {
            Column result = new Column(reader.GetString(0), reader.GetString(1), reader.GetInt64(2), reader.GetInt64(3));
            return result;
        }
        public override bool Insert(Column c) // insert given DAL colum into DB
        {
            bool fail = false;
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({Column.EmailAtt} ,{Column.NameAtt}," +
                        $"{Column.OrdAtt},{Column.LimitAtt}) " +
                        $"VALUES (@emailVal,@nameVal,@ordVal,@limitVal)";
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
                catch (Exception)
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
                return res > 0;
            }
        }
    }
}
