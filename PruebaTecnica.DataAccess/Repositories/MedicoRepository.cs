using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model;
using PruebaTecnica.Model.Model;

namespace PruebaTecnica.DataAccess.Repositories
{
    public class MedicoRepository : GenericRepository<Medico>
    {
        public MedicoRepository(DbModelContext context) : base(context)
        {

        }
    }
}