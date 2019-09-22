using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EmailSender
{
    public class EmailSendercs
    {

        MailAddress from;
        Timer timer;
        EmailSenderOption options;

        public EmailSendercs(EmailSenderOption _options)
        {
            timer = new Timer();

            timer.Interval = 24 * 3600000 - (DateTime.Now.Hour * 3600000 + DateTime.Now.Minute * 60000);

            timer.AutoReset = false;

            timer.Elapsed += OnTimedEvent;

            timer.Enabled = true;

           
            options = _options;
        }



        


        public void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {



            MailAddress from = new MailAddress(options.Mail, "name");
            MailMessage m;
            MailAddress to;
            SmtpClient smtp = new SmtpClient(options.SmptClient, 587);
            smtp.Credentials = new NetworkCredential(options.Mail, options.Password);
            smtp.EnableSsl = true;


            string res ;

            HttpWebRequest getAdmins = (HttpWebRequest)WebRequest.Create(options.Resource+"admins");
            HttpWebResponse response = (HttpWebResponse)getAdmins.GetResponse();
            using (Stream stre = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stre))
                {
                    res = reader.ReadToEnd();
                }
            }
            response.Close();

            var admins = JsonConvert.DeserializeObject<string[]>(res);


            getAdmins = (HttpWebRequest)WebRequest.Create(options.Resource + "JournalEmail");
           response = (HttpWebResponse)getAdmins.GetResponse();
            using (Stream stre = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stre))
                {
                    res = reader.ReadToEnd();
                }
            }
            response.Close();

            var array = JsonConvert.DeserializeObject<List<ResponseUser>>(res);

            var test = new StringBuilder();

            foreach (var ptr in array)
            {
                test.AppendLine(ptr.Date.ToString("yyyyMMdd") + "," + ptr.UserName + "," + ptr.Action);
            }

            byte[] byte_array = Encoding.UTF8.GetBytes(test.ToString());

            MemoryStream stream = new MemoryStream(byte_array);

 

            foreach (var ptr in admins)
            {

                to = new MailAddress(ptr);
                m = new MailMessage(from, to);
                m.Subject = options.Subject;
                m.Body = options.SmptClient;
                m.Attachments.Add(new Attachment(stream, $"report_{DateTime.Now.ToString("yyyyMMdd")}.csv"));

                smtp.Send(m);
            }

            timer.Interval = 24 * 3600000 - (DateTime.Now.Hour * 3600000 + DateTime.Now.Minute * 60000);

            timer.Enabled = true;
        }
    }
}
