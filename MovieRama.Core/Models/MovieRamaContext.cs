using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieRama.Core.Models
{
	/// <summary>
	/// MovieRama db context
	/// </summary>
    public partial class MovieRamaContext : DbContext
    {
        public MovieRamaContext()
        {
        }

        public MovieRamaContext(DbContextOptions<MovieRamaContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<UserOpinion> UserOpinion { get; set; }

        // Unable to generate entity type for table 'dbo.User'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.PublicationDate).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserOpinion>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Movie });

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateTime).HasColumnType("datetime");
            });
        }
    }
}
