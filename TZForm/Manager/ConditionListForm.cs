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
    public partial class ConditionListForm : Form
    {
        public ConditionListForm()
        {
            InitializeComponent();
        }

        private void BT_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConditionListForm_Load(object sender, EventArgs e)
        {
            ListCondition();
        }

        #region 列出列表
        private void ListCondition(string key = "")
        {
            List<DataLibrary.Condition> conditions = OperationLibrary.Globe.UserSettings.Conditions.Values.Where(p=>p.Name.Contains(key) || p.Formula.Contains(key)).OrderBy(p => p.Name).ToList();

            LV.Items.Clear();

            foreach (DataLibrary.Condition condition in conditions)
                AddToList(condition);
        }

        private ListViewItem AddToList(DataLibrary.Condition condition)
        {
            ListViewItem item = new ListViewItem(condition.Name);
            item.SubItems.Add(condition.Formula);
            item.Tag = condition;
            LV.Items.Add(item);
            return item;
        }

        private void BT_Search_Click(object sender, EventArgs e)
        {
            ListCondition(TB_Key.Text.Trim());
        }

        private void TB_Key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ListCondition(TB_Key.Text.Trim());
        }

        #endregion

        #region 编辑
        private void BT_New_Click(object sender, EventArgs e)
        {
            ConditionEditForm form = new ConditionEditForm();
            if (form.ShowDialog(this) != DialogResult.OK) return ;

            ListViewItem item =  AddToList(form.condition);
            item.Selected = true;

            LV.Focus();
        }

        private void BT_Edit_Click(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count <= 0)
                return;

            ListViewItem item = LV.SelectedItems[0];
            ConditionEditForm form = new ConditionEditForm((DataLibrary.Condition)item.Tag);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            item.Text = form.condition.Name;
            item.SubItems[1].Text = form.condition.Formula;
            item.Selected = true;

            LV.Focus();
        }

        private void BT_Delete_Click(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count <= 0)
                return;

            ListViewItem item = LV.SelectedItems[0];
            DataLibrary.Condition p = (DataLibrary.Condition)item.Tag;

            if (MessageBox.Show("删除条件可能导致现有策略无法正常运行, 请确认是否删除?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            #region 验证用户
            string rt = Globe.GET<string>("tz", "deletecondition", p.UUID);
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

            Globe.UserSettings.Conditions.Remove(p.UUID);

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
