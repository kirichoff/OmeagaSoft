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
        public bool AddUser(string user)
        {
            try
            {
                var us = JsonConvert.DeserializeObject<User>(user);               
                return db.AddUser(us);
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("[action]")]
        public bool UpdateUser(string user)
        {
            try
            {
                var us = JsonConvert.DeserializeObject<User>(user);              
                return db.UpdateUser(us);
            }
            catch 
            {
                return false;
            }
        }
        [HttpGet("[action]")]
        public string Login(string name, string password)
        {

         
                return JsonConvert.SerializeObject(
                       db.SignIn(name, password)
                    );
          
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
        public string admins()
        {
            return JsonConvert.SerializeObject(db.Admins());
        }
        [HttpGet("[action]")]
        public string JournalEmail()
        {
            return JsonConvert.SerializeObject(db.JournalUsers());
        }



        [HttpGet("[action]")]
        public string GetJournal()
        {
            try
            {
                return JsonConvert.SerializeObject(db.GetJournal());
            }
            catch
            {
                return "false";
            }
        }
    }
}
