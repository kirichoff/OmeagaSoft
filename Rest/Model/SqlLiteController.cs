using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Rest.Model
{
    public class SqlLiteController
    {
        SQLiteConnection connection;
        public SqlLiteController(string connectionString)
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }

        public User SignIn(string name, string password)
        {
            SQLiteCommand command = new SQLiteCommand($"SELECT * From Users where UserName='{name}' and Password='{password}'", connection);

            var readader = command.ExecuteReader();

            readader.Read();

            return new User()
            {
                Id = readader.GetInt32(0),
                UserName = readader.GetString(1),
                FirstName = readader.GetString(2),
                LastName = readader.GetString(3),
                Password = readader.GetString(4),
                Email = readader.GetString(5),
                Type = readader.GetBoolean(6),
                StartDate = readader.GetDateTime(7),
            };
        }
        public bool UpdateUser(User user)
        {
            SQLiteCommand command =
               new SQLiteCommand($"Update Users set " +
               $"UserName='{user.UserName}', FirstName ='{user.FirstName}'," +
               $"LastName='{user.LastName}'," +
               $"Password='{user.Password}'," +
               $"Email='{user.Email}'" +
               $",Type={user.Type}" +
               $",StartDate = '{user.StartDate.ToString()}' where Id={user.Id}"
               , connection);

            int res = command.ExecuteNonQuery();
            return (res > 0) ? true : false;
        }
        public List<Journal> GetJournal()
        {
            SQLiteCommand command = new SQLiteCommand($"SELECT * From Journal", connection);

            var readader = command.ExecuteReader();
            var list = new List<Journal>();
            while (readader.Read())
            {
                list.Add(new Journal()
                {
                    Id = readader.GetInt32(0),
                    Date = readader.GetDateTime(1),
                    Action = readader.GetString(2),
                    UserId = readader.GetInt32(3)
                });
            }

            return list;
        }
        public bool AddUser(User user)
        {
            SQLiteCommand command =
                new SQLiteCommand($"Insert Into Users(UserName,FirstName,LastName,Password,Email,Type,StartDate)" +
                $" values('{user.UserName}'," +
                $"'{user.FirstName}'," +
                $"'{user.LastName}'," +
                $"'{user.Password}'," +
                $"'{user.Email}'," +
                $"{user.Type}," +
                $"'{user.StartDate.ToString()}' )"
                , connection);

            int res = command.ExecuteNonQuery();
            return (res > 0) ? true : false;
        }
        public bool AddJournal(Journal journal)
        {
            SQLiteCommand command = new SQLiteCommand($"Insert Into Journal(Action,Date,UserId) " +
                $"values('{journal.Action}'," +
                $"'{journal.Date.ToString()}'," +
                $"'{journal.UserId}')", connection);

            int res = command.ExecuteNonQuery();

            return (res > 0) ? true : false;
        }
        public List<User> GetUsers()
        {
            SQLiteCommand command = new SQLiteCommand($"SELECT * From Users'", connection);

            var readader = command.ExecuteReader();
           
            var list = new List<User>();

            while (readader.Read())
            {
                list.Add(new User()
                {
                    Id = readader.GetInt32(0),
                    UserName = readader.GetString(1),
                    FirstName = readader.GetString(2),
                    LastName = readader.GetString(3),
                    Password = readader.GetString(4),
                    Email = readader.GetString(5),
                    Type = readader.GetBoolean(6),
                    StartDate = readader.GetDateTime(7),
                }
                );
            }

            return list;
        }


    }
}
