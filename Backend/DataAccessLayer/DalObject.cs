using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public abstract class DalObject<T> where T:DalObject<T>
    {

        public abstract string toJson();
        public abstract T fromJson(string filename);

        public void Write(string filename, string toJson)
        {
            string path= Directory.GetCurrentDirectory();
            path = path + "\\" + filename;
            FileStream file = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            StreamWriter sw = new StreamWriter(file);
            sw.Write(toJson);
            sw.Close();
            file.Close();

        }

        public string read(string filename)
        {
            string path = Directory.GetCurrentDirectory();
            path = path + "\\" + filename;
            FileStream file = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            StreamReader sr = new StreamReader(file);
            string ret=sr.ReadToEnd();
            file.Close();
            return ret;
        }

    }
}
