using System;
using System.Windows.Forms;

namespace HRS_V2
{
    public partial class CreateBookingForm : Form
    {
        private Data data;

        public CreateBookingForm(Data data)
        {
            this.data = data;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.textBoxRoomId = new System.Windows.Forms.TextBox();
            this.checkInDatePicker = new System.Windows.Forms.DateTimePicker();
            this.checkOutDatePicker = new System.Windows.Forms.DateTimePicker();
            this.buttonCreateBooking = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.textBoxUserId.Location = new System.Drawing.Point(23, 102);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(100, 20);
            this.textBoxUserId.TabIndex = 0;
            this.textBoxUserId.Text = "User ID";

            this.textBoxRoomId.Location = new System.Drawing.Point(442, 102);
            this.textBoxRoomId.Name = "textBoxRoomId";
            this.textBoxRoomId.Size = new System.Drawing.Size(100, 20);
            this.textBoxRoomId.TabIndex = 1;
            this.textBoxRoomId.Text = "Room ID";
 
            this.checkInDatePicker.Location = new System.Drawing.Point(442, 12);
            this.checkInDatePicker.Name = "checkInDatePicker";
            this.checkInDatePicker.Size = new System.Drawing.Size(200, 20);
            this.checkInDatePicker.TabIndex = 2;

            this.checkOutDatePicker.Location = new System.Drawing.Point(23, 12);
            this.checkOutDatePicker.Name = "checkOutDatePicker";
            this.checkOutDatePicker.Size = new System.Drawing.Size(200, 20);
            this.checkOutDatePicker.TabIndex = 3;

            this.buttonCreateBooking.Location = new System.Drawing.Point(35, 213);
            this.buttonCreateBooking.Name = "buttonCreateBooking";
            this.buttonCreateBooking.Size = new System.Drawing.Size(150, 30);
            this.buttonCreateBooking.TabIndex = 5;
            this.buttonCreateBooking.Text = "Create Booking";
            this.buttonCreateBooking.Click += new System.EventHandler(this.ButtonCreateBooking_Click);

            this.ClientSize = new System.Drawing.Size(692, 308);
            this.Controls.Add(this.textBoxUserId);
            this.Controls.Add(this.textBoxRoomId);
            this.Controls.Add(this.checkInDatePicker);
            this.Controls.Add(this.checkOutDatePicker);
            this.Controls.Add(this.buttonCreateBooking);
            this.Name = "CreateBookingForm";
            this.Text = "Create Booking Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ButtonCreateBooking_Click(object sender, EventArgs e)
        {
            try
            {
                int userId;
                if (!int.TryParse(textBoxUserId.Text, out userId) || userId < 0)
                {
                    MessageBox.Show("Please enter a valid User ID.");
                    return;
                }

                int roomId;
                if (!int.TryParse(textBoxRoomId.Text, out roomId) || roomId < 0)
                {
                    MessageBox.Show("Please enter a valid Room ID.");
                    return;
                }

                DateTime checkInDate = checkInDatePicker.Value;
                DateTime checkOutDate = checkOutDatePicker.Value;

                Console.WriteLine($"User ID: {userId}, Room ID: {roomId}, Check-in: {checkInDate}, Check-out: {checkOutDate}");

                data.MakeBooking(userId, roomId, checkInDate, checkOutDate);

                MessageBox.Show("Booking created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show("An error occurred while creating the booking. Please try again.");
            }
        }

        private TextBox textBoxUserId;
        private TextBox textBoxRoomId;
        private DateTimePicker checkInDatePicker;
        private DateTimePicker checkOutDatePicker;
        private Button buttonCreateBooking;
    }
}
