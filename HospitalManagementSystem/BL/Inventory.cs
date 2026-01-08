using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalManagementSystem.BL
{
    public class Inventory
    {
        // Attributes
        public int InventoryID { get; set; }
        public string MedicineName { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime LastRestocked { get; set; }
        public DateTime CreatedAt { get; set; }

        // Default Constructor
        public Inventory()
        {
            InventoryID = 0;
            MedicineName = "";
            Category = "";
            Manufacturer = "";
            UnitPrice = 0;
            Quantity = 0;
            ExpiryDate = null;
            ReorderLevel = 10;
            LastRestocked = DateTime.Now;
            CreatedAt = DateTime.Now;
        }

        // Parameterized Constructor
        public Inventory(string medicineName, string category, string manufacturer,
                        decimal unitPrice, int quantity, DateTime? expiryDate, int reorderLevel)
        {
            MedicineName = medicineName;
            Category = category;
            Manufacturer = manufacturer;
            UnitPrice = unitPrice;
            Quantity = quantity;
            ExpiryDate = expiryDate;
            ReorderLevel = reorderLevel;
            LastRestocked = DateTime.Now;
            CreatedAt = DateTime.Now;
        }

        // Copy Constructor
        public Inventory(Inventory other)
        {
            InventoryID = other.InventoryID;
            MedicineName = other.MedicineName;
            Category = other.Category;
            Manufacturer = other.Manufacturer;
            UnitPrice = other.UnitPrice;
            Quantity = other.Quantity;
            ExpiryDate = other.ExpiryDate;
            ReorderLevel = other.ReorderLevel;
            LastRestocked = other.LastRestocked;
            CreatedAt = other.CreatedAt;
        }

        // Behavior Methods
        public bool NeedsReorder()
        {
            return Quantity <= ReorderLevel;
        }

        public bool IsExpired()
        {
            return ExpiryDate.HasValue && ExpiryDate.Value < DateTime.Now;
        }

        public void AddStock(int amount)
        {
            Quantity += amount;
            LastRestocked = DateTime.Now;
        }

        public bool RemoveStock(int amount)
        {
            if (Quantity >= amount)
            {
                Quantity -= amount;
                return true;
            }
            return false;
        }

        public decimal CalculateTotalValue()
        {
            return UnitPrice * Quantity;
        }

        public void Display()
        {
            Console.WriteLine($"Inventory #{InventoryID}: {MedicineName} ({Category})");
            Console.WriteLine($"Qty: {Quantity}, Price: {UnitPrice:C}, Expiry: {ExpiryDate?.ToString("dd/MM/yyyy") ?? "N/A"}");
        }
    }
}
