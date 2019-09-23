using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Rest
{
    public class SqlLiteController
    {
        SQLiteConnection connection;
        public SqlLiteController(string connectionString)
        {
            connection = new SQLiteConnection(connectionString);
            
        }

        public User SignIn(string name, string password)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand($"SELECT * From Users where UserName='{name}' and Password='{password}'", connection);

            var readader =  command.ExecuteReader();

            readader.Read();

             User user = new User()
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
            connection.Close();
            return user;
        }

        public async Task<List<string>> Admins()
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand($"SELECT Email From Users where Type=1", connection);

            var readader = await command.ExecuteReaderAsync();
   
            List<string> val = new List<string>();

            while (readader.Read())
            {
                val.Add(readader.GetString(0));
            }

            connection.Close();
            return val;
        }


        public async Task<List<ResponseJournal>> JournalUsers()
        {
            connection.Open();            
            SQLiteCommand command = new SQLiteCommand($"SELECT Action,Date,UserName From Journal inner join Users on Users.Id=Journal.UserId", connection);

            var readader = await command.ExecuteReaderAsync();
          
            List<ResponseJournal> val = new List<ResponseJournal>();

            while (readader.Read())
            {
                val.Add( new ResponseJournal{                        
                       Action= readader.GetString(0),
                       Date = readader.GetDateTime(1),
                       UserName = readader.GetString(2)                       
                    });
            }
            connection.Close();
            return val;
        }

        

        public async Task<bool> UpdateUser(User user)
        {
            connection.Open();
            SQLiteCommand command =
               new SQLiteCommand($"Update Users set " +
               $"UserName='{user.UserName}', FirstName ='{user.FirstName}'," +
               $"LastName='{user.LastName}'," +
               $"Password='{user.Password}'," +
               $"Email='{user.Email}'" +
               $",Type={user.Type}" +
               $",StartDate = '{user.StartDate.ToString("o")} where Id={user.Id}"
               , connection);

            int res = await command.ExecuteNonQueryAsync();
            connection.Close();
            return (res > 0) ? true : false;
        }
        public async Task<List<Journal>> GetJournal()
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand($"SELECT * From Journal", connection);

            var readader = await command.ExecuteReaderAsync();
            var list = new List<Journal>();
            while (readader.Read())
            {
                list.Add(new Journal()
                {
                    Id = readader.GetInt32(0),                   
                    Action = readader.GetString(1),
                    Date = readader.GetDateTime(2),
                    UserId = readader.GetInt32(3)
                });
            }
            connection.Close();
            return list;
        }
        public async Task<bool> AddUser(User user)
        {
            connection.Open();
            SQLiteCommand command =
                new SQLiteCommand($"Insert Into Users(UserName,FirstName,LastName,Password,Email,Type,StartDate)" +
                $" values('{user.UserName}'," +
                $"'{user.FirstName}'," +
                $"'{user.LastName}'," +
                $"'{user.Password}'," +
                $"'{user.Email}'," +
                $"{user.Type}," +
                $"'{user.StartDate.ToString("o")}' )"
                , connection);

            int res = await command.ExecuteNonQueryAsync();
            connection.Close();
            return (res > 0) ? true : false;
        }
        public bool AddJournal(Journal journal)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand($"Insert Into Journal(Action,Date,UserId) " +
                $"values('{journal.Action}'," +
                $"'{journal.Date.ToString("o")}'," +
                $"'{journal.UserId}')", connection);

            int res =  command.ExecuteNonQuery();
            connection.Close();
            return (res > 0) ? true : false;
        }
        public async Task<List<User>> GetUsers()
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand($"SELECT * From Users", connection);
            var readader = await command.ExecuteReaderAsync();

            var list = new List<User>();

            while (readader.Read())
            {
                list.Add(
             new User()
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
            connection.Close();
            return list;
        }


    }
}
