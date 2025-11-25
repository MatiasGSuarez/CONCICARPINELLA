using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using PruebaTecnica.Model.Model;
using PruebaTecnica.Model.Base;
using PruebaTecnica.Model.ModelUser;

namespace PruebaTecnica.Model
{
    public class DbModelContext : DbContext
    {
        public DbSetEntities DbSets { get; } = new DbSetEntities();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                              .AddJsonFile("appsettings.json")
                              .Build();
                var str = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(str))
                    throw new Exception("No hay string de conexión...");

                optionsBuilder
                    .UseSqlServer(str)
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));

                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Estudio>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Medico>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Paciente>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Prestador>().HasKey(x => new { x.Id });

            //Elimina ciclos de eliminacion en cascada
            var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            AddMyFilters(ref modelBuilder);

            modelBuilder.Seed();
        }

        private void AddMyFilters(ref ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //other automated configurations left out   
                if (entityType.ClrType != null && typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType) && entityType.BaseType == null)
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
        }
    }

    public static class ModelBuilderExtensions
    {
        #region SEEDS
        public static void Seed(this ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                SeedAppUser(modelBuilder);
                SeedRole(modelBuilder);
            }
        }
        private static void SeedAppUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = 1,
                Name = "admin",
                Password = "admin",
                RoleId = 1,
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }
        private static void SeedRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Name = "Admin",
                Permissions = "FullAccess",
                CreatedBy = "System",
                Created = DateTime.Now,
            });
        }

        #endregion
    }
}
