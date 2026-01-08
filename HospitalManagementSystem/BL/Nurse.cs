using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class Nurse
    {
        // Attributes
        public int NurseID { get; set; }
        public int UserID { get; set; }
        public string Department { get; set; }
        public string Shift { get; set; }

        // Default Constructor
        public Nurse()
        {
            NurseID = 0;
            UserID = 0;
            Department = "General";
            Shift = "Morning";
        }

        // Parameterized Constructor (3 params)
        public Nurse(int userId, string department, string shift)
        {
            UserID = userId;
            Department = department;
            Shift = shift;
        }

        // Parameterized Constructor (full)
        public Nurse(int nurseId, int userId, string department, string shift)
        {
            NurseID = nurseId;
            UserID = userId;
            Department = department;
            Shift = shift;
        }

        // Copy Constructor
        public Nurse(Nurse other)
        {
            NurseID = other.NurseID;
            UserID = other.UserID;
            Department = other.Department;
            Shift = other.Shift;
        }

        // Behavior Methods
        public bool ValidateDepartment()
        {
            return !string.IsNullOrWhiteSpace(Department);
        }

        public bool ValidateShift()
        {
            string[] validShifts = { "Morning", "Evening", "Night" };
            foreach (string s in validShifts)
            {
                if (Shift.Equals(s, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public void Display()
        {
            Console.WriteLine($"NurseID: {NurseID}, UserID: {UserID}, Dept: {Department}, Shift: {Shift}");
        }
    }
}

