using PruebaTecnica.DataAccess.Generic;
using PruebaTecnica.Model.ModelUser;

namespace PruebaTecnica.Business
{
    public class AuthenticationBusiness
    {
        private readonly UnitOfWork unitOfWork;
        public AuthenticationBusiness(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Authenticate(string userName, string password)
        {
            var users = await unitOfWork.AddRepositories.GetRepository<AppUser>().GetAsync(x => x.Name == userName & x.Password == password);
            if (users.Count() != 1)
                throw new Exception("Credenciales inválidas");
        }
    }
}
