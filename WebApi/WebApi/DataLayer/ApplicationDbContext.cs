using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;
using WebApi.Models;

namespace WebApi.DataLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<User> User { get; set; }
        public DbSet<Cluster> Cluster { get; set; }
        public DbSet<URL> URL { get; set; }
        public DbSet<Strategy> Strategy { get; set; }
        public DbSet<UrlStrategy> UrlStrategies { get; set; }
        public DbSet<ExtendedCluster> ExtendedCluster { get; set; }
        public DbSet <ClusterData> ClusterData  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlStrategy>()
    .HasKey(us => new { us.UrlId, us.StrategyName });
            modelBuilder.Entity<UrlStrategy>()
                .HasOne(us => us.URL)
                .WithMany(u => u.UrlStrategies)
                .HasForeignKey(us => us.UrlId);
            modelBuilder.Entity<UrlStrategy>()
                .HasOne(us => us.Strategy)
                .WithMany(s => s.UrlStrategies)
                .HasForeignKey(us => us.StrategyName);
            base.OnModelCreating(modelBuilder);
        }
    }
}
