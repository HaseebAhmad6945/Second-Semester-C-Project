using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class Appointment
    {
        // Attributes
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }  // Pending, Completed, Cancelled

        // Default Constructor
        public Appointment()
        {
            AppointmentID = 0;
            PatientID = 0;
            DoctorID = 0;
            AppointmentDate = DateTime.Now;
            Status = "Pending";
        }

        // Parameterized Constructor (4 params)
        public Appointment(int patientId, int doctorId, DateTime date, string status = "Pending")
        {
            PatientID = patientId;
            DoctorID = doctorId;
            AppointmentDate = date;
            Status = status;
        }

        // Parameterized Constructor (full)
        public Appointment(int appointmentId, int patientId, int doctorId, DateTime date, string status)
        {
            AppointmentID = appointmentId;
            PatientID = patientId;
            DoctorID = doctorId;
            AppointmentDate = date;
            Status = status;
        }

        // Copy Constructor
        public Appointment(Appointment other)
        {
            AppointmentID = other.AppointmentID;
            PatientID = other.PatientID;
            DoctorID = other.DoctorID;
            AppointmentDate = other.AppointmentDate;
            Status = other.Status;
        }

        // Behavior Methods
        public bool IsPending()
        {
            return Status.Equals("Pending", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsCompleted()
        {
            return Status.Equals("Completed", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsCancelled()
        {
            return Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase);
        }

        public void MarkCompleted()
        {
            Status = "Completed";
        }

        public void MarkCancelled()
        {
            Status = "Cancelled";
        }

        public bool ValidateStatus()
        {
            string[] validStatuses = { "Pending", "Completed", "Cancelled" };
            foreach (string s in validStatuses)
            {
                if (Status.Equals(s, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public void Display()
        {
            Console.WriteLine($"AppointmentID: {AppointmentID}, PatientID: {PatientID}, DoctorID: {DoctorID}, Date: {AppointmentDate}, Status: {Status}");
        }

        public string GetInfo()
        {
            return $"Appointment #{AppointmentID} on {AppointmentDate:dd/MM/yyyy} - {Status}";
        }
    }
}

