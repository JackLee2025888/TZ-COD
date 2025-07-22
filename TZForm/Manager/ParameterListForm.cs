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
    public partial class ParameterListForm : Form
    {
        public ParameterListForm()
        {
            InitializeComponent();
        }

        private void BT_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ParameterListForm_Load(object sender, EventArgs e)
        {
            ListParameter();
        }

        #region 列出列表
        private void ListParameter(string key = "")
        {
            List<DataLibrary.Parameter> parameters = OperationLibrary.Globe.UserSettings.SavedParameters.Values.Where(p=>p.Name.Contains(key) || p.Discription.Contains(key) || p.FormulaString.Contains(key)).OrderBy(p => p.Name).ToList();

            LV.Items.Clear();

            foreach (DataLibrary.Parameter parameter in parameters)
                AddToList(parameter);
        }

        private ListViewItem AddToList(DataLibrary.Parameter parameter)
        {
            ListViewItem item = new ListViewItem(parameter.Name);
            item.SubItems.Add(parameter.Discription);
            item.SubItems.Add(parameter.FormulaString != "" ? parameter.FormulaString : parameter.value.ToString());
            item.Tag = parameter;
            LV.Items.Add(item);
            return item;
        }

        private void BT_Search_Click(object sender, EventArgs e)
        {
            ListParameter(TB_Key.Text.Trim());
        }

        private void TB_Key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ListParameter(TB_Key.Text.Trim());
        }

        #endregion

        #region 编辑
        private void BT_New_Click(object sender, EventArgs e)
        {
            ParameterEditForm form = new ParameterEditForm();
            if (form.ShowDialog(this) != DialogResult.OK) return ;
            Globe.UserSettings.SavedParameters.Add(form.parameter.Name, form.parameter);
            ListViewItem item =  AddToList(form.parameter);
            item.Selected = true;

            LV.Focus();
        }

        private void BT_Edit_Click(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count <= 0)
                return;

            ListViewItem item = LV.SelectedItems[0];
            ParameterEditForm form = new ParameterEditForm((DataLibrary.Parameter)item.Tag);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            item.Text = form.parameter.Name;
            item.SubItems[1].Text = form.parameter.Discription;
            item.SubItems[2].Text = form.parameter.FormulaString != "" ? form.parameter.FormulaString : form.parameter.value.ToString();
            item.Selected = true;

            LV.Focus();
        }

        private void BT_Delete_Click(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count <= 0)
                return;

            ListViewItem item = LV.SelectedItems[0];
            DataLibrary.Parameter p = (DataLibrary.Parameter)item.Tag;

            if (MessageBox.Show("删除自定义参数可能导致现有保存的公式无法正常运行, 请确认是否删除?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            #region 验证用户
            string rt = Globe.GET<string>("tz", "deleteparameter", p.UUID);
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

            Globe.UserSettings.SavedParameters.Remove(p.Name);

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
