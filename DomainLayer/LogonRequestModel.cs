﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class LogonRequest
    {
        [JsonRequired]
        public string Username { get; set; }

        public string psw { get; set; }
    }
}
