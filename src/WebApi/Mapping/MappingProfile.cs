using AutoMapper;
using Core.Data.DTO;
using Core.Data.Entity;
using System.Linq;

namespace WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Grade, GradeDTO>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.ToString()));
            CreateMap<Exam, ExamDTO>()
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.StudentExams.Select(x => x.Result)));
            CreateMap<Lesson, LessonDTO>()
                .ForMember(dest => dest.LessonName, opt => opt.MapFrom(src => src.Name));
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.StudentCode, opt => opt.MapFrom(src => src.StudentCode));





            CreateMap<Student, StudentDTO>();
        }
    }
}
