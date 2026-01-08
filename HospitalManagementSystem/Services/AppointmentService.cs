using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{
    public class AppointmentService
    {
        private readonly AppointmentRepo _appointmentRepo;
        private readonly PatientRepo _patientRepo;
        private readonly DoctorRepo _doctorRepo;

        public AppointmentService()
        {
            _appointmentRepo = new AppointmentRepo();
            _patientRepo = new PatientRepo();
            _doctorRepo = new DoctorRepo();
        }

        // Book new appointment
        public bool BookAppointment(int patientId, int doctorId, DateTime appointmentDate)
        {
            if (patientId <= 0 || doctorId <= 0)
                return false;
            // ensure referenced records exist to avoid FK constraint errors
            if (_patientRepo.GetById(patientId) == null)
                throw new InvalidOperationException("Patient not found (invalid PatientID).");
            if (_doctorRepo.GetById(doctorId) == null)
                throw new InvalidOperationException("Doctor not found (invalid DoctorID).");
            Appointment appointment = new Appointment(patientId, doctorId, appointmentDate, "Pending");

            return _appointmentRepo.Add(appointment);
        }

        // Add appointment object
        public bool AddAppointment(Appointment appointment)
        {
            if (appointment == null)
                return false;

            if (!appointment.ValidateStatus())
                return false;

            // ensure referenced patient & doctor exist before inserting
            if (_patientRepo.GetById(appointment.PatientID) == null)
                throw new InvalidOperationException("Patient not found (invalid PatientID).");
            if (_doctorRepo.GetById(appointment.DoctorID) == null)
                throw new InvalidOperationException("Doctor not found (invalid DoctorID).");

            return _appointmentRepo.Add(appointment);
        }

        // Get all appointments
        public List<Appointment> GetAllAppointments()
        {
            return _appointmentRepo.GetAll();
        }

        // Get appointment by ID
        public Appointment GetAppointmentById(int appointmentId)
        {
            if (appointmentId <= 0)
                return null;

            return _appointmentRepo.GetById(appointmentId);
        }

        // Get appointments by patient ID
        public List<Appointment> GetAppointmentsByPatientId(int patientId)
        {
            if (patientId <= 0)
                return new List<Appointment>();

            return _appointmentRepo.GetByPatientId(patientId);
        }

        // Get appointments by doctor ID
        public List<Appointment> GetAppointmentsByDoctorId(int doctorId)
        {
            if (doctorId <= 0)
                return new List<Appointment>();

            return _appointmentRepo.GetByDoctorId(doctorId);
        }

        // Update appointment
        public bool UpdateAppointment(Appointment appointment)
        {
            if (appointment == null || appointment.AppointmentID <= 0)
                return false;

            if (!appointment.ValidateStatus())
                return false;

            // validate referenced patient & doctor exist
            if (_patientRepo.GetById(appointment.PatientID) == null)
                throw new InvalidOperationException("Patient not found (invalid PatientID).");
            if (_doctorRepo.GetById(appointment.DoctorID) == null)
                throw new InvalidOperationException("Doctor not found (invalid DoctorID).");

            return _appointmentRepo.Update(appointment);
        }

        // Complete appointment
        public bool CompleteAppointment(int appointmentId)
        {
            Appointment appointment = GetAppointmentById(appointmentId);
            if (appointment == null)
                return false;

            appointment.MarkCompleted();
            return _appointmentRepo.Update(appointment);
        }

        // Cancel appointment
        public bool CancelAppointment(int appointmentId)
        {
            Appointment appointment = GetAppointmentById(appointmentId);
            if (appointment == null)
                return false;

            appointment.MarkCancelled();
            return _appointmentRepo.Update(appointment);
        }

        // Delete appointment
        public bool DeleteAppointment(int appointmentId)
        {
            if (appointmentId <= 0)
                return false;

            return _appointmentRepo.Delete(appointmentId);
        }

        // Display all appointments
        public void DisplayAllAppointments()
        {
            var appointments = GetAllAppointments();
            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            foreach (var appointment in appointments)
            {
                appointment.Display();
            }
        }
    }
}

