using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using TZ.OperationLibrary;
using System.IO;
using System.Text.RegularExpressions;

namespace TZ.TZForm
{
    public partial class StartSettingForm : Form
    {

        public DataLibrary.RunSetting setting = null;

        bool isDevire = false;
        public StartSettingForm(DataLibrary.RunSetting setting = null, bool isDevire = false)
        {
            this.isDevire = isDevire;
            this.setting = setting;
            if (this.setting == null)
                this.setting = new DataLibrary.RunSetting()
                {
                    BeginDate = new DateTime(2021, 1, 1),
                    EndDate = new DateTime(2021, 12, 31),
                    is00 = true,
                    is30 = false,
                    is60 = true,
                    is68 = false,
                    isCST = false,
                    CST = new List<string>(),
                    isST = false,
                    isProcess = true,
                    Process = 20,
                    Indicator1 = new List<string>(),
                    Indicator2 = new List<string>()

                };
            InitializeComponent();
        }

        private void BT_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void Form_Load(object sender, EventArgs e)
        {
            TB_Begin.Value = setting.BeginDate.Date;
            TB_End.Value = setting.EndDate.Date;
            TB_Process.Text = setting.Process.ToString();
            TB_CST.Text = string.Join(", ", setting.CST);

            CB_00.Checked = setting.is00;
            CB_30.Checked = setting.is30;
            CB_60.Checked = setting.is60;
            CB_68.Checked = setting.is68;
            CB_ST.Checked = setting.isST;
            CB_CST.Checked = setting.isCST;
            CB_Process.Checked = setting.isProcess;

            TB_Path.Text = setting.outPath;

            TB_Indicator1.Text = string.Join(", ", setting.Indicator1);
            TB_Indicator2.Text = string.Join(", ", setting.Indicator2);


            if (isDevire)
            {
                TB_DeriveAddress.Text = setting.DeriveAddress;
                TB_DevireKey.Text = setting.DeriveKey;
                CB_Year.Checked = setting.DeriveYear;
                LB_DeriveAddress.Visible = true;
                LB_DevireKey.Visible = true;
                TB_DeriveAddress.Visible = true;
                TB_DevireKey.Visible = true;
                CB_Year.Visible = true;
                this.Height =(int) (528*1.25);
            }
            else
            {
                LB_DeriveAddress.Visible = false;
                LB_DevireKey.Visible = false;
                TB_DeriveAddress.Visible = false;
                TB_DevireKey.Visible = false;
                CB_Year.Visible = false;
                this.Height =(int) (468*1.25);
            }


            TB_CST.Focus();
        }


        private void BT_OK_Click(object sender, EventArgs e)
        {
            #region 验证输入
            if (TB_Begin.Value.Date >= TB_End.Value.Date)
            {
                MessageBox.Show("起始时间必须小于结束时间!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_End.Focus();
                return;
            }
            string[] c = TB_CST.Text.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
            List<string> cst = c.Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p)).ToList();
            if (CB_CST.Checked && cst.Count <= 0)
            {
                MessageBox.Show("请输入自定义模拟代码!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_CST.Focus();
                return;
            }

            int v = 20;
            if (!int.TryParse(TB_Process.Text.Trim(), out v))
            {
                if (CB_Process.Checked)
                {
                    MessageBox.Show("线程数必须为数字!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TB_Process.Focus();
                    return;
                }
                v = 20;
            }
            if (CB_Process.Checked && v <= 0)
            {
                MessageBox.Show("线程数必须大于0!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Process.Focus();
                return;
            }

            if (!Directory.Exists(TB_Path.Text.Trim()))
            {
                MessageBox.Show("请选择正确的输出路径!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Path.Focus();
                return;
            }

            if (isDevire)
            {
                if (string.IsNullOrEmpty(TB_DeriveAddress.Text.Trim()))
                {
                    MessageBox.Show("请输入衍生引擎地址!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TB_DeriveAddress.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(TB_DevireKey.Text.Trim()))
                {
                    MessageBox.Show("请输入衍生计算Key!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TB_DevireKey.Focus();
                    return;
                }
            }


            string[] i1 = TB_Indicator1.Text.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
            string[] i2 = TB_Indicator2.Text.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
            List<string> ind1 = i1.Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p)).ToList();
            List<string> ind2 = i2.Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p)).ToList();

            #endregion


            setting = new DataLibrary.RunSetting()
            {
                BeginDate = TB_Begin.Value.Date,
                EndDate = TB_End.Value.Date,
                is00 = CB_00.Checked,
                is30 = CB_30.Checked,
                is60 = CB_60.Checked,
                is68 = CB_68.Checked,
                isCST = CB_CST.Checked,
                CST = cst,
                isProcess = CB_Process.Checked,
                Process = v,
                isST = CB_ST.Checked,
                Indicator1 = ind1,
                Indicator2 = ind2,
                outPath = TB_Path.Text.Trim(),

                DeriveAddress = TB_DeriveAddress.Text.Trim(),
                DeriveKey = TB_DevireKey.Text.Trim(),
                DeriveYear = CB_Year.Checked
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CB_CST_CheckedChanged(object sender, EventArgs e)
        {
            CB_00.Enabled = !CB_CST.Checked;
            CB_60.Enabled = !CB_CST.Checked;
            CB_30.Enabled = !CB_CST.Checked;
            CB_68.Enabled = !CB_CST.Checked;
            CB_ST.Enabled = !CB_CST.Checked;
            TB_CST.Enabled = CB_CST.Checked;
        }

        private void CB_Process_CheckedChanged(object sender, EventArgs e)
        {
            TB_Process.Enabled = CB_Process.Checked;
            TB_Process_LB.Enabled = CB_Process.Checked;
        }

        private void BT_Path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "选择输出路径...";
            fd.SelectedPath = TB_Path.Text.Trim();

            if (fd.ShowDialog(this) != DialogResult.OK) return;

            TB_Path.Text = fd.SelectedPath;
            TB_Path.Focus();
        }
    }
}
