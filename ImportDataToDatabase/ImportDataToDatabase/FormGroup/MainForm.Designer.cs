namespace ImportDataToDatabase.FormGroup
{
    partial class MainForm
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
            this.lbNumCSV = new System.Windows.Forms.Label();
            this.txtFolderSource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFSource = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtFolderSave = new System.Windows.Forms.TextBox();
            this.btnFSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.numTimer = new System.Windows.Forms.NumericUpDown();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbl_barcode = new System.Windows.Forms.Label();
            this.lbl_NG = new System.Windows.Forms.Label();
            this.lbl_OK = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimer)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbNumCSV);
            this.groupBox1.Controls.Add(this.txtFolderSource);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnFSource);
            this.groupBox1.Location = new System.Drawing.Point(18, 19);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(390, 126);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folder Source";
            // 
            // lbNumCSV
            // 
            this.lbNumCSV.AutoSize = true;
            this.lbNumCSV.Location = new System.Drawing.Point(190, 85);
            this.lbNumCSV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNumCSV.Name = "lbNumCSV";
            this.lbNumCSV.Size = new System.Drawing.Size(18, 20);
            this.lbNumCSV.TabIndex = 4;
            this.lbNumCSV.Text = "0";
            // 
            // txtFolderSource
            // 
            this.txtFolderSource.Location = new System.Drawing.Point(9, 29);
            this.txtFolderSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFolderSource.Name = "txtFolderSource";
            this.txtFolderSource.Size = new System.Drawing.Size(248, 26);
            this.txtFolderSource.TabIndex = 1;
            this.txtFolderSource.Text = "D:\\CSV";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Numbers of file .csv :";
            // 
            // btnFSource
            // 
            this.btnFSource.Location = new System.Drawing.Point(269, 26);
            this.btnFSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnFSource.Name = "btnFSource";
            this.btnFSource.Size = new System.Drawing.Size(112, 35);
            this.btnFSource.TabIndex = 2;
            this.btnFSource.Text = "Browser";
            this.btnFSource.UseVisualStyleBackColor = true;
            this.btnFSource.Click += new System.EventHandler(this.btnFSource_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtFolderSave);
            this.groupBox3.Controls.Add(this.btnFSave);
            this.groupBox3.Location = new System.Drawing.Point(18, 154);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(390, 81);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Wrong Format Folder";
            // 
            // txtFolderSave
            // 
            this.txtFolderSave.Location = new System.Drawing.Point(9, 29);
            this.txtFolderSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFolderSave.Name = "txtFolderSave";
            this.txtFolderSave.Size = new System.Drawing.Size(248, 26);
            this.txtFolderSave.TabIndex = 9;
            this.txtFolderSave.Text = "D:\\CSV_Backup";
            // 
            // btnFSave
            // 
            this.btnFSave.Location = new System.Drawing.Point(269, 26);
            this.btnFSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnFSave.Name = "btnFSave";
            this.btnFSave.Size = new System.Drawing.Size(112, 35);
            this.btnFSave.TabIndex = 2;
            this.btnFSave.Text = "Browser";
            this.btnFSave.UseVisualStyleBackColor = true;
            this.btnFSave.Click += new System.EventHandler(this.btnFSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.tsStatus,
            this.toolStripStatusLabel1,
            this.tsTimer});
            this.statusStrip1.Location = new System.Drawing.Point(0, 383);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(828, 36);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(73, 29);
            this.toolStripStatusLabel2.Text = "Status :";
            // 
            // tsStatus
            // 
            this.tsStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(625, 29);
            this.tsStatus.Spring = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(69, 29);
            this.toolStripStatusLabel1.Text = "Timer :";
            // 
            // tsTimer
            // 
            this.tsTimer.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsTimer.Name = "tsTimer";
            this.tsTimer.Size = new System.Drawing.Size(39, 29);
            this.tsTimer.Text = "0 s";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.numTimer);
            this.groupBox2.Location = new System.Drawing.Point(18, 245);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(300, 129);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Timer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Timer set :";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(130, 69);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(112, 35);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(9, 69);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 35);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // numTimer
            // 
            this.numTimer.Location = new System.Drawing.Point(130, 29);
            this.numTimer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numTimer.Name = "numTimer";
            this.numTimer.Size = new System.Drawing.Size(112, 26);
            this.numTimer.TabIndex = 7;
            this.numTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTimer.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(327, 278);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 72);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbl_barcode);
            this.groupBox4.Controls.Add(this.lbl_NG);
            this.groupBox4.Controls.Add(this.lbl_OK);
            this.groupBox4.Location = new System.Drawing.Point(417, 19);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(381, 355);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data";
            // 
            // lbl_barcode
            // 
            this.lbl_barcode.AutoSize = true;
            this.lbl_barcode.Location = new System.Drawing.Point(19, 41);
            this.lbl_barcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_barcode.Name = "lbl_barcode";
            this.lbl_barcode.Size = new System.Drawing.Size(73, 20);
            this.lbl_barcode.TabIndex = 11;
            this.lbl_barcode.Text = "Barcode:";
            // 
            // lbl_NG
            // 
            this.lbl_NG.AutoSize = true;
            this.lbl_NG.Location = new System.Drawing.Point(19, 135);
            this.lbl_NG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_NG.Name = "lbl_NG";
            this.lbl_NG.Size = new System.Drawing.Size(37, 20);
            this.lbl_NG.TabIndex = 5;
            this.lbl_NG.Text = "NG:";
            // 
            // lbl_OK
            // 
            this.lbl_OK.AutoSize = true;
            this.lbl_OK.Location = new System.Drawing.Point(19, 85);
            this.lbl_OK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_OK.Name = "lbl_OK";
            this.lbl_OK.Size = new System.Drawing.Size(35, 20);
            this.lbl_OK.TabIndex = 4;
            this.lbl_OK.Text = "OK:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 419);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Import Data To DB";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimer)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFSource;
        private System.Windows.Forms.TextBox txtFolderSource;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnFSave;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsTimer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.NumericUpDown numTimer;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lbNumCSV;
        private System.Windows.Forms.TextBox txtFolderSave;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbl_NG;
        private System.Windows.Forms.Label lbl_OK;
        private System.Windows.Forms.Label lbl_barcode;
        private System.Windows.Forms.Label label2;
    }
}

