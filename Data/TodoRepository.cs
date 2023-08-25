using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using first_api_project.Models;

namespace first_api_project.Data
{
    public class TodoRepository
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=todoitems;User=root;Password=Rishabhip@1";

        public IEnumerable<TodoItem> GetAllTodoItems()
        {
            List<TodoItem> todoItems = new List<TodoItem>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Title FROM TodoInfo";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid id = reader.GetGuid(0);
                            string title = reader.GetString(1);

                            todoItems.Add(new TodoItem { Id = id, Title = title });
                        }
                    }
                }
            }

            return todoItems;
        }

        public void AddTodoItem(TodoItem todoItem)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                todoItem.Id = Guid.NewGuid(); 
                todoItem.TodoTimeAdded = DateTime.Now;
                todoItem.TodoModifiedTime = DateTime.Now;
                string query = "INSERT INTO TodoInfo (Id, Title,IsCompleted,TodoTimeAdded,TodoModifiedTime) VALUES (@Id, @Title,@IsCompleted,@TodoTimeAdded,@TodoModifiedTime)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", todoItem.Id);
                    command.Parameters.AddWithValue("@Title", todoItem.Title);
                    command.Parameters.AddWithValue("@Iscompleted", todoItem.IsCompleted);
                    command.Parameters.AddWithValue("@TodoTimeAdded", todoItem.TodoTimeAdded);
                    command.Parameters.AddWithValue("@TodoModifiedTime", todoItem.TodoModifiedTime);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTodoItem(TodoItem todoItem)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
todoItem.TodoModifiedTime = DateTime.Now;
                string query = "UPDATE TodoInfo SET Title = @Title,IsCompleted=@IsCompleted,TodoModifiedTime=@TodoModifiedTime WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", todoItem.Id);
                    command.Parameters.AddWithValue("@Title", todoItem.Title);
command.Parameters.AddWithValue("@IsCompleted", todoItem.IsCompleted);
command.Parameters.AddWithValue("@TodoModifiedTime", todoItem.TodoModifiedTime);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTodoItem(Guid id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM TodoInfo WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
