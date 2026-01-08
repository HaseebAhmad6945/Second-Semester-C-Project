using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class Bill
    {
        // Attributes
        public int BillID { get; set; }
        public int PatientID { get; set; }
        public int? AppointmentID { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount => TotalAmount - PaidAmount;
        public string PaymentStatus { get; set; }   // Pending, Partial, Paid
        public string PaymentMethod { get; set; }   // Cash, Card, Online
        public DateTime BillingDate { get; set; }

        // Default Constructor
        public Bill()
        {
            BillID = 0;
            PatientID = 0;
            AppointmentID = null;
            TotalAmount = 0;
            PaidAmount = 0;
            PaymentStatus = "Pending";
            PaymentMethod = "Cash";
            BillingDate = DateTime.Now;
        }

        // Parameterized Constructor (basic)
        public Bill(int patientId, decimal totalAmount)
        {
            PatientID = patientId;
            TotalAmount = totalAmount;
            PaidAmount = 0;
            PaymentStatus = "Pending";
            PaymentMethod = "Cash";
            BillingDate = DateTime.Now;
        }

        // Parameterized Constructor (full)
        public Bill(int billId, int patientId, int? appointmentId, decimal totalAmount,
                    decimal paidAmount, string paymentStatus, string paymentMethod, DateTime billingDate)
        {
            BillID = billId;
            PatientID = patientId;
            AppointmentID = appointmentId;
            TotalAmount = totalAmount;
            PaidAmount = paidAmount;
            PaymentStatus = paymentStatus;
            PaymentMethod = paymentMethod;
            BillingDate = billingDate;
        }

        // Copy Constructor
        public Bill(Bill other)
        {
            BillID = other.BillID;
            PatientID = other.PatientID;
            AppointmentID = other.AppointmentID;
            TotalAmount = other.TotalAmount;
            PaidAmount = other.PaidAmount;
            PaymentStatus = other.PaymentStatus;
            PaymentMethod = other.PaymentMethod;
            BillingDate = other.BillingDate;
        }

        // Behavior Methods
        public void MakePayment(decimal amount)
        {
            PaidAmount += amount;
            UpdatePaymentStatus();
        }

        public void UpdatePaymentStatus()
        {
            if (PaidAmount >= TotalAmount)
                PaymentStatus = "Paid";
            else if (PaidAmount > 0)
                PaymentStatus = "Partial";
            else
                PaymentStatus = "Pending";
        }

        public bool IsPaid()
        {
            return PaymentStatus.Equals("Paid", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsPending()
        {
            return PaymentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase);
        }

        public decimal CalculateBalance()
        {
            return TotalAmount - PaidAmount;
        }

        public void Display()
        {
            Console.WriteLine($"BillID: {BillID}, Patient: {PatientID}, Total: {TotalAmount:C}, Paid: {PaidAmount:C}, Balance: {BalanceAmount:C}, Status: {PaymentStatus}");
        }

        public string GetInfo()
        {
            return $"Bill #{BillID} - Total: {TotalAmount:C}, Balance: {BalanceAmount:C}, Status: {PaymentStatus}";
        }
    }
}
