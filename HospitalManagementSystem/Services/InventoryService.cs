using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{
    public class InventoryService
    {
        private readonly InventoryRepo _inventoryRepo;

        public InventoryService()
        {
            _inventoryRepo = new InventoryRepo();
        }

        // Add new inventory item
        public bool AddInventoryItem(string medicineName, string category, string manufacturer,
                                     decimal unitPrice, int quantity, DateTime? expiryDate, int reorderLevel)
        {
            if (string.IsNullOrWhiteSpace(medicineName) || unitPrice < 0 || quantity < 0)
                return false;

            Inventory inventory = new Inventory(medicineName, category, manufacturer,
                                               unitPrice, quantity, expiryDate, reorderLevel);

            return _inventoryRepo.Add(inventory);
        }

        // Add inventory object
        public bool AddInventoryItem(Inventory inventory)
        {
            if (inventory == null)
                return false;

            return _inventoryRepo.Add(inventory);
        }

        // Get all inventory items
        public List<Inventory> GetAllInventoryItems()
        {
            return _inventoryRepo.GetAll();
        }

        // Get inventory item by ID
        public Inventory GetInventoryItemById(int inventoryId)
        {
            if (inventoryId <= 0)
                return null;

            return _inventoryRepo.GetById(inventoryId);
        }

        // Update inventory item
        public bool UpdateInventoryItem(Inventory inventory)
        {
            if (inventory == null || inventory.InventoryID <= 0)
                return false;

            return _inventoryRepo.Update(inventory);
        }

        // Delete inventory item
        public bool DeleteInventoryItem(int inventoryId)
        {
            if (inventoryId <= 0)
                return false;

            return _inventoryRepo.Delete(inventoryId);
        }

        // Add stock to existing item
        public bool AddStock(int inventoryId, int quantity)
        {
            if (inventoryId <= 0 || quantity <= 0)
                return false;

            Inventory item = GetInventoryItemById(inventoryId);
            if (item == null)
                return false;

            item.AddStock(quantity);
            return _inventoryRepo.Update(item);
        }

        // Remove stock from existing item
        public bool RemoveStock(int inventoryId, int quantity)
        {
            if (inventoryId <= 0 || quantity <= 0)
                return false;

            Inventory item = GetInventoryItemById(inventoryId);
            if (item == null)
                return false;

            if (!item.RemoveStock(quantity))
                return false;

            return _inventoryRepo.Update(item);
        }

        // Get items needing reorder
        public List<Inventory> GetItemsNeedingReorder()
        {
            return _inventoryRepo.GetItemsNeedingReorder();
        }

        // Get expired items
        public List<Inventory> GetExpiredItems()
        {
            var allItems = GetAllInventoryItems();
            var expiredItems = new List<Inventory>();

            foreach (var item in allItems)
            {
                if (item.IsExpired())
                {
                    expiredItems.Add(item);
                }
            }

            return expiredItems;
        }

        // Calculate total inventory value
        public decimal CalculateTotalInventoryValue()
        {
            var items = GetAllInventoryItems();
            decimal totalValue = 0;

            foreach (var item in items)
            {
                totalValue += item.CalculateTotalValue();
            }

            return totalValue;
        }

        // Display all inventory items
        public void DisplayAllInventoryItems()
        {
            var items = GetAllInventoryItems();
            if (items.Count == 0)
            {
                Console.WriteLine("No inventory items found.");
                return;
            }

            foreach (var item in items)
            {
                item.Display();
            }
        }

        // Display items needing reorder
        public void DisplayItemsNeedingReorder()
        {
            var items = GetItemsNeedingReorder();
            if (items.Count == 0)
            {
                Console.WriteLine("No items need reordering.");
                return;
            }

            Console.WriteLine("=== Items Needing Reorder ===");
            foreach (var item in items)
            {
                item.Display();
            }
        }

        // ============================================
        // ALIAS METHODS FOR FORM COMPATIBILITY
        // ============================================

        // Alias for AddInventoryItem - used by forms
        public bool AddInventory(Inventory inventory)
        {
            return AddInventoryItem(inventory);
        }

        // Alias for GetAllInventoryItems - used by forms
        public List<Inventory> GetAllInventory()
        {
            return GetAllInventoryItems();
        }

        // Alias for GetInventoryItemById - used by forms
        public Inventory GetInventoryById(int inventoryId)
        {
            return GetInventoryItemById(inventoryId);
        }

        // Alias for UpdateInventoryItem - used by forms
        public bool UpdateInventory(Inventory inventory)
        {
            return UpdateInventoryItem(inventory);
        }

        // Alias for DeleteInventoryItem - used by forms
        public bool DeleteInventory(int inventoryId)
        {
            return DeleteInventoryItem(inventoryId);
        }
    }
}
