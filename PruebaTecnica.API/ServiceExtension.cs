using PruebaTecnica.Business;
using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.DataAccess.Repositories;

namespace PruebaTecnica.API
{
    public static class ServiceExtension
    {
        public static void AddInfraestructureServices(this IServiceCollection services)
        {
        }

        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<UnitOfWork, UnitOfWork>();

            services.AddScoped<AppUserRepository, AppUserRepository>();
            
            services.AddScoped<PersonRepository, PersonRepository>();           
            services.AddScoped<MedicoRepository, MedicoRepository>();           
            services.AddScoped<EstudioRepository, EstudioRepository>();           
            services.AddScoped<PacienteRepository, PacienteRepository>();           
            services.AddScoped<PrestadorRepository, PrestadorRepository>();           
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationBusiness, AuthenticationBusiness>();
            
            //Person
            services.AddScoped<PersonBusiness, PersonBusiness>(); 
            services.AddScoped<MedicoBusiness, MedicoBusiness>(); 
            services.AddScoped<EstudioBusiness, EstudioBusiness>(); 
            services.AddScoped<PacienteBusiness, PacienteBusiness>(); 
            services.AddScoped<PrestadorBusiness, PrestadorBusiness>(); 
        }
    }
}