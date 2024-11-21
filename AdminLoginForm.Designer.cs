using System.Windows.Forms;
using System;

namespace HRS_V2
{
    partial class AdminLoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Data data;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.usernameTextBox.Location = new System.Drawing.Point(50, 20);
            this.usernameTextBox.Size = new System.Drawing.Size(200, 20);
            this.usernameTextBox.Text = "username";
            this.Controls.Add(usernameTextBox);

            this.passwordTextBox.Location = new System.Drawing.Point(50, 50);
            this.passwordTextBox.Size = new System.Drawing.Size(200, 20);
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Text = "Password";
            this.Controls.Add(passwordTextBox);

            this.loginButton.Location = new System.Drawing.Point(120, 80);
            this.loginButton.Size = new System.Drawing.Size(60, 30);
            this.loginButton.Text = "Login";
            this.loginButton.Click += LoginButton_Click;
            this.Controls.Add(loginButton);

            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Text = "Admin Login Form";
            this.ResumeLayout(false);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            data = new Data();

            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            if (data.CheckAdminCredentials(username, password))
            {
                MessageBox.Show("Admin login successful!");

                AdminDashboardForm adminDashboardForm = new AdminDashboardForm();
                adminDashboardForm.Show();
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("Invalid admin credentials. Please try again.");
            }
        }


    }
}
