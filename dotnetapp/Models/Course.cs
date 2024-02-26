using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public int FeesAmount { get; set; }

        

       [JsonIgnore]
        public ICollection<Student>? Students { get; set; }

        [JsonIgnore]
        public ICollection<Enquiry>? Enquiries { get; set; }

        [JsonIgnore]
        public ICollection<Admission>? Admissions { get; set; }

        [JsonIgnore]
        public ICollection<Payment>? Payments { get; set; }

    }
}
