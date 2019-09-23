using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<bool> AddUser(string user)
        {
          
                var us = JsonConvert.DeserializeObject<User>(user);
                if (user != null)
                {
                    if (await db.AddUser(us)) {
                        var on_reg =  db.SignIn(us.UserName, us.Password);

                    return db.AddJournal(new Journal { Action = "Register", Date = DateTime.Now, UserId = on_reg.Id }); ;
                    }
                }
                return false;
            
          
        }
        [HttpPost("[action]")]
        public async Task<bool> UpdateUser(string user)
        {
           

               var us = JsonConvert.DeserializeObject<User>(user);
                if (us != null)
                {
                bool b = db.AddJournal(new Journal { Action = "Update", Date = DateTime.Now, UserId = us.Id });
                    return await db.UpdateUser(us);
                }
                return false;
            
           
        }
        [HttpGet("[action]")]
        public string Login(string name, string password)
        {

            
                User user = db.SignIn(name, password);
                if (user != null)
                {
                   bool b= db.AddJournal(new Journal { Action = "Login", Date = DateTime.Now, UserId = user.Id });
                    return JsonConvert.SerializeObject(
                            user
                        );
                }
                return "false";
           
          
        }

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
        public async Task<string> admins()
        {
            return JsonConvert.SerializeObject(await db.Admins());
        }
        [HttpGet("[action]")]
        public async Task<string> GetJournal()
        {

            try
            {
                var ls = await db.JournalUsers();
                return JsonConvert.SerializeObject(ls);
            }
            catch{
                return "false";
            }
          
        }
    }
}
