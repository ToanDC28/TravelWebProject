using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<Itinerary> Itineraries { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourPlan> TourPlans { get; set; }
        public virtual DbSet<TransactionInfo> TransactionInfos { get; set; }
        public virtual DbSet<TransportationMode> TransportationModes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=TravelWeb;Trusted_Connection=True;TrustServerCertificate=True;User Id=sa;Password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.AmountOfPeople).HasColumnName("amountOfPeople");

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
                    .HasConstraintName("FK_Bookings_Users");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.Property(e => e.DestinationId).HasColumnName("DestinationID");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

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
                    .HasConstraintName("FK__Itinerari__Trans__4E88ABD4");
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

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.Property(e => e.TourId).HasColumnName("TourID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DestinateId).HasColumnName("DestinateID");

                entity.Property(e => e.Duration)
                    .IsRequired()
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

                entity.Property(e => e.TourName)
                    .IsRequired()
                    .HasMaxLength(255);

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

                entity.Property(e => e.PlanId).HasColumnName("planID");

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

            modelBuilder.Entity<TransactionInfo>(entity =>
            {
                entity.HasKey(e => e.InforId);

                entity.Property(e => e.InforId)
                    .ValueGeneratedNever()
                    .HasColumnName("InforID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.ArrangementId).HasColumnName("arrangementId");

                entity.Property(e => e.BookingDate).HasColumnName("bookingDate");

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CreditDebitIndicator).HasColumnName("creditDebitIndicator");

                entity.Property(e => e.CreditorBankNameEn).HasColumnName("creditorBankNameEn");

                entity.Property(e => e.CreditorBankNameVn).HasColumnName("creditorBankNameVn");

                entity.Property(e => e.Currency).HasColumnName("currency");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.OfsAcctName).HasColumnName("ofsAcctName");

                entity.Property(e => e.OfsAcctNo).HasColumnName("ofsAcctNo");

                entity.Property(e => e.Reference).HasColumnName("reference");

                entity.Property(e => e.RunningBalance).HasColumnName("runningBalance");

                entity.Property(e => e.ValueDate).HasColumnName("valueDate");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.TransactionInfos)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_TransactionInfos_Bookings");
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

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("avatar")
                    .IsFixedLength();

                entity.Property(e => e.Birthday)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("birthday")
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("gender")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
