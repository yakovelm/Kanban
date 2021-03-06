﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer.TaskControl;

namespace IntroSE.Kanban.Backend.Backend.BusinessLayer.TaskControl
{
    class Program
    {
        static void Main(string[] args)
        {
            String email = "yaki@gmail.com";
            Column a = new Column(email,"a");
            Column b = new Column(email, "b");
            Column c = new Column(email, "c");
            Board board = new Board(email, a, b, c);
            int i = 1;
            //test 1: add task
            board.AddTask(email, "first task", "asd", new DateTime(2020+i, 6, 10));
            Console.WriteLine("test " + i + " Succeeded");
                i++;
            //test 1: add task
            try
            {
                board.AddTask("asd", "" + i, "asd", new DateTime(2020+i, 6, 10));
            }
            catch(Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            board.AddTask(email, "" + i, "asd", new DateTime(2020+i, 6, 10));
            i++;
            board.AddTask(email, "" + i, "asd", new DateTime(2020 + i, 6, 10));
            i++;
            board.AddTask(email, "" + i, "asd", new DateTime(2020 + i, 6, 10));
            i++;
            board.AddTask(email, "" + i, "asd", new DateTime(2020 + i, 6, 10));
            i++;
            //test limit
            board.LimitColumnTask(email, 1, 8);
            if (board.GetColumn(email,1).getLimit() == 8)
            { Console.WriteLine("set Limit secceesed"); }
            else { Console.WriteLine("set Limit unsecceesed"); }
            i++;
            //test 1: add task
            try
            {
                board.LimitColumnTask("asd",2,89);
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.LimitColumnTask(email,6, 56);
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.LimitColumnTask(email, -2,56);
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.AddTask("asd", "" + i, "asd", new DateTime(2020 + i, 6, 10));
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDueDate(email,9, 5, new DateTime(2020 + i, 6, 10));
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDueDate(email,2, 0, new DateTime(2020 + i, 6, 10));
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDueDate(email,0, 5, new DateTime(2020 + i, 6, 10));
                Console.WriteLine(i+"Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDueDate(email,3, 5, new DateTime(2020 + i, 6, 10));
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDueDate(email,2, 89, new DateTime(2020 + i, 6, 10));
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDueDate(email,0, 5, new DateTime(2020 + i, 6, 10));
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDescription(email,9, 5, "sdf");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDescription(email,2, 0,"ds");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDescription(email,0, 5, "sdf");
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDescription(email,3, 5,"asf");
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDescription(email,2, 89, "sdf");
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskDescription(email,0, 5,"asd");
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskTitle(email,9, 5, "sdf");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskTitle(email,2, 0, "ds");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskTitle(email,0, 5, "sdf");
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskTitle(email,3, 5, "asf");
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskTitle(email,2, 89, "sdf");
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.UpdateTaskTitle(email,0, 5, "asd");
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.GetTask(55);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.GetTask(-5);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            if (board.GetTask(5) != null) { Console.WriteLine("test " + i + " Succeeded"); }
            else { Console.WriteLine(i + "Unsecceesed");}
            i++;
            if (board.GetTask(3) != null) { Console.WriteLine("test " + i + " Succeeded"); }
            else { Console.WriteLine(i + "Unsecceesed"); }
            i++;
            if (board.GetTask(1) != null) { Console.WriteLine("test " + i + " Succeeded"); }
            else { Console.WriteLine(i + "Unsecceesed"); }
            i++;
            board.AdvanceTask(email,1, 1);
            board.AdvanceTask(email,1, 3);
            board.AdvanceTask(email,1, 4);
            //test 1: add task
            try
            {
                board.AdvanceTask(email,3, 1);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.AdvanceTask(email,7, 1);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.AdvanceTask(email,2, 2);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            board.AdvanceTask(email,2, 1);
            //test 1: add task
            try
            {
                board.AdvanceTask(email,3, 1);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.GetColumn(email,"d");
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {

                Console.WriteLine(board.GetColumn(email,"a").getName() + "test " + i + " Succeeded");
            }
            catch (Exception e)
            {
                Console.WriteLine(i + "Unsecceesed");
            }
            i++;
            //test 1: add task
            try
            {
                board.GetColumn(email,89);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {

                Console.WriteLine(board.GetColumn(email,3).getName() + "test " + i + " Succeeded");
            }
            catch (Exception e)
            {
                Console.WriteLine(i + "Unsecceesed");
            }
            i++;
            //test 1: add task
            try
            {
                board.GetColumn(email,0);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.AdvanceTask(email,1, 1);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.AdvanceTask(email,3, 1);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;
            //test 1: add task
            try
            {
                board.AdvanceTask(email,3, 1);
                Console.WriteLine(i + "Unsecceesed");
            }
            catch (Exception e)
            {
                Console.WriteLine("test " + i + " Succeeded");
            }
            i++;


            Console.WriteLine(i - 1);



            Console.WriteLine("asdasd");
            Console.ReadLine();
        }
    }
}
