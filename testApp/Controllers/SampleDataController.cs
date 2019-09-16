using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace testApp.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        AppContext db = new AppContext();

        [HttpPost("[action]")]
        public bool AddUser(string user)
        {
            try
            {
                db.Users.Add(
                    JsonConvert.DeserializeObject<User>(user)
                    );
                db.SaveChanges();
                return true;                
            }
            catch {
                return false;
            }
        }
        [HttpGet("[action]")]
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
