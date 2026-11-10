using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppTest1
{
    public partial class PasswordForm : Form
    {
        private const int VK_RETURN = 13;

        public PasswordForm()
        {
            InitializeComponent();
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            Counter.passwordMatch = false;

            label1.Text = "Probe counter reaches the max number.\nPlease enter password to reset:";
        }

        private void textPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != VK_RETURN || textPassword.Text.Length == 0)
                return;

            buttonOK.PerformClick();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Counter.passwordMatch = false;
            if (textPassword.Text == Counter.resetPassword)
            {
                Counter.passwordMatch = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(null, "Password not correct!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textPassword.Clear();
                textPassword.Focus();
            }
        }
    }
}
