using AutoMapper;
using SmartSchool.API.DTO;
using SmartSchool.API.Models;

namespace SmartSchool.API.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDTO>()
            .ForMember(
                dest => dest.Nome,
                opt => opt.MapFrom(src => $"{src.Nome} {src.SobreNome}" )
            ).ForMember(
                dest => dest.Idade,
                opt => opt.MapFrom(src => src.DataNascimento.GetCurrentAge()) 
            );

            CreateMap<AlunoDTO, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDTO>().ReverseMap();

            CreateMap<Professor, ProfessorDTO>()
            .ForMember(
                dest => dest.Nome, 
                opt => opt.MapFrom(src => $"{src.Nome} {src.SobreNome}")
            );

            CreateMap<ProfessorDTO, Professor>();
            CreateMap<Professor, ProfessorResgistrarDTO>().ReverseMap();
        }
    }
}