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
        EmailSenderOption options;

        public EmailSendercs( IOptions<EmailSenderOption> context)
        {           
            options = context.Value;
        }


        public string Request(string action)
        {
            string res = "";
            try
            {
            
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
            }
            catch
            {

            }

            return res;
        }
        


        public void OnTimedEvent()
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
        }
    }
}
