using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ServiceDb.Models;

namespace ServiceDb.Data;

public partial class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("Game");

            entity.HasIndex(e => e.GameId, "IX_Game_game_id").IsUnique();

            entity.HasIndex(e => e.Name, "IX_Game_name").IsUnique();

            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.ToTable("Match");

            entity.HasIndex(e => e.MatchId, "IX_Match_match_id").IsUnique();

            entity.Property(e => e.MatchId).HasColumnName("match_id");
            entity.Property(e => e.FirstProfileId).HasColumnName("first_profile_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.SecondProfileId).HasColumnName("second_profile_id");

            entity.HasOne(d => d.FirstProfile).WithMany(p => p.MatchFirstProfiles)
                .HasForeignKey(d => d.FirstProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Game).WithMany(p => p.Matches)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SecondProfile).WithMany(p => p.MatchSecondProfiles)
                .HasForeignKey(d => d.SecondProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("Profile");

            entity.HasIndex(e => e.ProfileId, "IX_Profile_profile_id").IsUnique();

            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.BehaviorIndex).HasColumnName("behavior_index");
            entity.Property(e => e.Elo).HasColumnName("ELO");
            entity.Property(e => e.PrivacyBool)
                .HasDefaultValueSql("0")
                .HasColumnType("BOOLEAN")
                .HasColumnName("privacy_bool");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Users_Id").IsUnique();

            entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();

            entity.Property(e => e.ProfileId).HasColumnName("profile_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.Users).HasForeignKey(d => d.ProfileId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
