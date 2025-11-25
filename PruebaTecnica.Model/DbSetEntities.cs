using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Model.Model;
using PruebaTecnica.Model.ModelUser;

namespace PruebaTecnica.Model
{
    public class DbSetEntities
    {
        public DbSet<Estudio> Estudio { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Prestador> Prestador { get; set; }
    }
}
