using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PictureLinkMVC.Web.Models
{
    public class PictureLinkDb : DbContext
    {
        public DbSet<Mark> Marks { get; set; }
    }
}