using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.Model
{
    public class Paciente: Person
    {
        public string Dni { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }

        // Propiedad de navegación para Entity Framework
        public virtual ICollection<Estudio> Estudios { get; set; }
    }
}
