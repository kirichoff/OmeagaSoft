﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSender
{
    public class ResponseUser
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }
}
