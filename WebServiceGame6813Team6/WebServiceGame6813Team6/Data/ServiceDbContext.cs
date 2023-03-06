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

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Users_Id").IsUnique();

            entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
