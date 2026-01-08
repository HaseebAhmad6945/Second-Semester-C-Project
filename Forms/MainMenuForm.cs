using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagementSystem.BL;

namespace Forms
{
    public partial class MainMenuForm : Form
    {
        private Label lblWelcome;
        private Button btnManageDoctors;
        private Button btnManagePatients;
        private Button btnManageAppointments;
        private Button btnManageBills;
        private Button btnManagePrescriptions;
        private Button btnManageInventory;
        private Button btnManageUsers;
        private Button btnLogout;

        private readonly User _loggedInUser;

        public MainMenuForm(User loggedInUser)
        {
            _loggedInUser = loggedInUser;
            InitializeComponent();
            ConfigureMenuByRole();
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnManageDoctors = new System.Windows.Forms.Button();
            this.btnManagePatients = new System.Windows.Forms.Button();
            this.btnManageAppointments = new System.Windows.Forms.Button();
            this.btnManageBills = new System.Windows.Forms.Button();
            this.btnManagePrescriptions = new System.Windows.Forms.Button();
            this.btnManageInventory = new System.Windows.Forms.Button();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(50, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(200, 24);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome";

            // btnManageDoctors
            this.btnManageDoctors.Location = new System.Drawing.Point(50, 80);
            this.btnManageDoctors.Name = "btnManageDoctors";
            this.btnManageDoctors.Size = new System.Drawing.Size(200, 40);
            this.btnManageDoctors.TabIndex = 1;
            this.btnManageDoctors.Text = "Manage Doctors";
            this.btnManageDoctors.UseVisualStyleBackColor = true;
            this.btnManageDoctors.Click += new System.EventHandler(this.btnManageDoctors_Click);

            // btnManagePatients
            this.btnManagePatients.Location = new System.Drawing.Point(300, 80);
            this.btnManagePatients.Name = "btnManagePatients";
            this.btnManagePatients.Size = new System.Drawing.Size(200, 40);
            this.btnManagePatients.TabIndex = 2;
            this.btnManagePatients.Text = "Manage Patients";
            this.btnManagePatients.UseVisualStyleBackColor = true;
            this.btnManagePatients.Click += new System.EventHandler(this.btnManagePatients_Click);

            // btnManageAppointments
            this.btnManageAppointments.Location = new System.Drawing.Point(50, 140);
            this.btnManageAppointments.Name = "btnManageAppointments";
            this.btnManageAppointments.Size = new System.Drawing.Size(200, 40);
            this.btnManageAppointments.TabIndex = 3;
            this.btnManageAppointments.Text = "Manage Appointments";
            this.btnManageAppointments.UseVisualStyleBackColor = true;
            this.btnManageAppointments.Click += new System.EventHandler(this.btnManageAppointments_Click);

            // btnManageBills
            this.btnManageBills.Location = new System.Drawing.Point(300, 140);
            this.btnManageBills.Name = "btnManageBills";
            this.btnManageBills.Size = new System.Drawing.Size(200, 40);
            this.btnManageBills.TabIndex = 4;
            this.btnManageBills.Text = "Manage Bills";
            this.btnManageBills.UseVisualStyleBackColor = true;
            this.btnManageBills.Click += new System.EventHandler(this.btnManageBills_Click);

            // btnManagePrescriptions
            this.btnManagePrescriptions.Location = new System.Drawing.Point(50, 200);
            this.btnManagePrescriptions.Name = "btnManagePrescriptions";
            this.btnManagePrescriptions.Size = new System.Drawing.Size(200, 40);
            this.btnManagePrescriptions.TabIndex = 5;
            this.btnManagePrescriptions.Text = "Manage Prescriptions";
            this.btnManagePrescriptions.UseVisualStyleBackColor = true;
            this.btnManagePrescriptions.Click += new System.EventHandler(this.btnManagePrescriptions_Click);

            // btnManageInventory
            this.btnManageInventory.Location = new System.Drawing.Point(300, 200);
            this.btnManageInventory.Name = "btnManageInventory";
            this.btnManageInventory.Size = new System.Drawing.Size(200, 40);
            this.btnManageInventory.TabIndex = 6;
            this.btnManageInventory.Text = "Manage Inventory";
            this.btnManageInventory.UseVisualStyleBackColor = true;
            this.btnManageInventory.Click += new System.EventHandler(this.btnManageInventory_Click);

            // btnManageUsers
            this.btnManageUsers.Location = new System.Drawing.Point(50, 260);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(200, 40);
            this.btnManageUsers.TabIndex = 7;
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);

            // btnLogout
            this.btnLogout.Location = new System.Drawing.Point(300, 260);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 40);
            this.btnLogout.TabIndex = 8;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // MainMenuForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 350);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.btnManageInventory);
            this.Controls.Add(this.btnManagePrescriptions);
            this.Controls.Add(this.btnManageBills);
            this.Controls.Add(this.btnManageAppointments);
            this.Controls.Add(this.btnManagePatients);
            this.Controls.Add(this.btnManageDoctors);
            this.Controls.Add(this.lblWelcome);
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hospital Management - Main Menu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ConfigureMenuByRole()
        {
            lblWelcome.Text = $"Welcome, {_loggedInUser.Email} ({_loggedInUser.Role})";

            if (_loggedInUser.Role == "Admin")
            {
                return; // Admin has full access
            }

            if (_loggedInUser.Role == "Doctor")
            {
                btnManageDoctors.Enabled = false;
                btnManageUsers.Enabled = false;
                btnManageInventory.Enabled = false;
                return;
            }

            if (_loggedInUser.Role == "Patient")
            {
                btnManageDoctors.Enabled = false;
                btnManagePatients.Enabled = false;
                btnManageUsers.Enabled = false;
                btnManageInventory.Enabled = false;
                btnManagePrescriptions.Enabled = false;
                btnManageBills.Text = "View My Bills";
                btnManageAppointments.Text = "My Appointments";
                return;
            }

            if (_loggedInUser.Role == "Pharmacist")
            {
                btnManageDoctors.Enabled = false;
                btnManagePatients.Enabled = false;
                btnManageAppointments.Enabled = false;
                btnManageBills.Enabled = false;
                btnManageUsers.Enabled = false;
                return;
            }

            // Default: disable most features
            btnManageDoctors.Enabled = false;
            btnManageUsers.Enabled = false;
        }

        // Event Handlers - All connected to their respective forms
        private void btnManageDoctors_Click(object sender, EventArgs e)
        {
            ManageDoctorsForm form = new ManageDoctorsForm();
            form.ShowDialog();
        }

        private void btnManagePatients_Click(object sender, EventArgs e)
        {
            ManagePatientsForm form = new ManagePatientsForm();
            form.ShowDialog();
        }

        private void btnManageAppointments_Click(object sender, EventArgs e)
        {
            ManageAppointmentsForm form = new ManageAppointmentsForm(_loggedInUser);
            form.ShowDialog();
        }

        private void btnManageBills_Click(object sender, EventArgs e)
        {
            ManageBillsForm form = new ManageBillsForm(_loggedInUser);
            form.ShowDialog();
        }

        private void btnManagePrescriptions_Click(object sender, EventArgs e)
        {
            ManagePrescriptionsForm form = new ManagePrescriptionsForm(_loggedInUser);
            form.ShowDialog();
        }

        private void btnManageInventory_Click(object sender, EventArgs e)
        {
            ManageInventoryForm form = new ManageInventoryForm();
            form.ShowDialog();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            ManageUsersForm form = new ManageUsersForm();
            form.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
