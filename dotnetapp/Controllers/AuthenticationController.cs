using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
 
namespace dotnetapp.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ApplicationDbContext _context;
 
        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, ApplicationDbContext context)
        {
            _authService = authService;
            _logger = logger;
            _context = context;
        }
 
 
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Status = "Error", Message = "Invalid Payload" });
                var (status, message) = await _authService.Login(model);
                if (status == 0)
                    return BadRequest(new { Status = "Error", Message = message });
                return Ok(new { Status = "Success", token = message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
 
       [HttpPost]
[Route("register")]
public async Task<IActionResult> Register(RegistrationModel model)
{
    try
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Status = "Error", Message = "Invalid Payload" });

        Console.WriteLine("model", model);

        if (model.UserRole == "Admin" || model.UserRole == "OfficeStaff" || model.UserRole == "Student")
        {
            var (status, message) = await _authService.Registeration(model, model.UserRole);
            if (status == 0)
            {
                return BadRequest(new { Status = "Error", Message = message });
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                UserRole = model.UserRole,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (model.UserRole == "Student")
            {
                var student = new Student
                {
                    StudentName = model.Username,
                    StudentEmailId = model.Email,
                    UserId = user.UserId,
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            return Ok(new { Status = "Success", Message = message });
        }
        else
        {
            return BadRequest(new { Status = "Error", Message = "Invalid user role" });
        }

    }
    catch (Exception ex)
    {
        _logger.LogError(ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
}


       
        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _context.Users.ToList();
                return Ok(new { Users = users });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
 
