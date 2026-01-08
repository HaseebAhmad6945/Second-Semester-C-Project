using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.Services;

namespace Forms
{
    public partial class ManageUsersForm : Form
    {
        private DataGridView dgvUsers;
        private TextBox txtUserId;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private ComboBox cboRole;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblTitle;

        private readonly UserService _userService;

        public ManageUsersForm()
        {
            _userService = new UserService();
            InitializeComponent();
            LoadUsers();
        }

        private void InitializeComponent()
        {
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(340, 20);
            this.lblTitle.Text = "Manage Users";

            // dgvUsers
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(30, 70);
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(800, 200);
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);

            // Row 1
            var lblUserId = new Label { Text = "User ID:", Location = new Point(30, 300), AutoSize = true };
            this.txtUserId.Location = new Point(150, 297);
            this.txtUserId.Size = new Size(100, 20);
            this.txtUserId.ReadOnly = true;
            this.txtUserId.BackColor = System.Drawing.Color.LightGray;

            var lblEmail = new Label { Text = "Email:", Location = new Point(270, 300), AutoSize = true };
            this.txtEmail.Location = new Point(330, 297);
            this.txtEmail.Size = new Size(250, 20);

            // Row 2
            var lblPassword = new Label { Text = "Password:", Location = new Point(30, 335), AutoSize = true };
            this.txtPassword.Location = new Point(150, 332);
            this.txtPassword.Size = new Size(200, 20);
            this.txtPassword.PasswordChar = '*';

            var lblRole = new Label { Text = "Role:", Location = new Point(370, 335), AutoSize = true };
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.Items.AddRange(new object[] { "Admin", "Doctor", "Patient", "Nurse", "LabTechnician", "Pharmacist" });
            this.cboRole.Location = new Point(420, 332);
            this.cboRole.Size = new Size(160, 21);
            this.cboRole.SelectedIndex = 0;

            // Buttons
            this.btnAdd.Location = new Point(150, 390);
            this.btnAdd.Size = new Size(100, 35);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnUpdate.Location = new Point(270, 390);
            this.btnUpdate.Size = new Size(100, 35);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            this.btnDelete.Location = new Point(390, 390);
            this.btnDelete.Size = new Size(100, 35);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnRefresh.Location = new Point(510, 390);
            this.btnRefresh.Size = new Size(100, 35);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnClose.Location = new Point(630, 390);
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // Form
            this.ClientSize = new Size(860, 460);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cboRole);
            this.Controls.Add(lblRole);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(lblPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(lblEmail);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(lblUserId);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.lblTitle);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Manage Users";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadUsers()
        {
            var users = _userService.GetAllUsers();
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = users;
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsers.SelectedRows[0];
                txtUserId.Text = row.Cells["UserID"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtPassword.Text = ""; // Don't show password
                cboRole.SelectedItem = row.Cells["Role"].Value?.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = _userService.SignUp(txtEmail.Text, txtPassword.Text, cboRole.SelectedItem.ToString());
            if (success)
            {
                MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Email already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserId.Text))
            {
                MessageBox.Show("Please select a user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = new User
            {
                UserID = int.Parse(txtUserId.Text),
                Email = txtEmail.Text,
                Role = cboRole.SelectedItem.ToString()
            };

            // If password is not empty, update it
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                user.PasswordHash = txtPassword.Text; // Will be hashed in service
            }

            bool success = _userService.UpdateUser(user);
            if (success)
            {
                MessageBox.Show("User updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserId.Text)) return;

            DialogResult result = MessageBox.Show("Delete this user? This cannot be undone.", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool success = _userService.DeleteUser(int.Parse(txtUserId.Text));
                if (success)
                {
                    MessageBox.Show("User deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                    ClearFields();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
            ClearFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            txtUserId.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = 0;
        }
    }
}
