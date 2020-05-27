using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL= IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DALControllers
{
    class UserCtrl : DALCtrl<User>
    {
        private const string UserTableName = DAL.DB.FirstTableName;
        public UserCtrl(): base (UserTableName) { }
        public override bool Insert(User obj) //insert a specific user object into the DB
        {
            bool fail = false;
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({User.EmailAtt} ,{User.passwordAtt}," +
                        $"{User.nicknameAtt},{User.emailHostAtt},{User.UIDAtt}) " +
                        $"VALUES (@emailVal,@passVal,@nickVal,@emailHostVal,@UIDVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", obj.Email);
                    SQLiteParameter passParam = new SQLiteParameter(@"passVal", obj.password);
                    SQLiteParameter nickParam = new SQLiteParameter(@"nickVal", obj.nickname);
                    SQLiteParameter emailHostParam = new SQLiteParameter(@"emailHostVal", obj.emailHost);
                    SQLiteParameter UIDParam = new SQLiteParameter(@"UIDVal", obj.UID);


                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passParam);
                    command.Parameters.Add(nickParam);
                    command.Parameters.Add(emailHostParam);
                    command.Parameters.Add(UIDParam);
                    command.Prepare();
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
                    if (fail) { log.Error("fail to insert a user in UserCtrl");
                        throw new Exception("fail to insert a user in UserCtrl");
                    }
                }
                return res > 0;
            }
        }
        protected override User ConvertReaderToObject(SQLiteDataReader reader)
        {
            User result = new User(reader.GetString(0), reader.GetString(1), reader.GetString(2),reader.GetInt64(3),reader.GetInt64(4));
            return result;
        }
    }
}
