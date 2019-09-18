using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;

namespace testApp
{
    public class EmailSender
    {
        public static AppContext db = new AppContext();
        public static Timer aTimer;


        public EmailSender()
        {
   

            aTimer = new Timer();

            aTimer.Interval = 24* 3600000 - (DateTime.Now.Hour * 3600000 + DateTime.Now.Minute * 60000 + 30000);


            aTimer.AutoReset = false;

            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;

        }
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {



            MailAddress from = new MailAddress("kirichoff@gmail.com", "Tom");
            MailAddress to = new MailAddress("firirode@rev-mail.net");
            MailMessage m = new MailMessage(from, to);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("kirichoff@gmail.com", "3558076Dima");
            smtp.EnableSsl = true;


            var test = new StringBuilder();





            var u = from t in db.journal where t.Date.Day == DateTime.Now.Day - 1 select t;

            var SendList = new List<User>();
            

            foreach (var ptr in u)
            {
                 var user = from t in db.Users where t.UserName == ptr.UserName select t;

                if (user.FirstOrDefault().Type) SendList.Add(user.FirstOrDefault());
                else test.AppendLine(ptr.Date.ToLongDateString() +"  "+ ptr.UserName +"  "+ptr.Action);
            }

            byte[] byte_array = Encoding.UTF8.GetBytes(test.ToString());

            MemoryStream stream = new MemoryStream(byte_array);



            foreach (var ptr in SendList)
            {

                m.Subject = "Тест";
                m.Body = "Cписок действий пользователей";

                m.Attachments.Add(new Attachment(stream, "qwe.csv"));          

                to = new MailAddress(ptr.Email);

                m = new MailMessage(from, to);
            
                smtp.Send(m);            
            }
      
   
            


            aTimer.Interval = 24 * 3600000 - (DateTime.Now.Hour * 3600000 + DateTime.Now.Minute * 60000);

            aTimer.Enabled = true;
        }
    }
}
