using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BusinessObject.Model
{
    public partial class FBookingDBContext : DbContext
    {
        public FBookingDBContext()
        {
        }

        public FBookingDBContext(DbContextOptions<FBookingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlackList> BlackLists { get; set; }
        public virtual DbSet<BookingRecord> BookingRecords { get; set; }
        public virtual DbSet<BookingRequest> BookingRequests { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FootballField> FootballFields { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=FBookingDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BlackList>(entity =>
            {
                entity.ToTable("BlackList");

                entity.Property(e => e.BanDate).HasColumnType("datetime");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BlackLists)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BlackList__Custo__33D4B598");
            });

            modelBuilder.Entity<BookingRecord>(entity =>
            {
                entity.ToTable("BookingRecord");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.BookingRequest)
                    .WithMany(p => p.BookingRecords)
                    .HasForeignKey(d => d.BookingRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookingRe__Booki__300424B4");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.BookingRecords)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookingRe__Staff__30F848ED");
            });

            modelBuilder.Entity<BookingRequest>(entity =>
            {
                entity.ToTable("BookingRequest");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BookingRequests)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookingRe__Custo__2D27B809");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.BookingRequests)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookingRe__Field__2C3393D0");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534D27F4AE0")
                    .IsUnique();

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<FootballField>(entity =>
            {
                entity.ToTable("FootballField");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.HasIndex(e => e.Email, "UQ__Staff__A9D10534422AE275")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
