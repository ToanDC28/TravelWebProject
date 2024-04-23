using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models
{
    public partial class TravelWebContext : DbContext
    {
        public TravelWebContext()
        {
        }

        public TravelWebContext(DbContextOptions<TravelWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Destination> Destinations { get; set; } = null!;
        public virtual DbSet<Itinerary> Itineraries { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Tour> Tours { get; set; } = null!;
        public virtual DbSet<TourPlan> TourPlans { get; set; } = null!;
        public virtual DbSet<TransportationMode> TransportationModes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
                return builder.Build().GetConnectionString("DefaultConnection");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PaymentDeadline).HasColumnType("datetime");

                entity.Property(e => e.RemainingAmount).HasColumnType("money");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.TourId).HasColumnName("TourID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK__Bookings__TourID__3C69FB99");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Bookings__UserID__3B75D760");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.Property(e => e.DestinationId).HasColumnName("DestinationID");

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.Image)
                    .HasMaxLength(10)
                    .HasColumnName("image")
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.RegionId).HasColumnName("regionID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Destinations)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destinations_Region");
            });

            modelBuilder.Entity<Itinerary>(entity =>
            {
                entity.Property(e => e.ItineraryId).HasColumnName("ItineraryID");

                entity.Property(e => e.DestinationId).HasColumnName("DestinationID");

                entity.Property(e => e.TourId).HasColumnName("TourID");

                entity.Property(e => e.TransportationModeId).HasColumnName("TransportationModeID");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.Itineraries)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK__Itinerari__Desti__4AB81AF0");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Itineraries)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK__Itinerari__TourI__49C3F6B7");

                entity.HasOne(d => d.TransportationMode)
                    .WithMany(p => p.Itineraries)
                    .HasForeignKey(d => d.TransportationModeId)
                    .HasConstraintName("FK__Itinerari__Trans__4BAC3F29");
            });


            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.PaymentType).HasMaxLength(50);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__Payments__Bookin__3F466844");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.TourId).HasColumnName("TourID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_Reviews_Tours");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Reviews__UserID__4316F928");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.Property(e => e.TourId).HasColumnName("TourID");

                entity.Property(e => e.DestinateId).HasColumnName("DestinateID");

                entity.Property(e => e.Duration)
                    .HasMaxLength(10)
                    .HasColumnName("duration")
                    .IsFixedLength();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MinAge).HasColumnName("minAge");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TotalCost).HasColumnType("money");

                entity.Property(e => e.TotalRating)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TourName).HasMaxLength(255);

                entity.Property(e => e.TransportId).HasColumnName("transportID");

                entity.HasOne(d => d.Destinate)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.DestinateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tours_Destinations");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.TransportId)
                    .HasConstraintName("FK_Tours_TransportationModes");
            });

            modelBuilder.Entity<TourPlan>(entity =>
            {
                entity.HasKey(e => e.PlanId);

                entity.ToTable("TourPlan");

                entity.Property(e => e.PlanId)
                    .ValueGeneratedNever()
                    .HasColumnName("planID");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.TourId).HasColumnName("tourID");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourPlans)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_TourPlan_Tours");
            });

            modelBuilder.Entity<TransportationMode>(entity =>
            {
                entity.Property(e => e.TransportationModeId).HasColumnName("TransportationModeID");

                entity.Property(e => e.Mode).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "UQ__Users__536C85E4202D300C")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FullName).HasMaxLength(50)
                    .HasColumnName("FullName")
                    .IsFixedLength();

                entity.Property(e => e.Avatar)
                    .HasMaxLength(10)
                    .HasColumnName("avatar")
                    .IsFixedLength();

                entity.Property(e => e.Birthday)
                    .HasMaxLength(10)
                    .HasColumnName("birthday")
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender")
                    .IsFixedLength();

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.Username).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
