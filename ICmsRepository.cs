using Cms.Data.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Data.Repository.Repositories
{
    public interface ICmsRepository
    {
        IEnumerable<Course> GetAllCourses();
        
        Task<IEnumerable<Course>> GetAllCoursesAsync();

        Course AddCourse(Course newCourse);

        bool CourseExists(int courseId);

        Course GetCourse(int courseId);

        Course UpdateCourse(int courseId, Course updatedCourse);

        Course DeleteCourse(int courseId);
    }
}

