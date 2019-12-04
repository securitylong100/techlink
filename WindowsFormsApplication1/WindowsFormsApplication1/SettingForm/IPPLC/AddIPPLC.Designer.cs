namespace WindowsFormsApplication1.SettingForm.IPPLC
{
    partial class AddIPPLC
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
            this.cmb_factory = new System.Windows.Forms.ComboBox();
            this.lbl_linecode = new System.Windows.Forms.Label();
            this.txt_modelPLC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_process = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_ip = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_line = new System.Windows.Forms.ComboBox();
            this.chk_active = new System.Windows.Forms.CheckBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmb_factory
            // 
            this.cmb_factory.FormattingEnabled = true;
            this.cmb_factory.Location = new System.Drawing.Point(127, 88);
            this.cmb_factory.Name = "cmb_factory";
            this.cmb_factory.Size = new System.Drawing.Size(189, 28);
            this.cmb_factory.TabIndex = 21;
            this.cmb_factory.SelectedIndexChanged += new System.EventHandler(this.cmb_factory_SelectedIndexChanged);
            // 
            // lbl_linecode
            // 
            this.lbl_linecode.AutoSize = true;
            this.lbl_linecode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_linecode.Location = new System.Drawing.Point(409, 162);
            this.lbl_linecode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_linecode.Name = "lbl_linecode";
            this.lbl_linecode.Size = new System.Drawing.Size(116, 25);
            this.lbl_linecode.TabIndex = 20;
            this.lbl_linecode.Text = "Model PLC:";
            // 
            // txt_modelPLC
            // 
            this.txt_modelPLC.Location = new System.Drawing.Point(533, 165);
            this.txt_modelPLC.Name = "txt_modelPLC";
            this.txt_modelPLC.Size = new System.Drawing.Size(221, 26);
            this.txt_modelPLC.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 25);
            this.label1.TabIndex = 24;
            this.label1.Text = "Factory:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 164);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 25);
            this.label2.TabIndex = 26;
            this.label2.Text = "Process:";
            // 
            // cmb_process
            // 
            this.cmb_process.FormattingEnabled = true;
            this.cmb_process.Location = new System.Drawing.Point(127, 161);
            this.cmb_process.Name = "cmb_process";
            this.cmb_process.Size = new System.Drawing.Size(189, 28);
            this.cmb_process.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(489, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 25);
            this.label3.TabIndex = 28;
            this.label3.Text = "IP:";
            // 
            // cmb_ip
            // 
            this.cmb_ip.FormattingEnabled = true;
            this.cmb_ip.Location = new System.Drawing.Point(533, 88);
            this.cmb_ip.Name = "cmb_ip";
            this.cmb_ip.Size = new System.Drawing.Size(221, 28);
            this.cmb_ip.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 235);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 25);
            this.label4.TabIndex = 30;
            this.label4.Text = "Line:";
            // 
            // cmb_line
            // 
            this.cmb_line.FormattingEnabled = true;
            this.cmb_line.Location = new System.Drawing.Point(127, 232);
            this.cmb_line.Name = "cmb_line";
            this.cmb_line.Size = new System.Drawing.Size(189, 28);
            this.cmb_line.TabIndex = 29;
            // 
            // chk_active
            // 
            this.chk_active.AutoSize = true;
            this.chk_active.Location = new System.Drawing.Point(533, 238);
            this.chk_active.Name = "chk_active";
            this.chk_active.Size = new System.Drawing.Size(108, 24);
            this.chk_active.TabIndex = 31;
            this.chk_active.Text = "is Active";
            this.chk_active.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(332, 310);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(175, 43);
            this.btn_ok.TabIndex = 32;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // AddIPPLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 365);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.chk_active);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmb_line);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_process);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_modelPLC);
            this.Controls.Add(this.cmb_factory);
            this.Controls.Add(this.lbl_linecode);
            this.Name = "AddIPPLC";
            this.Text = "AddIPPLC";
            this.Load += new System.EventHandler(this.AddIPPLC_Load);
            this.Controls.SetChildIndex(this.lbl_linecode, 0);
            this.Controls.SetChildIndex(this.cmb_factory, 0);
            this.Controls.SetChildIndex(this.txt_modelPLC, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmb_process, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmb_ip, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmb_line, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.chk_active, 0);
            this.Controls.SetChildIndex(this.btn_ok, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_factory;
        private System.Windows.Forms.Label lbl_linecode;
        private System.Windows.Forms.TextBox txt_modelPLC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_process;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_ip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_line;
        private System.Windows.Forms.CheckBox chk_active;
        private System.Windows.Forms.Button btn_ok;
    }
}