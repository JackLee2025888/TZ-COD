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
    public partial class SystemParameterSettingListForm : Form
    {
        public SystemParameterSettingListForm()
        {
            InitializeComponent();
        }

        private void BT_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Load(object sender, EventArgs e)
        { 
            ListSystemParameterSetting();
        }

        #region 列出列表
        private void ListSystemParameterSetting(string key = "")
        {
            List<DataLibrary.SystemParameterSetting> parameters = Globe.UserSettings.SystemParameterSettings.Values.Where(p=>p.Name.Contains(key) || p.Type.Contains(key)).OrderBy(p => p.Name).OrderBy(p=>p.Type).ToList();

            LV.Items.Clear();

            foreach (DataLibrary.SystemParameterSetting parameter in parameters)
                AddToList(parameter);
        }

        private ListViewItem AddToList(DataLibrary.SystemParameterSetting parameter)
        {
            ListViewItem item = new ListViewItem(parameter.Name);
            item.SubItems.Add(parameter.Type);
            item.SubItems.Add(string.Join(", ", parameter.Settings));
            item.Tag = parameter;
            LV.Items.Add(item);
            return item;
        }

        private void BT_Search_Click(object sender, EventArgs e)
        {
            ListSystemParameterSetting(TB_Key.Text.Trim());
        }

        private void TB_Key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ListSystemParameterSetting(TB_Key.Text.Trim());
        }

        #endregion

        #region 编辑
        private void BT_New_Click(object sender, EventArgs e)
        {
            SystemParameterSettingEditForm form = new SystemParameterSettingEditForm();
            if (form.ShowDialog(this) != DialogResult.OK) return ;
            Globe.UserSettings.SystemParameterSettings.Add(form.parameter.Name, form.parameter);
            ListViewItem item =  AddToList(form.parameter);
            item.Selected = true;

            LV.Focus();
        }

        private void BT_Edit_Click(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count <= 0)
                return;

            ListViewItem item = LV.SelectedItems[0];
            SystemParameterSettingEditForm form = new SystemParameterSettingEditForm((DataLibrary.SystemParameterSetting)item.Tag);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            item.Text = form.parameter.Name;
            item.SubItems[1].Text = form.parameter.Type;
            item.SubItems[2].Text = string.Join(", ", form.parameter.Settings);
            item.Selected = true;

            LV.Focus();
        }

        private void BT_Delete_Click(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count <= 0)
                return;

            ListViewItem item = LV.SelectedItems[0];
            DataLibrary.SystemParameterSetting p = (DataLibrary.SystemParameterSetting)item.Tag;

            if (MessageBox.Show("删除指标可能导致现有保存的公式无法正常运行, 请确认是否删除?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            #region 验证用户
            string rt = Globe.GET<string>("tz", "deletesystemsetting", p.UUID);
            if (rt == "err")
            {
                MessageBox.Show("删除错误, 请重新打开设置界面!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LV.Focus();
                return;
            }
            else if (rt != "ok")
            {
                MessageBox.Show("用户码或服务器错误, 请使用 设置>系统设置 对用户码进行正确设置!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LV.Focus();
                return;
            }

            #endregion 
            Globe.UserSettings.SystemParameterSettings.Remove(p.Name);

            int index = item.Index;
            LV.Items.Remove(item);
            if (LV.Items.Count - 1 < index)
                index--;
            if (index >= 0)
                LV.Items[index].Selected = true;

            LV.Focus();
        }

        #endregion

        private void LV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count >0)
            {
                BT_Delete.Enabled = true;
                BT_Edit.Enabled = true;
            }
            else
            {
                BT_Delete.Enabled = false;
                BT_Edit.Enabled = false;
            }
        }

        private void LV_DoubleClick(object sender, EventArgs e)
        {
            BT_Edit_Click(sender, e);

        }
    }
}
