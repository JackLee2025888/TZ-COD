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
    public partial class ConditionSelectForm : Form
    {

        public DataLibrary.Condition condition = null;

        List<string> uuids = null;

        public ConditionSelectForm(List<string> uuids = null)
        {
            this.uuids = uuids;
            if (this.uuids == null)
                uuids = new List<string>();
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
            List<DataLibrary.Condition> conditions = Globe.UserSettings.Conditions.Values.Where(p=>p.Name.Contains(key) || p.Formula.Contains(key)).OrderBy(p => p.Name).ToList();

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


        private void LV_DoubleClick(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count <= 0)
                return;

                button1_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(LV.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择需要添加的条件!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            condition = (DataLibrary.Condition)LV.SelectedItems[0].Tag;

            if (uuids.Contains(condition.UUID))
            {
                MessageBox.Show("该条件已存在!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
