using System.Collections.Generic;

namespace dotnetapp.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmailId { get; set; }

        public long UserId { get; set; }

        // Navigation properties
        public ICollection<Course> Courses { get; set; }
        public ICollection<Enquiry> Enquiries { get; set; }
        public ICollection<Admission> Admissions { get; set; }

        // Reference to User
        public User User { get; set; }
    }
}
