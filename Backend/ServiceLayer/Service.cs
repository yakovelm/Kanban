﻿using System;
using TC = IntroSE.Kanban.Backend.BusinessLayer.TaskControl;
using UC = IntroSE.Kanban.Backend.BusinessLayer.UserControl;
using SS = IntroSE.Kanban.Backend.ServiceLayer.SubService;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// The service for using the Kanban board.
    /// It allows executing all of the required behaviors by the Kanban board.
    /// You are not allowed (and can't due to the interfance) to change the signatures
    /// Do not add public methods\members! Your client expects to use specifically these functions.
    /// You may add private, non static fields (if needed).
    /// You are expected to implement all of the methods.
    /// Good luck.
    /// </summary>
    public class Service : IService
    {
        private SS.BoardService BS;
        private SS.UserService US;


        /// <summary>
        /// Simple public constructor.
        /// </summary>
        public Service()
        {
            US = new SS.UserService();
            BS = new SS.BoardService();
            LoadData();
        }
               
        /// <summary>        
        /// Loads the data. Intended be invoked only when the program starts
        /// </summary>
        /// <returns>A response object. The response should contain a error message in case of an error.</returns>
        public Response LoadData()
        {
            return new Response();
        }


        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="email">The email address of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        /// <param name="nickname">The nickname of the user to register</param>
        /// <returns>A response object. The response should contain a error message in case of an error<returns>
        public Response Register(string email, string password, string nickname)
        {
            return US.register(email, password, nickname);
        }

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            Response<User> res = US.login(email, password);
            if (res.ErrorOccured) { return res; }
            BS = new SS.BoardService(email);
            return res;
            
        }

        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            Response res = US.logout(email);
            if (res.ErrorOccured) { return res; }
            BS = new SS.BoardService();
            return res;
        }

        /// <summary>
        /// Returns the board of a user. The user must be logged in
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<Board> GetBoard(string email)
        {
            return BS.GetBoard(email);
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            return BS.LimitColumnTask(email, columnOrdinal, limit);
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {
            return BS.AddTask(email, title, description, dueDate);
        }

        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            return BS.UpdateTaskDueDate(email,columnOrdinal,taskId,dueDate);
        }

        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            return BS.UpdateTaskTitle(email, columnOrdinal, taskId, title);
        }

        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            return BS.UpdateTaskDescription(email, columnOrdinal, taskId, description);
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            return BS.AdvanceTask(email, columnOrdinal, taskId);
        }


        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnName">Column name</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<Column> GetColumn(string email, string columnName)
        {
            return BS.GetColumn(email, columnName);
        }

        /// <summary>
        /// Returns a column given it's identifier.
        /// The first column is identified by 0, the ID increases by 1 for each column
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Column ID</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>

        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            return BS.GetColumn(email, columnOrdinal);
        }
    }
}
