using System.Data.Entity;
namespace WF_ManyToMany_EF.Models
{
    class SoccerContext:DbContext
    {
        public SoccerContext():base("SoccerDb2")
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
