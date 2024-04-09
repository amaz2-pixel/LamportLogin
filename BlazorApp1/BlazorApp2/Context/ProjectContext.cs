using Microsoft.EntityFrameworkCore;
using BlazorApp2.Authentication;
using System;

namespace BlazorApp2.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }

        public DbSet<UserAccount> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.ID); // Use HasKey to specify the key property
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Role).IsRequired();
                entity.Property(e => e.n).IsRequired();
            });

            // Add default users
            modelBuilder.Entity<UserAccount>().HasData(
                new UserAccount { ID = Guid.NewGuid().ToString(), UserName = "user", Password = "c22d5e599c667b0f955573b6d52fb6e7ad625cf770110d19bf813662a4f3d77c", Role = "User", n = 10 },
                new UserAccount { ID = Guid.NewGuid().ToString(), UserName = "admin", Password = "aaafefb91540a69a670dc143a5e23b82fb6330decf5fca6eabd53a7f27a90498", Role = "Administrator", n = 10 }
                // Add more default users as needed
            );
        }
    }
}
