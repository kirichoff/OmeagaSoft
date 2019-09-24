using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Rest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        SqlLiteController db;
      public  ValuesController(SqlLiteController context)
        {
            db = context;
        }
        
        [HttpPost("[action]")]
        public bool AddUser(string user)
        {
            try
            {
                var us = JsonConvert.DeserializeObject<User>(user);
                if (user != null)
                {
                    if (db.AddUser(us))
                    {
                        var on_reg = db.SignIn(us.UserName, us.Password);

                        db.AddJournal(new Journal { Action = "Register", Date = DateTime.Now, UserId = on_reg.Id }); ;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        [Authorize]
        [HttpPost("[action]")]
        public bool UpdateUser(string user)
        {

            try
            {
                var us = JsonConvert.DeserializeObject<User>(user);
                if (us != null)
                {
                    db.AddJournal(new Journal { Action = "Update", Date = DateTime.Now, UserId = us.Id });
                    return db.UpdateUser(us);
                }
                return false;

            }
            catch
            {
                return false;
            }
        }
        [HttpGet("[action]")]
        public string Login(string name, string password)
        {

            try
            {
                User user = db.SignIn(name, password);
                if (user != null)
                {
                    db.AddJournal(new Journal { Action = "Login", Date = DateTime.Now, UserId = user.Id });
                    return JsonConvert.SerializeObject(
                            user
                        );
                }
                return "false";
            }
            catch
            {
                return "false";
            }          
        }
        [Authorize]
        [HttpGet("[action]")]
        public string GetUser(string name)
        {

            try
            {
                User user = db.FindByName(name);
                if (user != null)
                {
                    db.AddJournal(new Journal { Action = "Login", Date = DateTime.Now, UserId = user.Id });
                    return JsonConvert.SerializeObject(
                            user
                        );
                }
                return "false";
            }
            catch
            {
                return "false";
            }
        }


        [Authorize]
        [HttpGet("[action]")]
        public string GetAllUsers()
        {

            try
            {              
                return JsonConvert.SerializeObject(
                        db.GetUsers()
                    );         
            }
            catch
            {
                return "false";
            }
        }

        [HttpGet("[action]")]
        public  string admins()
        {
            try
            {
                return JsonConvert.SerializeObject(db.Admins());
            }
            catch
            {
                return "";
            }
        }

        [HttpGet("[action]")]
        public string GetJournal()
        {

            try
            {
                var ls =  db.JournalUsers();
                return JsonConvert.SerializeObject(ls);
            }
            catch{
                return "false";
            }
          
        }
    }
}
