using System;
using System.Windows.Forms;

namespace HRS_V2
{
    public partial class BookingOverviewForm : Form
    {
        private Data data;

        public BookingOverviewForm(Data data)
        {
            this.data = data;

            InitializeComponents();
            LoadBookingData();
        }

        private void InitializeComponents()
        {
            dataGridViewBookings = new DataGridView();
            buttonRefresh = new Button();

            dataGridViewBookings.Location = new System.Drawing.Point(20, 20);
            dataGridViewBookings.Size = new System.Drawing.Size(400, 200);
            dataGridViewBookings.ReadOnly = true;
            dataGridViewBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBookings.AllowUserToAddRows = false;

            buttonRefresh.Location = new System.Drawing.Point(150, 240);
            buttonRefresh.Text = "Refresh";
            buttonRefresh.Click += ButtonRefresh_Click;

            Controls.Add(dataGridViewBookings);
            Controls.Add(buttonRefresh);
            ClientSize = new System.Drawing.Size(440, 300);
            Text = "Booking Overview";
        }

        private void LoadBookingData()
        {
            Console.WriteLine("Loading booking data...");
            if (data != null)
            {
                var bookingData = data.GetBookingOverview();
                Console.WriteLine($"Retrieved {bookingData.Rows.Count} bookings.");
                dataGridViewBookings.DataSource = bookingData;
            }
            else
            {
                Console.WriteLine("Data object is not initialized.");
            }
        }



        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadBookingData();
        }

        private DataGridView dataGridViewBookings;
        private Button buttonRefresh;
    }
}
