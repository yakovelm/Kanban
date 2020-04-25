using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    abstract class DalObject<T> where T:DalObject<T>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public abstract string toJson();
        public abstract T fromJson(string filename);

        public void Write(string filename, string toJson) //writes a json file with given string
        {
            string path= Directory.GetCurrentDirectory();
            path = path + "\\" + filename;
            File.Delete(path);
            log.Debug("JSON file deleted at "+path);
            FileStream file = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            StreamWriter sw = new StreamWriter(file);
            sw.Write(toJson);
            log.Debug("JSON file written at " + path);
            sw.Close();
            file.Close();

        }

        public string read(string filename) // reads from json file
        {
            string path = Directory.GetCurrentDirectory();
            path = path + "\\" + filename;
            log.Debug("reading file at " + path);
            FileStream file = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            StreamReader sr = new StreamReader(file);
            string ret=sr.ReadToEnd();
            file.Close();
            return ret;
        }

    }
}
