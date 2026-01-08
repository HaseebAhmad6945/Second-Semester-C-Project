using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{
    public class PatientService
    {
        private readonly PatientRepo _patientRepo;

        public PatientService()
        {
            _patientRepo = new PatientRepo();
        }

        // Add patient and return the created PatientID, or null on failure
        public int? AddPatientAndReturnId(int? userId, string bloodGroup, DateTime? dob)
        {
            Patient patient = new Patient(userId, bloodGroup, dob);

            if (!string.IsNullOrWhiteSpace(bloodGroup) && !patient.ValidateBloodGroup())
                return null;

            bool success = _patientRepo.Add(patient);
            if (!success) return null;
            return patient.PatientID;
        }

        // Add new patient
        public bool AddPatient(int? userId, string bloodGroup, DateTime? dob)
        {
            Patient patient = new Patient(userId, bloodGroup, dob);

            // Validate blood group if provided
            if (!string.IsNullOrWhiteSpace(bloodGroup) && !patient.ValidateBloodGroup())
                return false;

            return _patientRepo.Add(patient);
        }

        // Add patient object
        public bool AddPatient(Patient patient)
        {
            if (patient == null)
                return false;

            // Validate blood group if provided
            if (!string.IsNullOrWhiteSpace(patient.BloodGroup) && !patient.ValidateBloodGroup())
                return false;

            return _patientRepo.Add(patient);
        }

        // Get all patients
        public List<Patient> GetAllPatients()
        {
            return _patientRepo.GetAll();
        }

        // Get patient by ID
        public Patient GetPatientById(int patientId)
        {
            if (patientId <= 0)
                return null;

            return _patientRepo.GetById(patientId);
        }

        // Get patient by UserID
        public Patient GetPatientByUserId(int userId)
        {
            if (userId <= 0)
                return null;

            return _patientRepo.GetByUserId(userId);
        }

        // Search patients by blood group
        public List<Patient> SearchByBloodGroup(string bloodGroup)
        {
            if (string.IsNullOrWhiteSpace(bloodGroup))
                return new List<Patient>();

            return _patientRepo.SearchByBloodGroup(bloodGroup);
        }

        // Update patient
        public bool UpdatePatient(Patient patient)
        {
            if (patient == null || patient.PatientID <= 0)
                return false;

            // Validate blood group if provided
            if (!string.IsNullOrWhiteSpace(patient.BloodGroup) && !patient.ValidateBloodGroup())
                return false;

            return _patientRepo.Update(patient);
        }

        // Delete patient
        public bool DeletePatient(int patientId)
        {
            if (patientId <= 0)
                return false;

            return _patientRepo.Delete(patientId);
        }

        // Calculate patient age
        public int GetPatientAge(int patientId)
        {
            Patient patient = GetPatientById(patientId);
            if (patient == null)
                return 0;

            return patient.CalculateAge();
        }

        // Display all patients
        public void DisplayAllPatients()
        {
            var patients = GetAllPatients();
            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found.");
                return;
            }

            foreach (var patient in patients)
            {
                patient.Display();
            }
        }
    }
}

