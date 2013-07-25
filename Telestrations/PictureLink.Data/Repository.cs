using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLink.Data
{
    public class GameScoreContext : DbContext
    {
        public GameScoreContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Thing> Thingys { get; set; }
    }

    public class Thing
    {
        public int Id { get; set; }
        public string Whatever { get; set; }
    }

    public class Repository
    {
    }
}
