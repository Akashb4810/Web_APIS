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
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while inserting the DepartmentType", Error = ex.Message });
            }
        }

        [HttpGet("GetDepartmentType")]
        public async Task<IActionResult> GetDepartmentType(int? departmentTypeId)
        {
            try
            {
                var centers = await _testServices.GetDepartmentType(departmentTypeId);

                if (centers == null || !centers.Any())
                    return NotFound(new { StatusCode = 404, Message = "No DepartmentType found", Data = (object)null });

                return Ok(new { StatusCode = 200, Message = "DepartmentType retrieved successfully", Data = centers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An unexpected error occurred while retrieving DepartmentType", Error = ex.Message });
            }
        }

        [HttpGet("DeleteDepartmentType")]
        public async Task<IActionResult> DeleteDepartmentType(int departmentTypeId)
        {
            try
            {
                return await _testServices.DeleteDepartmentType(departmentTypeId)
                    ? Ok(new { StatusCode = 200, Message = "DepartmentType Deleted successfully" })
                    : BadRequest(new { StatusCode = 400, Message = "DepartmentType Deletion failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while Deleting the DepartmentType", Error = ex.Message });
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
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while inserting the SampleType", Error = ex.Message });
            }
        }

        [HttpGet("GetSampleType")]
        public async Task<IActionResult> GetSampleType(int? sampleTypeId)
        {
            try
            {
                var centers = await _testServices.GetSampleType(sampleTypeId);

                if (centers == null || !centers.Any())
                    return NotFound(new { StatusCode = 404, Message = "No SampleType found", Data = (object)null });

                return Ok(new { StatusCode = 200, Message = "SampleType retrieved successfully", Data = centers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An unexpected error occurred while retrieving SampleType", Error = ex.Message });
            }
        }

        [HttpGet("DeleteSampleType")]
        public async Task<IActionResult> DeleteSampleType(int sampleTypeId)
        {
            try
            {
                return await _testServices.DeleteSampleType(sampleTypeId)
                    ? Ok(new { StatusCode = 200, Message = "SampleTypeId Deleted successfully" })
                    : BadRequest(new { StatusCode = 400, Message = "SampleTypeId Deletion failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while Deleting the SampleType", Error = ex.Message });
            }
        }

        [HttpPost("InsertReportFormatTypeAsync")]
        public async Task<IActionResult> InsertReportFormatTypeAsync([FromBody] ReportFormatType reportFormatType)
        {
            try
            {
                if (reportFormatType == null)
                {
                    return BadRequest("Invalid data.");
                }
                var result = await _testServices.InsertReportFormatTypeAsync(reportFormatType);
                if (result)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while inserting the ReportFormatType", Error = ex.Message });
            }
        }
        [HttpGet("GetReportFormatType")]
        public async Task<IActionResult> GetReportFormatType(int? reportFormatTypeId)
        {
            try
            {
                var centers = await _testServices.GetReportFormatType(reportFormatTypeId);

                if (centers == null || !centers.Any())
                    return NotFound(new { StatusCode = 404, Message = "No ReportFormatType found", Data = (object)null });

                return Ok(new { StatusCode = 200, Message = "ReportFormatType retrieved successfully", Data = centers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An unexpected error occurred while retrieving ReportFormatType", Error = ex.Message });
            }
        }

        [HttpGet("DeleteReportFormatType")]
        public async Task<IActionResult> DeleteReportFormatType(int reportFormatTypeId)
        {
            try
            {
                return await _testServices.DeleteReportFormatType(reportFormatTypeId)
                    ? Ok(new { StatusCode = 200, Message = "reportFormatTypeId Deleted successfully" })
                    : BadRequest(new { StatusCode = 400, Message = "reportFormatTypeId Deletion failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while Deleting the reportFormatType", Error = ex.Message });
            }
        }

        [HttpPost("InsertReportTestAsync")]
        public async Task<IActionResult> InsertReportTestAsync([FromBody] Test test)
        {
            try
            {
                if (test == null)
                {
                    return BadRequest("Invalid data.");
                }
                var result = await _testServices.InsertUpdateTestAsync(test);
                if (result.ContainsKey(true) && !string.IsNullOrEmpty(result[true]))
                {
                    return Ok(new { Success = true, Message = result[true] });
                }

                if (result.ContainsKey(false) && !string.IsNullOrEmpty(result[false]))
                {
                    return BadRequest(new { Success = false, Message = result[false] });
                }
                return BadRequest("An unexpected error occurred.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while inserting the test", Error = ex.Message });
            }
        }
        [HttpGet("GetTest")]
        public async Task<IActionResult> GetTest(int? testId)
        {
            try
            {
                var centers = await _testServices.GetReportFormatType(testId);

                if (centers == null || !centers.Any())
                    return NotFound(new { StatusCode = 404, Message = "No ReportFormatType found", Data = (object)null });

                return Ok(new { StatusCode = 200, Message = "Test retrieved successfully", Data = centers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An unexpected error occurred while retrieving Test", Error = ex.Message });
            }
        }

        [HttpGet("DeleteTest")]
        public async Task<IActionResult> DeleteTest(int testId)
        {
            try
            {
                return await _testServices.DeleteReportFormatType(testId)
                    ? Ok(new { StatusCode = 200, Message = "testId Deleted successfully" })
                    : BadRequest(new { StatusCode = 400, Message = "testId Deletion failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while Deleting the test", Error = ex.Message });
            }
        }
    }
}
