using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);

                if (student == null)
                {
                    return NotFound(new { message = "Student not found" });
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

         [HttpGet("UserId/{userId}")]
    public async Task<ActionResult<Student>> GetStudentByUserId(long userId)
    {
        try
        {
            var student = await _studentService.GetStudentByUserId(userId);

            if (student == null)
            {
                return NotFound(new { message = "Student not found" });
            }

            return Ok(student);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student newStudent)
        {
            try
            {
                var addedStudent = await _studentService.AddStudent(newStudent);
                return CreatedAtAction(nameof(GetStudentById), new { studentId = addedStudent.StudentId }, addedStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int studentId, [FromBody] Student updatedStudent)
        {
            try
            {
                var success = await _studentService.UpdateStudent(studentId, updatedStudent);

                if (success)
                    return Ok(new { message = "Student updated successfully" });
                else
                    return NotFound(new { message = "Student not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            try
            {
                var success = await _studentService.DeleteStudent(studentId);

                if (success)
                    return Ok(new { message = "Student deleted successfully" });
                else
                    return NotFound(new { message = "Student not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
