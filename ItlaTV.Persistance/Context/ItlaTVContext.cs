

using ItlaTV.Domain.Entities.dbo;
using Microsoft.EntityFrameworkCore;

namespace ItlaTV.Persistance.Context
{
    public partial class ItlaTVContext : DbContext
    {
        public ItlaTVContext(DbContextOptions<ItlaTVContext> options) : base(options) 
        {
                
        }

        #region "dbo Entities"
        public DbSet<Generos> Generos { get; set; }
        public DbSet<Productoras> Productoras { get; set; }
        public DbSet<Series> Series { get; set; }
        #endregion
    }
}
