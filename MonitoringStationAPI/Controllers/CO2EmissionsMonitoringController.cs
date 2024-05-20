// TemperatureController.cs
using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;

namespace TemperatureMonitoringController.Controllers
{
    [Route("api/co2-emissions-monitoring")]
    [ApiController]
    public class CO2EmissionsMonitoringController : ControllerBase
    {
        private readonly MonitoringStationDbContext _dbContext;

        public CO2EmissionsMonitoringController(MonitoringStationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AirPollutionData>> GetCO2EmissionsData()
        {
            var cO2EmissionsData = _dbContext.AirPollutionData.ToList();
            return Ok(cO2EmissionsData);
        }

        [HttpPost]
        public IActionResult PostCO2EmissionsData([FromBody] CO2EmissionsData cO2EmissionsData)
        {
            if (cO2EmissionsData == null)
            {
                return BadRequest("Invalid CO2 emissions data");
            }

            try
            {
                // Save co2 Emissions data to the database
                _dbContext.CO2EmissionsData.Add(cO2EmissionsData);
                _dbContext.SaveChanges();

                return Ok(cO2EmissionsData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE endpoint to delete all CO2 Emissions data
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllCO2EmissionsData()
        {
            try
            {
                _dbContext.CO2EmissionsData.RemoveRange(_dbContext.CO2EmissionsData);
                _dbContext.SaveChanges();
                return Ok("All CO2 emissions data deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("updates")]
        public ActionResult<IEnumerable<CO2EmissionsData>> GetLatestDataWithLimit(int limit)
        {
            try
            {
                // Query database, order by id descending and limit to 3 records
                var cO2Data = _dbContext.CO2EmissionsData
                    .OrderByDescending(t => t.Id)
                    .Take(limit)
                    .ToList();

                return Ok(cO2Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
