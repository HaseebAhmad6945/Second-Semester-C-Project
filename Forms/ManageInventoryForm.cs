using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.Services;

namespace Forms
{
    public partial class ManageInventoryForm : Form
    {
        private DataGridView dgvInventory;
        private TextBox txtInventoryId;
        private TextBox txtMedicineName;
        private TextBox txtCategory;
        private TextBox txtManufacturer;
        private TextBox txtUnitPrice;
        private TextBox txtQuantity;
        private TextBox txtReorderLevel;
        private DateTimePicker dtpExpiryDate;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblTitle;

        private readonly InventoryService _inventoryService;

        public ManageInventoryForm()
        {
            _inventoryService = new InventoryService();
            InitializeComponent();
            LoadInventory();
        }

        private void InitializeComponent()
        {
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.txtInventoryId = new System.Windows.Forms.TextBox();
            this.txtMedicineName = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtReorderLevel = new System.Windows.Forms.TextBox();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(350, 20);
            this.lblTitle.Text = "Manage Inventory";

            // dgvInventory
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInventory.Location = new System.Drawing.Point(30, 70);
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.Size = new System.Drawing.Size(900, 200);
            this.dgvInventory.SelectionChanged += new System.EventHandler(this.dgvInventory_SelectionChanged);

            // Row 1
            var lblInventoryId = new Label { Text = "Inventory ID:", Location = new Point(30, 300), AutoSize = true };
            this.txtInventoryId.Location = new Point(130, 297);
            this.txtInventoryId.Size = new Size(80, 20);
            this.txtInventoryId.ReadOnly = true;
            this.txtInventoryId.BackColor = System.Drawing.Color.LightGray;

            var lblMedicineName = new Label { Text = "Medicine Name:", Location = new Point(230, 300), AutoSize = true };
            this.txtMedicineName.Location = new Point(340, 297);
            this.txtMedicineName.Size = new Size(250, 20);

            var lblCategory = new Label { Text = "Category:", Location = new Point(610, 300), AutoSize = true };
            this.txtCategory.Location = new Point(685, 297);
            this.txtCategory.Size = new Size(150, 20);

            // Row 2
            var lblManufacturer = new Label { Text = "Manufacturer:", Location = new Point(30, 335), AutoSize = true };
            this.txtManufacturer.Location = new Point(130, 332);
            this.txtManufacturer.Size = new Size(200, 20);

            var lblUnitPrice = new Label { Text = "Unit Price:", Location = new Point(350, 335), AutoSize = true };
            this.txtUnitPrice.Location = new Point(420, 332);
            this.txtUnitPrice.Size = new Size(100, 20);

            var lblQuantity = new Label { Text = "Quantity:", Location = new Point(540, 335), AutoSize = true };
            this.txtQuantity.Location = new Point(610, 332);
            this.txtQuantity.Size = new Size(100, 20);

            var lblReorderLevel = new Label { Text = "Reorder Level:", Location = new Point(730, 335), AutoSize = true };
            this.txtReorderLevel.Location = new Point(825, 332);
            this.txtReorderLevel.Size = new Size(80, 20);

            // Row 3
            var lblExpiryDate = new Label { Text = "Expiry Date:", Location = new Point(30, 370), AutoSize = true };
            this.dtpExpiryDate.Location = new Point(130, 367);
            this.dtpExpiryDate.Size = new Size(200, 20);
            this.dtpExpiryDate.Format = DateTimePickerFormat.Short;

            // Buttons
            this.btnAdd.Location = new Point(150, 420);
            this.btnAdd.Size = new Size(100, 35);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnUpdate.Location = new Point(270, 420);
            this.btnUpdate.Size = new Size(100, 35);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            this.btnDelete.Location = new Point(390, 420);
            this.btnDelete.Size = new Size(100, 35);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnRefresh.Location = new Point(510, 420);
            this.btnRefresh.Size = new Size(100, 35);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnClose.Location = new Point(630, 420);
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // Form
            this.ClientSize = new Size(960, 500);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dtpExpiryDate);
            this.Controls.Add(lblExpiryDate);
            this.Controls.Add(this.txtReorderLevel);
            this.Controls.Add(lblReorderLevel);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(lblQuantity);
            this.Controls.Add(this.txtUnitPrice);
            this.Controls.Add(lblUnitPrice);
            this.Controls.Add(this.txtManufacturer);
            this.Controls.Add(lblManufacturer);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(lblCategory);
            this.Controls.Add(this.txtMedicineName);
            this.Controls.Add(lblMedicineName);
            this.Controls.Add(this.txtInventoryId);
            this.Controls.Add(lblInventoryId);
            this.Controls.Add(this.dgvInventory);
            this.Controls.Add(this.lblTitle);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Manage Inventory";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadInventory()
        {
            var inventory = _inventoryService.GetAllInventory();
            dgvInventory.DataSource = null;
            dgvInventory.DataSource = inventory;
        }

