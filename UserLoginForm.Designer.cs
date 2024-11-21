using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HRS_V2
{
    public partial class UserLoginForm : Form
    {
        private Data data;
        private TextBox textBoxEmail; 


        private void InitializeComponent()
        {
            this.textBoxEmail = new TextBox();
            this.buttonLogin = new Button();
            this.SuspendLayout();

            this.textBoxEmail.Location = new System.Drawing.Point(50, 50);
            this.textBoxEmail.Size = new System.Drawing.Size(200, 20);
            this.textBoxEmail.Text = "Enter your email";
            this.textBoxEmail.Enter += TextBoxEmail_Enter;
            this.textBoxEmail.Leave += TextBoxEmail_Leave;
            this.Controls.Add(textBoxEmail);

            this.buttonLogin.Location = new System.Drawing.Point(120, 80);
            this.buttonLogin.Size = new System.Drawing.Size(60, 30);
            this.buttonLogin.Text = "Login";
            this.buttonLogin.Click += ButtonLogin_Click;
            this.Controls.Add(buttonLogin);

            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Text = "User Login Form";
            this.ResumeLayout(false);
        }

        private void TextBoxEmail_Enter(object sender, EventArgs e)
        {
            if (textBoxEmail.Text == "Enter your email")
            {
                textBoxEmail.Text = "";
            }
        }

        private void TextBoxEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                textBoxEmail.Text = "Enter your email";
            }
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            string userEmail = textBoxEmail.Text.Trim();

            if (!IsValidEmail(userEmail))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email address.");
                return;
            }

            if (data == null)
            {
                data = new Data();
            }

            int userId = data.GetUserIdByEmail(userEmail);

            Console.WriteLine(userId);

            if (userId >= 0)
            {
                
                if (data.DoesUserExist(userId))
                {
                    
                    UserSession.Login(userId);

                    MessageBox.Show("Login successful!");

                    
                    using (BookingForm bookingForm = new BookingForm())
                    {
                        bookingForm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("User with the provided email does not exist. Please register first.");
                }
            }
            else
            {
                MessageBox.Show("User with the provided email does not exist. Please register first.");
            }
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

        private Button buttonLogin;
    }
}
