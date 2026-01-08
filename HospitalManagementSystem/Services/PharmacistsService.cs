using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{
    public class PharmacistService
    {
        private readonly PharmacistRepo _pharmacistRepo;

        public PharmacistService()
        {
            _pharmacistRepo = new PharmacistRepo();
        }

        // Add new pharmacist
        public bool AddPharmacist(int userId, string licenseNumber)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(licenseNumber))
                return false;

            Pharmacist pharmacist = new Pharmacist(userId, licenseNumber);

            if (!pharmacist.ValidateLicenseNumber())
                return false;

            return _pharmacistRepo.Add(pharmacist);
        }

        // Add pharmacist object
        public bool AddPharmacist(Pharmacist pharmacist)
        {
            if (pharmacist == null)
                return false;

            if (!pharmacist.ValidateLicenseNumber())
                return false;

            return _pharmacistRepo.Add(pharmacist);
        }

        // Get all pharmacists
        public List<Pharmacist> GetAllPharmacists()
        {
            return _pharmacistRepo.GetAll();
        }

        // Get pharmacist by ID
        public Pharmacist GetPharmacistById(int id)
        {
            if (id <= 0)
                return null;

            return _pharmacistRepo.GetById(id);
        }

        // Update pharmacist
        public bool UpdatePharmacist(Pharmacist pharmacist)
        {
            if (pharmacist == null || pharmacist.PharmacistID <= 0)
                return false;

            if (!pharmacist.ValidateLicenseNumber())
                return false;

            return _pharmacistRepo.Update(pharmacist);
        }

        // Delete pharmacist
        public bool DeletePharmacist(int id)
        {
            if (id <= 0)
                return false;

            return _pharmacistRepo.Delete(id);
        }

        // Display all pharmacists
        public void DisplayAllPharmacists()
        {
            var pharmacists = GetAllPharmacists();
            if (pharmacists.Count == 0)
            {
                Console.WriteLine("No pharmacists found.");
                return;
            }

            foreach (var pharmacist in pharmacists)
            {
                pharmacist.Display();
            }
        }
    }
}
