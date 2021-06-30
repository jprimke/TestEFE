using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestEFE.Models;

namespace TestEFE.Database
{
    public partial class GenericDataContext : DbContext
    {
        private GenericDataContext()
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
        }

        public GenericDataContext(DbContextOptions<GenericDataContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
        }

        public virtual DbSet<AutoPaket> AutoPaketSet => Set<AutoPaket>();

        public virtual DbSet<DefaultPaket> DefaultPaketSet => Set<DefaultPaket>();

        public virtual DbSet<HomePaket> HomePaketSet => Set<HomePaket>();

        public virtual DbSet<Paket> PaketSet => Set<Paket>();

        public virtual DbSet<Policy> PolicySet => Set<Policy>();

        public virtual DbSet<TravelPaket> TravelPaketSet => Set<TravelPaket>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            OnModelCreatingPartial();
        }

        public override int SaveChanges()
        {
            SetLastChanged();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SetLastChanged();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetLastChanged()
        {
            var entries = ChangeTracker.Entries()
                                       .Where(x =>
                                                  (x.Entity is ITraceableData)
                                                  && ((x.State == EntityState.Added) || (x.State == EntityState.Modified)));

            foreach (var entry in entries)
            {
                var traceableEntry = ((ITraceableData)entry.Entity);

                if (entry.State == EntityState.Added)
                {
                    traceableEntry.Created = DateTime.UtcNow;
                    if (string.IsNullOrWhiteSpace(traceableEntry.CreatedBy))
                    {
                        traceableEntry.CreatedBy = "AXA Partners";
                    }
                }

                traceableEntry.LastChanged = DateTime.UtcNow;
                if (string.IsNullOrWhiteSpace(traceableEntry.ChangedBy))
                {
                    traceableEntry.ChangedBy = "AXA Partners";
                }
            }
        }

        partial void OnModelCreatingPartial();
    }


    public class GenericDataContextFactory : IDesignTimeDbContextFactory<GenericDataContext>
    {
        public GenericDataContext CreateDbContext(string[] args)
        {
            var envAppsettings = $"appsettings.{(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production")}.json";

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false)
                                                   .AddJsonFile(envAppsettings, optional: true)                                 
                                                   .Build();

            var optionsBuilder = new DbContextOptionsBuilder<GenericDataContext>();
            optionsBuilder
                // Uncomment the following line if you want to print generated
                // SQL statements on the console.
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer(config.GetConnectionString("GenericData"));

            return new GenericDataContext(optionsBuilder.Options);
        }
    }

}
