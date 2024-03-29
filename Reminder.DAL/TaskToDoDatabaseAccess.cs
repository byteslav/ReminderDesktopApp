﻿using System;
using System.ComponentModel;
using System.Data.SqlClient;
using Reminder.Entities;

namespace Reminder.DAL
{
    public class TaskToDoDatabaseAccess : IDatabaseAccess<TaskToDo>
    {
        private readonly string _connectionString;
        public TaskToDoDatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BindingList<TaskToDo> GetAll()
        {
            var result = new BindingList<TaskToDo>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                //var sqlQuery = "SELECT * FROM Tasks";
                var sqlQuery = "execute Tasks_GetAll";
                
                var command = new SqlCommand(sqlQuery, connection);
                var reader = command.ExecuteReader();
 
                while (reader.Read())
                {
                    var newTask = new TaskToDo
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        DeadlineTime = reader.GetDateTime(2),
                        Description = reader.GetString(3)
                    };
                    result.Add(newTask);
                }
            }

            return result;
        }

        public TaskToDo GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlQuery = $"SELECT * FROM Tasks WHERE Id='{id}'";
                
                var command = new SqlCommand(sqlQuery, connection);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var newTask = new TaskToDo
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        DeadlineTime = reader.GetDateTime(2),
                        Description = reader.GetString(3)
                    };
                    return newTask;
                }
                return new TaskToDo();
            }

        }

        public void Add(TaskToDo item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlQuery =
                    $"INSERT INTO Tasks (Title, Deadline, Description) VALUES ('{item.Title}', '{item.DeadlineTime}', '{item.Description}')";
                
                var command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(TaskToDo item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlQuery = $"UPDATE Tasks SET Title='{item.Title}', Deadline='{item.DeadlineTime}', Description='{item.Description}' WHERE Id='{item.Id}'";

                var command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlQuery = $"DELETE  FROM Tasks WHERE Id='{id}'";
                
                var command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
