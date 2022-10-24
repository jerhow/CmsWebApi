using Cms.Data.Repository.Models;
using System.Collections.Generic;

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
    }
}

