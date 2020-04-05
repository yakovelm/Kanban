using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using SSL = IntroSE.Kanban.Backend.ServiceLayer.SubService;

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
                Console.WriteLine(s.GetColumn("yaki@", 1).Value);//problem solved, try me..
                Console.WriteLine(s.GetBoard("yaki@").Value);
                print(s.GetColumn("yaki@",2).toString()); 
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
