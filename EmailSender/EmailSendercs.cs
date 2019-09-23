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

            timer.Interval = 1000;//24 * 3600000 - (DateTime.Now.Hour * 3600000 + DateTime.Now.Minute * 60000);

            timer.AutoReset = false;

            timer.Elapsed += OnTimedEvent;

            timer.Enabled = true;
            
            options = _options;
        }


        public string Request(string action)
        {
            string res;
            HttpWebRequest getAdmins = (HttpWebRequest)WebRequest.Create(options.Resource + action);
            HttpWebResponse response = (HttpWebResponse)getAdmins.GetResponse();
            using (Stream stre = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stre))
                {
                    res = reader.ReadToEnd();
                }
            }
            response.Close();

            return res;
        }
        


        public void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {



            MailAddress from = new MailAddress(options.Mail, "name");
            MailMessage m;
            MailAddress to;
            SmtpClient smtp = new SmtpClient(options.SmptClient, options.Port);
            smtp.Credentials = new NetworkCredential(options.Mail, options.Password);
            smtp.EnableSsl = true;


     

            var admins = JsonConvert.DeserializeObject<string[]>(Request("admins"));  

            var array = JsonConvert.DeserializeObject<List<ResponseUser>>(Request("GetJournal"));

            var test = new StringBuilder();

            foreach (var ptr in array)
            {
                test.AppendLine(ptr.Date.ToString("yyyyMMdd") + "," + ptr.UserName + "," + ptr.Action);
            }

            byte[] byte_array = Encoding.UTF8.GetBytes(test.ToString());

            MemoryStream stream = new MemoryStream(byte_array);


            try
            {
                foreach (var ptr in admins)
                {

                    to = new MailAddress(ptr);
                    m = new MailMessage(from, to);
                    m.Subject = options.Subject;
                    m.Body = options.SmptClient;
                    m.Attachments.Add(new Attachment(stream, $"report_{DateTime.Now.ToString("yyyyMMdd")}.csv"));

                    smtp.Send(m);
                }
            }
            catch
            {

            }

            timer.Interval = 24 * 3600000 - (DateTime.Now.Hour * 3600000 + DateTime.Now.Minute * 60000);

            timer.Enabled = true;
        }
    }
}
