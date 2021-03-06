﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricDb
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.Visible=false;
            EmployeeAdmin employeeForm = new EmployeeAdmin();
            employeeForm.Bounds = this.Bounds;
            employeeForm.ShowDialog();
           this.Visible=true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Terminal accessForm = new Terminal();
            accessForm.ShowDialog();
            this.Visible = true;

            string checkRestartFromTerminal = Terminal.restart;

            if (checkRestartFromTerminal == "1")
            {
                this.Visible = false;
                Terminal accessFormRestart = new Terminal();
                accessFormRestart.ShowDialog();
                this.Visible = true;
            }

        }

        private void button_3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            LocationAdmin locationForm = new LocationAdmin();
            locationForm.ShowDialog();
            this.Visible = true;
        }
    }
}
