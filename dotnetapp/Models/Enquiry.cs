using System;
using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Enquiry
    {
        public int EnquiryID { get; set; }
        public DateTime EnquiryDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EnquiryType { get; set; }

        // Foreign key
        public int StudentId { get; set; }

         public int CourseID { get; set; }

        // Navigation property

      [JsonIgnore]
        public Student? Student { get; set; }
         public Course? Course { get; set; }

    }
}