        private void dgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvInventory.SelectedRows[0];
                txtInventoryId.Text = row.Cells["InventoryID"].Value?.ToString();
                txtMedicineName.Text = row.Cells["MedicineName"].Value?.ToString();
                txtCategory.Text = row.Cells["Category"].Value?.ToString();
                txtManufacturer.Text = row.Cells["Manufacturer"].Value?.ToString();
                txtUnitPrice.Text = row.Cells["UnitPrice"].Value?.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value?.ToString();
                txtReorderLevel.Text = row.Cells["ReorderLevel"].Value?.ToString();

                if (row.Cells["ExpiryDate"].Value != null && row.Cells["ExpiryDate"].Value != DBNull.Value)
                {
                    dtpExpiryDate.Value = Convert.ToDateTime(row.Cells["ExpiryDate"].Value);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtMedicineName.Text))
            {
                MessageBox.Show("Please enter medicine name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("Please enter a valid unit price.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int reorderLevel = string.IsNullOrWhiteSpace(txtReorderLevel.Text) ?
                10 :
                int.Parse(txtReorderLevel.Text);

            // Create Inventory object
            Inventory inventory = new Inventory
            {
                MedicineName = txtMedicineName.Text.Trim(),
                Category = txtCategory.Text.Trim(),
                Manufacturer = txtManufacturer.Text.Trim(),
                UnitPrice = unitPrice,
                Quantity = quantity,
                ExpiryDate = dtpExpiryDate.Value,
                ReorderLevel = reorderLevel,
                LastRestocked = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            // Pass Inventory object to service
            bool success = _inventoryService.AddInventory(inventory);

            if (success)
            {
                MessageBox.Show("Inventory item added successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventory();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to add inventory item.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInventoryId.Text))
            {
                MessageBox.Show("Please select an inventory item to update.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("Please enter a valid unit price.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtReorderLevel.Text, out int reorderLevel))
            {
                reorderLevel = 10;
            }

            Inventory inventory = new Inventory
            {
                InventoryID = int.Parse(txtInventoryId.Text),
                MedicineName = txtMedicineName.Text.Trim(),
                Category = txtCategory.Text.Trim(),
                Manufacturer = txtManufacturer.Text.Trim(),
                UnitPrice = unitPrice,
                Quantity = quantity,
                ExpiryDate = dtpExpiryDate.Value,
                ReorderLevel = reorderLevel
            };

            bool success = _inventoryService.UpdateInventory(inventory);
            if (success)
            {
                MessageBox.Show("Inventory updated successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventory();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to update inventory.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInventoryId.Text))
            {
                MessageBox.Show("Please select an inventory item to delete.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this inventory item?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool success = _inventoryService.DeleteInventory(int.Parse(txtInventoryId.Text));
                if (success)
                {
                    MessageBox.Show("Inventory item deleted successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventory();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to delete inventory item.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInventory();
            ClearFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            txtInventoryId.Clear();
            txtMedicineName.Clear();
            txtCategory.Clear();
            txtManufacturer.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            txtReorderLevel.Text = "10"; // Default value
            dtpExpiryDate.Value = DateTime.Now.AddYears(1); // Default 1 year from now
        }
    }
}
