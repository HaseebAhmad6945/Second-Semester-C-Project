using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{
    public class PrescriptionService
    {
        private readonly PrescriptionRepo _prescriptionRepo;

        public PrescriptionService()
        {
            _prescriptionRepo = new PrescriptionRepo();
        }

        // Create new prescription
        public bool CreatePrescription(int appointmentId, int patientId, int doctorId,
                                      string medicineName, string dosage, string duration, string instructions)
        {
            if (appointmentId <= 0 || patientId <= 0 || doctorId <= 0)
                return false;

            Prescription prescription = new Prescription(appointmentId, patientId, doctorId,
                                                        medicineName, dosage, duration, instructions);

            if (!prescription.ValidateMedicine())
                return false;

            return _prescriptionRepo.Add(prescription);
        }

        // Add prescription object
        public bool AddPrescription(int appointmentId, Prescription prescription)
        {
            if (prescription == null)
                return false;

            if (!prescription.ValidateMedicine())
                return false;

            return _prescriptionRepo.Add(prescription);
        }

        // Get all prescriptions
        public List<Prescription> GetAllPrescriptions()
        {
            return _prescriptionRepo.GetAll();
        }

        // Get prescription by ID
        public Prescription GetPrescriptionById(int prescriptionId)
        {
            if (prescriptionId <= 0)
                return null;

            return _prescriptionRepo.GetById(prescriptionId);
        }

        // Get prescriptions by patient ID
        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            if (patientId <= 0)
                return new List<Prescription>();

            return _prescriptionRepo.GetByPatientId(patientId);
        }

        // Update prescription
        public bool UpdatePrescription(Prescription prescription)
        {
            if (prescription == null || prescription.PrescriptionID <= 0)
                return false;

            if (!prescription.ValidateMedicine())
                return false;

            return _prescriptionRepo.Update(prescription);
        }

        // Delete prescription
        public bool DeletePrescription(int prescriptionId)
        {
            if (prescriptionId <= 0)
                return false;

            return _prescriptionRepo.Delete(prescriptionId);
        }

        // Display all prescriptions
        public void DisplayAllPrescriptions()
        {
            var prescriptions = GetAllPrescriptions();
            if (prescriptions.Count == 0)
            {
                Console.WriteLine("No prescriptions found.");
                return;
            }

            foreach (var prescription in prescriptions)
            {
                prescription.Display();
            }
        }

        public bool AddPrescription(int appointmentId, int patientId, int doctorId, string text1, string text2, string text3, string text4)
        {
            throw new NotImplementedException();
        }
    }
}

