using Microsoft.EntityFrameworkCore;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Data
{
    public class AirportSystemContext : DbContext
    {
        public AirportSystemContext()
        {

        }
        public AirportSystemContext(DbContextOptions<AirportSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PlaneAirport> PlanesAirports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<PlaneAirport>().HasKey(e => new { e.PlaneId, e.AirportId });
            //modelBuilder.Entity<PlaneAirport>(entity =>
            //{
            //    entity.HasKey(e => new { e.PlaneId, e.AirportId });

            //});
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString).UseLazyLoadingProxies();
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
