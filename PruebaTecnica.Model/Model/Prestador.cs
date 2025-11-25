using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.Model
{
    public class Prestador: Person
    {

        // Propiedad de navegación
        public virtual ICollection<Estudio> Estudios { get; set; }
    }
}
