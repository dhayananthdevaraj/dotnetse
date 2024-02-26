using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class CourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }

          public async Task<IEnumerable<Course>> GetAllCoursesForStudent()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.CourseID == courseId);
        }

        public async Task<Course> AddCourse(Course newCourse)
        {
            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();
            return newCourse;
        }

        public async Task<bool> UpdateCourse(int courseId, Course updatedCourse)
        {
            var existingCourse = await _context.Courses.FindAsync(courseId);

            if (existingCourse == null)
                return false;

            existingCourse.CourseName = updatedCourse.CourseName;
            existingCourse.Description = updatedCourse.Description;
            existingCourse.Duration = updatedCourse.Duration;
            existingCourse.FeesAmount = updatedCourse.FeesAmount;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourse(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
