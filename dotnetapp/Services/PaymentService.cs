using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _context.Payments
                .Include(p => p.Student)
                .Include(p => p.Admission)
                .ToListAsync();
        }

        public async Task<Payment> GetPaymentById(int paymentId)
        {
            return await _context.Payments
                .Include(p => p.Student)
                .Include(p => p.Admission)
                .FirstOrDefaultAsync(p => p.PaymentID == paymentId);
        }

    public async Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId)
    {
        return await _context.Payments
            .Include(p => p.Student)
            .Include(p => p.Admission)
            .Where(p => p.Student.User.UserId == userId)
            .ToListAsync();
    }

        

        public async Task<Payment> AddPayment(Payment newPayment)
        {
            _context.Payments.Add(newPayment);
            await _context.SaveChangesAsync();
            return newPayment;
        }
    }
}
