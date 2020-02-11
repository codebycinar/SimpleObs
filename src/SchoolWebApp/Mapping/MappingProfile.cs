using AutoMapper;
using Core.Data.DTO;
using Core.Data.Entity;

namespace SchoolWebApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Grade, GradeDTO>()
                .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.GradeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GradeLessonResults, opt => opt.MapFrom(src => src.LessonResults));

            CreateMap<GradeLesson, GradeLessonResultDTO>()
                .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.Average, opt => opt.MapFrom(src => src.Average));

            CreateMap<Exam, ExamDTO>()
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson));

            CreateMap<Lesson, LessonDTO>()
                .ForMember(dest => dest.LessonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LessonName, opt => opt.MapFrom(src => src.Name));

            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.StudentCode, opt => opt.MapFrom(src => src.StudentCode))
                .ForMember(dest => dest.ExamsResults, opt => opt.MapFrom(src => src.StudentExams))
            .ForMember(dest => dest.LessonResults, opt => opt.MapFrom(src => src.StudentLessons));
          
            CreateMap<Student, StudentDTO>();
        }
    }
}
