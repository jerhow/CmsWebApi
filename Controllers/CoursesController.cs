using AutoMapper;
using Cms.Data.Repository.Models;
using Cms.Data.Repository.Repositories;
using Cms.WebApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICmsRepository cmsRepository;
        private IMapper mapper;

        public CoursesController(ICmsRepository cmsRepository, IMapper mapper) // <-- DI (making dependencies available to the controllers via the constructor)
        {
            this.cmsRepository = cmsRepository;
            this.mapper = mapper;
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
        //public IActionResult GetCourses()
        //{
        //    try
        //    {
        //        IEnumerable<Course> courses = cmsRepository.GetAllCourses();
        //        var result = MapCourseToCourseDTO(courses);
        //        return Ok(result);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        // *** Return type - Approach 3 - ActionResult<T>  ***
        //[HttpGet]
        //public ActionResult<IEnumerable<CourseDTO>> GetCourses()
        //{
        //    try
        //    {
        //        IEnumerable<Course> courses = cmsRepository.GetAllCourses();
        //        var result = MapCourseToCourseDTO(courses);
        //        return result.ToList(); // Convert to support ActionResult<T>
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        // ***  ***
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCoursesAsync()
        {
            try
            {
                IEnumerable<Course> courses = await cmsRepository.GetAllCoursesAsync();
                // var result = MapCourseToCourseDTO(courses); // Now we will use the AutoMapper instead (next line)
                var result = mapper.Map<CourseDTO[]>(courses); // This replaces the need for our custom mapping methods
                return result.ToList(); // Convert to support ActionResult<T>
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<CourseDTO> AddCourse([FromBody]CourseDTO course) // [FromBody] states that we expect the input for this DTO/model to be in the body of the HTTP request
        {
            try
            {
                var newCourse = mapper.Map<Course>(course);
                newCourse = cmsRepository.AddCourse(newCourse);
                return mapper.Map<CourseDTO>(newCourse); // Remember, we need to map this model back to a DTO to return to the client
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDTO> GetCourse(int courseId)
        {
            try
            {
                if (!cmsRepository.CourseExists(courseId))
                {
                    return NotFound();
                }

                Course course = cmsRepository.GetCourse(courseId);
                var result = mapper.Map<CourseDTO>(course);
                return result;
            }
            catch(System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{courseId}")]
        public ActionResult<CourseDTO> UpdateCourse(int courseId, CourseDTO course)
        {
            try
            {
                if (!cmsRepository.CourseExists(courseId))
                {
                    return NotFound();
                }

                Course updatedCourse = mapper.Map<Course>(course); // convert from DTO to model
                updatedCourse = cmsRepository.UpdateCourse(courseId, updatedCourse); // update course in the data repository
                var result = mapper.Map<CourseDTO>(updatedCourse); // convert updated model back to a DTO that can be sent back to the client
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{courseId}")]
        public ActionResult<CourseDTO> DeleteCourse(int courseId)
        {
            try
            {
                if (!cmsRepository.CourseExists(courseId))
                {
                    return NotFound();
                }

                Course course = cmsRepository.DeleteCourse(courseId);

                if (course == null)
                {
                    return BadRequest();
                }

                var result = mapper.Map<CourseDTO>(course);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET ../courses/1/students
        [HttpGet("{courseId}/students")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents(int courseId)
        {
            try
            {
                if (!cmsRepository.CourseExists(courseId))
                {
                    return NotFound();
                }

                IEnumerable<Student> students = cmsRepository.GetStudents(courseId);
                var result = mapper.Map<StudentDTO[]>(students);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST ../courses/1/students
        [HttpPost("{courseId}/students")]
        public ActionResult<StudentDTO> AddStudent(int courseId, StudentDTO student)
        {
            try
            {
                if (!cmsRepository.CourseExists(courseId))
                {
                    return NotFound();
                }

                Student newStudent = mapper.Map<Student>(student); // convert from DTO to model

                // Fetch the course in question and associate it with the student model
                Course course = cmsRepository.GetCourse(courseId);
                newStudent.Course = course;

                newStudent = cmsRepository.AddStudent(newStudent); // add new student to the data repository
                var result = mapper.Map<StudentDTO>(newStudent); // convert updated model back to a DTO that can be sent back to the client

                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #region Custom mapper methods
        //private CourseDTO MapCourseToCourseDTO(Course course)
        //{
        //    return new CourseDTO()
        //    {
        //        CourseId = course.CourseId,
        //        CourseName = course.CourseName,
        //        CourseDuration = course.CourseDuration,
        //        CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)course.CourseType
        //    };
        //}

        //private IEnumerable<CourseDTO> MapCourseToCourseDTO(IEnumerable<Course> courses)
        //{
        //    IEnumerable<CourseDTO> result;
        //    result = courses.Select(c => new CourseDTO()
        //    {
        //        CourseId = c.CourseId,
        //        CourseName = c.CourseName,
        //        CourseDuration = c.CourseDuration,
        //        CourseType = (Cms.WebApi.DTOs.COURSE_TYPE)c.CourseType
        //    });

        //    return result;
        //}
        #endregion Custom mapper methods
    }
}
