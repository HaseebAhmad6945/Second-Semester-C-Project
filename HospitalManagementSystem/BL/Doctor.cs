using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class Doctor
    {
        // Attributes
        public int DoctorID { get; set; }
        public int UserID { get; set; }
        public string Specialization { get; set; }

        // Default Constructor
        public Doctor()
        {
            DoctorID = 0;
            UserID = 0;
            Specialization = "General";
        }

        // Parameterized Constructor (2 params)
        public Doctor(int userId, string specialization)
        {
            UserID = userId;
            Specialization = specialization;
        }

        // Parameterized Constructor (full - for DB read)
        public Doctor(int doctorId, int userId, string specialization)
        {
            DoctorID = doctorId;
            UserID = userId;
            Specialization = specialization;
        }

        // Copy Constructor
        public Doctor(Doctor other)
        {
            DoctorID = other.DoctorID;
            UserID = other.UserID;
            Specialization = other.Specialization;
        }

        // Behavior Methods
        public bool ValidateSpecialization()
        {
            return !string.IsNullOrWhiteSpace(Specialization);
        }

        public void Display()
        {
            Console.WriteLine($"DoctorID: {DoctorID}, UserID: {UserID}, Specialization: {Specialization}");
        }

        public string GetInfo()
        {
            return $"Dr. (ID: {DoctorID}) - {Specialization}";
        }
    }
}

