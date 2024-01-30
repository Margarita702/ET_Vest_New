using ET_Vest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ET_Vest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<PrintedEdition> PrintedEditions { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<TradeObject > TradeObjects { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<PrintedEditionProvider> PrintedEditionProviders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Define relationships between entities

            modelBuilder.Entity<PrintedEditionProvider>()
                .HasKey(ma => new { ma.PrintedEditionId, ma.ProviderId });

            modelBuilder.Entity<PrintedEditionProvider>()
                .HasOne(ma => ma.PrintedEdition)
                .WithMany(m => m.PrintEditionProviders)
                .HasForeignKey(ma => ma.PrintedEditionId);

            modelBuilder.Entity<PrintedEditionProvider>()
                .HasOne(ma => ma.Provider)
                .WithMany(a => a.PrintEditionProviders)
                .HasForeignKey(ma => ma.ProviderId);
        }

        }
}
