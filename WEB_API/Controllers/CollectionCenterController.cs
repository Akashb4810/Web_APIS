using DLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Web_APIS.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionCenterController : ControllerBase
    {
        private readonly ICollectionCenterService _collectionCenterService;
        private readonly ILogExceptionService _logExceptionService;
        public CollectionCenterController(ICollectionCenterService collectionCenterService, ILogExceptionService logExceptionService)
        {
            _collectionCenterService = collectionCenterService;
            _logExceptionService = logExceptionService;
        }

        [HttpGet("GetCenterDetails")]
        public async Task<IActionResult> GetCenterDetails(int? centerID)
        {
            try
            {
                var centers = await _collectionCenterService.GetCenterDetails(centerID);

                if (centers == null || !centers.Any())
                    return NotFound(new { StatusCode = 404, Message = "No centers found", Data = (object)null });

                return Ok(new { StatusCode = 200, Message = "centers retrieved successfully", Data = centers });
            }
            catch (Exception ex)
            {
                await _logExceptionService.InsertLog(ex.Message, DateTime.Now, "0");
                return StatusCode(500, new { StatusCode = 500, Message = "An unexpected error occurred while retrieving centers", Error = ex.Message });
            }
        }


        [HttpPost("InsertUpdateCenter")]
        public async Task<IActionResult> InsertUpdateCenter([FromBody] CollectionCenter collectionCenter)
        {
            try
            {
                return await _collectionCenterService.InsertUpdateCenterAsync(collectionCenter)
                    ? Ok(new { StatusCode = 200, Message = "User Inserted or Updated successfully" })
                    : BadRequest(new { StatusCode = 400, Message = "User Insert or Update failed" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while inserting the user", Error = ex.Message });
            }
        }

        [HttpPost("DeleteCenter")]
        public async Task<IActionResult> DeleteCenter(int centerID)
        {
            try
            {
                return await _collectionCenterService.DeleteCenterAsync(centerID)
                    ? Ok(new { StatusCode = 200, Message = "User Deleted successfully" })
                    : BadRequest(new { StatusCode = 400, Message = "User Deletion failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred while Deleting the user", Error = ex.Message });
            }
        }

    }
}
