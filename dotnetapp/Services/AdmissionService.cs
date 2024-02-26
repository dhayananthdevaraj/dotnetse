using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class AdmissionService
    {
        private readonly ApplicationDbContext _context;

        public AdmissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admission>> GetAllAdmissions()
        {
            return await _context.Admissions
                .Include(a => a.Student)
                .Include(a => a.Course)
                .ToListAsync();
        }

        public async Task<Admission> GetAdmissionById(int id)
        {
            return await _context.Admissions
                .Include(a => a.Student)
                .Include(a => a.Course)
                .FirstOrDefaultAsync(a => a.AdmissionID == id);
        }

      public async Task<Admission> AddAdmission(Admission newAdmission)
{
    // Check if admission already exists
    if (await _context.Admissions.AnyAsync(a => a.StudentId == newAdmission.StudentId && a.CourseID == newAdmission.CourseID))
    {
        // Admission already exists, handle accordingly
        throw new InvalidOperationException("Admission with the same StudentId and CourseID already exists.");
    }

    // Add the new admission
    _context.Admissions.Add(newAdmission);
    await _context.SaveChangesAsync();

    return newAdmission;
}

        public async Task<bool> UpdateAdmission(int id, Admission updatedAdmission)
        {
            var existingAdmission = await _context.Admissions.FindAsync(id);

            if (existingAdmission == null)
                return false;

            existingAdmission.Status = updatedAdmission.Status;
            // Update other properties as needed

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAdmission(int id)
        {
            var admission = await _context.Admissions.FindAsync(id);

            if (admission == null)
                return false;

            _context.Admissions.Remove(admission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
