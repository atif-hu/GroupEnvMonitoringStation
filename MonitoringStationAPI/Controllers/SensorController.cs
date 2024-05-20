using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;

namespace SensorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SensorController : ControllerBase
    {       
        private readonly MonitoringStationDbContext _context;
        public SensorController()
        {
            _context = DatabaseContextSingleton.Instance.GetDbContext();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Sensor>> GetSensorsData()
        {
            var sensorsData = _context.Sensor.ToList();
            return Ok(sensorsData);
        }

        [HttpPost]
        public IActionResult AddSensor([FromBody] Sensor sensor)
        {
            try
            {
                if (sensor == null)
                {
                    return BadRequest("Sensor data is null.");
                }

            
                // Add the new sensor entity to the DbContext
                _context.Sensor.Add(sensor);

                // Save changes to the database
                _context.SaveChanges();

                return Ok("Sensor added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSensor(int id, [FromBody] Sensor sensor)
        {
            if (sensor == null || sensor.SensorId != id)
            {
                return BadRequest("Invalid sensor data.");
            }

            try
            {
                _context.Entry(sensor).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok($"Sensor updated successfully. {sensor}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSensor(int id)
        {
            var sensor = _context.Sensor.Find(id);
            if (sensor == null)
            {
                return NotFound("Sensor not found.");
            }

            try
            {
                _context.Sensor.Remove(sensor);
                _context.SaveChanges();
                return Ok("Sensor deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

