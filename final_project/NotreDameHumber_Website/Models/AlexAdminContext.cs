using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NotreDameHumber_Website.Models
{
    public class AlexAdminContext
    {
        public DbSet<AlexAdmin> AlexAdmins { get; set; }
    }
}