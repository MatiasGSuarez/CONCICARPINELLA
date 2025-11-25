using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model;
using PruebaTecnica.Model.Model;

namespace PruebaTecnica.DataAccess.Repositories
{
    public class EstudioRepository : GenericRepository<Estudio>
    {
        public EstudioRepository(DbModelContext context) : base(context)
        {

        }
    }
}