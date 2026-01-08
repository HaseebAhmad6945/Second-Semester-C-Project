using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.Services;

namespace Forms
{
    public partial class ManageAppointmentsForm : Form
    {
        private DataGridView dgvAppointments;
        private TextBox txtAppointmentId;
        private TextBox txtPatientId;
        private TextBox txtDoctorId;
        private DateTimePicker dtpAppointmentDate;
        private ComboBox cboStatus;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblTitle;

        // OPTIONAL: add labels so user knows what to enter
        private Label lblAppointmentId;
        private Label lblPatientId;
        private Label lblDoctorId;
        private Label lblDate;
        private Label lblStatus;

        private readonly AppointmentService _appointmentService;
        private readonly PatientService _patientService;
        private readonly DoctorService _doctorService;
        private readonly User _loggedInUser;

        public ManageAppointmentsForm(User loggedInUser)
        {
            _appointmentService = new AppointmentService();
            _patientService = new PatientService();
            _doctorService = new DoctorService();
            _loggedInUser = loggedInUser;

            InitializeComponent();
            LoadAppointments();
            ClearInputs();
        }

        private void InitializeComponent()
        {
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.txtAppointmentId = new System.Windows.Forms.TextBox();
            this.txtPatientId = new System.Windows.Forms.TextBox();
            this.txtDoctorId = new System.Windows.Forms.TextBox();
            this.dtpAppointmentDate = new System.Windows.Forms.DateTimePicker();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            this.lblAppointmentId = new System.Windows.Forms.Label();
            this.lblPatientId = new System.Windows.Forms.Label();
            this.lblDoctorId = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(300, 20);
            this.lblTitle.Text = "Manage Appointments";

            // dgvAppointments
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointments.Location = new System.Drawing.Point(30, 70);
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.MultiSelect = false;
            this.dgvAppointments.Size = new System.Drawing.Size(740, 200);
            this.dgvAppointments.SelectionChanged += new System.EventHandler(this.dgvAppointments_SelectionChanged);

            // Labels (so user understands inputs)
            this.lblAppointmentId.AutoSize = true;
            this.lblAppointmentId.Location = new System.Drawing.Point(50, 303);
            this.lblAppointmentId.Text = "AppointmentID";

            this.lblPatientId.AutoSize = true;
            this.lblPatientId.Location = new System.Drawing.Point(50, 333);
            this.lblPatientId.Text = "PatientID";

            this.lblDoctorId.AutoSize = true;
            this.lblDoctorId.Location = new System.Drawing.Point(50, 363);
            this.lblDoctorId.Text = "DoctorID";

            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(320, 303);
            this.lblDate.Text = "Appointment Date";

            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(320, 333);
            this.lblStatus.Text = "Status";

            // txtAppointmentId
            this.txtAppointmentId.Location = new System.Drawing.Point(150, 300);
            this.txtAppointmentId.ReadOnly = true;
            this.txtAppointmentId.Size = new System.Drawing.Size(140, 20);
            this.txtAppointmentId.BackColor = System.Drawing.Color.LightGray;

            // txtPatientId
            this.txtPatientId.Location = new System.Drawing.Point(150, 330);
            this.txtPatientId.Size = new System.Drawing.Size(140, 20);

            // txtDoctorId
            this.txtDoctorId.Location = new System.Drawing.Point(150, 360);
            this.txtDoctorId.Size = new System.Drawing.Size(140, 20);

            // dtpAppointmentDate
            this.dtpAppointmentDate.Location = new System.Drawing.Point(450, 300);
            this.dtpAppointmentDate.Size = new System.Drawing.Size(220, 20);
            this.dtpAppointmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAppointmentDate.CustomFormat = "dd/MM/yyyy hh:mm tt";

            // cboStatus
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Items.Clear();
            this.cboStatus.Items.AddRange(new object[] { "Pending", "Completed", "Cancelled" });
            this.cboStatus.Location = new System.Drawing.Point(450, 330);
            this.cboStatus.Size = new System.Drawing.Size(150, 21);
            this.cboStatus.SelectedIndex = 0;

            // Buttons
            this.btnAdd.Location = new System.Drawing.Point(150, 410);
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Location = new System.Drawing.Point(270, 410);
            this.btnUpdate.Size = new System.Drawing.Size(100, 35);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Location = new System.Drawing.Point(390, 410);
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnRefresh.Location = new System.Drawing.Point(510, 410);
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.btnClose.Location = new System.Drawing.Point(630, 410);
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.dtpAppointmentDate);
            this.Controls.Add(this.txtDoctorId);
            this.Controls.Add(this.txtPatientId);
            this.Controls.Add(this.txtAppointmentId);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblDoctorId);
            this.Controls.Add(this.lblPatientId);
            this.Controls.Add(this.lblAppointmentId);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.lblTitle);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Appointments";

            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadAppointments()
        {
            var appointments = _appointmentService.GetAllAppointments();
            dgvAppointments.DataSource = null;
            dgvAppointments.DataSource = appointments;
        }

        private void ClearInputs()
        {
            txtAppointmentId.Text = "";
            txtPatientId.Text = "";
            txtDoctorId.Text = "";
            dtpAppointmentDate.Value = DateTime.Now;
            cboStatus.SelectedIndex = 0;
        }

        private bool TryReadInputs(out int patientId, out int doctorId, out DateTime date, out string status)
        {
            patientId = 0;
            doctorId = 0;
            date = dtpAppointmentDate.Value;
            status = cboStatus.SelectedItem?.ToString() ?? "Pending";

            if (!int.TryParse(txtPatientId.Text, out int rawPatientInput) || rawPatientInput <= 0)
            {
                MessageBox.Show("Enter a valid Patient ID or User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Try resolve as PatientID first, then as UserID -> Patient
            var patient = _patientService.GetPatientById(rawPatientInput);
            if (patient == null)
            {
                patient = _patientService.GetPatientByUserId(rawPatientInput);
            }

            if (patient == null)
            {
                MessageBox.Show("Patient not found. Enter an existing PatientID or associated UserID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            patientId = patient.PatientID;

            if (!int.TryParse(txtDoctorId.Text, out int rawDoctorInput) || rawDoctorInput <= 0)
            {
                MessageBox.Show("Enter a valid Doctor ID or User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Try resolve as DoctorID first, then as UserID -> Doctor
            var doctor = _doctorService.GetDoctorById(rawDoctorInput);
            if (doctor == null)
            {
                doctor = _doctorService.GetDoctorByUserId(rawDoctorInput);
            }

            if (doctor == null)
            {
                MessageBox.Show("Doctor not found. Enter an existing DoctorID or associated UserID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            doctorId = doctor.DoctorID;

            return true;
        }

        private void dgvAppointments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count <= 0) return;

            var row = dgvAppointments.SelectedRows[0];
            if (row?.DataBoundItem is Appointment apt)
            {
                txtAppointmentId.Text = apt.AppointmentID.ToString();
                txtPatientId.Text = apt.PatientID.ToString();
                txtDoctorId.Text = apt.DoctorID.ToString();
                dtpAppointmentDate.Value = apt.AppointmentDate;

                if (apt.Status != null && cboStatus.Items.Contains(apt.Status))
                    cboStatus.SelectedItem = apt.Status;
                else
                    cboStatus.SelectedIndex = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryReadInputs(out int patientId, out int doctorId, out DateTime date, out string status))
                return;

            // FIX: Use AddAppointment so status is saved (BookAppointment always uses Pending)
            Appointment appointment = new Appointment(patientId, doctorId, date, status);

            try
            {
                bool success = _appointmentService.AddAppointment(appointment);
                if (success)
                {
                    MessageBox.Show("Appointment added successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointments();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Failed to add appointment.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add appointment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAppointmentId.Text, out int appointmentId) || appointmentId <= 0)
            {
                MessageBox.Show("Please select an appointment first.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryReadInputs(out int patientId, out int doctorId, out DateTime date, out string status))
                return;

            Appointment apt = new Appointment(appointmentId, patientId, doctorId, date, status);

            try
            {
                bool success = _appointmentService.UpdateAppointment(apt);
                if (success)
                {
                    MessageBox.Show("Appointment updated.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointments();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Failed to update appointment.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update appointment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAppointmentId.Text, out int appointmentId) || appointmentId <= 0)
            {
                MessageBox.Show("Please select an appointment first.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Delete this appointment?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            bool success = _appointmentService.DeleteAppointment(appointmentId);
            if (success)
            {
                MessageBox.Show("Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAppointments();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Failed to delete appointment.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAppointments();
            ClearInputs();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
