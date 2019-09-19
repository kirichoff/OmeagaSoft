using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace testApp.Controllers {
	[Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private AppContext db;

        public SampleDataController(AppContext context)
        {
            db = context;
        }

        [HttpPost]
        public bool AddUser(string user)
        {
            try
            {
                var us = JsonConvert.DeserializeObject<User>(user);

                db.Users.Add(us);                
                db.journal.Add(new Journal { Action = "Register", Date = DateTime.Now, UserName = us.UserName });
                db.SaveChanges();

                return true;              
            }catch {
                return false;
            }
        }

        [HttpPost]
        public bool UpdateUser(string user)
        {
            try
            {
                var us = JsonConvert.DeserializeObject<User>(user);
                var userq = db.Users.Find(us.Id);
                db.Users.Attach(userq);
                db.Users.Remove(userq);
                db.Users.Add(us);                      
                db.journal.Add(new Journal { Action = "Update", Date = DateTime.Now, UserName =  us.UserName });
                db.SaveChanges();
                return true;
            }
            catch{
                return false;
            }
        }

        [HttpGet]
        public string Login(string  name, string password)
        {

            try
            {
                var u = from t in db.Users where t.UserName == name && t.Password == password select t;
                db.journal.Add(new Journal { Action = "Login", Date = DateTime.Now, UserName =  u.FirstOrDefault().UserName });
                db.SaveChanges();
                return JsonConvert.SerializeObject(
                    u.FirstOrDefault()
                    );
            }
            catch
            {
                return "false";
            }
        }

        [HttpGet]
        public string GetJournal()
        {
            try
            {
                return JsonConvert.SerializeObject(db.journal.ToArray());
            }
            catch
            {
                return "false";
            }
        }
 
        [HttpGet]
        public string GetAllUsers()
        {

            try
            {
                return JsonConvert.SerializeObject(db.Users.ToArray());
            }
            catch
            {
                return "false";
            }
        }



    }
}
