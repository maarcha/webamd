﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;

namespace WebMajasdarbs.Models
{
    public class OurDbContext: DbContext
    {
        public DbSet<UserAccount> userAccount { get; set; }
    }
}
