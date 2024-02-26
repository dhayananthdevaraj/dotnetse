using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetStudentByUserId(long userID)
        {
            return await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == userID);
        }


        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            return await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task<Student> AddStudent(Student newStudent)
        {
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            return newStudent;
        }

        public async Task<bool> UpdateStudent(int studentId, Student updatedStudent)
        {
            var existingStudent = await _context.Students.FindAsync(studentId);

            if (existingStudent == null)
                return false;

            existingStudent.StudentName = updatedStudent.StudentName;
            existingStudent.StudentEmailId = updatedStudent.StudentEmailId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);

            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
