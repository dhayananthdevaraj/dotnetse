    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    namespace dotnetapp.Models
    {
        public class User
        {

        public string Email { get; set; }
        public long UserId { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string MobileNumber { get; set; }
        public string UserRole { get; set; }

        // Navigation property for Student
        public Student? Student { get; set; }

        // Navigation property for Courses
        public ICollection<Course>? Courses { get; set; }

        }
    }



