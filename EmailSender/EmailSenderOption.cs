using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailSender
{
    public class EmailSenderOption
    {        
        public string Subject { get; set; }
        public  string MessegeBody { get; set; }
        public string SmptClient { get; set; }
        public int Port { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public string Resource { get; set; }
    }
}
