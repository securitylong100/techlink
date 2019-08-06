﻿namespace WindowsFormsApplication1
{
    partial class MainFr
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
            this.tpc_main = new System.Windows.Forms.TabControl();
            this.tap_setting = new System.Windows.Forms.TabPage();
            this.btn_permission = new System.Windows.Forms.Button();
            this.btn_changepass = new System.Windows.Forms.Button();
            this.btn_registeruser = new System.Windows.Forms.Button();
            this.tap_local = new System.Windows.Forms.TabPage();
            this.btn_model = new System.Windows.Forms.Button();
            this.btn_line = new System.Windows.Forms.Button();
            this.btn_modelline = new System.Windows.Forms.Button();
            this.tpc_main.SuspendLayout();
            this.tap_setting.SuspendLayout();
            this.tap_local.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpc_main
            // 
            this.tpc_main.Controls.Add(this.tap_setting);
            this.tpc_main.Controls.Add(this.tap_local);
            this.tpc_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpc_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpc_main.Location = new System.Drawing.Point(0, 60);
            this.tpc_main.Name = "tpc_main";
            this.tpc_main.SelectedIndex = 0;
            this.tpc_main.Size = new System.Drawing.Size(788, 434);
            this.tpc_main.TabIndex = 2;
            // 
            // tap_setting
            // 
            this.tap_setting.Controls.Add(this.btn_permission);
            this.tap_setting.Controls.Add(this.btn_changepass);
            this.tap_setting.Controls.Add(this.btn_registeruser);
            this.tap_setting.Location = new System.Drawing.Point(4, 25);
            this.tap_setting.Name = "tap_setting";
            this.tap_setting.Padding = new System.Windows.Forms.Padding(3);
            this.tap_setting.Size = new System.Drawing.Size(780, 405);
            this.tap_setting.TabIndex = 0;
            this.tap_setting.Text = "Seting";
            this.tap_setting.UseVisualStyleBackColor = true;
            // 
            // btn_permission
            // 
            this.btn_permission.Enabled = false;
            this.btn_permission.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_permission.Location = new System.Drawing.Point(430, 27);
            this.btn_permission.Name = "btn_permission";
            this.btn_permission.Size = new System.Drawing.Size(153, 48);
            this.btn_permission.TabIndex = 2;
            this.btn_permission.Text = "Permission";
            this.btn_permission.UseVisualStyleBackColor = true;
            this.btn_permission.Click += new System.EventHandler(this.btn_permission_Click);
            // 
            // btn_changepass
            // 
            this.btn_changepass.Enabled = false;
            this.btn_changepass.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_changepass.Location = new System.Drawing.Point(231, 27);
            this.btn_changepass.Name = "btn_changepass";
            this.btn_changepass.Size = new System.Drawing.Size(153, 48);
            this.btn_changepass.TabIndex = 1;
            this.btn_changepass.Text = "Change Password";
            this.btn_changepass.UseVisualStyleBackColor = true;
            this.btn_changepass.Click += new System.EventHandler(this.btn_changepass_Click);
            // 
            // btn_registeruser
            // 
            this.btn_registeruser.Enabled = false;
            this.btn_registeruser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_registeruser.Location = new System.Drawing.Point(33, 27);
            this.btn_registeruser.Name = "btn_registeruser";
            this.btn_registeruser.Size = new System.Drawing.Size(153, 48);
            this.btn_registeruser.TabIndex = 0;
            this.btn_registeruser.Text = "Register User";
            this.btn_registeruser.UseVisualStyleBackColor = true;
            this.btn_registeruser.Click += new System.EventHandler(this.btn_registeruser_Click);
            // 
            // tap_local
            // 
            this.tap_local.Controls.Add(this.btn_modelline);
            this.tap_local.Controls.Add(this.btn_model);
            this.tap_local.Controls.Add(this.btn_line);
            this.tap_local.Location = new System.Drawing.Point(4, 25);
            this.tap_local.Name = "tap_local";
            this.tap_local.Padding = new System.Windows.Forms.Padding(3);
            this.tap_local.Size = new System.Drawing.Size(780, 405);
            this.tap_local.TabIndex = 1;
            this.tap_local.Text = "Local";
            this.tap_local.UseVisualStyleBackColor = true;
            // 
            // btn_model
            // 
            this.btn_model.Enabled = false;
            this.btn_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_model.Location = new System.Drawing.Point(233, 15);
            this.btn_model.Name = "btn_model";
            this.btn_model.Size = new System.Drawing.Size(153, 48);
            this.btn_model.TabIndex = 5;
            this.btn_model.Text = "Model Master";
            this.btn_model.UseVisualStyleBackColor = true;
            this.btn_model.Click += new System.EventHandler(this.btn_modelmaster_Click);
            // 
            // btn_line
            // 
            this.btn_line.Enabled = false;
            this.btn_line.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_line.Location = new System.Drawing.Point(21, 15);
            this.btn_line.Name = "btn_line";
            this.btn_line.Size = new System.Drawing.Size(153, 48);
            this.btn_line.TabIndex = 4;
            this.btn_line.Text = "Line Master";
            this.btn_line.UseVisualStyleBackColor = true;
            this.btn_line.Click += new System.EventHandler(this.btn_line_Click);
            // 
            // btn_modelline
            // 
            this.btn_modelline.Enabled = false;
            this.btn_modelline.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_modelline.Location = new System.Drawing.Point(430, 15);
            this.btn_modelline.Name = "btn_modelline";
            this.btn_modelline.Size = new System.Drawing.Size(153, 48);
            this.btn_modelline.TabIndex = 6;
            this.btn_modelline.Text = "Model Line";
            this.btn_modelline.UseVisualStyleBackColor = true;
            this.btn_modelline.Click += new System.EventHandler(this.btn_modelline_Click);
            // 
            // MainFr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 494);
            this.Controls.Add(this.tpc_main);
            this.Name = "MainFr";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.MainFr_Load);
            this.Controls.SetChildIndex(this.tpc_main, 0);
            this.tpc_main.ResumeLayout(false);
            this.tap_setting.ResumeLayout(false);
            this.tap_local.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tpc_main;
        private System.Windows.Forms.TabPage tap_setting;
        private System.Windows.Forms.TabPage tap_local;
        private System.Windows.Forms.Button btn_registeruser;
        private System.Windows.Forms.Button btn_changepass;
        private System.Windows.Forms.Button btn_permission;
        private System.Windows.Forms.Button btn_line;
        private System.Windows.Forms.Button btn_model;
        private System.Windows.Forms.Button btn_modelline;
    }
}