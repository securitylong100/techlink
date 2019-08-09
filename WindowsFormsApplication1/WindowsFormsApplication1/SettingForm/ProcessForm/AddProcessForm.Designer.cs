namespace WindowsFormsApplication1.SettingForm.ProcessForm
{
    partial class AddProcessForm
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
            this.cmb_modelcode = new System.Windows.Forms.ComboBox();
            this.lbl_modelcode = new System.Windows.Forms.Label();
            this.lbl_processcode = new System.Windows.Forms.Label();
            this.txt_processcode = new System.Windows.Forms.TextBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_processname = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmb_modelcode
            // 
            this.cmb_modelcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_modelcode.FormattingEnabled = true;
            this.cmb_modelcode.Location = new System.Drawing.Point(225, 81);
            this.cmb_modelcode.Name = "cmb_modelcode";
            this.cmb_modelcode.Size = new System.Drawing.Size(231, 28);
            this.cmb_modelcode.TabIndex = 23;
            // 
            // lbl_modelcode
            // 
            this.lbl_modelcode.AutoSize = true;
            this.lbl_modelcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_modelcode.Location = new System.Drawing.Point(103, 82);
            this.lbl_modelcode.Name = "lbl_modelcode";
            this.lbl_modelcode.Size = new System.Drawing.Size(98, 20);
            this.lbl_modelcode.TabIndex = 22;
            this.lbl_modelcode.Text = "Model Code:";
            // 
            // lbl_processcode
            // 
            this.lbl_processcode.AutoSize = true;
            this.lbl_processcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_processcode.Location = new System.Drawing.Point(103, 136);
            this.lbl_processcode.Name = "lbl_processcode";
            this.lbl_processcode.Size = new System.Drawing.Size(116, 20);
            this.lbl_processcode.TabIndex = 21;
            this.lbl_processcode.Text = " Process Code:";
            // 
            // txt_processcode
            // 
            this.txt_processcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_processcode.Location = new System.Drawing.Point(225, 133);
            this.txt_processcode.Name = "txt_processcode";
            this.txt_processcode.Size = new System.Drawing.Size(231, 26);
            this.txt_processcode.TabIndex = 20;
            // 
            // btn_ok
            // 
            this.btn_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ok.Location = new System.Drawing.Point(266, 239);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(131, 46);
            this.btn_ok.TabIndex = 24;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = " Process Name:";
            // 
            // txt_processname
            // 
            this.txt_processname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_processname.Location = new System.Drawing.Point(225, 185);
            this.txt_processname.Name = "txt_processname";
            this.txt_processname.Size = new System.Drawing.Size(231, 26);
            this.txt_processname.TabIndex = 25;
            // 
            // AddProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 323);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_processname);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.cmb_modelcode);
            this.Controls.Add(this.lbl_modelcode);
            this.Controls.Add(this.lbl_processcode);
            this.Controls.Add(this.txt_processcode);
            this.Name = "AddProcessForm";
            this.Text = "AddProcessForm";
            this.Load += new System.EventHandler(this.AddProcessForm_Load_2);
            this.Controls.SetChildIndex(this.txt_processcode, 0);
            this.Controls.SetChildIndex(this.lbl_processcode, 0);
            this.Controls.SetChildIndex(this.lbl_modelcode, 0);
            this.Controls.SetChildIndex(this.cmb_modelcode, 0);
            this.Controls.SetChildIndex(this.btn_ok, 0);
            this.Controls.SetChildIndex(this.txt_processname, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_modelcode;
        private System.Windows.Forms.Label lbl_modelcode;
        private System.Windows.Forms.Label lbl_processcode;
        private System.Windows.Forms.TextBox txt_processcode;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_processname;
    }
}