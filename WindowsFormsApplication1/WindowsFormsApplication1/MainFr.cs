﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.SettingForm;

namespace WindowsFormsApplication1
{
    public partial class MainFr : CommonForm
    {
        public MainFr()
        {
            InitializeComponent();

        }
      
        private void btn_registeruser_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.SettingForm.RegisterUsesForm.RegisterUserForm fr = new SettingForm.RegisterUsesForm.RegisterUserForm();
            fr.Show();
        }
        private void MainFr_Load(object sender, EventArgs e)
        {
            sqlCON connect = new sqlCON();
            connect.sqlDatatablePermision("RegisterUser", btn_registeruser);
            connect.sqlDatatablePermision("Password", btn_changepass);
            connect.sqlDatatablePermision("PermissionForm", btn_permission);
            connect.sqlDatatablePermision("LineMaster", btn_line);
            connect.sqlDatatablePermision("ModelMaster", btn_model);

        }

        private void btn_changepass_Click(object sender, EventArgs e)
        {
            SettingForm.Change_Password passform = new Change_Password();
            passform.Show();

        }

        private void btn_permission_Click(object sender, EventArgs e)
        {
            PermissionForm fr = new PermissionForm();
            fr.ShowDialog();
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            SettingForm.LineForm.LineForm fr = new SettingForm.LineForm.LineForm();
            fr.Show();
        }

        private void btn_modelmaster_Click(object sender, EventArgs e)
        {
            SettingForm.ModelForm.ModelForm fre = new SettingForm.ModelForm.ModelForm();
            fre.Show();
        }
    }
}
