using AutoMapper;
using StarterAPI.Dto;
using StarterAPI.Entities;

namespace StarterAPI.Commons.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Student, StudentLedgerItemDto>().ReverseMap();


            CreateMap<Class, ClassDto>().ReverseMap();

        }
    }
}
