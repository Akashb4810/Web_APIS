using DLL.Services.Implementation;
using DLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_APIS.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _doctorservices;

        public DoctorController(IDoctorServices doctorServices)
        {
            _doctorservices= doctorServices;
        }

        [HttpGet("GetDoctors")]
        public async Task<IActionResult> GetDoctorsAsync([FromQuery] int? CenterId, [FromQuery] string? doctername)
        {
            try
            {
                var lst = await _doctorservices.GetDoctorsAsync(CenterId, doctername);

                if (lst == null || lst.Count == 0)
                    return NotFound("No doctors found.");

                return Ok(lst);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Something went wrong while fetching doctors.");
            }
        }
        [HttpPost("InsertOrUpdateDoctor")]
        public async Task<IActionResult> InsertOrUpdateDoctor([FromBody] DoctorClass doctor)
        {
            if (doctor == null)
                return BadRequest("Doctor data is required.");

            try
            {
                var doctorId = await _doctorservices.InsertOrUpdateDoctorAsync(doctor);

                if (doctorId <= 0)
                    return StatusCode(500, "Insert/Update operation failed.");

                return Ok(new
                {
                    DoctorID = doctorId,
                    Message = doctor.DoctorID == 0 ? "Doctor inserted successfully." : "Doctor updated successfully."
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("DeleteDoctor/{doctorId}")]
        public async Task<IActionResult>    DeleteDoctor(int doctorId)
        {
            var result = await _doctorservices.DeleteDoctorAsync(doctorId);

            if (result)
                return Ok(new { message = "Doctor deleted (soft) successfully." });
            else
                return BadRequest(new { message = "Failed to delete doctor." });
        }



    }
}
