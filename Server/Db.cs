using ChatApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options ) : base( options ) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Message>().HasKey(m => m.Id);

            modelBuilder.Entity<User>().HasMany(u => u.Messages).WithOne(m => m.User);
        }
    }
}
