using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;

namespace testApp {
	public class EmailSender
    {
        private AppContext db;
        private Timer aTimer;


        public EmailSender(AppContext context)
        {
            db = context;
            aTimer = new Timer();

            aTimer.Interval = 10000; //24* 3600000 - (DateTime.Now.Hour * 3600000 + DateTime.Now.Minute * 60000 + 30000);
            aTimer.AutoReset = false;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;

        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var from = new MailAddress("kirichoff@gmail.com", "Tom");
			var to = new MailAddress("waja@temp-link.net");
			var m = new MailMessage(from, to);
			var smtp = new SmtpClient("smtp.gmail.com", 587);
            var SendList = new List<User>();
            var test = new StringBuilder();

            smtp.Credentials = new NetworkCredential("kirichoff@gmail.com", "3558076Dima");
            smtp.EnableSsl = true;


            var u = from t in db.journal where t.Date.Day == DateTime.Now.Day - 1 select t;

            var admins = from t in db.Users where t.Type select t;

            SendList = admins.ToList();
            
            var array = db.journal.ToList();

            foreach (var ptr in  array){    
				
                 test.AppendLine(ptr.Date.ToString("yyyyMMdd") +","+ ptr.UserName +","+ptr.Action);

            }

            byte[] byte_array = Encoding.UTF8.GetBytes(test.ToString());

            var stream = new MemoryStream(byte_array);

            foreach (var ptr in SendList)
            {            
                to = new MailAddress(ptr.Email);
                m = new MailMessage(from, to);

                m.Subject = "Тест";
                m.Body = "Cписок действий пользователей";            
                m.Attachments.Add(new Attachment(stream, $"report_{DateTime.Now.ToString("yyyyMMdd")}.csv"));

                smtp.Send(m);            
            }

            aTimer.Interval = 24 * 3600000 - (DateTime.Now.Hour * 3600000 + DateTime.Now.Minute * 60000);
            aTimer.Enabled = true;
        }
    }
}
