using System;
using System.Windows.Forms;

namespace HRS_V2
{
    public partial class AdminRegistrationForm : Form
    {
        private Data data;

        public AdminRegistrationForm()
        {
            InitializeComponent();
            data = new Data();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            if (data.RegisterAdmin(username, password))
            {
                MessageBox.Show("Admin registration successful!");
            }
            else
            {
                MessageBox.Show("Admin registration failed. Username might already be taken.");
            }
        }

        private void InitializeComponent()
        {
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();

            this.usernameTextBox.Location = new System.Drawing.Point(50, 50);
            this.usernameTextBox.Size = new System.Drawing.Size(200, 20);
            this.usernameTextBox.Text = "Username";
            this.Controls.Add(this.usernameTextBox);

            this.passwordTextBox.Location = new System.Drawing.Point(50, 80);
            this.passwordTextBox.Size = new System.Drawing.Size(200, 20);
            this.passwordTextBox.UseSystemPasswordChar = true;
            this.passwordTextBox.Text = "Password";
            this.Controls.Add(this.passwordTextBox);

            this.registerButton.Location = new System.Drawing.Point(120, 110);
            this.registerButton.Size = new System.Drawing.Size(100, 30);
            this.registerButton.Text = "Register";
            this.registerButton.Click += new System.EventHandler(this.RegisterButton_Click);
            this.Controls.Add(this.registerButton);

            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Text = "Admin Registration";
        }

        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button registerButton;
    }
}
