using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class Pharmacist
    {
        // Attributes
        public int PharmacistID { get; set; }
        public int UserID { get; set; }
        public string LicenseNumber { get; set; }

        // Default Constructor
        public Pharmacist()
        {
            PharmacistID = 0;
            UserID = 0;
            LicenseNumber = "";
        }

        // Parameterized Constructor (2 params)
        public Pharmacist(int userId, string licenseNumber)
        {
            UserID = userId;
            LicenseNumber = licenseNumber;
        }

        // Parameterized Constructor (full)
        public Pharmacist(int pharmacistId, int userId, string licenseNumber)
        {
            PharmacistID = pharmacistId;
            UserID = userId;
            LicenseNumber = licenseNumber;
        }

        // Copy Constructor
        public Pharmacist(Pharmacist other)
        {
            PharmacistID = other.PharmacistID;
            UserID = other.UserID;
            LicenseNumber = other.LicenseNumber;
        }

        // Behavior Methods
        public bool ValidateLicenseNumber()
        {
            return !string.IsNullOrWhiteSpace(LicenseNumber) && LicenseNumber.Length >= 5;
        }

        public void Display()
        {
            Console.WriteLine($"PharmacistID: {PharmacistID}, UserID: {UserID}, License: {LicenseNumber}");
        }
    }
}
