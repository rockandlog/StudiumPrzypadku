using Microsoft.EntityFrameworkCore;
using Zasobowo.API.Models;

namespace Zasobowo.API.Data
{
    public class ZasobowoContext : DbContext
    {
        public ZasobowoContext(DbContextOptions<ZasobowoContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.AssignedUser)
                .WithMany()
                .HasForeignKey(d => d.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Device>().Property(d => d.Name).IsRequired();
            modelBuilder.Entity<Device>().Property(d => d.Type).IsRequired();
            modelBuilder.Entity<Device>().Property(d => d.Status).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Role).IsRequired();

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "ola.dev", PasswordHash = "1234", Role = "User" },
                new User { Id = 2, Username = "kamil.sys", PasswordHash = "1234", Role = "User" },
                new User { Id = 3, Username = "ania.ui", PasswordHash = "1234", Role = "User" },
                new User { Id = 4, Username = "mario.q", PasswordHash = "1234", Role = "User" },
                new User { Id = 5, Username = "ewelina.pmo", PasswordHash = "1234", Role = "User" },
                new User { Id = 6, Username = "dawid.admin", PasswordHash = "1234", Role = "Admin" },
                new User { Id = 7, Username = "szymon.fullstack", PasswordHash = "1234", Role = "User" },
                new User { Id = 8, Username = "gosia.test", PasswordHash = "1234", Role = "User" },
                new User { Id = 9, Username = "adam.secu", PasswordHash = "1234", Role = "Admin" },
                new User { Id = 10, Username = "karolina.ux", PasswordHash = "1234", Role = "User" }
            );
        }
    }
}
