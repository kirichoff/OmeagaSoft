using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Rest
{
    public class SqlLiteController
    {

        string connection;
        public SqlLiteController(string connectionString)
        {
            connection = connectionString;
            
        }

        public User SignIn(string name, string password)
        {           
            User user;

            using (SQLiteConnection c = new SQLiteConnection(connection))
            {      
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * From Users where UserName='{name}' and Password='{password}'", c))
                {
                    using (SQLiteDataReader readader = cmd.ExecuteReader())
                    {
                        readader.Read();
                         user = new User()
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
                }
            }
            return user;
        }

        public  List<string> Admins()
        {      
            List<string> val = new List<string>();
            using (SQLiteConnection c = new SQLiteConnection(connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT Action,Date,UserName From Journal inner join Users on Users.Id=Journal.UserId", c))
                {
                    using (SQLiteDataReader readader = cmd.ExecuteReader())
                    {
                        while (readader.Read())
                        {
                            val.Add(readader.GetString(0));
                        }
                    }
                }
            }
            return val;
        }


        public  List<ResponseJournal> JournalUsers()
        {
            List<ResponseJournal> val = new List<ResponseJournal>();
            using (SQLiteConnection c = new SQLiteConnection(connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT Action,Date,UserName From Journal inner join Users on Users.Id=Journal.UserId",c))
                {
                    using (SQLiteDataReader readader = cmd.ExecuteReader())
                    {

                        while (readader.Read())
                        {
                            val.Add(new ResponseJournal
                            {
                                Action = readader.GetString(0),
                                Date = readader.GetDateTime(1),
                                UserName = readader.GetString(2)
                            });
                        }                       
                    }
                }
            }
            return val;
        }

        

        public  bool UpdateUser(User user)
        {
            int res = 0;          
            using (SQLiteConnection c = new SQLiteConnection(connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand($"Update Users set " +
               $"UserName='{user.UserName}', FirstName ='{user.FirstName}'," +
               $"LastName='{user.LastName}'," +
               $"Password='{user.Password}'," +
               $"Email='{user.Email}'" +
               $",Type={user.Type}" +
               $",StartDate = '{user.StartDate.ToString("o")}' where Id={user.Id}", c))
                {
                      res = cmd.ExecuteNonQuery();
                 }
                }           
            return (res > 0) ? true : false;
        }
        public  List<Journal> GetJournal()
        {
            var list = new List<Journal>();          
            using (SQLiteConnection c = new SQLiteConnection(connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * From Journal", c))
                {
                    using (SQLiteDataReader readader = cmd.ExecuteReader())
                    {                       
                        
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
                    }
                }
            }
            return list;
        }
        public bool AddUser(User user)
        {         
            using (SQLiteConnection c = new SQLiteConnection(connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand($"Insert Into Users(UserName,FirstName,LastName,Password,Email,Type,StartDate)" +
                $" values('{user.UserName}'," +
                $"'{user.FirstName}'," +
                $"'{user.LastName}'," +
                $"'{user.Password}'," +
                $"'{user.Email}'," +
                $"{user.Type}," +
                $"'{user.StartDate.ToString("o")}' )", c))
                {
                    int res = cmd.ExecuteNonQuery();      
                    return (res > 0) ? true : false;
                }
            }
        }



        public bool AddJournal(Journal journal)
        {
            using (SQLiteConnection c = new SQLiteConnection(connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand($"Insert Into Journal(Action,Date,UserId) " +
                $"values('{journal.Action}'," +
                $"'{journal.Date.ToString("o")}'," +
                $"'{journal.UserId}')", c))
                {

                    int res = cmd.ExecuteNonQuery();
                    return (res > 0) ? true : false;
                }
            }
        }
        public List<User> GetUsers()
        {
            var list = new List<User>();
            using (SQLiteConnection c = new SQLiteConnection(connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * From Users", c))
                {
                    using (SQLiteDataReader readader = cmd.ExecuteReader())
                    {
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

          
                    }
                }
            }
            return list;
        }


    }
}
