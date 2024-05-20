using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;

namespace TemperatureMonitoringController.Controllers
{
    [Route("api/rainfall-monitoring")]
    [ApiController]
    public class RainfallMonitoringController : ControllerBase
    {
        private readonly MonitoringStationDbContext _dbContext;

        public RainfallMonitoringController(MonitoringStationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RainfallData>> GetRainfallData()
        {
            var rainfallData = _dbContext.RainfallData.ToList();
            return Ok(rainfallData);
        }

        [HttpPost]
        public IActionResult PostHumidityData([FromBody] RainfallData rainfallData)
        {
            if (rainfallData == null)
            {
                return BadRequest("Invalid Rainfall data");
            }

            try
            {
                // Save humidity data to the database
                _dbContext.RainfallData.Add(rainfallData);
                _dbContext.SaveChanges();

                return Ok(rainfallData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE endpoint to delete all temperature data
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllRainfallData()
        {
            try
            {
                _dbContext.RainfallData.RemoveRange(_dbContext.RainfallData);
                _dbContext.SaveChanges();
                return Ok("All rainfall data deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("updates")]
        public ActionResult<IEnumerable<RainfallData>> GetLatestDataWithLimit(int limit)
        {
            try
            {
                // Query database, order by id descending and limit to 3 records
                var rainfallData = _dbContext.RainfallData
                    .OrderByDescending(t => t.Id)
                    .Take(limit)
                    .ToList();

                return Ok(rainfallData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
