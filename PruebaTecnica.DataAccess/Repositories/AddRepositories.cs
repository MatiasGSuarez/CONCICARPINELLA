using PruebaTecnica.Model;
using PruebaTecnica.Model.Model;
using PruebaTecnica.Model.ModelUser;
using PruebaTecnica.DataAccess.Generic;

namespace PruebaTecnica.DataAccess.Repositories
{
    public class AddRepositories
    {
        private readonly DbModelContext _context;

        // Diccionario para almacenar las instancias de los repositorios
        private readonly Dictionary<Type, object> _repositories;

        public AddRepositories(DbModelContext context)
        {
            _context = context;

            // ACA DEFINIR LOS REPOSITORIOS <-----------------------------------------------------------------------
            _repositories = new Dictionary<Type, object>
            {
                //Usuarios
                { typeof(AppUser), new AppUserRepository(_context) },
                //Persona
                { typeof(Person), new PersonRepository(_context) },
                { typeof(Medico), new MedicoRepository(_context) },
                { typeof(Paciente), new PacienteRepository(_context) },
                { typeof(Estudio), new EstudioRepository(_context) },
                { typeof(Prestador), new PrestadorRepository(_context) },
            };
        }

        // Método genérico GetRepository para obtener el repositorio basado en el tipo
        public GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            if (_repositories.TryGetValue(type, out var repository))
            {
                return repository as GenericRepository<TEntity>;
            }
            throw new NotSupportedException($"Repository for type {type.Name} not found.");
        }
    }
}
