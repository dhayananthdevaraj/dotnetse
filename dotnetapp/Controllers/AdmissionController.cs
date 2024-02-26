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
    // [Authorize(Roles = "Admin")]
    public class AdmissionController : ControllerBase
    {
        private readonly AdmissionService _admissionService;

        public AdmissionController(AdmissionService admissionService)
        {
            _admissionService = admissionService;
        }

        [Route("api/admissions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admission>>> GetAllAdmissions()
        {
            try
            {
                var admissions = await _admissionService.GetAllAdmissions();
                return Ok(admissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [Route("api/admission/{admissionId}")]
        [HttpGet]
        public async Task<ActionResult<Admission>> GetAdmissionById(int admissionId)
        {
            try
            {
                var admission = await _admissionService.GetAdmissionById(admissionId);

                if (admission == null)
                {
                    return NotFound(new { message = "Cannot find the admission" });
                }

                return Ok(admission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Route("api/admissions")]
        [HttpPost]
        public async Task<ActionResult> AddAdmission([FromBody] Admission newAdmission)
        {
            try
            {
                var addedAdmission = await _admissionService.AddAdmission(newAdmission);
                return CreatedAtAction(nameof(GetAdmissionById), new { id = addedAdmission.AdmissionID }, addedAdmission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Route("/api/updateadmission/{admissionId}")]
        [HttpPut]
        public async Task<ActionResult> UpdateAdmission(int admissionId, [FromBody] Admission updatedAdmission)
        {
            try
            {
                var success = await _admissionService.UpdateAdmission(admissionId, updatedAdmission);

                if (success)
                    return Ok(new { message = "Admission updated successfully" });
                else
                    return NotFound(new { message = "Cannot find the admission" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [Route("api/admission/{admissionId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAdmission(int admissionId)
        {
            try
            {
                var success = await _admissionService.DeleteAdmission(admissionId);

                if (success)
                    return Ok(new { message = "Admission deleted successfully" });
                else
                    return NotFound(new { message = "Cannot find the admission" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
