using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;

namespace MonitoringStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringStationController : ControllerBase
    {
        private readonly EnvironmentDbContext _dbContext;

        public MonitoringStationController(EnvironmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        [HttpGet]
        public IActionResult GetAllEnvironmentSensors()
        {
            var environmentSensors = _dbContext.EnvironmentSensors.ToList();
            return Ok(environmentSensors);
        }

        [HttpGet("{id}")]
        public IActionResult GetEnvironmentSensor(int id)
        {
            var environmentSensor = _dbContext.EnvironmentSensors.FirstOrDefault(e => e.Id == id);
            if (environmentSensor == null)
            {
                return NotFound();
            }
            return Ok(environmentSensor);
        }

        
        [HttpPost("Add")]
        public IActionResult CreateEnvironmentSensor(EnvironmentSensor environmentSensor)
        {
            environmentSensor.CreatedAt = DateTime.Now;
            _dbContext.EnvironmentSensors.Add(environmentSensor);
            _dbContext.SaveChanges();

            // Generate Warnings for Sensors
            {
                if (environmentSensor.EnvironmentParameter == "Temperature" && environmentSensor.Value < -9 || environmentSensor.Value > 30)
                {
                    Console.WriteLine("Warning Temperature is over than Normal!");
                }
                else if (environmentSensor.EnvironmentParameter == "Rainfall" && environmentSensor.Value < 0 || environmentSensor.Value > 32)
                {
                    Console.WriteLine("Warning Rainfall is over than Normal!");
                }
                else if (environmentSensor.EnvironmentParameter == "Humidity" && environmentSensor.Value >0)
                {
                    Console.WriteLine(" Humidity values are also Generates!");
                }
                else if (environmentSensor.EnvironmentParameter == "Air Pollution" && environmentSensor.Value < 1 || environmentSensor.Value > 9)
                {
                    Console.WriteLine("Warning Air Pollution is over than Normal!");
                }
                else if (environmentSensor.EnvironmentParameter == "CO2 Emission" && environmentSensor.Value >0)
                {
                    Console.WriteLine("CO2 Emission is Normal!");
                }
            }

            return StatusCode(StatusCodes.Status201Created);

        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateEnvironmentSensor(int id, EnvironmentSensor environmentSensor)
        {
            var existingSensor = _dbContext.EnvironmentSensors.FirstOrDefault(e => e.Id == id);
            if (existingSensor == null)
            {
                return NotFound();
            }

            existingSensor.EnvironmentParameter = environmentSensor.EnvironmentParameter;
            existingSensor.Value = environmentSensor.Value;
            existingSensor.CreatedAt = environmentSensor.CreatedAt;

            _dbContext.SaveChanges();

            

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteEnvironmentSensor(int id)
        {
            var existingSensor = _dbContext.EnvironmentSensors.FirstOrDefault(e => e.Id == id);
            if (existingSensor == null)
            {
                return NotFound();
            }

            _dbContext.EnvironmentSensors.Remove(existingSensor);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
