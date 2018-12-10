namespace ParkDashboard
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonStatusGivenMoment = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonSpecificParkFreeSpots = new System.Windows.Forms.Button();
            this.dateTimePickerGivenMoment = new System.Windows.Forms.DateTimePicker();
            this.textBoxGivenMomentHour = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonAllSpotsForAPark = new System.Windows.Forms.Button();
            this.buttonOccupancyRate = new System.Windows.Forms.Button();
            this.buttonSpecificParkDetails = new System.Windows.Forms.Button();
            this.buttonSpecificLowBatterySensors = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxEndingHour = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxStartingHour = new System.Windows.Forms.TextBox();
            this.dateTimePickerEndingDay = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerStartingDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStatusGivenPeriod = new System.Windows.Forms.Button();
            this.comboBoxPark = new System.Windows.Forms.ComboBox();
            this.buttonGetAllParks = new System.Windows.Forms.Button();
            this.Plataform = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSpotName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonInfoSpotGivenMoment = new System.Windows.Forms.Button();
            this.dateTimePickerGivenMomentSpot = new System.Windows.Forms.DateTimePicker();
            this.textBoxGivenMomentHourSpot = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonAllSensorsLowBattery = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.Plataform.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 461);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List";
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(6, 19);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(318, 436);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.comboBoxPark);
            this.groupBox2.Location = new System.Drawing.Point(349, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 299);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Specific Park Actions";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonStatusGivenMoment);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.buttonSpecificParkFreeSpots);
            this.groupBox5.Controls.Add(this.dateTimePickerGivenMoment);
            this.groupBox5.Controls.Add(this.textBoxGivenMomentHour);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(6, 215);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(447, 78);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Given Moment Actions";
            // 
            // buttonStatusGivenMoment
            // 
            this.buttonStatusGivenMoment.Location = new System.Drawing.Point(256, 17);
            this.buttonStatusGivenMoment.Name = "buttonStatusGivenMoment";
            this.buttonStatusGivenMoment.Size = new System.Drawing.Size(185, 20);
            this.buttonStatusGivenMoment.TabIndex = 18;
            this.buttonStatusGivenMoment.Text = "Status of all spots";
            this.buttonStatusGivenMoment.UseVisualStyleBackColor = true;
            this.buttonStatusGivenMoment.Click += new System.EventHandler(this.buttonStatusGivenMoment_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Hour";
            // 
            // buttonSpecificParkFreeSpots
            // 
            this.buttonSpecificParkFreeSpots.Location = new System.Drawing.Point(256, 43);
            this.buttonSpecificParkFreeSpots.Name = "buttonSpecificParkFreeSpots";
            this.buttonSpecificParkFreeSpots.Size = new System.Drawing.Size(185, 23);
            this.buttonSpecificParkFreeSpots.TabIndex = 5;
            this.buttonSpecificParkFreeSpots.Text = "Free spots";
            this.buttonSpecificParkFreeSpots.UseVisualStyleBackColor = true;
            this.buttonSpecificParkFreeSpots.Click += new System.EventHandler(this.buttonSpecificParkFreeSpots_Click);
            // 
            // dateTimePickerGivenMoment
            // 
            this.dateTimePickerGivenMoment.Location = new System.Drawing.Point(38, 17);
            this.dateTimePickerGivenMoment.Name = "dateTimePickerGivenMoment";
            this.dateTimePickerGivenMoment.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerGivenMoment.TabIndex = 16;
            // 
            // textBoxGivenMomentHour
            // 
            this.textBoxGivenMomentHour.Location = new System.Drawing.Point(38, 48);
            this.textBoxGivenMomentHour.Name = "textBoxGivenMomentHour";
            this.textBoxGivenMomentHour.Size = new System.Drawing.Size(200, 20);
            this.textBoxGivenMomentHour.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Day";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonAllSpotsForAPark);
            this.groupBox4.Controls.Add(this.buttonOccupancyRate);
            this.groupBox4.Controls.Add(this.buttonSpecificParkDetails);
            this.groupBox4.Controls.Add(this.buttonSpecificLowBatterySensors);
            this.groupBox4.Location = new System.Drawing.Point(4, 46);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(148, 163);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Real time actions";
            // 
            // buttonAllSpotsForAPark
            // 
            this.buttonAllSpotsForAPark.Location = new System.Drawing.Point(6, 110);
            this.buttonAllSpotsForAPark.Name = "buttonAllSpotsForAPark";
            this.buttonAllSpotsForAPark.Size = new System.Drawing.Size(134, 23);
            this.buttonAllSpotsForAPark.TabIndex = 5;
            this.buttonAllSpotsForAPark.Text = "All spots for this park";
            this.buttonAllSpotsForAPark.UseVisualStyleBackColor = true;
            this.buttonAllSpotsForAPark.Click += new System.EventHandler(this.buttonAllSpotsForAPark_Click);
            // 
            // buttonOccupancyRate
            // 
            this.buttonOccupancyRate.Location = new System.Drawing.Point(6, 51);
            this.buttonOccupancyRate.Name = "buttonOccupancyRate";
            this.buttonOccupancyRate.Size = new System.Drawing.Size(134, 23);
            this.buttonOccupancyRate.TabIndex = 3;
            this.buttonOccupancyRate.Text = "Occupancy Rate";
            this.buttonOccupancyRate.UseVisualStyleBackColor = true;
            this.buttonOccupancyRate.Click += new System.EventHandler(this.buttonOccupancyRate_Click);
            // 
            // buttonSpecificParkDetails
            // 
            this.buttonSpecificParkDetails.Location = new System.Drawing.Point(6, 22);
            this.buttonSpecificParkDetails.Name = "buttonSpecificParkDetails";
            this.buttonSpecificParkDetails.Size = new System.Drawing.Size(134, 23);
            this.buttonSpecificParkDetails.TabIndex = 2;
            this.buttonSpecificParkDetails.Text = "Details for park";
            this.buttonSpecificParkDetails.UseVisualStyleBackColor = true;
            this.buttonSpecificParkDetails.Click += new System.EventHandler(this.buttonSpecificParkDetails_Click);
            // 
            // buttonSpecificLowBatterySensors
            // 
            this.buttonSpecificLowBatterySensors.Location = new System.Drawing.Point(7, 81);
            this.buttonSpecificLowBatterySensors.Name = "buttonSpecificLowBatterySensors";
            this.buttonSpecificLowBatterySensors.Size = new System.Drawing.Size(133, 23);
            this.buttonSpecificLowBatterySensors.TabIndex = 4;
            this.buttonSpecificLowBatterySensors.Text = "Sensors with low battery";
            this.buttonSpecificLowBatterySensors.UseVisualStyleBackColor = true;
            this.buttonSpecificLowBatterySensors.Click += new System.EventHandler(this.buttonSpecificLowBatterySensors_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxEndingHour);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBoxStartingHour);
            this.groupBox3.Controls.Add(this.dateTimePickerEndingDay);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dateTimePickerStartingDate);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.buttonStatusGivenPeriod);
            this.groupBox3.Location = new System.Drawing.Point(166, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(287, 190);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Given Period Actions";
            // 
            // textBoxEndingHour
            // 
            this.textBoxEndingHour.Location = new System.Drawing.Point(75, 108);
            this.textBoxEndingHour.Name = "textBoxEndingHour";
            this.textBoxEndingHour.Size = new System.Drawing.Size(200, 20);
            this.textBoxEndingHour.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Ending Hour";
            // 
            // textBoxStartingHour
            // 
            this.textBoxStartingHour.Location = new System.Drawing.Point(75, 79);
            this.textBoxStartingHour.Name = "textBoxStartingHour";
            this.textBoxStartingHour.Size = new System.Drawing.Size(200, 20);
            this.textBoxStartingHour.TabIndex = 11;
            // 
            // dateTimePickerEndingDay
            // 
            this.dateTimePickerEndingDay.Location = new System.Drawing.Point(75, 50);
            this.dateTimePickerEndingDay.Name = "dateTimePickerEndingDay";
            this.dateTimePickerEndingDay.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEndingDay.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ending day";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Starting day";
            // 
            // dateTimePickerStartingDate
            // 
            this.dateTimePickerStartingDate.Location = new System.Drawing.Point(75, 19);
            this.dateTimePickerStartingDate.Name = "dateTimePickerStartingDate";
            this.dateTimePickerStartingDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStartingDate.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Starting Hour";
            // 
            // buttonStatusGivenPeriod
            // 
            this.buttonStatusGivenPeriod.Location = new System.Drawing.Point(9, 146);
            this.buttonStatusGivenPeriod.Name = "buttonStatusGivenPeriod";
            this.buttonStatusGivenPeriod.Size = new System.Drawing.Size(266, 23);
            this.buttonStatusGivenPeriod.TabIndex = 3;
            this.buttonStatusGivenPeriod.Text = "Status of all spots";
            this.buttonStatusGivenPeriod.UseVisualStyleBackColor = true;
            this.buttonStatusGivenPeriod.Click += new System.EventHandler(this.buttonStatusGivenPeriod_Click);
            // 
            // comboBoxPark
            // 
            this.comboBoxPark.FormattingEnabled = true;
            this.comboBoxPark.Items.AddRange(new object[] {
            "Park A",
            "Park B"});
            this.comboBoxPark.Location = new System.Drawing.Point(6, 19);
            this.comboBoxPark.Name = "comboBoxPark";
            this.comboBoxPark.Size = new System.Drawing.Size(134, 21);
            this.comboBoxPark.TabIndex = 1;
            // 
            // buttonGetAllParks
            // 
            this.buttonGetAllParks.Location = new System.Drawing.Point(6, 19);
            this.buttonGetAllParks.Name = "buttonGetAllParks";
            this.buttonGetAllParks.Size = new System.Drawing.Size(134, 23);
            this.buttonGetAllParks.TabIndex = 0;
            this.buttonGetAllParks.Text = "Parks Information";
            this.buttonGetAllParks.UseVisualStyleBackColor = true;
            this.buttonGetAllParks.Click += new System.EventHandler(this.buttonGetAllParks_Click);
            // 
            // Plataform
            // 
            this.Plataform.Controls.Add(this.groupBox6);
            this.Plataform.Controls.Add(this.buttonAllSensorsLowBattery);
            this.Plataform.Controls.Add(this.buttonGetAllParks);
            this.Plataform.Location = new System.Drawing.Point(347, 318);
            this.Plataform.Name = "Plataform";
            this.Plataform.Size = new System.Drawing.Size(474, 155);
            this.Plataform.TabIndex = 2;
            this.Plataform.TabStop = false;
            this.Plataform.Text = "Plataform Actions";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.textBoxSpotName);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.buttonInfoSpotGivenMoment);
            this.groupBox6.Controls.Add(this.dateTimePickerGivenMomentSpot);
            this.groupBox6.Controls.Add(this.textBoxGivenMomentHourSpot);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Location = new System.Drawing.Point(6, 48);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(447, 101);
            this.groupBox6.TabIndex = 19;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Given Moment Actions";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Spot Name";
            // 
            // textBoxSpotName
            // 
            this.textBoxSpotName.Location = new System.Drawing.Point(74, 75);
            this.textBoxSpotName.Name = "textBoxSpotName";
            this.textBoxSpotName.Size = new System.Drawing.Size(200, 20);
            this.textBoxSpotName.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Hour";
            // 
            // buttonInfoSpotGivenMoment
            // 
            this.buttonInfoSpotGivenMoment.Location = new System.Drawing.Point(280, 19);
            this.buttonInfoSpotGivenMoment.Name = "buttonInfoSpotGivenMoment";
            this.buttonInfoSpotGivenMoment.Size = new System.Drawing.Size(161, 76);
            this.buttonInfoSpotGivenMoment.TabIndex = 5;
            this.buttonInfoSpotGivenMoment.Text = "Details For spot";
            this.buttonInfoSpotGivenMoment.UseVisualStyleBackColor = true;
            this.buttonInfoSpotGivenMoment.Click += new System.EventHandler(this.buttonInfoSpotGivenMoment_Click);
            // 
            // dateTimePickerGivenMomentSpot
            // 
            this.dateTimePickerGivenMomentSpot.Location = new System.Drawing.Point(74, 22);
            this.dateTimePickerGivenMomentSpot.Name = "dateTimePickerGivenMomentSpot";
            this.dateTimePickerGivenMomentSpot.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerGivenMomentSpot.TabIndex = 16;
            // 
            // textBoxGivenMomentHourSpot
            // 
            this.textBoxGivenMomentHourSpot.Location = new System.Drawing.Point(74, 48);
            this.textBoxGivenMomentHourSpot.Name = "textBoxGivenMomentHourSpot";
            this.textBoxGivenMomentHourSpot.Size = new System.Drawing.Size(200, 20);
            this.textBoxGivenMomentHourSpot.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Day";
            // 
            // buttonAllSensorsLowBattery
            // 
            this.buttonAllSensorsLowBattery.Location = new System.Drawing.Point(316, 19);
            this.buttonAllSensorsLowBattery.Name = "buttonAllSensorsLowBattery";
            this.buttonAllSensorsLowBattery.Size = new System.Drawing.Size(133, 23);
            this.buttonAllSensorsLowBattery.TabIndex = 6;
            this.buttonAllSensorsLowBattery.Text = "Sensors with low battery";
            this.buttonAllSensorsLowBattery.UseVisualStyleBackColor = true;
            this.buttonAllSensorsLowBattery.Click += new System.EventHandler(this.buttonAllSensorsLowBattery_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 485);
            this.Controls.Add(this.Plataform);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Admin Dashboard";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Plataform.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStatusGivenPeriod;
        private System.Windows.Forms.Button buttonSpecificParkFreeSpots;
        private System.Windows.Forms.Button buttonSpecificLowBatterySensors;
        private System.Windows.Forms.Button buttonOccupancyRate;
        private System.Windows.Forms.Button buttonSpecificParkDetails;
        private System.Windows.Forms.ComboBox comboBoxPark;
        private System.Windows.Forms.Button buttonGetAllParks;
        private System.Windows.Forms.GroupBox Plataform;
        private System.Windows.Forms.Button buttonAllSensorsLowBattery;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartingDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxStartingHour;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndingDay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxEndingHour;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonStatusGivenMoment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePickerGivenMoment;
        private System.Windows.Forms.TextBox textBoxGivenMomentHour;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAllSpotsForAPark;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonInfoSpotGivenMoment;
        private System.Windows.Forms.DateTimePicker dateTimePickerGivenMomentSpot;
        private System.Windows.Forms.TextBox textBoxGivenMomentHourSpot;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSpotName;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}

