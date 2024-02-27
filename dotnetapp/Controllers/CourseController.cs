using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }
        
        [Route("course")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCourses();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
       
        [Route("student/course")]
        [HttpGet]
        public IActionResult Swagger()
        {
            return Ok();
        }


        [Route("student/courses")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCoursesForStudent()
        {
            try
            {
                var courses = await _courseService.GetAllCoursesForStudent();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [Route("course/{id}")]
        [HttpGet]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            try
            {
                var course = await _courseService.GetCourseById(id);

                if (course == null)
                {
                    return NotFound(new { message = "Cannot find the course" });
                }

                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // [Authorize(Roles = "Admin")]
        [Route("course")]
        [HttpPost]
        public async Task<ActionResult> AddCourse([FromBody] Course newCourse)
        {
            try
            {
                var addedCourse = await _courseService.AddCourse(newCourse);
                var fetchedCourse = await _courseService.GetCourseById(addedCourse.CourseID);

       
                return Ok(new { message = "Course added successfully", course = fetchedCourse });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // [Authorize(Roles = "Admin")]
         [Route("course/{id}")]
        [HttpPut]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] Course updatedCourse)
        {
            try
            {
                var success = await _courseService.UpdateCourse(id, updatedCourse);

                if (success)
                    return Ok(new { message = "Course updated successfully" });
                else
                    return NotFound(new { message = "Cannot find the course" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // [Authorize(Roles = "Admin")]
        [Route("course/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                var success = await _courseService.DeleteCourse(id);

                if (success)
                    return Ok(new { message = "Course deleted successfully" });
                else
                    return NotFound(new { message = "Cannot find the course" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
