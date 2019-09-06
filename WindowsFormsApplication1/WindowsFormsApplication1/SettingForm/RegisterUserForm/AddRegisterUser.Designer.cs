namespace WindowsFormsApplication1.SettingForm.RegisterUserForm
{
    partial class AddRegisterUser
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
            this.lbl_username = new System.Windows.Forms.Label();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.lbl_usercode = new System.Windows.Forms.Label();
            this.txt_usercode = new System.Windows.Forms.TextBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_permission = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_username.Location = new System.Drawing.Point(130, 152);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(93, 20);
            this.lbl_username.TabIndex = 10;
            this.lbl_username.Text = "User Name:";
            // 
            // txt_username
            // 
            this.txt_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_username.Location = new System.Drawing.Point(225, 147);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(258, 26);
            this.txt_username.TabIndex = 9;
            // 
            // lbl_usercode
            // 
            this.lbl_usercode.AutoSize = true;
            this.lbl_usercode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_usercode.Location = new System.Drawing.Point(130, 94);
            this.lbl_usercode.Name = "lbl_usercode";
            this.lbl_usercode.Size = new System.Drawing.Size(89, 20);
            this.lbl_usercode.TabIndex = 8;
            this.lbl_usercode.Text = "User Code:";
            // 
            // txt_usercode
            // 
            this.txt_usercode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_usercode.Location = new System.Drawing.Point(225, 89);
            this.txt_usercode.Name = "txt_usercode";
            this.txt_usercode.Size = new System.Drawing.Size(258, 26);
            this.txt_usercode.TabIndex = 7;
            // 
            // btn_ok
            // 
            this.btn_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ok.Location = new System.Drawing.Point(292, 261);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(131, 46);
            this.btn_ok.TabIndex = 11;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(129, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Permission:";
            // 
            // cmb_permission
            // 
            this.cmb_permission.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_permission.FormattingEnabled = true;
            this.cmb_permission.Location = new System.Drawing.Point(225, 206);
            this.cmb_permission.Name = "cmb_permission";
            this.cmb_permission.Size = new System.Drawing.Size(258, 28);
            this.cmb_permission.TabIndex = 13;
            // 
            // AddRegisterUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 319);
            this.Controls.Add(this.cmb_permission);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.lbl_username);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.lbl_usercode);
            this.Controls.Add(this.txt_usercode);
            this.Name = "AddRegisterUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddRegisterUser";
            this.Load += new System.EventHandler(this.AddRegisterUser_Load);
            this.Controls.SetChildIndex(this.txt_usercode, 0);
            this.Controls.SetChildIndex(this.lbl_usercode, 0);
            this.Controls.SetChildIndex(this.txt_username, 0);
            this.Controls.SetChildIndex(this.lbl_username, 0);
            this.Controls.SetChildIndex(this.btn_ok, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmb_permission, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.Label lbl_usercode;
        private System.Windows.Forms.TextBox txt_usercode;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_permission;
    }
}