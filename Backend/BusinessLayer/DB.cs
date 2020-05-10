using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class DB
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string database_name = "KanbanDB.db";
        private bool ExistDB;
        private bool Load;
        public DB() 
        {
            Load = false;
            ExistDB = sqlfilexist();
        }
        public void ExistDataBase()
        {
            if(!ExistDB)
            {
                log.Warn("Cannot delete non-existent Data.");
                throw new Exception("Cannot delete non-existent Data.");
            }
        }
        public bool DBexist()
        {
            if (ExistDB)
            {
                try
                {
                    log.Debug("create SQL file");
                    DataAccessLayer.DB db = new DataAccessLayer.DB(database_name);
                    return false;
                }
                catch (Exception e)
                {
                    log.Error("fail to create SQL file: " + e.Message);
                    throw new Exception(e.Message);
                }
            }
            return true;
        }
        private bool sqlfilexist()
        {
            string check = Directory.GetCurrentDirectory() + $"//{database_name}";
            return File.Exists(check);
        }
        public void LoadData()
        {
            Load = true;
        }
        public void IsLoad()
        {
            if (Load)
            {
                log.Warn("the Data already loaded.");
                throw new Exception("the Data already loaded.");
            }
        }
    }
}
