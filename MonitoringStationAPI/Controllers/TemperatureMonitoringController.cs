using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;

namespace TemperatureMonitoringController.Controllers
{
    [Route("api/temperature-monitoring")]
    [ApiController]
    public class TemperatureMonitoringController : ControllerBase
    {
        private readonly MonitoringStationDbContext _dbContext;

        public TemperatureMonitoringController(MonitoringStationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TemperatureData>> GetTemperatureData()
        {
            var temperatureData = _dbContext.TemperatureData.ToList();
            return Ok(temperatureData);
        }

        [HttpPost]
        public IActionResult PostTemperatureData([FromBody] TemperatureData temperatureData)
        {
            if (temperatureData == null)
            {
                return BadRequest("Invalid temperature data");
            }

            // Save temperature data to the database
            _dbContext.TemperatureData.Add(temperatureData);
            _dbContext.SaveChanges();

            return Ok(temperatureData);
        }

        // DELETE endpoint to delete all temperature data
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllTemperatureData()
        {
            try
            {
                _dbContext.TemperatureData.RemoveRange(_dbContext.TemperatureData);
                _dbContext.SaveChanges();
                return Ok("All temperature data deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("updates")]
        public ActionResult<IEnumerable<TemperatureData>> GetTemperatureDataWithLimit(int limit)
        {
            try
            {
                // Query database, order by id descending and limit to 3 records
                var temperatureData = _dbContext.TemperatureData
                    .OrderByDescending(t => t.Id)
                    .Take(limit)
                    .ToList();

                return Ok(temperatureData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
