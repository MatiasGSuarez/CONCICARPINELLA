using AutoMapper;
using PruebaTecnica.Model.DTO;
using PruebaTecnica.Model.Model;

namespace PruebaTecnica.API.Mapper
{
    namespace PruebaTecnica.API.Mappings
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                // Mapeo para Customer y CustomerDto
                CreateMap<Person, PersonDTO>()
                    .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Nombre))
                    .ReverseMap();

                // Mapeo para Paciente y PacienteDTO
                CreateMap<Paciente, PacienteDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Dni, opt => opt.MapFrom(src => src.Dni))
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                    .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento))
                    .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => CalcularEdad(src.FechaNacimiento)))
                    .ReverseMap()
                    .ForMember(dest => dest.Estudios, opt => opt.Ignore());

                // Mapeo para Estudio y EstudioDTO
                CreateMap<Estudio, EstudioDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
                    .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                    .ForMember(dest => dest.FechaSolicitud, opt => opt.MapFrom(src => src.FechaSolicitud))
                    .ForMember(dest => dest.PacienteId, opt => opt.MapFrom(src => src.PacienteId))
                    .ForMember(dest => dest.PacienteNombre, opt => opt.MapFrom(src => src.Paciente != null ? src.Paciente.Nombre : string.Empty))
                    .ForMember(dest => dest.PacienteApellido, opt => opt.MapFrom(src => src.Paciente != null ? src.Paciente.Apellido : string.Empty))
                    .ForMember(dest => dest.MedicoId, opt => opt.MapFrom(src => src.MedicoId))
                    .ForMember(dest => dest.MedicoNombre, opt => opt.MapFrom(src => src.Medico != null ? src.Medico.Nombre : string.Empty))
                    .ForMember(dest => dest.PrestadorId, opt => opt.MapFrom(src => src.PrestadorId))
                    .ForMember(dest => dest.PrestadorNombre, opt => opt.MapFrom(src => src.Prestador != null ? src.Prestador.Nombre : string.Empty))
                    .ReverseMap()
                    .ForMember(dest => dest.Paciente, opt => opt.Ignore())
                    .ForMember(dest => dest.Medico, opt => opt.Ignore())
                    .ForMember(dest => dest.Prestador, opt => opt.Ignore());

                // Mapeo para Medico y MedicoDTO
                CreateMap<Medico, MedicoDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.Matricula, opt => opt.MapFrom(src => src.Matricula))
                    .ReverseMap()
                    .ForMember(dest => dest.Estudios, opt => opt.Ignore());

                // Mapeo para Prestador y PrestadorDTO
                CreateMap<Prestador, PrestadorDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ReverseMap()
                    .ForMember(dest => dest.Estudios, opt => opt.Ignore());
            }

            private int CalcularEdad(DateTime fechaNacimiento)
            {
                var hoy = DateTime.Today;
                int edad = hoy.Year - fechaNacimiento.Year;

                if (fechaNacimiento.Date > hoy.AddYears(-edad))
                    edad--;

                return edad;
            }
        }
    }
}
