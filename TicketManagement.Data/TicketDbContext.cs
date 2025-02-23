using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.Model;

namespace TicketManagement.Data
{
    public class TicketDbContext: DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventTicket> EventTickets { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ConcertTicketsDb;Integrated Security=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Reservation>()
                .HasOne(tr => tr.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(tr => tr.UserId);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventTickets)
                .WithOne(e => e.Event)
                .HasForeignKey(fk => fk.EventId);

            modelBuilder.Entity<Reservation>()
                .HasOne(tr => tr.EventTicket) 
                .WithMany(et => et.Reservations)
                .HasForeignKey(tr => tr.EventTicketId);

            modelBuilder.Entity<EventTicket>()
                .Property(tt => tt.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin", RegistrationDate = DateTime.UtcNow },
                new User { Id = 2, Username = "user", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"), Role = "User", RegistrationDate = DateTime.UtcNow },
                new User { Id = 3, Username = "bob", PasswordHash = BCrypt.Net.BCrypt.HashPassword("bob123"), Role = "Event Manager", RegistrationDate = DateTime.UtcNow }
            );
        }

    }
}
