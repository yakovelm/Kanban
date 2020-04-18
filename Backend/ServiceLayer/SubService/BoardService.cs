using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL = IntroSE.Kanban.Backend.BusinessLayer.TaskControl;

namespace IntroSE.Kanban.Backend.ServiceLayer.SubService
{
    public class BoardService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private BL.BoardController BC;
        public BoardService()
        {
            BC = new BL.BoardController();
            log.Debug("create BoardService");
        }

        public Response LoadData()
        {
            log.Info("Load Data in the BoardController and Boards.");
            try
            {
                BC.LoadData();
                return new Response();
            }
            catch(Exception e) { return new Response(e.Message); }
        }
        public Response Login(string email)
        {
            log.Info("Login to board of " +email);
            try
            {
                BC.Login(email);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response Logout(string email)
        {
            log.Info("LogOut to board of " + email);
            try
            {
                BC.Logout(email);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response LimitColumnTask(string email, int ColumnOrdinal, int limit)
        {
            log.Info(email + " ask to chenge the limit of column " +ColumnOrdinal+ " to limit "+limit);
            try
            {
                BC.LimitColumnTask(email, ColumnOrdinal, limit);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response<Task> AddTask(string email, string title, string desciption, DateTime dueTime)
        {
            log.Info(email + " ask to add task.");
            try
            {
                BL.Task task=BC.AddTask(email, title, desciption, dueTime);
                return new Response<Task>(new Task(task.getID(),task.getCreation(),title, desciption));
            }
            catch (Exception e) { return (new Response<Task>(e.Message)); }
        }
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskID, DateTime Due)
        {
            log.Info(email + " ask to Update the DueDate of task " + taskID);
            try
            {
                BC.UpdateTaskDueDate(email, columnOrdinal, taskID, Due);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskID, string title)
        {
            log.Info(email + " ask to Update the title of task " + taskID);
            try
            {
                BC.UpdateTaskTitle(email, columnOrdinal, taskID, title);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskID, string Desc)
        {
            log.Info(email + " ask to Update the description of task " + taskID);
            try
            {
                BC.UpdateTaskDescription(email, columnOrdinal, taskID, Desc);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response<Task> GetTask(int ID)
        {
            log.Info("ask to get the task " + ID);
            try { return new Response<Task>(chengeType(BC.GetTask(ID))); }
            catch (Exception e) { return (new Response<Task>(new Task(), e.Message)); }
        }
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            log.Info(email + " ask to advanve the task " + taskId);
            try
            {
                BC.AdvanceTask(email, columnOrdinal, taskId);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response<Column> GetColumn(string email, string columnName)
        {
            log.Info(email + " ask to get the column " + columnName);
            try
            {
                BL.Column columnBL = BC.GetColumn(email, columnName);
                return new Response<Column>(chengeType(columnBL));
            }
            catch (Exception e) { return (new Response<Column>(new Column(), e.Message)); }
        }
        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            log.Info(email + " ask to get the column " + columnOrdinal);
            try
            {
                BL.Column columnBL = BC.GetColumn(email, columnOrdinal);
                return new Response<Column>(chengeType(columnBL));
            }
            catch (Exception e) { return (new Response<Column>(new Column(), e.Message)); }
        }
        public Response<Board> GetBoard(string email)
        {
            log.Info(email + " ask to get the board." );
            Dictionary<string, BL.Column> listColumnBL = BC.getColumns(email);
            List<string> listNames = new List<string>();
            foreach (BL.Column a in listColumnBL.Values) { listNames.Add(a.getName()); }
            return new Response<Board>(new Board(listNames));
        }
        private Task chengeType(BL.Task taskBL)
        {
            return new Task(taskBL.getID(), taskBL.getCreation(), taskBL.getTitle(), taskBL.getDesc());
        }
        private Column chengeType(BL.Column columnBL)
        {
            List<Task> TaskListSL = new List<Task>();
            foreach (BL.Task taskBL in columnBL.getListTask())
            { TaskListSL.Add(chengeType(taskBL)); }
            return new Column(TaskListSL, columnBL.getName(), columnBL.getLimit());
        }
    }
}