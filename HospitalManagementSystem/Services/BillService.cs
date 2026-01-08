using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{

    public class BillService
    {
        private readonly BillRepo _billRepo;
        private readonly PatientRepo _patientRepo;
        private readonly AppointmentRepo _appointmentRepo;

        public BillService()
        {
            _billRepo = new BillRepo();
            _patientRepo = new PatientRepo();
            _appointmentRepo = new AppointmentRepo();
        }

        // Generate new bill
        public bool GenerateBill(int patientId, decimal totalAmount, int? appointmentId = null)
        {
            if (patientId <= 0 || totalAmount < 0)
                return false;
            // validate patient exists
            if (_patientRepo.GetById(patientId) == null)
                throw new InvalidOperationException("Patient not found (invalid PatientID).");

            // if appointment provided, validate it exists
            if (appointmentId.HasValue && _appointmentRepo.GetById(appointmentId.Value) == null)
                throw new InvalidOperationException("Appointment not found (invalid AppointmentID).");

            Bill bill = new Bill(patientId, totalAmount)
            {
                AppointmentID = appointmentId
            };

            return _billRepo.Add(bill);
        }

        // Add bill object
        public bool AddBill(Bill bill)
        {
            if (bill == null)
                return false;
            // validate patient exists
            if (_patientRepo.GetById(bill.PatientID) == null)
                throw new InvalidOperationException("Patient not found (invalid PatientID).");

            // if appointment provided, validate it exists
            if (bill.AppointmentID.HasValue && _appointmentRepo.GetById(bill.AppointmentID.Value) == null)
                throw new InvalidOperationException("Appointment not found (invalid AppointmentID).");

            return _billRepo.Add(bill);
        }

        // Get all bills
        public List<Bill> GetAllBills()
        {
            return _billRepo.GetAll();
        }

        // Get bill by ID
        public Bill GetBillById(int billId)
        {
            if (billId <= 0)
                return null;

            return _billRepo.GetById(billId);
        }

        // Get bills by patient ID
        public List<Bill> GetBillsByPatientId(int patientId)
        {
            if (patientId <= 0)
                return new List<Bill>();

            return _billRepo.GetByPatientId(patientId);
        }

        // Update bill
        public bool UpdateBill(Bill bill)
        {
            if (bill == null || bill.BillID <= 0)
                return false;

            // validate patient exists
            if (_patientRepo.GetById(bill.PatientID) == null)
                throw new InvalidOperationException("Patient not found (invalid PatientID).");

            // if appointment provided, validate it exists
            if (bill.AppointmentID.HasValue && _appointmentRepo.GetById(bill.AppointmentID.Value) == null)
                throw new InvalidOperationException("Appointment not found (invalid AppointmentID).");

            return _billRepo.Update(bill);
        }

        // Make payment
        public bool MakePayment(int billId, decimal amount, string paymentMethod)
        {
            if (billId <= 0 || amount <= 0)
                return false;

            Bill bill = GetBillById(billId);
            if (bill == null)
                return false;

            bill.MakePayment(amount);
            bill.PaymentMethod = paymentMethod;

            return _billRepo.Update(bill);
        }

        // Delete bill
        public bool DeleteBill(int billId)
        {
            if (billId <= 0)
                return false;

            return _billRepo.Delete(billId);
        }

        // Calculate total revenue (all paid bills)
        public decimal CalculateTotalRevenue()
        {
            var bills = GetAllBills();
            decimal total = 0;
            foreach (var bill in bills)
            {
                total += bill.PaidAmount;
            }
            return total;
        }

        // Get pending bills
        public List<Bill> GetPendingBills()
        {
            var allBills = GetAllBills();
            var pendingBills = new List<Bill>();

            foreach (var bill in allBills)
            {
                if (bill.IsPending() || bill.BalanceAmount > 0)
                {
                    pendingBills.Add(bill);
                }
            }

            return pendingBills;
        }

        // Display all bills
        public void DisplayAllBills()
        {
            var bills = GetAllBills();
            if (bills.Count == 0)
            {
                Console.WriteLine("No bills found.");
                return;
            }

            foreach (var bill in bills)
            {
                bill.Display();
            }
        }
    }
}
