    using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjektInz.Data.Models
{
    public partial class DevDbContext : DbContext
    {
        private readonly DbContextOptions<DevDbContext> _options;
        public DevDbContext(DbContextOptions<DevDbContext> options)
            : base(options)
        {

            _options = options;
        }

        public virtual DbSet<CurrentWeatherFromApi> CurrentWeatherFromApi { get; set; }
        public virtual DbSet<ForecastWeatherFromApi> ForecastWeatherFromApi { get; set; }
        public virtual DbSet<SensorRead> SensorRead { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//               // optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DevDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CurrentWeatherFromApi>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ForecastWeatherFromApi>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                //entity.Property(e => e.DtTxt)
                //    .IsRequired()
                //    .HasColumnName("dt_txt")
                //    .HasMaxLength(100)
                //    .IsUnicode(false);
                entity.Property(e => e.DtTxt)
                .HasColumnName("dt_txt")
                .HasColumnType("datetime");

                entity.Property(e => e.ReadDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SensorRead>(entity =>
            {
                entity.Property(e => e.ReadDate).HasColumnType("datetime");

                entity.Property(e => e.SensorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
