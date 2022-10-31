using Cms.Data.Repository.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Data.Repository.Repositories
{
    public class InMemoryCmsRepository : ICmsRepository
    {
        List<Course> courses = null;

        public InMemoryCmsRepository()
        {
            courses = new List<Course>();
            
            courses.Add(
                new Course()
                {
                    CourseId = 1,
                    CourseName = "Computer Science",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE.ENGINEERING
                }
            );

            courses.Add(
                new Course()
                {
                    CourseId = 2,
                    CourseName = "Information Technology",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE.ENGINEERING
                }
            );
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return courses;
        }

        public Course AddCourse(Course newCourse)
        {
            var maxCourseId = courses.Max(c => c.CourseId);
            newCourse.CourseId = maxCourseId + 1;
            courses.Add(newCourse);

            return newCourse;
        }

        public bool CourseExists(int courseId)
        {
            return courses.Any(c => c.CourseId == courseId);
        }

        public Course GetCourse(int courseId)
        {
            var result = courses.Where(c => c.CourseId == courseId)
                                .SingleOrDefault();
            return result;
        }

        public Course UpdateCourse(int courseId, Course updatedCourse)
        {
            var course = courses.Where(c => c.CourseId == courseId)
                                .SingleOrDefault();

            if(course != null)
            {
                course.CourseName = updatedCourse.CourseName;
                course.CourseDuration = updatedCourse.CourseDuration;
                course.CourseType = updatedCourse.CourseType;
            }

            return course;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await Task.Run(() => courses.ToList());
        }
    }
}

