using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Data.Context
{
    public partial class GeneralDynamicsAIContext : DbContext
    {

        public GeneralDynamicsAIContext()
        {
        }

        public GeneralDynamicsAIContext(DbContextOptions<GeneralDynamicsAIContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("GDRole");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.HasKey(x => x.Id);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("Code");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("Description");

                //entity.HasMany(e => e.Users)
                //.WithOne(d => d.Role)
                //.HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("GDUser");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.HasKey(x => x.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("LastName");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("Email");

                entity.Property(e => e.Phone)
                    .HasColumnName("Phone");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("UserName");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("Password");

                entity.Property(e => e.Token)
                    .HasColumnName("Token");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleId");

                entity.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");

                //entity.HasOne(p => p.Role)
                //.WithMany(b => b.Users)
                //.HasConstraintName("FK_User_Role");
            });

        }
    }
}
