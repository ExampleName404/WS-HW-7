using UniversityCoursesAPI.Models;

namespace UniversityCoursesAPI.Services;

public class CourseService
{
    private readonly object _lock = new();
    private int _nextId = 6;

    private readonly List<Course> _courses = new()
    {
        new Course { Id = 1, Title = "Основы программирования на C#", Description = "Синтаксис языка, ООП и работа с коллекциями", Teacher = "Алексей Смирнов", Duration = 72 },
        new Course { Id = 2, Title = "Базы данных", Description = "Реляционная модель, SQL и проектирование схем", Teacher = "Айгерим Нурланова", Duration = 48 },
        new Course { Id = 3, Title = "Разработка веб-сервисов", Description = "ASP.NET Core Web API, аутентификация и авторизация", Teacher = "Евгений Герцен", Duration = 64 },
        new Course { Id = 4, Title = "Алгоритмы и структуры данных", Description = "Сортировки, деревья, графы и оценка сложности", Teacher = "Дмитрий Ковалёв", Duration = 80 },
        new Course { Id = 5, Title = "Математический анализ", Description = "Пределы, производные и интегралы", Teacher = "Мария Петрова", Duration = 96 }
    };

    public List<Course> GetAll()
    {
        lock (_lock)
        {
            return _courses.ToList();
        }
    }

    public Course? Get(int id)
    {
        lock (_lock)
        {
            return _courses.FirstOrDefault(c => c.Id == id);
        }
    }

    public Course Add(Course course)
    {
        lock (_lock)
        {
            course.Id = _nextId++;
            _courses.Add(course);
            return course;
        }
    }

    public Course? Update(int id, Course course)
    {
        lock (_lock)
        {
            var existing = _courses.FirstOrDefault(c => c.Id == id);
            if (existing == null)
                return null;

            existing.Title = course.Title;
            existing.Description = course.Description;
            existing.Teacher = course.Teacher;
            existing.Duration = course.Duration;
            return existing;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            return _courses.RemoveAll(c => c.Id == id) > 0;
        }
    }
}
