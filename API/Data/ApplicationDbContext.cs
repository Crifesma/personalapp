using API.Data;
using API.Data.Entities;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAE.AIQ.API.Data
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        private IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Create relation into tables
            modelBuilder.Entity<Role>()
            .HasMany(m => m.FunctionalCharacteristics)
            .WithMany(m => m.Roles)
            .UsingEntity<FunctionalCharacteristicByRole>(
                x => x
                .HasOne(ma => ma.FunctionalCharacteristic)
                .WithMany(a => a.FunctionalCharacteristicsByRole)
                .HasForeignKey(ma => ma.FunctionalCharacteristicId),
                x => x
                .HasOne(ma => ma.Role)
                .WithMany(m => m.FunctionalCharacteristicsByRole)
                .HasForeignKey(ma => ma.RoleId),
                x => {
                    x.Property(ma => ma.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    x.HasKey(m => new { m.RoleId, m.FunctionalCharacteristicId });
                });

            modelBuilder.Entity<Role>()
                .HasMany(m => m.User)
                .WithOne(m => m.Role)
                .HasForeignKey(m => m.RoleId);

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<FunctionalCharacteristic> FunctionalCharacteristics { get; set; }
        public DbSet<FunctionalCharacteristicByRole> FunctionalCharacteristicByRoles { get; set; }
    }
}
