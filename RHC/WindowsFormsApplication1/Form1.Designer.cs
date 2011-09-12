﻿namespace WindowsFormsApplication1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RPMData = new System.Windows.Forms.Label();
            this.heartRateData = new System.Windows.Forms.Label();
            this.currentPower = new System.Windows.Forms.Label();
            this.energy = new System.Windows.Forms.Label();
            this.power = new System.Windows.Forms.Label();
            this.distance = new System.Windows.Forms.Label();
            this.speed = new System.Windows.Forms.Label();
            this.RPM = new System.Windows.Forms.Label();
            this.heartRate = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tempChartLabel = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RPMData);
            this.groupBox1.Controls.Add(this.heartRateData);
            this.groupBox1.Controls.Add(this.currentPower);
            this.groupBox1.Controls.Add(this.energy);
            this.groupBox1.Controls.Add(this.power);
            this.groupBox1.Controls.Add(this.distance);
            this.groupBox1.Controls.Add(this.speed);
            this.groupBox1.Controls.Add(this.RPM);
            this.groupBox1.Controls.Add(this.heartRate);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // RPMData
            // 
            this.RPMData.AutoSize = true;
            this.RPMData.Location = new System.Drawing.Point(99, 46);
            this.RPMData.Name = "RPMData";
            this.RPMData.Size = new System.Drawing.Size(13, 13);
            this.RPMData.TabIndex = 8;
            this.RPMData.Text = "0";
            // 
            // heartRateData
            // 
            this.heartRateData.AutoSize = true;
            this.heartRateData.Location = new System.Drawing.Point(99, 20);
            this.heartRateData.Name = "heartRateData";
            this.heartRateData.Size = new System.Drawing.Size(13, 13);
            this.heartRateData.TabIndex = 7;
            this.heartRateData.Text = "0";
            // 
            // currentPower
            // 
            this.currentPower.AutoSize = true;
            this.currentPower.Location = new System.Drawing.Point(7, 166);
            this.currentPower.Name = "currentPower";
            this.currentPower.Size = new System.Drawing.Size(76, 13);
            this.currentPower.TabIndex = 6;
            this.currentPower.Text = "Current power:";
            // 
            // energy
            // 
            this.energy.AutoSize = true;
            this.energy.Location = new System.Drawing.Point(7, 142);
            this.energy.Name = "energy";
            this.energy.Size = new System.Drawing.Size(43, 13);
            this.energy.TabIndex = 5;
            this.energy.Text = "Energy:";
            // 
            // power
            // 
            this.power.AutoSize = true;
            this.power.Location = new System.Drawing.Point(7, 117);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(40, 13);
            this.power.TabIndex = 4;
            this.power.Text = "Power:";
            // 
            // distance
            // 
            this.distance.AutoSize = true;
            this.distance.Location = new System.Drawing.Point(7, 93);
            this.distance.Name = "distance";
            this.distance.Size = new System.Drawing.Size(52, 13);
            this.distance.TabIndex = 3;
            this.distance.Text = "Distance:";
            // 
            // speed
            // 
            this.speed.AutoSize = true;
            this.speed.Location = new System.Drawing.Point(7, 70);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(41, 13);
            this.speed.TabIndex = 2;
            this.speed.Text = "Speed:";
            // 
            // RPM
            // 
            this.RPM.AutoSize = true;
            this.RPM.Location = new System.Drawing.Point(7, 46);
            this.RPM.Name = "RPM";
            this.RPM.Size = new System.Drawing.Size(34, 13);
            this.RPM.TabIndex = 1;
            this.RPM.Text = "RPM:";
            // 
            // heartRate
            // 
            this.heartRate.AutoSize = true;
            this.heartRate.Location = new System.Drawing.Point(7, 20);
            this.heartRate.Name = "heartRate";
            this.heartRate.Size = new System.Drawing.Size(57, 13);
            this.heartRate.TabIndex = 0;
            this.heartRate.Text = "Heart rate:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tempChartLabel);
            this.groupBox2.Location = new System.Drawing.Point(234, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 213);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Charts";
            // 
            // tempChartLabel
            // 
            this.tempChartLabel.AutoSize = true;
            this.tempChartLabel.Location = new System.Drawing.Point(6, 20);
            this.tempChartLabel.Name = "tempChartLabel";
            this.tempChartLabel.Size = new System.Drawing.Size(91, 13);
            this.tempChartLabel.TabIndex = 0;
            this.tempChartLabel.Text = "No data selected.";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(12, 246);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(430, 70);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Controls";
            // 
            // comboBox1
            // 
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Heart rate",
            "RPM",
            "Speed",
            "Distance",
            "Power",
            "Energy",
            "CurrentPower"});
            this.comboBox1.Location = new System.Drawing.Point(92, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(10, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 44);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.SystemColors.Control;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem});
            this.menu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(455, 23);
            this.menu.TabIndex = 3;
            this.menu.Text = "menu";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.filesToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 318);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(455, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(125, 17);
            this.statusLabel.Text = "Waiting for user input.";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 340);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label RPM;
        private System.Windows.Forms.Label heartRate;
        private System.Windows.Forms.Label currentPower;
        private System.Windows.Forms.Label energy;
        private System.Windows.Forms.Label power;
        private System.Windows.Forms.Label distance;
        private System.Windows.Forms.Label speed;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Label RPMData;
        private System.Windows.Forms.Label heartRateData;
        private System.Windows.Forms.Label tempChartLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    }
}

