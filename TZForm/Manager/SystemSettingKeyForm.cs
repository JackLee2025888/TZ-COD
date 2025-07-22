using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZ.OperationLibrary;

namespace TZ.TZForm
{
    public partial class SystemSettingKeyForm : Form
    {

       public SystemSettingKeyForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SystemSettingKeyForm_Load(object sender, EventArgs e)
        {
            TB_Key.Text = Globe.SystemID;
            TB_Key.Focus();
        }
    }
}
