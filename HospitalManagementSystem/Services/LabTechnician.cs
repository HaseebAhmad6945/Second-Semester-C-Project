using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{
    public class LabTechnicianService
    {
        private readonly LabTechnicianRepo _labTechRepo;

        public LabTechnicianService()
        {
            _labTechRepo = new LabTechnicianRepo();
        }

        // Add new lab technician
        public bool AddLabTechnician(int userId, string labType)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(labType))
                return false;

            LabTechnician labTech = new LabTechnician(userId, labType);

            if (!labTech.ValidateLabType())
                return false;

            return _labTechRepo.Add(labTech);
        }

        // Add lab technician object
        public bool AddLabTechnician(LabTechnician labTech)
        {
            if (labTech == null)
                return false;

            if (!labTech.ValidateLabType())
                return false;

            return _labTechRepo.Add(labTech);
        }

        // Get all lab technicians
        public List<LabTechnician> GetAllLabTechnicians()
        {
            return _labTechRepo.GetAll();
        }

        // Get lab technician by ID
        public LabTechnician GetLabTechnicianById(int id)
        {
            if (id <= 0)
                return null;

            return _labTechRepo.GetById(id);
        }

        // Update lab technician
        public bool UpdateLabTechnician(LabTechnician labTech)
        {
            if (labTech == null || labTech.LabTechnicianID <= 0)
                return false;

            if (!labTech.ValidateLabType())
                return false;

            return _labTechRepo.Update(labTech);
        }

        // Delete lab technician
        public bool DeleteLabTechnician(int id)
        {
            if (id <= 0)
                return false;

            return _labTechRepo.Delete(id);
        }

        // Display all lab technicians
        public void DisplayAllLabTechnicians()
        {
            var labTechs = GetAllLabTechnicians();
            if (labTechs.Count == 0)
            {
                Console.WriteLine("No lab technicians found.");
                return;
            }

            foreach (var labTech in labTechs)
            {
                labTech.Display();
            }
        }
    }
}

