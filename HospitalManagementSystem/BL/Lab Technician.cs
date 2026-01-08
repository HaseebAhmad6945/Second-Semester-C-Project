using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class LabTechnician
    {
        // Attributes
        public int LabTechnicianID { get; set; }
        public int UserID { get; set; }
        public string LabType { get; set; }

        // Default Constructor
        public LabTechnician()
        {
            LabTechnicianID = 0;
            UserID = 0;
            LabType = "General";
        }

        // Parameterized Constructor (2 params)
        public LabTechnician(int userId, string labType)
        {
            UserID = userId;
            LabType = labType;
        }

        // Parameterized Constructor (full)
        public LabTechnician(int labTechId, int userId, string labType)
        {
            LabTechnicianID = labTechId;
            UserID = userId;
            LabType = labType;
        }

        // Copy Constructor
        public LabTechnician(LabTechnician other)
        {
            LabTechnicianID = other.LabTechnicianID;
            UserID = other.UserID;
            LabType = other.LabType;
        }

        // Behavior Methods
        public bool ValidateLabType()
        {
            return !string.IsNullOrWhiteSpace(LabType);
        }

        public void Display()
        {
            Console.WriteLine($"LabTechnicianID: {LabTechnicianID}, UserID: {UserID}, LabType: {LabType}");
        }
    }
}
