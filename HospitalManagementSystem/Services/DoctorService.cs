using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{
    public class DoctorService
    {
        private readonly DoctorRepo _doctorRepo;

        public DoctorService()
        {
            _doctorRepo = new DoctorRepo();
        }

        // Add new doctor
        public bool AddDoctor(int userId, string specialization)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(specialization))
                return false;

            Doctor doctor = new Doctor(userId, specialization);

            // Validate
            if (!doctor.ValidateSpecialization())
                return false;

            return _doctorRepo.Add(doctor);
        }

        // Add doctor object
        public bool AddDoctor(Doctor doctor)
        {
            if (doctor == null)
                return false;

            if (!doctor.ValidateSpecialization())
                return false;

            return _doctorRepo.Add(doctor);
        }

        // Get all doctors
        public List<Doctor> GetAllDoctors()
        {
            return _doctorRepo.GetAll();
        }

        // Get doctor by ID
        public Doctor GetDoctorById(int doctorId)
        {
            if (doctorId <= 0)
                return null;

            return _doctorRepo.GetById(doctorId);
        }

        // Get doctor by UserID
        public Doctor GetDoctorByUserId(int userId)
        {
            if (userId <= 0)
                return null;

            return _doctorRepo.GetByUserId(userId);
        }

        // Search doctors by specialization
        public List<Doctor> SearchBySpecialization(string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
                return new List<Doctor>();

            return _doctorRepo.SearchBySpecialization(specialization);
        }

        // Update doctor
        public bool UpdateDoctor(Doctor doctor)
        {
            if (doctor == null || doctor.DoctorID <= 0)
                return false;

            if (!doctor.ValidateSpecialization())
                return false;

            return _doctorRepo.Update(doctor);
        }

        // Delete doctor
        public bool DeleteDoctor(int doctorId)
        {
            if (doctorId <= 0)
                return false;

            return _doctorRepo.Delete(doctorId);
        }

        // Display all doctors
        public void DisplayAllDoctors()
        {
            var doctors = GetAllDoctors();
            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctors found.");
                return;
            }

            foreach (var doctor in doctors)
            {
                doctor.Display();
            }
        }
    }
}

