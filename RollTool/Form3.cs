using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RollTool
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Thanks_TextChanged(object sender, EventArgs e)
        {

        }
        /***protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (txtID.Text != "" || txtPassword.Text != "")
            {
                base.OnFormClosing(e);
                if (e.CloseReason == CloseReason.WindowsShutDown
                    || e.CloseReason == CloseReason.ApplicationExitCall)
                    return;

                // Confirm user wants to close
                using (var closeForm = new formConfirmExit())
                {
                    var result = closeForm.ShowDialog();
                    if (result == DialogResult.No)
                        e.Cancel = true;
                }
            }
        }***/
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
