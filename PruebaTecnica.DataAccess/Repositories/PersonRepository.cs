using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model;
using PruebaTecnica.Model.Model;

namespace PruebaTecnica.DataAccess.Repositories
{
    public class PersonRepository : GenericRepository<Person>
    {
        public PersonRepository(DbModelContext context) : base(context)
        {

        }
    }
}