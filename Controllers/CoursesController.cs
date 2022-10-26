using Cms.Data.Repository.Models;
using Cms.Data.Repository.Repositories;
using Cms.WebApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Cms.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICmsRepository cmsRepository;

        public CoursesController(ICmsRepository cmsRepository) // <-- dependency injection
        {
            this.cmsRepository = cmsRepository;
        }

        // *** Return type - Approach 1 - primitive or complex type ***
        // Approach 1: Before implementing our model as a DTO (Data Transfer Object),
        // which is the preferred way to expose data in a segregated manner (DTOs are
        // preferred for client-facing, and data models are considered to be used internally
        // for direct mapping of database tables).
        //[HttpGet]
        //public IEnumerable<Course> GetCourses()
        //{
        //    return cmsRepository.GetAllCourses();
        //}

        // *** Return type - Approach 1 - primitive or complex type ***
        //[HttpGet]
        //public IEnumerable<CourseDTO> GetCourses()
        //{
        //    try
        //    {
        //        IEnumerable<Course> courses = cmsRepository.GetAllCourses();
        //        var result = MapCourseToCourseDTO(courses);
        //        return result;
        //    }
        //    catch (System.Exception)
        //    {
        //        throw;
        //    }
        //}

        // *** Return type - Approach 2 - IActionResult  ***
        [HttpGet]
        public IActionResult GetCourses()
        {
            try
            {
                IEnumerable<Course> courses = cmsRepository.GetAllCourses();
                var result = MapCourseToCourseDTO(courses);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Custom mapper functions
        private CourseDTO MapCourseToCourseDTO(Course course)
        {
            return new CourseDTO()
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseDuration = course.CourseDuration,
                CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)course.CourseType
            };
        }

        private IEnumerable<CourseDTO> MapCourseToCourseDTO(IEnumerable<Course> courses)
        {
            IEnumerable<CourseDTO> result;
            result = courses.Select(c => new CourseDTO()
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseDuration = c.CourseDuration,
                CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)c.CourseType
            });

            return result;
        }
    }
}
