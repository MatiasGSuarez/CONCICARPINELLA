using PruebaTecnica.Model.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.DTO
{
    //Tiene esta forma porque asi ingresa en el json
    public class SolicitudEstudioDTO
    {
        public int SolicitudId { get; set; }
        public PacienteSolicitudDTO Paciente { get; set; }
        public EstudioSolicitudDTO Estudio { get; set; }
        public MedicoSolicitudDTO Medico { get; set; }
        public int PrestadorId { get; set; }
    }

    public class PacienteSolicitudDTO
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }

    public class EstudioSolicitudDTO
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
    }

    public class MedicoSolicitudDTO
    {
        public string Nombre { get; set; }
        public string Matricula { get; set; }
    }

    public class SolicitudResponseDTO
    {
        public int EstudioId { get; set; }
        public string Message { get; set; }
        public string CodigoEstudioGenerado { get; set; }
    }
}
