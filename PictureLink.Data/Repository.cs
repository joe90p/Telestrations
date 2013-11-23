using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PictureLink.Data
{
    public class PLinkContext : DbContext
    {
        public PLinkContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Chain> Chains { get; set; }
        public DbSet<Guess> Guesses { get; set; }
        public DbSet<Mark> Marks { get; set; }
    }

    [Table("Chain")]
    public class Chain
    {

        public int Id { get; set; }

        public virtual ICollection<Guess> Guesses { get; set; }
    }

    [Table("Guess")]
    public class Guess
    {


        public int Id { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public virtual Chain Chain { get; set; }

        //public int ChainID { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }

        public virtual UserProfile Contributor { get; set; }

        //public int ContributorID { get; set; }
    }

    [Table("Mark")]
    public class Mark 
    {

        public int Id
        {
            get;
            set;
        }


        public int Score
        {
            get;
            set;
        }

        public virtual UserProfile Marker { get; set; }

        //public int MarkerID { get; set; }

        public virtual Guess Guess { get; set; }

        //public int GuessID { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<Guess> Guesses { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
    }

    public class Repository : IRepository
    {
        PLinkContext context = new PLinkContext();

        public void AddChain(IEnumerable<IGuessDTO> guesses)
        {
            var gs = new List<Guess>();
            foreach(var g in guesses)
            {
                var up = this.context.UserProfiles.Include(x => x.Guesses).FirstOrDefault(x => x.UserId == g.Contributor.Id);
                var guess = new Guess() {Content = g.Content, Type = g.Type.ToString()};
                up.Guesses.Add(guess);
                gs.Add(guess);
            }

            var chain = new Chain() {Guesses = gs};
            this.context.Chains.Add(chain);
            this.context.SaveChanges();
        }

        public IEnumerable<T> Query<T>(System.Linq.Expressions.Expression<Func<T, bool>> where) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T target)
        {
            throw new NotImplementedException();
        }

        public void Save<T>(T target)
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(object target)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Chain> GetUnMarkedChains(int playerId)
        {
            var test = this.context.Guesses.Include(gs => gs.Marks).ToList();
            

            return this.context.Guesses.Include(gs => gs.Marks)
                .Where(gs => gs.Contributor.UserId == playerId && !gs.Marks.Any())
                .Select(x => x.Chain).ToList();
        }

        public Chain GetChain(int index)
        {
            return this.context.Chains.Find(index);
        }



    }
}
