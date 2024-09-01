﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class CustomActionResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class CustomActionResult<T> : CustomActionResult
    {
        public T Data { get; set; }
    }
}
