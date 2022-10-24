using Cms.Data.Repository.Models;
using System.Collections.Generic;

namespace Cms.Data.Repository.Repositories
{
    public class SqlCmsRepository : ICmsRepository
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

