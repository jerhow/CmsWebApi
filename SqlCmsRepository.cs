using Cms.Data.Repository.Models;
using System.Collections.Generic;

namespace Cms.Data.Repository.Repositories
{
    public class SqlCmsRepository // : ICmsRepository <-- Removing this requirement for now since we are focusing on the in-memory data repository
    {
        public SqlCmsRepository()
        {

        }

        public IEnumerable<Course> GetAllCourses()
        {
            return null;
        }
    }
}

