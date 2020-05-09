﻿using System;
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
        public DB() 
        {
        }
        public bool DBexist()
        {
            if (!sqlfilexist())
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
            string check = Directory.GetCurrentDirectory() + $"\\{database_name}";
            return File.Exists(database_name);
        }
    }
}
