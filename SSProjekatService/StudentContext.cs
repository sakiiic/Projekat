using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SSProjekatService.DTO;
using SSProjekatService.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SSProjekatService
{
    public class StudentContext : DbContext, IUnitOfWork
    {
        [Obsolete]
        public static readonly LoggerFactory LoggerFactory
            = new LoggerFactory(new[] {
                new ConsoleLoggerProvider((category, level)
                    // filtering what is logged
                    => true/*category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information*/, true)
            });

        public DbSet<StudentModel> Studenti { get; set; }
        public IConfiguration Configuration { get; }


        public StudentContext(IConfiguration configuration)
            // C# 7.0 in a Nutshell (p. 104)
            // If a constructor in a subclass omits the base keyword, the base
            // type’s parameterless constructor is implicitly called.
            : base()
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //PM> Install-Package Microsoft.EntityFrameworkCore -Version 2.2.3
            //PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 2.2.3
            //PM> Install-Package Microsoft.Extensions.Logging.Console -Version 2.2.0

            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Database"))
                .UseLoggerFactory(LoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
                .EnableSensitiveDataLogging(true)
                // Entity Framework - Loading Related Data - Eager loading (p. 233)
                // Change the behavior when an include operator is ignored to either throw or do nothing
                .ConfigureWarnings(warnings => warnings.Throw(CoreEventId.IncludeIgnoredWarning))
                // Throw an exception for client evaluation
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var internalBuilder = modelBuilder.GetInfrastructure<InternalModelBuilder>();

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                internalBuilder
                    .Entity(entity.Name, ConfigurationSource.Convention)
                    .Relational(ConfigurationSource.Convention)
                    .ToTable(entity.ClrType.Name);
            }
        }
        public void Commit()
        {
            base.SaveChanges();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
