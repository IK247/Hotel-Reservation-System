using System;
using System.Windows.Forms;

namespace HRS_V2
{
    partial class AdminDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private Data data;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        public AdminDashboardForm()
        {
            InitializeComponent();
            this.data = new Data();
        }

        private void InitializeComponent()
        {
            this.CreateBookingButton = new System.Windows.Forms.Button();
            this.DeleteBookingButton = new System.Windows.Forms.Button();
            this.AddRoomButton = new System.Windows.Forms.Button();
            this.ManageAvailabilityButton = new System.Windows.Forms.Button();
            this.BookingOverviewButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.CreateBookingButton.Click += new System.EventHandler(this.CreateBookingButton_Click);
            this.DeleteBookingButton.Click += new System.EventHandler(this.DeleteBookingButton_Click);
            this.AddRoomButton.Click += new System.EventHandler(this.AddRoomButton_Click);
            this.ManageAvailabilityButton.Click += new System.EventHandler(this.ManageAvailabilityButton_Click);
            this.BookingOverviewButton.Click += new System.EventHandler(this.BookingOverviewButton_Click);

            this.CreateBookingButton.Location = new System.Drawing.Point(50, 50);
            this.CreateBookingButton.Name = "CreateBookingButton";
            this.CreateBookingButton.Size = new System.Drawing.Size(150, 30);
            this.CreateBookingButton.TabIndex = 0;
            this.CreateBookingButton.Text = "Create Booking";

            this.DeleteBookingButton.Location = new System.Drawing.Point(250, 50);
            this.DeleteBookingButton.Name = "DeleteBookingButton";
            this.DeleteBookingButton.Size = new System.Drawing.Size(150, 30);
            this.DeleteBookingButton.TabIndex = 1;
            this.DeleteBookingButton.Text = "Delete Booking";

            this.AddRoomButton.Location = new System.Drawing.Point(450, 50);
            this.AddRoomButton.Name = "AddRoomButton";
            this.AddRoomButton.Size = new System.Drawing.Size(150, 30);
            this.AddRoomButton.TabIndex = 2;
            this.AddRoomButton.Text = "Add Room";

            this.ManageAvailabilityButton.Location = new System.Drawing.Point(650, 50);
            this.ManageAvailabilityButton.Name = "ManageAvailabilityButton";
            this.ManageAvailabilityButton.Size = new System.Drawing.Size(150, 30);
            this.ManageAvailabilityButton.TabIndex = 3;
            this.ManageAvailabilityButton.Text = "Manage Availability";

            this.BookingOverviewButton.Location = new System.Drawing.Point(350, 126);
            this.BookingOverviewButton.Name = "BookingOverviewButton";
            this.BookingOverviewButton.Size = new System.Drawing.Size(150, 30);
            this.BookingOverviewButton.TabIndex = 4;
            this.BookingOverviewButton.Text = "Booking Overview";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 450);
            this.Controls.Add(this.CreateBookingButton);
            this.Controls.Add(this.DeleteBookingButton);
            this.Controls.Add(this.AddRoomButton);
            this.Controls.Add(this.ManageAvailabilityButton);
            this.Controls.Add(this.BookingOverviewButton);
            this.Name = "AdminDashboardForm";
            this.Text = "Admin Dashboard";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button CreateBookingButton;
        private System.Windows.Forms.Button DeleteBookingButton;
        private System.Windows.Forms.Button AddRoomButton;
        private System.Windows.Forms.Button ManageAvailabilityButton;
        private System.Windows.Forms.Button BookingOverviewButton;

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
        }

        private void BookingOverviewButton_Click(object sender, EventArgs e)
        {
            using (BookingOverviewForm bookingOverviewForm = new BookingOverviewForm(data))
            {
                bookingOverviewForm.ShowDialog();
            }
        }



        private void CreateBookingButton_Click(object sender, EventArgs e)
        {
            using (CreateBookingForm createBookingForm = new CreateBookingForm(data))
            {
                createBookingForm.ShowDialog();
            }
        }

        private void DeleteBookingButton_Click(object sender, EventArgs e)
        {
            using (DeleteBookingForm deletingBookingForm = new DeleteBookingForm())
            {
                deletingBookingForm.ShowDialog();
            }
        }

        private void AddRoomButton_Click(object sender, EventArgs e)
        {
            using (AddRoomForm addRoomForm = new AddRoomForm())
            {
                addRoomForm.ShowDialog();
            }
        }

        private void ManageAvailabilityButton_Click(object sender, EventArgs e)
        {
            using (ManageAvailabilityForm manageAvailabilityForm = new ManageAvailabilityForm())
            {
                manageAvailabilityForm.ShowDialog();
            }
        }
    }
}
