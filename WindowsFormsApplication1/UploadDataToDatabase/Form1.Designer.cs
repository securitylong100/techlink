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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.dgv_show = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_seconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_minutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_hours)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_show)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(578, 201);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(137, 55);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // pbar_upload
            // 
            this.pbar_upload.Location = new System.Drawing.Point(-6, 259);
            this.pbar_upload.Name = "pbar_upload";
            this.pbar_upload.Size = new System.Drawing.Size(721, 44);
            this.pbar_upload.TabIndex = 2;
            // 
            // lbl_tittle
            // 
            this.lbl_tittle.AutoSize = true;
            this.lbl_tittle.Location = new System.Drawing.Point(15, 14);
            this.lbl_tittle.Name = "lbl_tittle";
            this.lbl_tittle.Size = new System.Drawing.Size(0, 17);
            this.lbl_tittle.TabIndex = 3;
            // 
            // dtp_from
            // 
            this.dtp_from.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_from.CustomFormat = "yyyy-MM-dd";
            this.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_from.Location = new System.Drawing.Point(11, 112);
            this.dtp_from.Margin = new System.Windows.Forms.Padding(4);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(150, 23);
            this.dtp_from.TabIndex = 23;
            // 
            // lal_fromdate
            // 
            this.lal_fromdate.AutoSize = true;
            this.lal_fromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lal_fromdate.Location = new System.Drawing.Point(15, 86);
            this.lal_fromdate.Name = "lal_fromdate";
            this.lal_fromdate.Size = new System.Drawing.Size(89, 20);
            this.lal_fromdate.TabIndex = 24;
            this.lal_fromdate.Text = "From Date";
            // 
            // lbl_todate
            // 
            this.lbl_todate.AutoSize = true;
            this.lbl_todate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_todate.Location = new System.Drawing.Point(201, 86);
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
            this.dtp_todate.Location = new System.Drawing.Point(198, 112);
            this.dtp_todate.Margin = new System.Windows.Forms.Padding(4);
            this.dtp_todate.Name = "dtp_todate";
            this.dtp_todate.Size = new System.Drawing.Size(150, 23);
            this.dtp_todate.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chb_uploadMaterial);
            this.groupBox1.Controls.Add(this.chb_uploadProduction);
            this.groupBox1.Controls.Add(this.chb_uploadShipping);
            this.groupBox1.Location = new System.Drawing.Point(3, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 109);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Task Update";
            // 
            // chb_uploadMaterial
            // 
            this.chb_uploadMaterial.AutoSize = true;
            this.chb_uploadMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_uploadMaterial.Location = new System.Drawing.Point(8, 89);
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
            this.chb_uploadProduction.Location = new System.Drawing.Point(8, 57);
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
            this.chb_uploadShipping.Location = new System.Drawing.Point(8, 20);
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
            this.groupBox3.Location = new System.Drawing.Point(385, 55);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 95);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Timer";
            // 
            // num_seconds
            // 
            this.num_seconds.Location = new System.Drawing.Point(205, 56);
            this.num_seconds.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.num_seconds.Name = "num_seconds";
            this.num_seconds.Size = new System.Drawing.Size(79, 23);
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
            this.lbl_seconds.Location = new System.Drawing.Point(200, 19);
            this.lbl_seconds.Name = "lbl_seconds";
            this.lbl_seconds.Size = new System.Drawing.Size(74, 20);
            this.lbl_seconds.TabIndex = 33;
            this.lbl_seconds.Text = "Seconds";
            // 
            // num_minutes
            // 
            this.num_minutes.Location = new System.Drawing.Point(105, 56);
            this.num_minutes.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.num_minutes.Name = "num_minutes";
            this.num_minutes.Size = new System.Drawing.Size(79, 23);
            this.num_minutes.TabIndex = 32;
            // 
            // lbl_minutes
            // 
            this.lbl_minutes.AutoSize = true;
            this.lbl_minutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_minutes.Location = new System.Drawing.Point(99, 19);
            this.lbl_minutes.Name = "lbl_minutes";
            this.lbl_minutes.Size = new System.Drawing.Size(68, 20);
            this.lbl_minutes.TabIndex = 31;
            this.lbl_minutes.Text = "Minutes";
            // 
            // num_hours
            // 
            this.num_hours.Location = new System.Drawing.Point(11, 56);
            this.num_hours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.num_hours.Name = "num_hours";
            this.num_hours.Size = new System.Drawing.Size(79, 23);
            this.num_hours.TabIndex = 30;
            // 
            // lbl_hours
            // 
            this.lbl_hours.AutoSize = true;
            this.lbl_hours.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hours.Location = new System.Drawing.Point(7, 19);
            this.lbl_hours.Name = "lbl_hours";
            this.lbl_hours.Size = new System.Drawing.Size(55, 20);
            this.lbl_hours.TabIndex = 29;
            this.lbl_hours.Text = "Hours";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.5942F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.4058F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 113);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.51546F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1552, 527);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtp_from);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.btn_Start);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.pbar_upload);
            this.panel1.Controls.Add(this.lbl_todate);
            this.panel1.Controls.Add(this.lal_fromdate);
            this.panel1.Controls.Add(this.dtp_todate);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 521);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 25);
            this.label1.TabIndex = 29;
            this.label1.Text = "Upload data to database";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btn_Remove);
            this.panel2.Controls.Add(this.btn_add);
            this.panel2.Controls.Add(this.dgv_show);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(728, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(819, 517);
            this.panel2.TabIndex = 1;
            // 
            // btn_Remove
            // 
            this.btn_Remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Remove.Location = new System.Drawing.Point(665, 21);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(146, 52);
            this.btn_Remove.TabIndex = 33;
            this.btn_Remove.Text = "REMOVE";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.Btn_Remove_Click);
            // 
            // btn_add
            // 
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(512, 21);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(146, 52);
            this.btn_add.TabIndex = 32;
            this.btn_add.Text = "ADD";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.Btn_add_Click);
            // 
            // dgv_show
            // 
            this.dgv_show.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_show.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_show.Location = new System.Drawing.Point(4, 112);
            this.dgv_show.Name = "dgv_show";
            this.dgv_show.RowHeadersWidth = 51;
            this.dgv_show.RowTemplate.Height = 24;
            this.dgv_show.Size = new System.Drawing.Size(814, 402);
            this.dgv_show.TabIndex = 31;
            this.dgv_show.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_show_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 25);
            this.label2.TabIndex = 30;
            this.label2.Text = "Set Time to Send Mail";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1569, 640);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lbl_tittle);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.lbl_tittle, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_seconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_minutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_hours)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_show)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.DataGridView dgv_show;
        private System.Windows.Forms.Label label2;
    }
}

