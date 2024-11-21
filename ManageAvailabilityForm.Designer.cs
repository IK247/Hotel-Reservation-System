using System.Windows.Forms;
using System;

namespace HRS_V2
{
    partial class ManageAvailabilityForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridViewRooms;
        private Button buttonUpdateAvailability;
        private Data data;


        private void InitializeComponent()
        {

            this.dataGridViewRooms = new System.Windows.Forms.DataGridView();
            this.dataGridViewRooms.Location = new System.Drawing.Point(20, 20);
            this.dataGridViewRooms.Size = new System.Drawing.Size(400, 200);
            this.dataGridViewRooms.ReadOnly = true;
            this.dataGridViewRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRooms.AllowUserToAddRows = false;
            this.Controls.Add(this.dataGridViewRooms);

            this.buttonUpdateAvailability = new System.Windows.Forms.Button();
            this.buttonUpdateAvailability.Location = new System.Drawing.Point(150, 240);
            this.buttonUpdateAvailability.Text = "Update Availability";
            this.buttonUpdateAvailability.Click += ButtonUpdateAvailability_Click;
            this.Controls.Add(this.buttonUpdateAvailability);

            this.ClientSize = new System.Drawing.Size(440, 300);
            this.Text = "Manage Room Availability";
            this.Load += ManageAvailabilityForm_Load;
            this.ResumeLayout(false);
        }

 
        private void ManageAvailabilityForm_Load(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void LoadRoomData()
        {
            dataGridViewRooms.DataSource = data.GetAllRooms();
        }

        private void ButtonUpdateAvailability_Click(object sender, EventArgs e)
        {
            if (dataGridViewRooms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room to update availability.");
                return;
            }

            int selectedRoomId = Convert.ToInt32(dataGridViewRooms.SelectedRows[0].Cells["RoomID"].Value);

            bool isAvailabilityChanged = data.ChangeAvailability(selectedRoomId);

            if (isAvailabilityChanged)
            {
                MessageBox.Show("Room availability changed successfully!");
                LoadRoomData(); 
            }
            else
            {
                MessageBox.Show("Failed to change room availability. Please try again.");
            }
        }


    }
}
