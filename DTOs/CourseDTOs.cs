using Cms.Data.Repository.Models;
using System.Text.Json.Serialization;

namespace Cms.WebApi.DTOs
{
    public class CourseDTO
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int CourseDuration { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] // You need this attribute to convert strings to enum values
        public COURSE_TYPE CourseType { get; set; }

    }

    public enum COURSE_TYPE
    {
        ENGINEERING,
        MEDICAL,
        MANAGEMENT
    }
}
