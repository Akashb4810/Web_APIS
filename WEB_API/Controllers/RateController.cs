using DLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Web_APIS.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateServices _PatientServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RateController(IRateServices RateServices, IHttpContextAccessor httpContextAccessor)
        {
            _PatientServices = RateServices;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("GetRateList")]
        public async Task<IActionResult> GetRateList([FromQuery] int? id)
        {
            var data = await _PatientServices.GetRateList(id);

            if (data == null || !data.Any())
            {
                return NotFound(new
                {
                    StatusCode = 404,Message = "No data found.",Data = (object?)null
                });
            }

            return Ok(new
            {
                StatusCode = 200,Message = "Rate list fetched successfully.",Data = data
            });
        }
        [HttpPost("InsertUpdateRateList")]
        public async Task<ActionResult> InsertUpdateRateList([FromBody] RateListRequestDto requestDto)
        {
            if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.RateTypeName))
            {
                return BadRequest(new
                {
                    StatusCode = 400,Message = "Rate Type is required", Data = (object?)null
                });
            }
            var mstRateList = new MstRateList
            {
                RateListId = requestDto.RateListId,
                RateTypeName = requestDto.RateTypeName,
                IsDeleted = false 
            };

            var rowsAffected = await _PatientServices.InsertUpdateRateList(mstRateList);

            if (rowsAffected > 0)
            {
                return Ok(new
                {
                    StatusCode = 200,Message = mstRateList.RateListId == 0 ? "Rate Type inserted successfully" : "Rate Type updated successfully"
                });
            }
            else
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,Message = "Failed to insert/update Rate Type",Data = (object?)null
                });
            }
        }
        [HttpDelete("DeleteRateList/{id}")]
        public async Task<IActionResult> DeleteRateList(int id)
        {
            var success = await _PatientServices.SoftDeleteRateList(id);

            if (success)
            {
                return Ok(new
                {
                    StatusCode = 200,Message = "Rate type deleted successfully."
                });
            }
            return NotFound(new
            {
                StatusCode = 404,Message = "Rate type not found or already deleted.",Data = (object?)null
            });
        }
    }
}

