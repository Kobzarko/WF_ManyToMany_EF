using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WF_ManyToMany_EF
{
  public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public Player()
        {
            Teams = new List<Team>();
        }
    }
}
