using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.Model
{
    public class Estudio
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }

        // Foreign Keys
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int PrestadorId { get; set; }

        // Propiedades de navegación
        public virtual Paciente Paciente { get; set; }
        public virtual Medico Medico { get; set; }
        public virtual Prestador Prestador { get; set; }
    }
}
