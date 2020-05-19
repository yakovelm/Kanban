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
        private DAL.DB db;
        public DB() 
        {
            db = new DAL.DB();
        }
        public void DBexist() // make sure DB exists during startup
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
        }
        private bool sqlfilexist()
        {
            string check =Path.Combine( Directory.GetCurrentDirectory(),DAL.DB.database_name);
            return File.Exists(check);
        }
        public void DropAll()
        {
            db.DropAll();
        }
    }
}
