using PruebaTecnica.Model.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.DTO
{
    public  class PersonDTO: BaseEntityDTO<int>
    {
        //Person
        public string PersonName { get; set; }
        public string PersonLastName { get; set; }
        public string PersonDNI { get; set; }
        public DateTime PersonBirthDate { get; set; }
        public string PersonPhone { get; set; }
        public string PersonEmail { get; set; }
        public int PersonAge { get; set; }

    }
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

    // DTOs para respuestas y consultas
    public class SolicitudResponseDTO
    {
        public int EstudioId { get; set; }
        public string Message { get; set; }
        public string CodigoEstudioGenerado { get; set; }
    }

    public class PacienteDTO
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
    }

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

    public class MedicoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Matricula { get; set; }
    }

    public class PrestadorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
