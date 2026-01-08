using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.Services;

namespace Forms
{
    public partial class ManagePrescriptionsForm : Form
    {
        private DataGridView dgvPrescriptions;
        private TextBox txtPrescriptionId;
        private TextBox txtAppointmentId;
        private TextBox txtPatientId;
        private TextBox txtDoctorId;
        private TextBox txtMedicineName;
        private TextBox txtDosage;
        private TextBox txtDuration;
        private TextBox txtInstructions;
        private DateTimePicker dtpPrescribedDate;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblTitle;

        private readonly PrescriptionService _prescriptionService;
        private readonly User _loggedInUser;

        public ManagePrescriptionsForm(User loggedInUser)
        {
            _prescriptionService = new PrescriptionService();
            _loggedInUser = loggedInUser;
            InitializeComponent();
            LoadPrescriptions();
        }

        private void InitializeComponent()
        {
            this.dgvPrescriptions = new System.Windows.Forms.DataGridView();
            this.txtPrescriptionId = new System.Windows.Forms.TextBox();
            this.txtAppointmentId = new System.Windows.Forms.TextBox();
            this.txtPatientId = new System.Windows.Forms.TextBox();
            this.txtDoctorId = new System.Windows.Forms.TextBox();
            this.txtMedicineName = new System.Windows.Forms.TextBox();
            this.txtDosage = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.dtpPrescribedDate = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescriptions)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(320, 20);
            this.lblTitle.Text = "Manage Prescriptions";

            // dgvPrescriptions
            this.dgvPrescriptions.AllowUserToAddRows = false;
            this.dgvPrescriptions.AllowUserToDeleteRows = false;
            this.dgvPrescriptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPrescriptions.Location = new System.Drawing.Point(30, 70);
            this.dgvPrescriptions.ReadOnly = true;
            this.dgvPrescriptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrescriptions.Size = new System.Drawing.Size(900, 200);
            this.dgvPrescriptions.SelectionChanged += new System.EventHandler(this.dgvPrescriptions_SelectionChanged);

            // Row 1
            var lblPrescriptionId = new Label { Text = "Prescription ID:", Location = new Point(30, 300), AutoSize = true };
            this.txtPrescriptionId.Location = new Point(140, 297);
            this.txtPrescriptionId.Size = new Size(80, 20);
            this.txtPrescriptionId.ReadOnly = true;
            this.txtPrescriptionId.BackColor = System.Drawing.Color.LightGray;

            var lblAppointmentId = new Label { Text = "Appointment ID:", Location = new Point(240, 300), AutoSize = true };
            this.txtAppointmentId.Location = new Point(360, 297);
            this.txtAppointmentId.Size = new Size(80, 20);

            var lblPatientId = new Label { Text = "Patient ID:", Location = new Point(460, 300), AutoSize = true };
            this.txtPatientId.Location = new Point(540, 297);
            this.txtPatientId.Size = new Size(80, 20);

            var lblDoctorId = new Label { Text = "Doctor ID:", Location = new Point(640, 300), AutoSize = true };
            this.txtDoctorId.Location = new Point(720, 297);
            this.txtDoctorId.Size = new Size(80, 20);

            // Row 2
            var lblMedicineName = new Label { Text = "Medicine Name:", Location = new Point(30, 335), AutoSize = true };
            this.txtMedicineName.Location = new Point(140, 332);
            this.txtMedicineName.Size = new Size(250, 20);

            var lblDosage = new Label { Text = "Dosage:", Location = new Point(410, 335), AutoSize = true };
            this.txtDosage.Location = new Point(470, 332);
            this.txtDosage.Size = new Size(150, 20);

            var lblDuration = new Label { Text = "Duration:", Location = new Point(640, 335), AutoSize = true };
            this.txtDuration.Location = new Point(710, 332);
            this.txtDuration.Size = new Size(150, 20);

            // Row 3
            var lblInstructions = new Label { Text = "Instructions:", Location = new Point(30, 370), AutoSize = true };
            this.txtInstructions.Location = new Point(140, 367);
            this.txtInstructions.Size = new Size(480, 20);
            this.txtInstructions.Multiline = true;
            this.txtInstructions.Height = 60;

            var lblPrescribedDate = new Label { Text = "Prescribed Date:", Location = new Point(640, 370), AutoSize = true };
            this.dtpPrescribedDate.Location = new Point(745, 367);
            this.dtpPrescribedDate.Size = new Size(180, 20);
            this.dtpPrescribedDate.Format = DateTimePickerFormat.Short;

