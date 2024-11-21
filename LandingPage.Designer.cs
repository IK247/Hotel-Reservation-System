using System;
using System.Windows.Forms;

namespace HRS_V2
{
    public partial class LandingPage : Form
    {

        private void InitializeComponent()
        {
            this.AdminLoginButton = new System.Windows.Forms.Button();
            this.AdminRegistrationButton = new System.Windows.Forms.Button();
            this.UserLoginButton = new System.Windows.Forms.Button();
            this.UserRegistrationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.AdminLoginButton.Location = new System.Drawing.Point(50, 50);
            this.AdminLoginButton.Name = "AdminLoginButton";
            this.AdminLoginButton.Size = new System.Drawing.Size(75, 23);
            this.AdminLoginButton.TabIndex = 0;
            this.AdminLoginButton.Text = "Admin Login";
            this.AdminLoginButton.Click += new System.EventHandler(this.AdminLoginButton_Click);

            this.AdminRegistrationButton.Location = new System.Drawing.Point(200, 50);
            this.AdminRegistrationButton.Name = "AdminRegistrationButton";
            this.AdminRegistrationButton.Size = new System.Drawing.Size(75, 36);
            this.AdminRegistrationButton.TabIndex = 1;
            this.AdminRegistrationButton.Text = "Admin Registration";
            this.AdminRegistrationButton.Click += new System.EventHandler(this.AdminRegistrationButton_Click);

            this.UserLoginButton.Location = new System.Drawing.Point(50, 150);
            this.UserLoginButton.Name = "UserLoginButton";
            this.UserLoginButton.Size = new System.Drawing.Size(75, 23);
            this.UserLoginButton.TabIndex = 2;
            this.UserLoginButton.Text = "User Login";
            this.UserLoginButton.Click += new System.EventHandler(this.UserLoginButton_Click);

            this.UserRegistrationButton.Location = new System.Drawing.Point(200, 150);
            this.UserRegistrationButton.Name = "UserRegistrationButton";
            this.UserRegistrationButton.Size = new System.Drawing.Size(75, 42);
            this.UserRegistrationButton.TabIndex = 3;
            this.UserRegistrationButton.Text = "User Registration";
            this.UserRegistrationButton.Click += new System.EventHandler(this.UserRegistrationButton_Click);

            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.AdminLoginButton);
            this.Controls.Add(this.AdminRegistrationButton);
            this.Controls.Add(this.UserLoginButton);
            this.Controls.Add(this.UserRegistrationButton);
            this.Name = "LandingPage";
            this.Text = "Welcome to HRS";
            this.ResumeLayout(false);

        }

        private void AdminLoginButton_Click(object sender, EventArgs e)
        {
            using (AdminLoginForm adminLoginForm = new AdminLoginForm())
            {
                adminLoginForm.ShowDialog();
            }
        }

        private void AdminRegistrationButton_Click(object sender, EventArgs e)
        {
            using (AdminRegistrationForm adminRegistrationForm = new AdminRegistrationForm())
            {
                adminRegistrationForm.ShowDialog();
            }
        }

        private void UserLoginButton_Click(object sender, EventArgs e)
        {
            using (UserLoginForm userLoginForm = new UserLoginForm())
            {
                userLoginForm.ShowDialog();
            }
        }

        private void UserRegistrationButton_Click(object sender, EventArgs e)
        {
            using (UserRegistrationForm userRegistrationForm = new UserRegistrationForm())
            {
                userRegistrationForm.ShowDialog();
            }
        }

        private System.Windows.Forms.Button AdminLoginButton;
        private System.Windows.Forms.Button AdminRegistrationButton;
        private System.Windows.Forms.Button UserLoginButton;
        private System.Windows.Forms.Button UserRegistrationButton;
    }
}
