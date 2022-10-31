using AutoMapper;
using Cms.Data.Repository.Models;
using Cms.WebApi.DTOs;

namespace Cms.WebApi.Mappers
{
    public class CmsMapper : Profile
    {
        public CmsMapper()
        {
            // By calling ReverseMap(), we will get a mapping in each direction.
            // This would be the same as stacking two CreateMap<T> calls with the DTO and the model reversed, as in:
            // CreateMap<CourseDTO, Course>();
            // CreateMap<Course, CourseDTO>();
            //
            // CreateMap<T> creates a mapping configuration profile, which we will then add to the service collection
            // (via dipendency injection)
            CreateMap<CourseDTO, Course>()
                .ReverseMap();

            CreateMap<StudentDTO, Student>()
                .ReverseMap();
        }
    }
}