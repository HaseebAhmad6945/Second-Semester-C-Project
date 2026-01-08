using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.Services;

namespace Forms
{
    public partial class ManageBillsForm : Form
    {
        private DataGridView dgvBills;
        private TextBox txtBillId;
        private TextBox txtPatientId;
        private TextBox txtAppointmentId;
        private TextBox txtTotalAmount;
        private TextBox txtPaidAmount;
        private TextBox txtBalanceAmount;
        private ComboBox cboPaymentStatus;
        private ComboBox cboPaymentMethod;
        private DateTimePicker dtpBillingDate;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblTitle;

        private readonly BillService _billService;
        private readonly User _loggedInUser;

        public ManageBillsForm(User loggedInUser)
        {
            _billService = new BillService();
            _loggedInUser = loggedInUser;
            InitializeComponent();
            LoadBills();
        }

        private void InitializeComponent()
        {
            this.dgvBills = new System.Windows.Forms.DataGridView();
            this.txtBillId = new System.Windows.Forms.TextBox();
            this.txtPatientId = new System.Windows.Forms.TextBox();
            this.txtAppointmentId = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.txtPaidAmount = new System.Windows.Forms.TextBox();
            this.txtBalanceAmount = new System.Windows.Forms.TextBox();
            this.cboPaymentStatus = new System.Windows.Forms.ComboBox();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.dtpBillingDate = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(350, 20);
            this.lblTitle.Text = "Manage Bills";

            // dgvBills
            this.dgvBills.AllowUserToAddRows = false;
            this.dgvBills.AllowUserToDeleteRows = false;
            this.dgvBills.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBills.Location = new System.Drawing.Point(30, 70);
            this.dgvBills.ReadOnly = true;
            this.dgvBills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBills.Size = new System.Drawing.Size(900, 200);
            this.dgvBills.SelectionChanged += new System.EventHandler(this.dgvBills_SelectionChanged);

            // Labels and TextBoxes - Row 1
            var lblBillId = new Label { Text = "Bill ID:", Location = new Point(30, 300), AutoSize = true };
            this.txtBillId.Location = new System.Drawing.Point(120, 297);
            this.txtBillId.Size = new System.Drawing.Size(80, 20);
            this.txtBillId.ReadOnly = true;
            this.txtBillId.BackColor = System.Drawing.Color.LightGray;

            var lblPatientId = new Label { Text = "Patient ID:", Location = new Point(220, 300), AutoSize = true };
            this.txtPatientId.Location = new System.Drawing.Point(310, 297);
            this.txtPatientId.Size = new System.Drawing.Size(80, 20);

            var lblAppointmentId = new Label { Text = "Appointment ID:", Location = new Point(410, 300), AutoSize = true };
            this.txtAppointmentId.Location = new System.Drawing.Point(520, 297);
            this.txtAppointmentId.Size = new System.Drawing.Size(80, 20);

            var lblTotalAmount = new Label { Text = "Total Amount:", Location = new Point(620, 300), AutoSize = true };
            this.txtTotalAmount.Location = new System.Drawing.Point(720, 297);
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 20);

            // Row 2
            var lblPaidAmount = new Label { Text = "Paid Amount:", Location = new Point(30, 335), AutoSize = true };
            this.txtPaidAmount.Location = new System.Drawing.Point(120, 332);
            this.txtPaidAmount.Size = new System.Drawing.Size(100, 20);

            var lblBalanceAmount = new Label { Text = "Balance:", Location = new Point(240, 335), AutoSize = true };
            this.txtBalanceAmount.Location = new System.Drawing.Point(310, 332);
            this.txtBalanceAmount.Size = new System.Drawing.Size(100, 20);
            this.txtBalanceAmount.ReadOnly = true;
            this.txtBalanceAmount.BackColor = System.Drawing.Color.LightGray;

            var lblPaymentStatus = new Label { Text = "Payment Status:", Location = new Point(430, 335), AutoSize = true };
            this.cboPaymentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentStatus.Items.AddRange(new object[] { "Pending", "Partial", "Paid" });
            this.cboPaymentStatus.Location = new System.Drawing.Point(540, 332);
            this.cboPaymentStatus.Size = new System.Drawing.Size(120, 21);
            this.cboPaymentStatus.SelectedIndex = 0;

            var lblPaymentMethod = new Label { Text = "Method:", Location = new Point(680, 335), AutoSize = true };
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.Items.AddRange(new object[] { "Cash", "Card", "Online" });
            this.cboPaymentMethod.Location = new System.Drawing.Point(750, 332);
            this.cboPaymentMethod.Size = new System.Drawing.Size(100, 21);
            this.cboPaymentMethod.SelectedIndex = 0;