            // Buttons
            this.btnAdd.Location = new Point(150, 450);
            this.btnAdd.Size = new Size(100, 35);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnUpdate.Location = new Point(270, 450);
            this.btnUpdate.Size = new Size(100, 35);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            this.btnDelete.Location = new Point(390, 450);
            this.btnDelete.Size = new Size(100, 35);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnRefresh.Location = new Point(510, 450);
            this.btnRefresh.Size = new Size(100, 35);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnClose.Location = new Point(630, 450);
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // Form
            this.ClientSize = new Size(960, 520);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dtpPrescribedDate);
            this.Controls.Add(lblPrescribedDate);
            this.Controls.Add(this.txtInstructions);
            this.Controls.Add(lblInstructions);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(lblDuration);
            this.Controls.Add(this.txtDosage);
            this.Controls.Add(lblDosage);
            this.Controls.Add(this.txtMedicineName);
            this.Controls.Add(lblMedicineName);
            this.Controls.Add(this.txtDoctorId);
            this.Controls.Add(lblDoctorId);
            this.Controls.Add(this.txtPatientId);
            this.Controls.Add(lblPatientId);
            this.Controls.Add(this.txtAppointmentId);
            this.Controls.Add(lblAppointmentId);
            this.Controls.Add(this.txtPrescriptionId);
            this.Controls.Add(lblPrescriptionId);
            this.Controls.Add(this.dgvPrescriptions);
            this.Controls.Add(this.lblTitle);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Manage Prescriptions";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescriptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadPrescriptions()
        {
            var prescriptions = _prescriptionService.GetAllPrescriptions();
            dgvPrescriptions.DataSource = null;
            dgvPrescriptions.DataSource = prescriptions;
        }

        private void dgvPrescriptions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPrescriptions.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPrescriptions.SelectedRows[0];
                txtPrescriptionId.Text = row.Cells["PrescriptionID"].Value?.ToString();
                txtAppointmentId.Text = row.Cells["AppointmentID"].Value?.ToString();
                txtPatientId.Text = row.Cells["PatientID"].Value?.ToString();
                txtDoctorId.Text = row.Cells["DoctorID"].Value?.ToString();
                txtMedicineName.Text = row.Cells["MedicineName"].Value?.ToString();
                txtDosage.Text = row.Cells["Dosage"].Value?.ToString();
                txtDuration.Text = row.Cells["Duration"].Value?.ToString();
                txtInstructions.Text = row.Cells["Instructions"].Value?.ToString();
                dtpPrescribedDate.Value = Convert.ToDateTime(row.Cells["PrescribedDate"].Value);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAppointmentId.Text, out int appointmentId) ||
                !int.TryParse(txtPatientId.Text, out int patientId) ||
                !int.TryParse(txtDoctorId.Text, out int doctorId))
            {
                MessageBox.Show("Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = _prescriptionService.AddPrescription(appointmentId, patientId, doctorId,
                txtMedicineName.Text, txtDosage.Text, txtDuration.Text, txtInstructions.Text);

            if (success)
            {
                MessageBox.Show("Prescription added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPrescriptions();
                ClearFields();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrescriptionId.Text))
            {
                MessageBox.Show("Please select a prescription.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Prescription prescription = new Prescription
            {
                PrescriptionID = int.Parse(txtPrescriptionId.Text),
                AppointmentID = int.Parse(txtAppointmentId.Text),
                PatientID = int.Parse(txtPatientId.Text),
                DoctorID = int.Parse(txtDoctorId.Text),
                MedicineName = txtMedicineName.Text,
                Dosage = txtDosage.Text,
                Duration = txtDuration.Text,
                Instructions = txtInstructions.Text,
                PrescribedDate = dtpPrescribedDate.Value
            };

            bool success = _prescriptionService.UpdatePrescription(prescription);
            if (success)
            {
                MessageBox.Show("Prescription updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPrescriptions();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrescriptionId.Text)) return;

            DialogResult result = MessageBox.Show("Delete this prescription?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool success = _prescriptionService.DeletePrescription(int.Parse(txtPrescriptionId.Text));
                if (success)
                {
                    MessageBox.Show("Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPrescriptions();
                    ClearFields();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPrescriptions();
            ClearFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            txtPrescriptionId.Clear();
            txtAppointmentId.Clear();
            txtPatientId.Clear();
            txtDoctorId.Clear();
            txtMedicineName.Clear();
            txtDosage.Clear();
            txtDuration.Clear();
            txtInstructions.Clear();
        }
    }
}
