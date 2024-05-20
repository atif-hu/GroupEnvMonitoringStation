using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;

namespace TemperatureMonitoringController.Controllers
{
    [Route("api/humidity-monitoring")]
    [ApiController]
    public class HumidityMonitoringController : ControllerBase
    {
        private readonly MonitoringStationDbContext _dbContext;

        public HumidityMonitoringController(MonitoringStationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HumidityData>> GetHumidityData()
        {
            var humidityData = _dbContext.HumidityData.ToList();
            return Ok(humidityData);
        }

        [HttpPost]
        public IActionResult PostHumidityData([FromBody] HumidityData humidityData)
        {
            if (humidityData == null)
            {
                return BadRequest("Invalid humidity data");
            }

            try
            {
                // Save humidity data to the database
                _dbContext.HumidityData.Add(humidityData);
                _dbContext.SaveChanges();

                return Ok(humidityData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE endpoint to delete all humidity data
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllHumidityData()
        {
            try
            {
                _dbContext.HumidityData.RemoveRange(_dbContext.HumidityData);
                _dbContext.SaveChanges();
                return Ok("All humidity data deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("updates")]
        public ActionResult<IEnumerable<HumidityData>> GetLatestDataWithLimit(int limit)
        {
            try
            {
                // Query database, order by id descending and limit to 3 records
                var humidityData = _dbContext.HumidityData
                    .OrderByDescending(t => t.Id)
                    .Take(limit)
                    .ToList();

                return Ok(humidityData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
