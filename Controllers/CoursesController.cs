using Cms.Data.Repository.Models;
using Cms.Data.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cms.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICmsRepository cmsRepository;

        public CoursesController(ICmsRepository cmsRepository)
        {
            this.cmsRepository = cmsRepository;
        }

        [HttpGet]
        public IEnumerable<Course> GetCourses()
        {
            return cmsRepository.GetAllCourses();
        }
    }
}
