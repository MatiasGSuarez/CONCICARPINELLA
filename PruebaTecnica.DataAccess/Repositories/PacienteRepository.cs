using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model;
using PruebaTecnica.Model.Model;

namespace PruebaTecnica.DataAccess.Repositories
{
    public class PacienteRepository : GenericRepository<Paciente>
    {
        public PacienteRepository(DbModelContext context) : base(context)
        {

        }
    }
}