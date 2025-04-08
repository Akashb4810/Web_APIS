using DLL.Services.Implementation;
using DLL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_APIS.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testServices;

        public TestController(ITestService testServices)
        {
            _testServices = testServices;
        }

        [HttpPost("InsertUpdateDepartmentType")]
        public async Task<IActionResult> InsertUpdateDepartmentType([FromBody] DepartmentType departmentType)
        {
            try
            {
                if (departmentType == null)
                {
                    return BadRequest("Invalid data.");
                }
                var result = await _testServices.InsertDepartmentTypeAsync(departmentType);
                if (result)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while inserting the user", Error = ex.Message });
            }
        }

        [HttpGet("GetDepartmentType")]
        public async Task<IActionResult> GetDepartmentType(int? departmentTypeId)
        {
            try
            {
                var centers = await _testServices.GetDepartmentType(departmentTypeId);

                if (centers == null || !centers.Any())
                    return NotFound(new { StatusCode = 404, Message = "No centers found", Data = (object)null });

                return Ok(new { StatusCode = 200, Message = "centers retrieved successfully", Data = centers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An unexpected error occurred while retrieving centers", Error = ex.Message });
            }
        }

        [HttpPost("InsertUpdateSampleType")]
        public async Task<IActionResult> InsertUpdateSampleType([FromBody] SampleType sampleType)
        {
            try
            {
                if (sampleType == null)
                {
                    return BadRequest("Invalid data.");
                }
                var result = await _testServices.InsertUpdateSampleType(sampleType);
                if (result)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while inserting the user", Error = ex.Message });
            }
        }

        [HttpGet("GetSampleType")]
        public async Task<IActionResult> GetSampleType(int? sampleTypeId)
        {
            try
            {
                var centers = await _testServices.GetSampleType(sampleTypeId);

                if (centers == null || !centers.Any())
                    return NotFound(new { StatusCode = 404, Message = "No centers found", Data = (object)null });

                return Ok(new { StatusCode = 200, Message = "centers retrieved successfully", Data = centers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An unexpected error occurred while retrieving centers", Error = ex.Message });
            }
        }

    }
}
