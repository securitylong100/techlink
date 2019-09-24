namespace WindowsFormsApplication1
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
            this.btn_emailconfig = new System.Windows.Forms.Button();
            this.btn_order_pdc = new System.Windows.Forms.Button();
            this.btn_process = new System.Windows.Forms.Button();
            this.btn_dept = new System.Windows.Forms.Button();
            this.btn_modelline = new System.Windows.Forms.Button();
            this.btn_model = new System.Windows.Forms.Button();
            this.btn_line = new System.Windows.Forms.Button();
            this.tap_function = new System.Windows.Forms.TabPage();
            this.btn_production = new System.Windows.Forms.Button();
            this.btn_shipping = new System.Windows.Forms.Button();
            this.btn_pqmshow = new System.Windows.Forms.Button();
            this.tpc_main.SuspendLayout();
            this.tap_setting.SuspendLayout();
            this.tap_local.SuspendLayout();
            this.tap_function.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpc_main
            // 
            this.tpc_main.Controls.Add(this.tap_setting);
            this.tpc_main.Controls.Add(this.tap_local);
            this.tpc_main.Controls.Add(this.tap_function);
            this.tpc_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpc_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpc_main.Location = new System.Drawing.Point(0, 60);
            this.tpc_main.Margin = new System.Windows.Forms.Padding(4);
            this.tpc_main.Name = "tpc_main";
            this.tpc_main.SelectedIndex = 0;
            this.tpc_main.Size = new System.Drawing.Size(1013, 586);
            this.tpc_main.TabIndex = 2;
            // 
            // tap_setting
            // 
            this.tap_setting.Controls.Add(this.btn_permission);
            this.tap_setting.Controls.Add(this.btn_changepass);
            this.tap_setting.Controls.Add(this.btn_registeruser);
            this.tap_setting.Location = new System.Drawing.Point(4, 29);
            this.tap_setting.Margin = new System.Windows.Forms.Padding(4);
            this.tap_setting.Name = "tap_setting";
            this.tap_setting.Padding = new System.Windows.Forms.Padding(4);
            this.tap_setting.Size = new System.Drawing.Size(1005, 553);
            this.tap_setting.TabIndex = 0;
            this.tap_setting.Text = "Seting";
            this.tap_setting.UseVisualStyleBackColor = true;
            // 
            // btn_permission
            // 
            this.btn_permission.Enabled = false;
            this.btn_permission.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_permission.Location = new System.Drawing.Point(539, 35);
            this.btn_permission.Margin = new System.Windows.Forms.Padding(4);
            this.btn_permission.Name = "btn_permission";
            this.btn_permission.Size = new System.Drawing.Size(197, 63);
            this.btn_permission.TabIndex = 2;
            this.btn_permission.Text = "Permission";
            this.btn_permission.UseVisualStyleBackColor = true;
            this.btn_permission.Click += new System.EventHandler(this.btn_permission_Click);
            // 
            // btn_changepass
            // 
            this.btn_changepass.Enabled = false;
            this.btn_changepass.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_changepass.Location = new System.Drawing.Point(287, 35);
            this.btn_changepass.Margin = new System.Windows.Forms.Padding(4);
            this.btn_changepass.Name = "btn_changepass";
            this.btn_changepass.Size = new System.Drawing.Size(197, 63);
            this.btn_changepass.TabIndex = 1;
            this.btn_changepass.Text = "Change Password";
            this.btn_changepass.UseVisualStyleBackColor = true;
            this.btn_changepass.Click += new System.EventHandler(this.btn_changepass_Click);
            // 
            // btn_registeruser
            // 
            this.btn_registeruser.Enabled = false;
            this.btn_registeruser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_registeruser.Location = new System.Drawing.Point(42, 35);
            this.btn_registeruser.Margin = new System.Windows.Forms.Padding(4);
            this.btn_registeruser.Name = "btn_registeruser";
            this.btn_registeruser.Size = new System.Drawing.Size(197, 63);
            this.btn_registeruser.TabIndex = 0;
            this.btn_registeruser.Text = "Register User";
            this.btn_registeruser.UseVisualStyleBackColor = true;
            this.btn_registeruser.Click += new System.EventHandler(this.btn_registeruser_Click);
            // 
            // tap_local
            // 
            this.tap_local.Controls.Add(this.btn_emailconfig);
            this.tap_local.Controls.Add(this.btn_order_pdc);
            this.tap_local.Controls.Add(this.btn_process);
            this.tap_local.Controls.Add(this.btn_dept);
            this.tap_local.Controls.Add(this.btn_modelline);
            this.tap_local.Controls.Add(this.btn_model);
            this.tap_local.Controls.Add(this.btn_line);
            this.tap_local.Location = new System.Drawing.Point(4, 29);
            this.tap_local.Margin = new System.Windows.Forms.Padding(4);
            this.tap_local.Name = "tap_local";
            this.tap_local.Padding = new System.Windows.Forms.Padding(4);
            this.tap_local.Size = new System.Drawing.Size(1005, 553);
            this.tap_local.TabIndex = 1;
            this.tap_local.Text = "Local";
            this.tap_local.UseVisualStyleBackColor = true;
            // 
            // btn_emailconfig
            // 
            this.btn_emailconfig.Enabled = false;
            this.btn_emailconfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_emailconfig.Location = new System.Drawing.Point(300, 114);
            this.btn_emailconfig.Margin = new System.Windows.Forms.Padding(4);
            this.btn_emailconfig.Name = "btn_emailconfig";
            this.btn_emailconfig.Size = new System.Drawing.Size(197, 63);
            this.btn_emailconfig.TabIndex = 10;
            this.btn_emailconfig.Text = "Email Config";
            this.btn_emailconfig.UseVisualStyleBackColor = true;
            this.btn_emailconfig.Click += new System.EventHandler(this.Btn_emailconfig_Click);
            // 
            // btn_order_pdc
            // 
            this.btn_order_pdc.Enabled = false;
            this.btn_order_pdc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_order_pdc.Location = new System.Drawing.Point(27, 319);
            this.btn_order_pdc.Margin = new System.Windows.Forms.Padding(4);
            this.btn_order_pdc.Name = "btn_order_pdc";
            this.btn_order_pdc.Size = new System.Drawing.Size(197, 63);
            this.btn_order_pdc.TabIndex = 9;
            this.btn_order_pdc.Text = "Order-PDC";
            this.btn_order_pdc.UseVisualStyleBackColor = true;
            // 
            // btn_process
            // 
            this.btn_process.Enabled = false;
            this.btn_process.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_process.Location = new System.Drawing.Point(27, 114);
            this.btn_process.Margin = new System.Windows.Forms.Padding(4);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(197, 63);
            this.btn_process.TabIndex = 8;
            this.btn_process.Text = "Process Inspect";
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // btn_dept
            // 
            this.btn_dept.Enabled = false;
            this.btn_dept.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dept.Location = new System.Drawing.Point(27, 213);
            this.btn_dept.Margin = new System.Windows.Forms.Padding(4);
            this.btn_dept.Name = "btn_dept";
            this.btn_dept.Size = new System.Drawing.Size(197, 63);
            this.btn_dept.TabIndex = 7;
            this.btn_dept.Text = "Department";
            this.btn_dept.UseVisualStyleBackColor = true;
            this.btn_dept.Click += new System.EventHandler(this.btn_dept_Click_1);
            // 
            // btn_modelline
            // 
            this.btn_modelline.Enabled = false;
            this.btn_modelline.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_modelline.Location = new System.Drawing.Point(553, 20);
            this.btn_modelline.Margin = new System.Windows.Forms.Padding(4);
            this.btn_modelline.Name = "btn_modelline";
            this.btn_modelline.Size = new System.Drawing.Size(197, 63);
            this.btn_modelline.TabIndex = 6;
            this.btn_modelline.Text = "Model - Line";
            this.btn_modelline.UseVisualStyleBackColor = true;
            this.btn_modelline.Click += new System.EventHandler(this.btn_modelline_Click);
            // 
            // btn_model
            // 
            this.btn_model.Enabled = false;
            this.btn_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_model.Location = new System.Drawing.Point(300, 20);
            this.btn_model.Margin = new System.Windows.Forms.Padding(4);
            this.btn_model.Name = "btn_model";
            this.btn_model.Size = new System.Drawing.Size(197, 63);
            this.btn_model.TabIndex = 5;
            this.btn_model.Text = "Model Master";
            this.btn_model.UseVisualStyleBackColor = true;
            this.btn_model.Click += new System.EventHandler(this.btn_modelmaster_Click);
            // 
            // btn_line
            // 
            this.btn_line.Enabled = false;
            this.btn_line.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_line.Location = new System.Drawing.Point(27, 20);
            this.btn_line.Margin = new System.Windows.Forms.Padding(4);
            this.btn_line.Name = "btn_line";
            this.btn_line.Size = new System.Drawing.Size(197, 63);
            this.btn_line.TabIndex = 4;
            this.btn_line.Text = "Line Master";
            this.btn_line.UseVisualStyleBackColor = true;
            this.btn_line.Click += new System.EventHandler(this.btn_line_Click);
            // 
            // tap_function
            // 
            this.tap_function.Controls.Add(this.btn_production);
            this.tap_function.Controls.Add(this.btn_shipping);
            this.tap_function.Controls.Add(this.btn_pqmshow);
            this.tap_function.Location = new System.Drawing.Point(4, 29);
            this.tap_function.Margin = new System.Windows.Forms.Padding(4);
            this.tap_function.Name = "tap_function";
            this.tap_function.Size = new System.Drawing.Size(1005, 553);
            this.tap_function.TabIndex = 2;
            this.tap_function.Text = "Function";
            this.tap_function.UseVisualStyleBackColor = true;
            // 
            // btn_production
            // 
            this.btn_production.Enabled = false;
            this.btn_production.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_production.Location = new System.Drawing.Point(712, 35);
            this.btn_production.Margin = new System.Windows.Forms.Padding(4);
            this.btn_production.Name = "btn_production";
            this.btn_production.Size = new System.Drawing.Size(197, 63);
            this.btn_production.TabIndex = 9;
            this.btn_production.Text = "Production Report";
            this.btn_production.UseVisualStyleBackColor = true;
            this.btn_production.Click += new System.EventHandler(this.Btn_production_Click);
            // 
            // btn_shipping
            // 
            this.btn_shipping.Enabled = false;
            this.btn_shipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_shipping.Location = new System.Drawing.Point(372, 35);
            this.btn_shipping.Margin = new System.Windows.Forms.Padding(4);
            this.btn_shipping.Name = "btn_shipping";
            this.btn_shipping.Size = new System.Drawing.Size(197, 63);
            this.btn_shipping.TabIndex = 6;
            this.btn_shipping.Text = "Shipping Report";
            this.btn_shipping.UseVisualStyleBackColor = true;
            this.btn_shipping.Click += new System.EventHandler(this.Btn_OrderReport_Click);
            // 
            // btn_pqmshow
            // 
            this.btn_pqmshow.Enabled = false;
            this.btn_pqmshow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pqmshow.Location = new System.Drawing.Point(31, 35);
            this.btn_pqmshow.Margin = new System.Windows.Forms.Padding(4);
            this.btn_pqmshow.Name = "btn_pqmshow";
            this.btn_pqmshow.Size = new System.Drawing.Size(197, 63);
            this.btn_pqmshow.TabIndex = 5;
            this.btn_pqmshow.Text = "PQM Data";
            this.btn_pqmshow.UseVisualStyleBackColor = true;
            this.btn_pqmshow.Click += new System.EventHandler(this.btn_pqmshow_Click);
            // 
            // MainFr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 646);
            this.Controls.Add(this.tpc_main);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainFr";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.MainFr_Load);
            this.Controls.SetChildIndex(this.tpc_main, 0);
            this.tpc_main.ResumeLayout(false);
            this.tap_setting.ResumeLayout(false);
            this.tap_local.ResumeLayout(false);
            this.tap_function.ResumeLayout(false);
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
        private System.Windows.Forms.Button btn_dept;
        private System.Windows.Forms.TabPage tap_function;
        private System.Windows.Forms.Button btn_pqmshow;
        private System.Windows.Forms.Button btn_process;
        private System.Windows.Forms.Button btn_shipping;
        private System.Windows.Forms.Button btn_order_pdc;
        private System.Windows.Forms.Button btn_emailconfig;
        private System.Windows.Forms.Button btn_production;
    }
}