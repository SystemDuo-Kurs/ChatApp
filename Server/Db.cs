using ChatApp.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server
{
    public class Db : IdentityDbContext<User>
    {
        public Db(DbContextOptions<Db> options ) : base( options ) { }

        public DbSet<User> AppUsers { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>().HasKey(m => m.Id);
            modelBuilder.Entity<User>().HasMany(u => u.Messages).WithOne(m => m.User);
            modelBuilder.Entity<User>().Ignore(u => u.Password);
            
        }
    }
}
