using PruebaTecnica.Model.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.DTO
{ 
    public class EstudioDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; }
        public string PacienteApellido { get; set; }
        public int MedicoId { get; set; }
        public string MedicoNombre { get; set; }
        public int PrestadorId { get; set; }
        public string PrestadorNombre { get; set; }
    }
}
