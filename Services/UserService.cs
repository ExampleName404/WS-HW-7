using UniversityCoursesAPI.Models;

namespace UniversityCoursesAPI.Services;

public class UserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, UserName = "AdminUser", Password = "Admin123", Email = "admin@university.test", Role = "Admin" },
        new User { Id = 2, UserName = "TeacherUser", Password = "Teacher123", Email = "teacher@university.test", Role = "Teacher" },
        new User { Id = 3, UserName = "StudentUser", Password = "Student123", Email = "student@university.test", Role = "Student" }
    };

    public User? Validate(string userName, string password)
    {
        return _users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
    }
}
