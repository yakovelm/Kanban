using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class test
    {
        static int count = 0;
        static void Main(string[] args)
        {
            try
            {
                Service s = new Service();
                print(s.Register("yaki@", "123", "yaki"));
                print(s.Login("yaki@", "123").toString());
                print(s.GetBoard("yaki@").toString()); // this one has a problem
                print(s.Logout("yaki@"));
               


            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            Console.Read();
        }
        static void print(Response res) 
        {
            count++;
            if (res.ErrorOccured) { Console.WriteLine(count+":\n"+res.ErrorMessage); }
            else { Console.WriteLine(count + ":\n" + "no error."); }
        }
        static void print(string prt) 
        {
            count++;
            Console.WriteLine(count + ":\n" + prt); 
        }
    }
}
