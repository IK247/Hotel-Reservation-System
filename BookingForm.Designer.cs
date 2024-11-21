using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace HRS_V2
{
    public partial class BookingForm : Form
    {
        private Data data;
        private DateTimePicker checkInDatePicker;
        private DateTimePicker checkOutDatePicker;
        private DataGridView availableRoomsGrid;
        private Label roomInfoLabel;
        private Button bookButton;
        private DataGridView bookingHistoryGrid;
        private Button cancelBookingButton;
        private ComboBox roomTypeComboBox;

        public BookingForm()
        {
            InitializeComponent();
            data = new Data();
        }

        private void InitializeComponent()
        {
            this.checkInDatePicker = new DateTimePicker();
            this.checkOutDatePicker = new DateTimePicker();
            this.availableRoomsGrid = new DataGridView();
            this.roomInfoLabel = new Label();
            this.bookButton = new Button();
            this.bookingHistoryGrid = new DataGridView();
            this.cancelBookingButton = new Button();


 
            this.checkInDatePicker.Location = new Point(50, 50);
            this.checkInDatePicker.Size = new Size(200, 20);
            this.checkInDatePicker.ValueChanged += CheckInDatePicker_ValueChanged;
            this.Controls.Add(checkInDatePicker);

            this.checkOutDatePicker.Location = new Point(300, 50);
            this.checkOutDatePicker.Size = new Size(200, 20);
            this.checkOutDatePicker.ValueChanged += CheckOutDatePicker_ValueChanged;
            this.Controls.Add(checkOutDatePicker);

            this.availableRoomsGrid.Location = new Point(50, 100);
            this.availableRoomsGrid.Size = new Size(450, 200);
            this.availableRoomsGrid.SelectionChanged += AvailableRoomsGrid_SelectionChanged;
            this.Controls.Add(availableRoomsGrid);

            this.roomInfoLabel.Location = new Point(50, 320);
            this.roomInfoLabel.AutoSize = true;
            this.Controls.Add(roomInfoLabel);

            this.bookButton.Location = new Point(50, 360);
            this.bookButton.Size = new Size(100, 30);
            this.bookButton.Text = "Book";
            this.bookButton.Click += BookButton_Click;
            this.Controls.Add(bookButton);

            this.bookingHistoryGrid.Location = new Point(550, 100);
            this.bookingHistoryGrid.Size = new Size(200, 200);
            this.bookingHistoryGrid.SelectionChanged += BookingHistoryGrid_SelectionChanged;
            this.Controls.Add(bookingHistoryGrid);

            this.cancelBookingButton.Location = new Point(550, 360);
            this.cancelBookingButton.Size = new Size(150, 30);
            this.cancelBookingButton.Text = "Cancel Booking";
            this.cancelBookingButton.Click += CancelBookingButton_Click;
            this.Controls.Add(cancelBookingButton);

            this.roomTypeComboBox = new ComboBox();
            this.roomTypeComboBox.Location = new Point(300, 20);
            this.roomTypeComboBox.Size = new Size(150, 20);

            this.roomTypeComboBox.Items.AddRange(new string[] { "Single", "Double", "Suite" });
            this.Controls.Add(roomTypeComboBox);

            this.ClientSize = new Size(800, 600);
            this.Text = "Booking Form";
            this.ResumeLayout(false);

            this.Shown += BookingForm_Shown;
        }



        private void DisplayRoomInformation(Room room)
        {
            roomInfoLabel.Text = $"Room Type: {room.RoomType}\nFeatures: {room.Features}";
        }

        private void ClearRoomInformation()
        {
            roomInfoLabel.Text = "Select a room to view details.";
        }


        private void CheckInDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (checkInDatePicker.Value > checkOutDatePicker.Value)
            {
                MessageBox.Show("Check-in date cannot be later than check-out date.");
                checkInDatePicker.Value = checkOutDatePicker.Value;
                return;
            }

            UpdateAvailableRoomsGrid();
        }


        private void CheckOutDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (checkOutDatePicker.Value < checkInDatePicker.Value)
            {
                MessageBox.Show("Check-out date cannot be earlier than check-in date.");
                checkOutDatePicker.Value = checkInDatePicker.Value;
                return;
            }

            UpdateAvailableRoomsGrid();
        }


        private void AvailableRoomsGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (availableRoomsGrid.SelectedRows.Count > 0)
            {
                object selectedRoomIdValue = availableRoomsGrid.SelectedRows[0].Cells[0].Value;

                if (selectedRoomIdValue != DBNull.Value)
                {
                    int selectedRoomId = Convert.ToInt32(selectedRoomIdValue);

                    Room room = data.GetRoomById(selectedRoomId);

                    DisplayRoomInformation(room);
                }
                else
                {
                    ClearRoomInformation();
                }
            }
            else
            {
                ClearRoomInformation();
            }
        }



        private void BookButton_Click(object sender, EventArgs e)
        {
            if (availableRoomsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room to book.");
                return;
            }

            int selectedRoomId = Convert.ToInt32(availableRoomsGrid.SelectedRows[0].Cells[0].Value);

            if (PerformBooking(selectedRoomId))
            {
                int userId = UserSession.GetUserId(); 

                string userEmail = data.GetUserEmailById(userId);

                if (!string.IsNullOrEmpty(userEmail))
                {
                    
                    string senderEmail = "inwakanma247@gmail.com"; 
                    string senderPassword = "treu ukxd lwiq jlse"; 

                    using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                    {
                        client.Port = 587;
                        client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        client.EnableSsl = true;

                        using (MailMessage message = new MailMessage(senderEmail, userEmail))
                        {
                            message.Subject = "Booking Confirmation";
                            message.Body = "Your booking has been confirmed. Thank you!";
                            client.Send(message);

                            
                            MessageBox.Show("Booking successful!\nA confirmation email has been sent.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Booking successful! However, user email not found.");
                }

                
                UpdateAvailableRoomsGrid();
                UpdateBookingHistoryGrid();
            }
            else
            {
                MessageBox.Show("Booking failed. The room might have been booked by someone else.");
            }
        }
        private void BookingForm_Shown(object sender, EventArgs e)
        {
            
            UpdateBookingHistoryGrid();
        }



        private void CancelBookingButton_Click(object sender, EventArgs e)
        {
            CancelSelectedBooking();
        }

        private void UpdateBookingHistoryGrid()
        {
            
            int userId = UserSession.GetUserId();

            
            DataTable bookingHistoryData = data.GetUserBookingHistory(userId);

            
            bookingHistoryGrid.DataSource = bookingHistoryData;
        }



        private void BookingHistoryGrid_SelectionChanged(object sender, EventArgs e)
        {
            
            EnableDisableCancelBookingButton();
        }

        private void CancelSelectedBooking()
        {
            if (bookingHistoryGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to cancel.");
                return;
            }

            
            int bookingId = Convert.ToInt32(bookingHistoryGrid.SelectedRows[0].Cells[0].Value);

            
            if (data.CancelBooking(bookingId))
            {
                MessageBox.Show("Booking canceled successfully.");
               
                UpdateBookingHistoryGrid();
            }
            else
            {
                MessageBox.Show("Failed to cancel the booking.");
            }
        }



        private void UpdateAvailableRoomsGrid()
        {
            
            DateTime checkInDate = checkInDatePicker.Value;
            DateTime checkOutDate = checkOutDatePicker.Value;

            
            string selectedRoomType = roomTypeComboBox.SelectedItem.ToString();

            DataTable availableRoomsData = data.GetAvailableRooms(checkInDate, checkOutDate, selectedRoomType);

            availableRoomsGrid.DataSource = availableRoomsData;

            if (availableRoomsGrid.Columns.Contains("RoomID"))
            {
                availableRoomsGrid.Columns["RoomID"].Visible = false;
            }
        }



        private void DisplayRoomInfo()
        {
 
        }

        private bool PerformBooking(int roomId)
        {
            int userId = UserSession.GetUserId();

            DateTime checkInDate = checkInDatePicker.Value;
            DateTime checkOutDate = checkOutDatePicker.Value;

            try
            {
                data.MakeBooking(userId, roomId, checkInDate, checkOutDate);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while making the booking: " + ex.Message);
                return false;
            }
        }




        private void EnableDisableCancelBookingButton()
        {
            if (bookingHistoryGrid.SelectedRows.Count > 0)
            {
                cancelBookingButton.Enabled = true;
            }
            else
            {
                cancelBookingButton.Enabled = false;
            }
        }
    }
}
