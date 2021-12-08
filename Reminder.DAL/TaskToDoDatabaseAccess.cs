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
                var sqlQuery = "SELECT * FROM TasksToDo";
                
                var command = new SqlCommand(sqlQuery, connection);
                var reader = command.ExecuteReader();
 
                while (reader.Read())
                {
                    var newTask = new TaskToDo
                    {
                        Id = int.Parse(reader.GetValue(0).ToString()),
                        Title = reader.GetValue(1).ToString(),
                        DeadlineTime = Convert.ToDateTime(reader.GetValue(2)),
                        Description = reader.GetValue(3).ToString()
                    };
                    result.Add(newTask);
                }
            }

            return result;
        }

        public void Add(TaskToDo item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlQuery =
                    $"INSERT INTO TasksToDo (Title, DeadlineTime, Description) VALUES ('{item.Title}', '{item.DeadlineTime}', '{item.Description}')";
                
                var command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(TaskToDo item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlQuery = $"UPDATE TasksToDo SET Title='{item.Title}', DeadlineTime='{item.DeadlineTime}', Description='{item.Description}' WHERE Id='{item.Id}'";

                var command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlQuery = $"DELETE  FROM TasksToDo WHERE Id='{id}'";
                
                var command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}