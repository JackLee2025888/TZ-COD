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

namespace TZ.TZForm
{
    public partial class SystemSettingForm : Form
    {
        public SystemSettingForm()
        {
            InitializeComponent();
        }

        

        private void BT_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BT_Path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FB = new FolderBrowserDialog();
            FB.Description = "选择数据文件位置...";
            if (FB.ShowDialog(this) != DialogResult.OK) return;

            TB_Path.Text = FB.SelectedPath;
            TB_Path.Focus();
        }

        private void SystemSettingForm_Load(object sender, EventArgs e)
        {
            TB_Server.Text = Globe.SystemSettings.Server;
            TB_Key.Text = Globe.SystemSettings.SecretKey;
            TB_Path.Text = Globe.SystemSettings.DataPath;
            TB_Server.Focus();
        }

        private void BT_Key_Click(object sender, EventArgs e)
        {
            SystemSettingKeyForm form = new SystemSettingKeyForm();
            form.ShowDialog(this);
        }



        private void BT_OK_Click(object sender, EventArgs e)
        {
            #region 验证输入
            if (string.IsNullOrEmpty(TB_Server.Text.Trim()))
            {
                MessageBox.Show("请输入服务器地址!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Server.Focus();
                return;
            }

            if (string.IsNullOrEmpty(TB_Key.Text.Trim()))
            {
                MessageBox.Show("请输入用户码!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Key.Focus();
                return;
            }

            if (string.IsNullOrEmpty(TB_Path.Text.Trim()))
            {
                MessageBox.Show("请输入数据位置!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Path.Focus();
                return;
            }
            #endregion

            #region 验证用户码
            string oldServer = Globe.SystemSettings.Server;
            Globe.SystemSettings.Server = TB_Server.Text.Trim();

            DataLibrary.UserSetting setting = Globe.POST<DataLibrary.UserSetting>(new DataLibrary.UserSetting()
            {
                UUID = TB_Key.Text.Trim(),
                PCID = Globe.SystemID
            }, "user", "login");
            if (setting == null)
            {
                Globe.SystemSettings.Server = oldServer;
                MessageBox.Show("服务器地址或用户码输入错误, 请重新输入!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Key.Focus();
                return;
            }

            #endregion 

            Globe.SystemSettings.DataPath = TB_Path.Text.Trim();
            Globe.SystemSettings.SecretKey = TB_Key.Text.Trim();
            Globe.UserSettings = setting;
            Globe.SaveFile(Path.Combine(Globe.LoadPath, "setting.dat"), Globe.SystemSettings);


            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
