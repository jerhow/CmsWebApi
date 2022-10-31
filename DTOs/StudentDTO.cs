using Cms.Data.Repository.Models;
using System.ComponentModel.DataAnnotations;

namespace Cms.WebApi.DTOs
{
    public class StudentDTO
    {
        public int StudentId { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }
    }
}
