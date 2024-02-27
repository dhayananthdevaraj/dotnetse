using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class EnquiryService
    {
        private readonly ApplicationDbContext _context;

        public EnquiryService(ApplicationDbContext context)
        {
            _context = context;
        }

     public async Task<IEnumerable<Enquiry>> GetAllEnquiries()
{
    return await _context.Enquiries
        .Include(e => e.Student)
        .Include(e => e.Course)
        .ToListAsync();
}

      public async Task<Enquiry> GetEnquiryById(int id)
{
    return await _context.Enquiries
        .Include(e => e.Student)
        .Include(e => e.Course)
        .FirstOrDefaultAsync(e => e.EnquiryID == id);
}

        public async Task<Enquiry> AddEnquiry(Enquiry newEnquiry)
        {
            _context.Enquiries.Add(newEnquiry);
            await _context.SaveChangesAsync();
            return newEnquiry;
        }

        public async Task<bool> UpdateEnquiry(int id, Enquiry updatedEnquiry)
        {
            var existingEnquiry = await _context.Enquiries.FindAsync(id);

            if (existingEnquiry == null)
                return false;

            existingEnquiry.Title = updatedEnquiry.Title;
            existingEnquiry.Description = updatedEnquiry.Description;
            existingEnquiry.EnquiryType = updatedEnquiry.EnquiryType;
            // Update other properties as needed

            await _context.SaveChangesAsync();
            return true;
        }

    //        public async Task<IEnumerable<Enquiry>> GetEnquiriesByStudentID(int studentId)
    // {
    //     return await _context.Enquiries
    //         .Include(e => e.Student)
    //         .Include(e => e.Course)
    //         .Where(e => e.StudentId == studentId)
    //         .ToListAsync();
    // }
    public async Task<IEnumerable<Enquiry>> GetEnquiriesByUserID(int userId)
    {
        return await _context.Enquiries
            .Include(e => e.Student)
            .Include(e => e.Course)
            .Where(e => e.Student.User.UserId == userId)
            .ToListAsync();
    }
        public async Task<bool> DeleteEnquiry(int id)
        {
            var enquiry = await _context.Enquiries.FindAsync(id);

            if (enquiry == null)
                return false;

            _context.Enquiries.Remove(enquiry);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
