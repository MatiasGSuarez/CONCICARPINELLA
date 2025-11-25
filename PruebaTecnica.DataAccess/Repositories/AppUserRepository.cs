using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model;
using PruebaTecnica.Model.ModelUser;

namespace PruebaTecnica.DataAccess.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>
    {
        public AppUserRepository(DbModelContext context) : base(context)
        { }
    }
}
