using System.ComponentModel.DataAnnotations;

namespace UniversityCoursesAPI.Models;

public class Course
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Teacher { get; set; } = string.Empty;

    [Range(1, 1000)]
    public int Duration { get; set; }
}
