namespace GroupEnvironmentMonitoringStation.Models
{
    public class Users
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
    }
}
