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

            // Relacja Device → User (AssignedUser)
            modelBuilder.Entity<Device>()
                .HasOne(d => d.AssignedUser)
                .WithMany()
                .HasForeignKey(d => d.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Device>().Property(d => d.Name).IsRequired();
            modelBuilder.Entity<Device>().Property(d => d.Type).IsRequired();
            modelBuilder.Entity<Device>().Property(d => d.Status).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired(); // <--- Email dodany
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Role).IsRequired();

            // Wstępne dane użytkowników (Email + Username)
            modelBuilder.Entity<User>().HasData(
     new User { Id = 1, Username = "ola.dev", Email = "ola@bitpol.pl", FirstName = "Ola", LastName = "Jaskólska", PasswordHash = "1234", Role = "User" },
     new User { Id = 2, Username = "kamil.sys", Email = "kamil@bitpol.pl", FirstName = "Kamil", LastName = "Sys", PasswordHash = "1234", Role = "User" },
     new User { Id = 3, Username = "ania.ui", Email = "ania@bitpol.pl", FirstName = "Ania", LastName = "UI", PasswordHash = "1234", Role = "User" },
     new User { Id = 4, Username = "mario.q", Email = "mario@bitpol.pl", FirstName = "Mario", LastName = "Q", PasswordHash = "1234", Role = "User" },
     new User { Id = 5, Username = "ewelina.pmo", Email = "ewelina@bitpol.pl", FirstName = "Ewelina", LastName = "PMO", PasswordHash = "1234", Role = "User" },
     new User { Id = 6, Username = "dawid.admin", Email = "dawid@bitpol.pl", FirstName = "Dawid", LastName = "Admin", PasswordHash = "1234", Role = "Admin" },
     new User { Id = 7, Username = "szymon.fullstack", Email = "szymon@bitpol.pl", FirstName = "Szymon", LastName = "Fullstack", PasswordHash = "1234", Role = "User" },
     new User { Id = 8, Username = "gosia.test", Email = "gosia@bitpol.pl", FirstName = "Gosia", LastName = "Testerka", PasswordHash = "1234", Role = "User" },
     new User { Id = 9, Username = "adam.secu", Email = "adam@bitpol.pl", FirstName = "Adam", LastName = "Security", PasswordHash = "1234", Role = "Admin" },
     new User { Id = 10, Username = "karolina.ux", Email = "karolina@bitpol.pl", FirstName = "Karolina", LastName = "UX", PasswordHash = "1234", Role = "User" }
 );

        }
    }
}
