﻿namespace backend.Models
{
    public class Users
    {
        public Users()
        {
            IsAdmin = false; 
        }
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }
    }
}
