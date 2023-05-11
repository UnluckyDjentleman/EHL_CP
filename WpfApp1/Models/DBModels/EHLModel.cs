using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WpfApp1.Models.DBModels
{
    public partial class EHLModel : DbContext
    {
        public EHLModel()
            : base("name=EHLModel")
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<bonus> bonus { get; set; }
        public virtual DbSet<MATCHES> MATCHES { get; set; }
        public virtual DbSet<PLAYERS> PLAYERS { get; set; }
        public virtual DbSet<TEAMS> TEAMS { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<TOURNAMENT> TOURNAMENT { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bonus>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.bonus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TEAMS>()
                .HasMany(e => e.MATCHES)
                .WithOptional(e => e.TEAMS)
                .HasForeignKey(e => e.Team1);

            modelBuilder.Entity<TEAMS>()
                .HasMany(e => e.MATCHES1)
                .WithOptional(e => e.TEAMS1)
                .HasForeignKey(e => e.Team2);

            modelBuilder.Entity<TEAMS>()
                .HasMany(e => e.PLAYERS)
                .WithRequired(e => e.TEAMS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TEAMS>()
                .HasMany(e => e.USERS)
                .WithOptional(e => e.TEAMS)
                .HasForeignKey(e => e.Favourite_team);

            modelBuilder.Entity<USERS>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.USERS)
                .HasForeignKey(e => e.userid)
                .WillCascadeOnDelete(false);
        }
    }
}
