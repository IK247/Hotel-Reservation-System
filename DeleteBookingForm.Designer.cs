using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace HRS_V2
{
    partial class DeleteBookingForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView bookingHistoryGrid;
        private Button deleteBookingButton;
        private Data data;

        public DeleteBookingForm()
        {
            InitializeComponent();
            data = new Data();
            UpdateBookingHistoryGrid();
        }

        private void InitializeComponent()
        {
            this.bookingHistoryGrid = new DataGridView();
            this.deleteBookingButton = new Button();


            this.bookingHistoryGrid.Location = new Point(50, 50);
            this.bookingHistoryGrid.Size = new Size(700, 300);
            this.bookingHistoryGrid.SelectionChanged += BookingHistoryGrid_SelectionChanged;
            this.Controls.Add(bookingHistoryGrid);

            this.deleteBookingButton.Location = new Point(300, 400);
            this.deleteBookingButton.Size = new Size(150, 30);
            this.deleteBookingButton.Text = "Delete Booking";
            this.deleteBookingButton.Click += DeleteBookingButton_Click;
            this.Controls.Add(deleteBookingButton);

            this.ClientSize = new Size(800, 500);
            this.Text = "Delete Booking Form";
            this.ResumeLayout(false);
        }

        private void BookingHistoryGrid_SelectionChanged(object sender, EventArgs e)
        {
            EnableDisableDeleteBookingButton();
        }

        private void DeleteBookingButton_Click(object sender, EventArgs e)
        {
            if (bookingHistoryGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to delete.");
                return;
            }

            int bookingId = Convert.ToInt32(bookingHistoryGrid.SelectedRows[0].Cells[0].Value);

            if (data.CancelBooking(bookingId))
            {
                MessageBox.Show("Booking deleted successfully.");
                UpdateBookingHistoryGrid();
            }
            else
            {
                MessageBox.Show("Failed to delete the booking.");
            }
        }

        private void UpdateBookingHistoryGrid()
        {
            DataTable bookingHistoryData = data.GetAllBookings();

            bookingHistoryGrid.DataSource = bookingHistoryData;
        }

        private void EnableDisableDeleteBookingButton()
        {
            if (bookingHistoryGrid.SelectedRows.Count > 0)
            {
                deleteBookingButton.Enabled = true;
            }
            else
            {
                deleteBookingButton.Enabled = false;
            }
        }
    }
}
