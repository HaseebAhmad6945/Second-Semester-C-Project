using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class Patient
    {
        // Attributes
        public int PatientID { get; set; }
        public int? UserID { get; set; }
        public string BloodGroup { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Default Constructor
        public Patient()
        {
            PatientID = 0;
            UserID = null;
            BloodGroup = "Unknown";
            DateOfBirth = null;
        }

        // Parameterized Constructor (3 params)
        public Patient(int? userId, string bloodGroup, DateTime? dob)
        {
            UserID = userId;
            BloodGroup = bloodGroup;
            DateOfBirth = dob;
        }

        // Parameterized Constructor (full - for DB read)
        public Patient(int patientId, int? userId, string bloodGroup, DateTime? dob)
        {
            PatientID = patientId;
            UserID = userId;
            BloodGroup = bloodGroup;
            DateOfBirth = dob;
        }

        // Copy Constructor
        public Patient(Patient other)
        {
            PatientID = other.PatientID;
            UserID = other.UserID;
            BloodGroup = other.BloodGroup;
            DateOfBirth = other.DateOfBirth;
        }

        // Behavior Methods
        public int CalculateAge()
        {
            if (!DateOfBirth.HasValue)
                return 0;

            int age = DateTime.Now.Year - DateOfBirth.Value.Year;
            if (DateTime.Now.DayOfYear < DateOfBirth.Value.DayOfYear)
                age--;
            return age;
        }

        public bool ValidateBloodGroup()
        {
            string[] validGroups = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            foreach (string group in validGroups)
            {
                if (BloodGroup.Equals(group, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public void Display()
        {
            Console.WriteLine($"PatientID: {PatientID}, UserID: {UserID}, Blood: {BloodGroup}, Age: {CalculateAge()}");
        }

        public string GetInfo()
        {
            return $"Patient (ID: {PatientID}) - {BloodGroup}, Age: {CalculateAge()}";
        }
    }
}
