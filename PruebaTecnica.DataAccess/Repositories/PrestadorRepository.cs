using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model;
using PruebaTecnica.Model.Model;

namespace PruebaTecnica.DataAccess.Repositories
{
    public class PrestadorRepository : GenericRepository<Prestador>
    {
        public PrestadorRepository(DbModelContext context) : base(context)
        {

        }
    }
}