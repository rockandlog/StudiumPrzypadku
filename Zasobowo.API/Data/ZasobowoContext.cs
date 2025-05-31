using Microsoft.EntityFrameworkCore;
using Zasobowo.API.Models;

namespace Zasobowo.API.Data
{
    public class ZasobowoContext : DbContext
    {
        public ZasobowoContext(DbContextOptions<ZasobowoContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed użytkowników
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "ola.dev", Role = "User", PasswordHash = "1234" },
                new User { Id = 2, Username = "kamil.sys", Role = "User", PasswordHash = "1234" },
                new User { Id = 3, Username = "ania.ui", Role = "User", PasswordHash = "1234" },
                new User { Id = 4, Username = "mario.q", Role = "User", PasswordHash = "1234" },
                new User { Id = 5, Username = "ewelina.pmo", Role = "User", PasswordHash = "1234" },
                new User { Id = 6, Username = "dawid.admin", Role = "Admin", PasswordHash = "1234" },
                new User { Id = 7, Username = "szymon.fullstack", Role = "User", PasswordHash = "1234" },
                new User { Id = 8, Username = "gosia.test", Role = "User", PasswordHash = "1234" },
                new User { Id = 9, Username = "adam.secu", Role = "Admin", PasswordHash = "1234" },
                new User { Id = 10, Username = "karolina.ux", Role = "User", PasswordHash = "1234" }
            );

            // Relacja Device → User (opcjonalna)
            modelBuilder.Entity<Device>()
                .HasOne(d => d.AssignedUser)
                .WithMany()
                .HasForeignKey(d => d.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
