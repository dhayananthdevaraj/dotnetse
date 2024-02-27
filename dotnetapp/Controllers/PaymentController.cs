using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
   
    [ApiController]
    // [Authorize(Roles = "Admin")] // Adjust authorization based on your requirements
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [Route("api/payments")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAllPayments()
        {
            try
            {
                var payments = await _paymentService.GetAllPayments();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [Route("api/payments/{id}")]
        [HttpGet]
        public async Task<ActionResult<Payment>> GetPaymentById(int id)
        {
            try
            {
                var payment = await _paymentService.GetPaymentById(id);

                if (payment == null)
                {
                    return NotFound(new { message = "Cannot find the payment" });
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

           [Route("api/payment/UserId/{userId}")]
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsByUserId(int userId)
            {
                try
                {
                    var payments = await _paymentService.GetPaymentsByUserId(userId);
                    return Ok(payments);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = ex.Message });
                }
            }

        [Route("api/student/make-payment")]
        [HttpPost]
        public async Task<ActionResult> AddPayment([FromBody] Payment newPayment)
        {
            try
            {
                var addedPayment = await _paymentService.AddPayment(newPayment);
                return CreatedAtAction(nameof(GetPaymentById), new { id = addedPayment.PaymentID }, addedPayment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
