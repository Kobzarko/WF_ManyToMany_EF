using System.Data.Entity;
namespace WF_ManyToMany_EF.Models
{
    class SoccerContext:DbContext
    {
        public SoccerContext():base("SoccerDb")
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
