using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{
    public class NurseService
    {
        private readonly NurseRepo _nurseRepo;

        public NurseService()
        {
            _nurseRepo = new NurseRepo();
        }

        // Add new nurse
        public bool AddNurse(int userId, string department, string shift)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(department))
                return false;

            Nurse nurse = new Nurse(userId, department, shift);

            // Validate
            if (!nurse.ValidateDepartment() || !nurse.ValidateShift())
                return false;

            return _nurseRepo.Add(nurse);
        }

        // Add nurse object
        public bool AddNurse(Nurse nurse)
        {
            if (nurse == null)
                return false;

            if (!nurse.ValidateDepartment() || !nurse.ValidateShift())
                return false;

            return _nurseRepo.Add(nurse);
        }

        // Get all nurses
        public List<Nurse> GetAllNurses()
        {
            return _nurseRepo.GetAll();
        }

        // Get nurse by ID
        public Nurse GetNurseById(int nurseId)
        {
            if (nurseId <= 0)
                return null;

            return _nurseRepo.GetById(nurseId);
        }

        // Update nurse
        public bool UpdateNurse(Nurse nurse)
        {
            if (nurse == null || nurse.NurseID <= 0)
                return false;

            if (!nurse.ValidateDepartment() || !nurse.ValidateShift())
                return false;

            return _nurseRepo.Update(nurse);
        }

        // Delete nurse
        public bool DeleteNurse(int nurseId)
        {
            if (nurseId <= 0)
                return false;

            return _nurseRepo.Delete(nurseId);
        }

        // Display all nurses
        public void DisplayAllNurses()
        {
            var nurses = GetAllNurses();
            if (nurses.Count == 0)
            {
                Console.WriteLine("No nurses found.");
                return;
            }

            foreach (var nurse in nurses)
            {
                nurse.Display();
            }
        }
    }
}
