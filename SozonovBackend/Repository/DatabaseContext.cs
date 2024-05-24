using Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace SozonovBackend.Repository
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Proposal> Proposals { get; set; }
    }
}
