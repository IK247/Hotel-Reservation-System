using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Windows.Forms;

namespace HRS_V2
{
    public partial class UserRegistrationForm : Form
    {
        private Data data;
        private TextBox userEmailTextBox; 
        private TextBox userNameTextBox; 
        private Button userRegisterButton;

        public UserRegistrationForm()
        {
            InitializeComponent();
            data = new Data();
        }

        private void InitializeComponent()
        {
            this.userEmailTextBox = new TextBox();
            this.userNameTextBox = new TextBox();
            this.userRegisterButton = new Button();
            this.SuspendLayout();

            this.userEmailTextBox.Location = new System.Drawing.Point(50, 50);
            this.userEmailTextBox.Size = new System.Drawing.Size(200, 20);
            this.userEmailTextBox.Text = "Enter your email";
            this.userEmailTextBox.Enter += TextBoxEmail_Enter;
            this.userEmailTextBox.Leave += TextBoxEmail_Leave;
            this.Controls.Add(userEmailTextBox);

            this.userNameTextBox.Location = new System.Drawing.Point(50, 80);
            this.userNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.userNameTextBox.Text = "Enter your name";
            this.userNameTextBox.Enter += TextBoxName_Enter;
            this.userNameTextBox.Leave += TextBoxName_Leave;
            this.Controls.Add(userNameTextBox);

            this.userRegisterButton.Location = new System.Drawing.Point(120, 110);
            this.userRegisterButton.Size = new System.Drawing.Size(60, 30);
            this.userRegisterButton.Text = "Register";
            this.userRegisterButton.Click += UserRegisterButton_Click;
            this.Controls.Add(userRegisterButton);

            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Text = "User Registration Form";
            this.ResumeLayout(false);
        }

        private void TextBoxEmail_Enter(object sender, EventArgs e)
        {
            if (userEmailTextBox.Text == "Enter your email")
            {
                userEmailTextBox.Text = "";
            }
        }

        private void TextBoxEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userEmailTextBox.Text))
            {
                userEmailTextBox.Text = "Enter your email";
            }
        }

        private void TextBoxName_Enter(object sender, EventArgs e)
        {
            if (userNameTextBox.Text == "Enter your name")
            {
                userNameTextBox.Text = "";
            }
        }

        private void TextBoxName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userNameTextBox.Text))
            {
                userNameTextBox.Text = "Enter your name";
            }
        }

        private void UserRegisterButton_Click(object sender, EventArgs e)
        {
            string emailAddress = userEmailTextBox.Text.Trim();
            string name = userNameTextBox.Text.Trim();

            if (!IsValidEmail(emailAddress))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email address.");
                return;
            }

            data.RegisterUser(emailAddress, name);

            MessageBox.Show("Registration successful! You can now log in.");

            this.Close();

            UserLoginForm userLoginForm = new UserLoginForm();
            userLoginForm.Show();
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
