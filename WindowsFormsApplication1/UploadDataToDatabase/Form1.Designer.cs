namespace UploadDataToDatabase
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
            this.components = new System.ComponentModel.Container();
            this.btn_Start = new System.Windows.Forms.Button();
            this.pbar_upload = new System.Windows.Forms.ProgressBar();
            this.lbl_tittle = new System.Windows.Forms.Label();
            this.dtp_from = new System.Windows.Forms.DateTimePicker();
            this.lal_fromdate = new System.Windows.Forms.Label();
            this.lbl_todate = new System.Windows.Forms.Label();
            this.dtp_todate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chb_uploadMaterial = new System.Windows.Forms.CheckBox();
            this.chb_uploadProduction = new System.Windows.Forms.CheckBox();
            this.chb_uploadShipping = new System.Windows.Forms.CheckBox();
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.num_seconds = new System.Windows.Forms.NumericUpDown();
            this.lbl_seconds = new System.Windows.Forms.Label();
            this.num_minutes = new System.Windows.Forms.NumericUpDown();
            this.lbl_minutes = new System.Windows.Forms.Label();
            this.num_hours = new System.Windows.Forms.NumericUpDown();
            this.lbl_hours = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_seconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_minutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_hours)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(531, 162);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(122, 52);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // pbar_upload
            // 
            this.pbar_upload.Location = new System.Drawing.Point(12, 217);
            this.pbar_upload.Name = "pbar_upload";
            this.pbar_upload.Size = new System.Drawing.Size(641, 41);
            this.pbar_upload.TabIndex = 2;
            // 
            // lbl_tittle
            // 
            this.lbl_tittle.AutoSize = true;
            this.lbl_tittle.Location = new System.Drawing.Point(13, 13);
            this.lbl_tittle.Name = "lbl_tittle";
            this.lbl_tittle.Size = new System.Drawing.Size(0, 17);
            this.lbl_tittle.TabIndex = 3;
            // 
            // dtp_from
            // 
            this.dtp_from.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_from.CustomFormat = "yyyy-MM-dd";
            this.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_from.Location = new System.Drawing.Point(13, 78);
            this.dtp_from.Margin = new System.Windows.Forms.Padding(4);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(134, 22);
            this.dtp_from.TabIndex = 23;
            // 
            // lal_fromdate
            // 
            this.lal_fromdate.AutoSize = true;
            this.lal_fromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lal_fromdate.Location = new System.Drawing.Point(16, 54);
            this.lal_fromdate.Name = "lal_fromdate";
            this.lal_fromdate.Size = new System.Drawing.Size(89, 20);
            this.lal_fromdate.TabIndex = 24;
            this.lal_fromdate.Text = "From Date";
            // 
            // lbl_todate
            // 
            this.lbl_todate.AutoSize = true;
            this.lbl_todate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_todate.Location = new System.Drawing.Point(196, 54);
            this.lbl_todate.Name = "lbl_todate";
            this.lbl_todate.Size = new System.Drawing.Size(69, 20);
            this.lbl_todate.TabIndex = 26;
            this.lbl_todate.Text = "To Date";
            // 
            // dtp_todate
            // 
            this.dtp_todate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_todate.CustomFormat = "yyyy-MM-dd";
            this.dtp_todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_todate.Location = new System.Drawing.Point(193, 78);
            this.dtp_todate.Margin = new System.Windows.Forms.Padding(4);
            this.dtp_todate.Name = "dtp_todate";
            this.dtp_todate.Size = new System.Drawing.Size(134, 22);
            this.dtp_todate.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chb_uploadMaterial);
            this.groupBox1.Controls.Add(this.chb_uploadProduction);
            this.groupBox1.Controls.Add(this.chb_uploadShipping);
            this.groupBox1.Location = new System.Drawing.Point(20, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(474, 103);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Task Update";
            // 
            // chb_uploadMaterial
            // 
            this.chb_uploadMaterial.AutoSize = true;
            this.chb_uploadMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_uploadMaterial.Location = new System.Drawing.Point(7, 84);
            this.chb_uploadMaterial.Name = "chb_uploadMaterial";
            this.chb_uploadMaterial.Size = new System.Drawing.Size(148, 24);
            this.chb_uploadMaterial.TabIndex = 2;
            this.chb_uploadMaterial.Text = "Upload Material";
            this.chb_uploadMaterial.UseVisualStyleBackColor = true;
            // 
            // chb_uploadProduction
            // 
            this.chb_uploadProduction.AutoSize = true;
            this.chb_uploadProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_uploadProduction.Location = new System.Drawing.Point(7, 54);
            this.chb_uploadProduction.Name = "chb_uploadProduction";
            this.chb_uploadProduction.Size = new System.Drawing.Size(168, 24);
            this.chb_uploadProduction.TabIndex = 1;
            this.chb_uploadProduction.Text = "Upload Production";
            this.chb_uploadProduction.UseVisualStyleBackColor = true;
            // 
            // chb_uploadShipping
            // 
            this.chb_uploadShipping.AutoSize = true;
            this.chb_uploadShipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_uploadShipping.Location = new System.Drawing.Point(7, 19);
            this.chb_uploadShipping.Name = "chb_uploadShipping";
            this.chb_uploadShipping.Size = new System.Drawing.Size(152, 24);
            this.chb_uploadShipping.TabIndex = 0;
            this.chb_uploadShipping.Text = "Upload Shipping";
            this.chb_uploadShipping.UseVisualStyleBackColor = true;
            // 
            // timer_update
            // 
            this.timer_update.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.num_seconds);
            this.groupBox3.Controls.Add(this.lbl_seconds);
            this.groupBox3.Controls.Add(this.num_minutes);
            this.groupBox3.Controls.Add(this.lbl_minutes);
            this.groupBox3.Controls.Add(this.num_hours);
            this.groupBox3.Controls.Add(this.lbl_hours);
            this.groupBox3.Location = new System.Drawing.Point(359, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 89);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Timer";
            // 
            // num_seconds
            // 
            this.num_seconds.Location = new System.Drawing.Point(182, 53);
            this.num_seconds.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.num_seconds.Name = "num_seconds";
            this.num_seconds.Size = new System.Drawing.Size(70, 22);
            this.num_seconds.TabIndex = 34;
            this.num_seconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbl_seconds
            // 
            this.lbl_seconds.AutoSize = true;
            this.lbl_seconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_seconds.Location = new System.Drawing.Point(178, 18);
            this.lbl_seconds.Name = "lbl_seconds";
            this.lbl_seconds.Size = new System.Drawing.Size(74, 20);
            this.lbl_seconds.TabIndex = 33;
            this.lbl_seconds.Text = "Seconds";
            // 
            // num_minutes
            // 
            this.num_minutes.Location = new System.Drawing.Point(93, 53);
            this.num_minutes.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.num_minutes.Name = "num_minutes";
            this.num_minutes.Size = new System.Drawing.Size(70, 22);
            this.num_minutes.TabIndex = 32;
            // 
            // lbl_minutes
            // 
            this.lbl_minutes.AutoSize = true;
            this.lbl_minutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_minutes.Location = new System.Drawing.Point(88, 18);
            this.lbl_minutes.Name = "lbl_minutes";
            this.lbl_minutes.Size = new System.Drawing.Size(68, 20);
            this.lbl_minutes.TabIndex = 31;
            this.lbl_minutes.Text = "Minutes";
            // 
            // num_hours
            // 
            this.num_hours.Location = new System.Drawing.Point(10, 53);
            this.num_hours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.num_hours.Name = "num_hours";
            this.num_hours.Size = new System.Drawing.Size(70, 22);
            this.num_hours.TabIndex = 30;
            // 
            // lbl_hours
            // 
            this.lbl_hours.AutoSize = true;
            this.lbl_hours.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hours.Location = new System.Drawing.Point(6, 18);
            this.lbl_hours.Name = "lbl_hours";
            this.lbl_hours.Size = new System.Drawing.Size(55, 20);
            this.lbl_hours.TabIndex = 29;
            this.lbl_hours.Text = "Hours";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 270);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_todate);
            this.Controls.Add(this.dtp_todate);
            this.Controls.Add(this.lal_fromdate);
            this.Controls.Add(this.dtp_from);
            this.Controls.Add(this.lbl_tittle);
            this.Controls.Add(this.pbar_upload);
            this.Controls.Add(this.btn_Start);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_seconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_minutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_hours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.ProgressBar pbar_upload;
        private System.Windows.Forms.Label lbl_tittle;
        private System.Windows.Forms.DateTimePicker dtp_from;
        private System.Windows.Forms.Label lal_fromdate;
        private System.Windows.Forms.Label lbl_todate;
        private System.Windows.Forms.DateTimePicker dtp_todate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chb_uploadMaterial;
        private System.Windows.Forms.CheckBox chb_uploadProduction;
        private System.Windows.Forms.CheckBox chb_uploadShipping;
        private System.Windows.Forms.Timer timer_update;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown num_seconds;
        private System.Windows.Forms.Label lbl_seconds;
        private System.Windows.Forms.NumericUpDown num_minutes;
        private System.Windows.Forms.Label lbl_minutes;
        private System.Windows.Forms.NumericUpDown num_hours;
        private System.Windows.Forms.Label lbl_hours;
    }
}

