using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UU=IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer.UserControl;
using CCC = IntroSE.Kanban.Backend.DataAccessLayer.User;
using SSL = IntroSE.Kanban.Backend.ServiceLayer.SubService;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class test
    {
        static int count = 0;
        static void Main(string[] args) 
            //when using this tester make sure to run everything under the print function to keep counter in sync
        {

            try
            {
                UU.Service s = new UU.Service();
                string te = "yaki@";
                string tp = "123zsaZSA1212";
                string tn = "yaki";
                
                print(s.Login(te, tp).toString());
                print(s.GetBoard(te).toString());
                print(s.GetColumn(te, 1).toString());
                print(s.GetColumn(te, 2).toString());
                print(s.GetColumn(te, 3).toString());
                
                /*
                print(s.Register(te, tp, tn));
                print(s.Login(te, tp).toString());
                print(s.AddTask(te, "T1", "this is 1 test task", DateTime.Now));
                print(s.AddTask(te, "T2", "this is 2 test task", DateTime.Now));
                print(s.AdvanceTask(te, 1, 1));
                print(s.GetColumn(te, 1).toString());
                print(s.GetColumn(te, 2).toString());
                */

                /*
                print(s.Register(te, tp, tn));
                print(s.Login(te, tp).toString());

                print(s.GetBoard(te).toString());
                print(s.GetColumn(te, 1).toString());
                print(s.AddTask(te, "T0", "this is 0 test task", DateTime.Now));
                print(s.GetColumn(te, 1).Value.ToString());
                print(s.LimitColumnTasks(te, 1, 7));
                for (int i=1; i <= 10; i++)
                {
                    print(s.AddTask(te, "T"+i, "this is "+i+" test task", DateTime.Now));
                }
                print(s.LimitColumnTasks(te, 1, 5));
                print(s.GetColumn(te, 1).Value.ToString());
                print(s.LimitColumnTasks(te, 2, 4));
                for(int i = 1; i < 10; i++)
                {
                    print(s.AdvanceTask(te,1,i));
                }
                print(s.GetColumn(te, 1).Value.ToString());
                print(s.GetColumn(te, 2).Value.ToString());
                print(s.LimitColumnTasks(te, 3, 2));
                for (int i = 1; i < 10; i++)
                {
                    print(s.AdvanceTask(te, 2, i));
                }
                print(s.GetColumn(te, 2).Value.ToString());
                print(s.GetColumn(te, 3).Value.ToString());
                print(s.LimitColumnTasks(te, 4, 1));
                for (int i = 1; i < 10; i++)
                {
                    print(s.AdvanceTask(te, 3, i));
                }
                print(s.GetColumn(te, 3).Value.ToString());
                
                try { print(s.GetColumn(te, 4).toString()); }
                catch (Exception e) { print(e.Message); }

                print(s.Logout(te));
                try { print(s.GetBoard(te).Value.ToString()); }
                catch(Exception e) { print(e.Message); }
               */
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            Console.Read();
        }
        static void print(Response res) //i miss python T_T
        {
            count++;
            if (res.ErrorOccured) { Console.WriteLine(count+":\n"+res.ErrorMessage); }
            else { Console.WriteLine(count + ":\n" + "no error."); }
            Console.Read();
            Console.Read();
        }
        static void print(Response<UU.User> res) //i miss python T_T
        {
            count++;
            if (res.ErrorOccured) { Console.WriteLine(count + ":\n" + res.ErrorMessage); }
            else { Console.WriteLine(count + ":\n" + "no error."); }
            Console.Read();
            Console.Read();
        }
        static void print(string prt) 
        {
            count++;
            Console.WriteLine(count + ":\n" + prt);
            Console.Read();
            Console.Read();
        }
    }
    
}
