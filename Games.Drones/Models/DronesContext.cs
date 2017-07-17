using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Games.Drones.Models
{
    public class DronesContext : DbContext 
    {
        public DbSet<Move> Moves { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Round> Rounds { get; set; }
    }
}