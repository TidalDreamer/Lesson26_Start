﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Models
{
    public class CustomerSignIn
    {
        public int CustomerId { get; set; }
        public string Password { get; set; }
    }
}