using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class Prescription
    {
        // Attributes
        public int PrescriptionID { get; set; }
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Instructions { get; set; }
        public DateTime PrescribedDate { get; set; }

        // Default Constructor
        public Prescription()
        {
            PrescriptionID = 0;
            AppointmentID = 0;
            PatientID = 0;
            DoctorID = 0;
            MedicineName = "";
            Dosage = "";
            Duration = "";
            Instructions = "";
            PrescribedDate = DateTime.Now;
        }

        // Parameterized Constructor
        public Prescription(int appointmentId, int patientId, int doctorId,
                           string medicineName, string dosage, string duration, string instructions)
        {
            AppointmentID = appointmentId;
            PatientID = patientId;
            DoctorID = doctorId;
            MedicineName = medicineName;
            Dosage = dosage;
            Duration = duration;
            Instructions = instructions;
            PrescribedDate = DateTime.Now;
        }

        // Copy Constructor
        public Prescription(Prescription other)
        {
            PrescriptionID = other.PrescriptionID;
            AppointmentID = other.AppointmentID;
            PatientID = other.PatientID;
            DoctorID = other.DoctorID;
            MedicineName = other.MedicineName;
            Dosage = other.Dosage;
            Duration = other.Duration;
            Instructions = other.Instructions;
            PrescribedDate = other.PrescribedDate;
        }

        // Behavior Methods
        public bool ValidateMedicine()
        {
            return !string.IsNullOrWhiteSpace(MedicineName);
        }

        public void Display()
        {
            Console.WriteLine($"Prescription #{PrescriptionID}: {MedicineName} - {Dosage} for {Duration}");
            Console.WriteLine($"Instructions: {Instructions}");
        }
    }
}

