using Cms.Data.Repository.Models;
using System.Collections.Generic;

namespace Cms.Data.Repository.Repositories
{
    public interface ICmsRepository
    {
        IEnumerable<Course> GetAllCourses();
    }
}

