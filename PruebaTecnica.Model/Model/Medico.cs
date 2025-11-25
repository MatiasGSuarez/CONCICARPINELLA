using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.Model
{
    public class Medico: Person
    {
        //Lo cambio para hacer las modif que pide sobre la matricula
        //public int Matricula { get; set; }
        public string Matricula { get; set; }

        // Propiedad de navegación
        public virtual ICollection<Estudio> Estudios { get; set; }
    }
}
