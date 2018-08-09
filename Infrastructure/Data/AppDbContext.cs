using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<FlightInstance> FlightInstances { get; set; }
        public DbSet<FlightRoute> FlightRoutes { get; set; }
        public DbSet<FlightTicket> FlightTickets { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Airport>(ConfigureAirport);
            builder.Entity<City>(ConfigureCity);
            builder.Entity<Country>(ConfigureCountry);
            builder.Entity<FlightInstance>(ConfigureFlightInstance);
            builder.Entity<FlightRoute>(ConfigureFlightRoute);
            builder.Entity<FlightTicket>(ConfigureFlightTicket);
            builder.Entity<RequestLog>(ConfigureRequestLog);
            builder.Entity<ApplicationUser>(ConfigureApplicationUser);
        }

        private void ConfigureAirport(EntityTypeBuilder<Airport> builder)
        {
            builder.ToTable("Airport");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Code).IsRequired().HasMaxLength(20);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(200);
            builder.HasOne(a => a.City).WithMany().HasForeignKey(a => a.CityId);
            builder.HasMany(a => a.Origins).WithOne(f => f.Origin);
            builder.HasMany(a => a.Destinations).WithOne(f => f.Destination);
        }

        private void ConfigureCity(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasOne(c => c.Country).WithMany().HasForeignKey(c => c.CountryId);
        }

        private void ConfigureCountry(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(c => c.Code).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        }

        private void ConfigureFlightInstance(EntityTypeBuilder<FlightInstance> builder)
        {
            builder.ToTable("FlightInstance");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(f => f.Code).IsRequired().HasMaxLength(20);
            builder.HasOne(f => f.FlightRoute).WithMany().HasForeignKey(f => f.FlightRouteId);
        }

        private void ConfigureFlightRoute(EntityTypeBuilder<FlightRoute> builder)
        {
            builder.ToTable("FlightRoute");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(f => f.Code).IsRequired().HasMaxLength(20);
            builder.HasOne(f => f.Origin).WithMany(a => a.Origins).HasForeignKey(f => f.OriginId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(f => f.Destination).WithMany(a => a.Destinations).HasForeignKey(f => f.DestinationId).OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureFlightTicket(EntityTypeBuilder<FlightTicket> builder)
        {
            builder.ToTable("FlightTicket");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(f => f.Code).IsRequired().HasMaxLength(20);
            builder.HasOne(f => f.FlightInstance).WithMany().HasForeignKey(f => f.FlightInstanceId);
        }

        private void ConfigureRequestLog(EntityTypeBuilder<RequestLog> builder)
        {
            builder.ToTable("RequestLog");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(r => r.RequestMethod).IsRequired().HasMaxLength(20);
            builder.Property(r => r.ResponseStatusCode).IsRequired().HasMaxLength(20);
            builder.Property(r => r.UrlPath).IsRequired().HasMaxLength(200);
        }

        private void ConfigureApplicationUser(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd().IsRequired();
        }
    }
}
