using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;

namespace TemperatureMonitoringController.Controllers
{
    [Route("api/air-pollution-monitoring")]
    [ApiController]
    public class AirPollutionMonitoringController : ControllerBase
    {
        private readonly MonitoringStationDbContext _dbContext;

        public AirPollutionMonitoringController(MonitoringStationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AirPollutionData>> GetRainfallData()
        {
            var airPollutionData = _dbContext.AirPollutionData.ToList();
            return Ok(airPollutionData);
        }

        [HttpPost]
        public IActionResult PostAirPollutionData([FromBody] AirPollutionData airPollutionData)
        {
            if (airPollutionData == null)
            {
                return BadRequest("Invalid air pollution data");
            }

            try
            {
                // Save air pollution data to the database
                _dbContext.AirPollutionData.Add(airPollutionData);
                _dbContext.SaveChanges();

                return Ok(airPollutionData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE endpoint to delete all air pollution data
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllAirPollutionData()
        {
            try
            {
                _dbContext.AirPollutionData.RemoveRange(_dbContext.AirPollutionData);
                _dbContext.SaveChanges();
                return Ok("All air pollution data deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("updates")]
        public ActionResult<IEnumerable<AirPollutionData>> GetLatestDataWithLimit(int limit)
        {
            try
            {
                // Query database, order by id descending and limit to 3 records
                var airPollutionData = _dbContext.AirPollutionData
                    .OrderByDescending(t => t.Id)
                    .Take(limit)
                    .ToList();

                return Ok(airPollutionData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
