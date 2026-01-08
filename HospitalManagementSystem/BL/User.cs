using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class User
    {
        // Attributes
        public int UserID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }  // Admin, Doctor, Patient, Nurse, LabTechnician, Pharmacist
        public DateTime CreatedAt { get; set; }

        // Default Constructor
        public User()
        {
            UserID = 0;
            Email = "";
            PasswordHash = "";
            Role = "Patient";
            CreatedAt = DateTime.Now;
        }

        // Parameterized Constructor (3 params)
        public User(string email, string password, string role)
        {
            Email = email;
            PasswordHash = password;
            Role = role;
            CreatedAt = DateTime.Now;
        }

        // Parameterized Constructor (full - for reading from DB)
        public User(int userId, string email, string password, string role, DateTime createdAt)
        {
            UserID = userId;
            Email = email;
            PasswordHash = password;
            Role = role;
            CreatedAt = createdAt;
        }

        // Copy Constructor
        public User(User other)
        {
            UserID = other.UserID;
            Email = other.Email;
            PasswordHash = other.PasswordHash;
            Role = other.Role;
            CreatedAt = other.CreatedAt;
        }

        // Behavior Methods
        public bool IsAdmin()
        {
            return Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsDoctor()
        {
            return Role.Equals("Doctor", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsPatient()
        {
            return Role.Equals("Patient", StringComparison.OrdinalIgnoreCase);
        }

        public bool ValidateEmail()
        {
            return !string.IsNullOrWhiteSpace(Email) && Email.Contains("@");
        }

        public bool ValidatePassword()
        {
            return !string.IsNullOrWhiteSpace(PasswordHash) && PasswordHash.Length >= 6;
        }

        public bool ValidateRole()
        {
            string[] validRoles = { "Admin", "Doctor", "Patient", "Nurse", "LabTechnician", "Pharmacist" };
            foreach (string r in validRoles)
            {
                if (Role.Equals(r, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public void Display()
        {
            Console.WriteLine($"UserID: {UserID}, Email: {Email}, Role: {Role}, Created: {CreatedAt}");
        }
    }
}

