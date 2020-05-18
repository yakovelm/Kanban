using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class DB
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool Load;
        private DAL.DB db;
        public DB() 
        {
            Load = false;
            db = new DAL.DB();
        }

        public void DBexist()
        {
            try
            {
                if (!sqlfilexist())
                {
                    log.Debug("create SQL file");
                    db.Build();
                }
            }
            catch (Exception e)
            {
                log.Error("fail to create SQL file: " + e.Message);
                throw new Exception(e.Message);
            }
            Load = true;
        }
        private bool sqlfilexist()
        {
            string check =Path.Combine( Directory.GetCurrentDirectory(),DAL.DB.database_name);
            return File.Exists(check);
        }
        public bool IsLoad()
        {
            return Load;
        }
        public void DropAll()
        {
            log.Debug("DB BL");
            db.DropAll();
        }
    }
}
