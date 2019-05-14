using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SwordAndFather.Models;
using Dapper;
using System.Linq;

namespace SwordAndFather.Data
{
    public class UserRepository
    {
        const string ConnectionString = "Server=localhost;Database=SwordAndFather;Trusted_Connection=True;";

        // Inserting 

        public User AddUser(string username, string password)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newUser = db.QueryFirstOrDefault<User>($@"
                    Insert into users (username,password)
                    Output inserted.*
                    Values(@username,@password)",
                    new { username, password });

                if (newUser != null)
                {
                    return newUser;
                }
                //connection.Open();
                //var insertUserCommand = connection.CreateCommand();
                //insertUserCommand.CommandText = $@"Insert into users (username,password)
                //Output inserted.*
                //Values(@username,@password)";

                //insertUserCommand.Parameters.AddWithValue("username", username);
                //insertUserCommand.Parameters.AddWithValue("password", password);

                //var reader = insertUserCommand.ExecuteReader();

                //if (reader.Read())
                //{
                // //var insertedPassword = reader["password"].ToString();
                // var insertedUsername = reader["username"].ToString();
                // var insertedId = (int)reader["Id"];

                // var newUser = new User(insertedUsername, insertedPassword) { Id = insertedId };
            }

            throw new Exception("No user found");
        }


        //public List<User> GetAll()
        //{
        //var users = new List<User>();

        //var connection = new SqlConnection("Server=localhost;Database=SwordAndFather;Trusted_Connection=True;");
        //connection.Open();

        //var getAllUsersCommand = connection.CreateCommand();
        //getAllUsersCommand.CommandText = @"select username,password,id  
        //from users";

        //var reader = getAllUsersCommand.ExecuteReader();

        //while (reader.Read())
        //{
        //var id = (int)reader["Id"];
        //var username = reader["username"].ToString();
        //var password = reader["password"].ToString();
        //var user = new User(username, password) { Id = id };

        //users.Add(user);
        // }

        //connection.Close();

        // return users;
        //}

        // Getting Users

        public IEnumerable<User> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var users = db.Query<User>("select username, password, id from users").ToList();
                var targets = db.Query<Target>("Select * from Targets").ToList();

                foreach (var user in users)
                {
                    user.Targets = targets.Where(x => x.UserId == user.Id).ToList();
                }

                return users;
            }
        }

        public void DeleteUser(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                // Executes don't return anything 
                // The second parameter is an anonymus type
                var rowsAffected = db.Execute("delete from Users where Id = @id", new { id });

                if (rowsAffected != 1)
                {
                    throw new Exception("It didn't work");
                }
            }
        }

        public User UpdateUser(User userToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute(@"update Users 
                                                Set username = @username, 
                                                password = @password 
                                                Where id = @id", userToUpdate);

                if (rowsAffected == 1)
                {
                    return userToUpdate;
                }

                throw new Exception("Could not return a user");
            }
        }
    }

}