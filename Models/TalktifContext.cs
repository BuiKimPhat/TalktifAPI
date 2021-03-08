using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TalktifAPI.Models
{
    public partial class TalktifContext : DbContext
    {
        public TalktifContext()
        {
        }

        public TalktifContext(DbContextOptions<TalktifContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFav> UserFavs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-TPTMLAN; Initial Catalog=Talktif;User ID=Talktif; Password=vanhuuan89");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasOne(d => d.FromNavigation)
                    .WithMany(p => p.MessageFromNavigations)
                    .HasForeignKey(d => d.From)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__from__2D27B809");

                entity.HasOne(d => d.ToNavigation)
                    .WithMany(p => p.MessageToNavigations)
                    .HasForeignKey(d => d.To)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__to__2E1BDC42");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasOne(d => d.ReporterNavigation)
                    .WithMany(p => p.ReportReporterNavigations)
                    .HasForeignKey(d => d.Reporter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reporter__30F848ED");

                entity.HasOne(d => d.SuspectNavigation)
                    .WithMany(p => p.ReportSuspectNavigations)
                    .HasForeignKey(d => d.Suspect)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__suspect__31EC6D26");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdmin).HasDefaultValueSql("((1))");

                entity.Property(e => e.Password).IsUnicode(false);
            });

            modelBuilder.Entity<UserFav>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Favourite })
                    .HasName("PK__User_Fav__A323CB9348DF7B6E");

                entity.HasOne(d => d.FavouriteNavigation)
                    .WithMany(p => p.UserFavFavouriteNavigations)
                    .HasForeignKey(d => d.Favourite)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Favs__favou__2A4B4B5E");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.UserFavUserNavigations)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Favs__user__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
