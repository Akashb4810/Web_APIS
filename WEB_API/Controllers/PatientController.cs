using DLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_APIS.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientServices _PatientServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PatientController(IPatientServices patientServices, IHttpContextAccessor httpContextAccessor)
        {
            _PatientServices = patientServices;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("GetAllPatientTypes")]
        public async Task<IActionResult> GetAllPatientTypes()
        {
            var patientTypes = await _PatientServices.GetAllPatientTypes();

            if (patientTypes == null || !patientTypes.Any())
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "No patient types found",
                    Data = (object?)null
                });
            }

            return Ok(new
            {
                StatusCode = 200,
                Message = "Patient types retrieved successfully",
                Data = patientTypes
            });
        }

        [HttpPost("InsertPatientType")]
        public async Task<IActionResult> InsertPatientType([FromBody] PatientType patientType)
        {
            if (patientType == null || string.IsNullOrWhiteSpace(patientType.PatientTypeName))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "PatientTypeName is required",
                    Data = (object?)null
                });
            }
            var result = await _PatientServices.InsertPatientType(patientType);

            if (result > 0)
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Patient type inserted successfully",
                });
            }
            else
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Failed to insert patient type",
                    Data = (object?)null
                });
            }
        }

        [HttpDelete("DeletePatientType/{id}")]
        public async Task<IActionResult> DeletePatientType(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid PatientTypeID",
                    Data = (object?)null
                });
            }

            var result = await _PatientServices.DeletePatientType(id);

            if (result == 1)
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Patient type deleted successfully"
                });
            }
            else
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Patient type not found",
                    Data = (object?)null
                });
            }
        }
    }
}
