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
                    command.CommandText = $"INSERT INTO {UserTableName} ({User.EmailAtt},{User.passwordAtt}," +
                        $"{User.nicknameAtt},{User.emailHostAtt}) " +
                        $"VALUES (@emailVal,@passVal,@nickVal,@emailHostVal);";
                    
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", obj.Email);
                    SQLiteParameter passParam = new SQLiteParameter(@"passVal", obj.password);
                    SQLiteParameter nickParam = new SQLiteParameter(@"nickVal", obj.nickname);
                    SQLiteParameter emailHostParam = new SQLiteParameter(@"emailHostVal", obj.emailHost);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passParam);
                    command.Parameters.Add(nickParam);
                    command.Parameters.Add(emailHostParam);
                    
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    fail = true;
                    Console.WriteLine(e.Message);
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
            User result = new User(reader.GetInt64(0), reader.GetString(1), reader.GetString(2),reader.GetString(3),reader.GetInt64(4));
            return result;
        }
    }
}
