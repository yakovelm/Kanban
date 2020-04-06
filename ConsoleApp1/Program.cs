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
                string te = "yaki@";
                string tp = "123";
                string tn = "yaki";

                print(s.Register(te, tp, tn));
                print(s.Login(te, tp).toString());
                Console.WriteLine(s.GetBoard(te).Value);
                print(s.GetColumn(te, 1).Value.ToString());
                print(s.AddTask(te, "T0", "this is 0 test task", DateTime.Now));
                print(s.GetColumn(te, 1).Value.ToString());
                print(s.LimitColumnTasks(te, 1, 7));
                for (int i=1; i <= 10; i++)
                {
                    print(s.AddTask(te, "T"+i, "this is "+i+" test task", DateTime.Now));
                }
                print(s.LimitColumnTasks(te, 1, 5));
                print(s.GetColumn(te, 1).Value.ToString());
                print(s.LimitColumnTasks(te, 2, 3));
                for(int i = 1; i < 10; i++)
                {
                    print(s.AdvanceTask(te,1,i));
                }
                print(s.GetColumn(te, 1).Value.ToString());
                print(s.GetColumn(te, 2).Value.ToString());
                print(s.Logout(te));

            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            Console.Read();
        }
        static void print(Response res) //i miss python T_T
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
