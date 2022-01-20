using Microsoft.EntityFrameworkCore;

namespace TesterAPI
{
    public class QAContext : DbContext
    {
        public QAContext(DbContextOptions options) : base(options) { }

        public DbSet<Case> Cases => Set<Case>();
        public DbSet<Task> Tasks => Set<Task>();
    }
}