            // Row 3
            var lblBillingDate = new Label { Text = "Billing Date:", Location = new Point(30, 370), AutoSize = true };
            this.dtpBillingDate.Location = new System.Drawing.Point(120, 367);
            this.dtpBillingDate.Size = new System.Drawing.Size(200, 20);
            this.dtpBillingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // Buttons
            this.btnAdd.Location = new System.Drawing.Point(150, 420);
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Location = new System.Drawing.Point(270, 420);
            this.btnUpdate.Size = new System.Drawing.Size(100, 35);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Location = new System.Drawing.Point(390, 420);
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnRefresh.Location = new System.Drawing.Point(510, 420);
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.btnClose.Location = new System.Drawing.Point(630, 420);
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(960, 500);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dtpBillingDate);
            this.Controls.Add(lblBillingDate);
            this.Controls.Add(this.cboPaymentMethod);
            this.Controls.Add(lblPaymentMethod);
            this.Controls.Add(this.cboPaymentStatus);
            this.Controls.Add(lblPaymentStatus);
            this.Controls.Add(this.txtBalanceAmount);
            this.Controls.Add(lblBalanceAmount);
            this.Controls.Add(this.txtPaidAmount);
            this.Controls.Add(lblPaidAmount);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(lblTotalAmount);
            this.Controls.Add(this.txtAppointmentId);
            this.Controls.Add(lblAppointmentId);
            this.Controls.Add(this.txtPatientId);
            this.Controls.Add(lblPatientId);
            this.Controls.Add(this.txtBillId);
            this.Controls.Add(lblBillId);
            this.Controls.Add(this.dgvBills);
            this.Controls.Add(this.lblTitle);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Bills";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadBills()
        {
            var bills = _billService.GetAllBills();
            dgvBills.DataSource = null;
            dgvBills.DataSource = bills;
        }

        private void dgvBills_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBills.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvBills.SelectedRows[0];
                txtBillId.Text = row.Cells["BillID"].Value?.ToString();
                txtPatientId.Text = row.Cells["PatientID"].Value?.ToString();
                txtAppointmentId.Text = row.Cells["AppointmentID"].Value?.ToString();
                txtTotalAmount.Text = row.Cells["TotalAmount"].Value?.ToString();
                txtPaidAmount.Text = row.Cells["PaidAmount"].Value?.ToString();
                txtBalanceAmount.Text = row.Cells["BalanceAmount"].Value?.ToString();
                cboPaymentStatus.SelectedItem = row.Cells["PaymentStatus"].Value?.ToString();
                cboPaymentMethod.SelectedItem = row.Cells["PaymentMethod"].Value?.ToString();

                if (row.Cells["BillingDate"].Value != null && row.Cells["BillingDate"].Value != DBNull.Value)
                {
                    dtpBillingDate.Value = Convert.ToDateTime(row.Cells["BillingDate"].Value);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPatientId.Text, out int patientId) ||
                !decimal.TryParse(txtTotalAmount.Text, out decimal totalAmount))
            {
                MessageBox.Show("Invalid input. Please enter valid Patient ID and Total Amount.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Parse optional fields
            int? appointmentId = string.IsNullOrWhiteSpace(txtAppointmentId.Text) ?
                (int?)null :
                int.Parse(txtAppointmentId.Text);

            decimal paidAmount = string.IsNullOrWhiteSpace(txtPaidAmount.Text) ?
                0 :
                decimal.Parse(txtPaidAmount.Text);

            // Create Bill object using constructor (patientId, totalAmount)
            Bill bill = new Bill(patientId, totalAmount)
            {
                AppointmentID = appointmentId,
                PaidAmount = paidAmount,
                PaymentStatus = cboPaymentStatus.SelectedItem?.ToString() ?? "Pending",
                PaymentMethod = cboPaymentMethod.SelectedItem?.ToString() ?? "Cash",
                BillingDate = dtpBillingDate.Value
            };

            // Call AddBill with Bill object
            try
            {
                bool success = _billService.AddBill(bill);
                if (success)
                {
                    MessageBox.Show("Bill added successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBills();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to add bill. Please check inputs.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add bill: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillId.Text))
            {
                MessageBox.Show("Please select a bill to update.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtPatientId.Text, out int patientId) ||
                !decimal.TryParse(txtTotalAmount.Text, out decimal totalAmount))
            {
                MessageBox.Show("Invalid input.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? appointmentId = string.IsNullOrWhiteSpace(txtAppointmentId.Text) ?
                (int?)null :
                int.Parse(txtAppointmentId.Text);

            decimal paidAmount = string.IsNullOrWhiteSpace(txtPaidAmount.Text) ?
                0 :
                decimal.Parse(txtPaidAmount.Text);

            // Create Bill object using constructor
            Bill bill = new Bill(patientId, totalAmount)
            {
                BillID = int.Parse(txtBillId.Text),
                AppointmentID = appointmentId,
                PaidAmount = paidAmount,
                PaymentStatus = cboPaymentStatus.SelectedItem?.ToString() ?? "Pending",
                PaymentMethod = cboPaymentMethod.SelectedItem?.ToString() ?? "Cash",
                BillingDate = dtpBillingDate.Value
            };

            try
            {
                bool success = _billService.UpdateBill(bill);
                if (success)
                {
                    MessageBox.Show("Bill updated successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBills();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to update bill.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update bill: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillId.Text))
            {
                MessageBox.Show("Please select a bill to delete.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this bill?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool success = _billService.DeleteBill(int.Parse(txtBillId.Text));
                if (success)
                {
                    MessageBox.Show("Bill deleted successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBills();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to delete bill.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBills();
            ClearFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            txtBillId.Clear();
            txtPatientId.Clear();
            txtAppointmentId.Clear();
            txtTotalAmount.Clear();
            txtPaidAmount.Clear();
            txtBalanceAmount.Clear();
            cboPaymentStatus.SelectedIndex = 0;
            cboPaymentMethod.SelectedIndex = 0;
            dtpBillingDate.Value = DateTime.Now;
        }
    }
}
