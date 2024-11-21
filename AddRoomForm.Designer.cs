using System.Windows.Forms;
using System;

namespace HRS_V2
{
    partial class AddRoomForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private RadioButton radioAvailable;
        private RadioButton radioReserved;
        private TextBox textBoxRoomID;
        private TextBox textBoxFeatures;
        private Label labelRoomType;
        private ComboBox comboBoxRoomType;
        private Label labelRoomNumber;
        private TextBox textBoxRoomNumber;
        private Button buttonAddRoom;
        private Data data;

        public AddRoomForm()
        {
            InitializeComponent();
            this.data = new Data();
        }

        private void InitializeComponent()
        {
            this.radioAvailable = new System.Windows.Forms.RadioButton();
            this.radioReserved = new System.Windows.Forms.RadioButton();
            this.textBoxRoomID = new System.Windows.Forms.TextBox();
            this.textBoxFeatures = new System.Windows.Forms.TextBox();
            this.labelRoomType = new System.Windows.Forms.Label();
            this.comboBoxRoomType = new System.Windows.Forms.ComboBox();
            this.labelRoomNumber = new System.Windows.Forms.Label();
            this.textBoxRoomNumber = new System.Windows.Forms.TextBox();
            this.buttonAddRoom = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.radioAvailable.Checked = true;
            this.radioAvailable.Location = new System.Drawing.Point(120, 220);
            this.radioAvailable.Name = "radioAvailable";
            this.radioAvailable.Size = new System.Drawing.Size(104, 24);
            this.radioAvailable.TabIndex = 0;
            this.radioAvailable.TabStop = true;
            this.radioAvailable.Text = "Available";

            this.radioReserved.Location = new System.Drawing.Point(220, 220);
            this.radioReserved.Name = "radioReserved";
            this.radioReserved.Size = new System.Drawing.Size(104, 24);
            this.radioReserved.TabIndex = 1;
            this.radioReserved.Text = "Reserved";

            this.textBoxRoomID.Location = new System.Drawing.Point(120, 260);
            this.textBoxRoomID.Name = "textBoxRoomID";
            this.textBoxRoomID.Size = new System.Drawing.Size(200, 20);
            this.textBoxRoomID.TabIndex = 2;
            this.textBoxRoomID.Text = "Enter Room ID";

            this.textBoxFeatures.Location = new System.Drawing.Point(120, 300);
            this.textBoxFeatures.Multiline = true;
            this.textBoxFeatures.Name = "textBoxFeatures";
            this.textBoxFeatures.Size = new System.Drawing.Size(200, 60);
            this.textBoxFeatures.TabIndex = 3;
            this.textBoxFeatures.Text = "Enter Features";

            this.labelRoomType.Location = new System.Drawing.Point(20, 20);
            this.labelRoomType.Name = "labelRoomType";
            this.labelRoomType.Size = new System.Drawing.Size(100, 23);
            this.labelRoomType.TabIndex = 4;
            this.labelRoomType.Text = "Room Type:";

            this.comboBoxRoomType.Items.AddRange(new object[] {
                "Single",
                "Double",
                "Suite"
            });
            this.comboBoxRoomType.Location = new System.Drawing.Point(150, 20);
            this.comboBoxRoomType.Name = "comboBoxRoomType";
            this.comboBoxRoomType.Size = new System.Drawing.Size(200, 21);
            this.comboBoxRoomType.TabIndex = 5;

            this.labelRoomNumber.Location = new System.Drawing.Point(20, 60);
            this.labelRoomNumber.Name = "labelRoomNumber";
            this.labelRoomNumber.Size = new System.Drawing.Size(100, 23);
            this.labelRoomNumber.TabIndex = 6;
            this.labelRoomNumber.Text = "Room Number:";

            this.textBoxRoomNumber.Location = new System.Drawing.Point(150, 60);
            this.textBoxRoomNumber.Name = "textBoxRoomNumber";
            this.textBoxRoomNumber.Size = new System.Drawing.Size(200, 20);
            this.textBoxRoomNumber.TabIndex = 7;

            this.buttonAddRoom.Location = new System.Drawing.Point(150, 100);
            this.buttonAddRoom.Name = "buttonAddRoom";
            this.buttonAddRoom.Size = new System.Drawing.Size(75, 23);
            this.buttonAddRoom.TabIndex = 8;
            this.buttonAddRoom.Text = "Add Room";

            this.buttonAddRoom.Click += new System.EventHandler(this.ButtonAddRoom_Click);
            this.textBoxRoomID.Enter += new System.EventHandler(this.TextBoxRoomID_Enter);
            this.textBoxRoomID.Leave += new System.EventHandler(this.TextBoxRoomID_Leave);
            this.textBoxFeatures.Enter += new System.EventHandler(this.TextBoxFeatures_Enter);
            this.textBoxFeatures.Leave += new System.EventHandler(this.TextBoxFeatures_Leave);

            this.ClientSize = new System.Drawing.Size(443, 406);
            this.Controls.Add(this.radioAvailable);
            this.Controls.Add(this.radioReserved);
            this.Controls.Add(this.textBoxRoomID);
            this.Controls.Add(this.textBoxFeatures);
            this.Controls.Add(this.labelRoomType);
            this.Controls.Add(this.comboBoxRoomType);
            this.Controls.Add(this.labelRoomNumber);
            this.Controls.Add(this.textBoxRoomNumber);
            this.Controls.Add(this.buttonAddRoom);
            this.Name = "AddRoomForm";
            this.Text = "Add Room";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void TextBoxRoomID_Enter(object sender, EventArgs e)
        {
            if (textBoxRoomID.Text == "Enter Room ID")
            {
                textBoxRoomID.Text = "";
            }
        }

        private void TextBoxRoomID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRoomID.Text))
            {
                textBoxRoomID.Text = "Enter Room ID";
            }
        }

        private void TextBoxFeatures_Enter(object sender, EventArgs e)
        {
            if (textBoxFeatures.Text == "Enter Features")
            {
                textBoxFeatures.Text = "";
            }
        }

        private void TextBoxFeatures_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFeatures.Text))
            {
                textBoxFeatures.Text = "Enter Features";
            }
        }

        private void ButtonAddRoom_Click(object sender, EventArgs e)
        {
            string selectedRoomType = comboBoxRoomType.SelectedItem.ToString();
            string roomNumber = textBoxRoomNumber.Text;
            string availability = radioAvailable.Checked ? "Available" : "Reserved";
            string features = textBoxFeatures.Text;

            if (!int.TryParse(textBoxRoomID.Text, out int roomID))
            {
                MessageBox.Show("Invalid Room ID. Please enter a valid number.");
                return;
            }

            bool isRoomAdded = data.AddRoom(roomID, selectedRoomType, roomNumber, availability, features);

            if (isRoomAdded)
            {
                MessageBox.Show("Room added successfully!");
            }
            else
            {
                MessageBox.Show("Failed to add room. Please try again.");
            }
        }
    }
}
