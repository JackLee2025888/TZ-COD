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
    public partial class GroupEditForm : Form
    {
        public DataLibrary.Policy.ConditionGroup group = null;
        private DataLibrary.Policy policy = null;
        List<string> uuids = null;
        int type = 0;
        bool isNew = false;
        int index = 0;

        List<DataLibrary.Policy.ConditionGroup> groups = null;
        public GroupEditForm(DataLibrary.Policy policy, bool isNew = false, int type = 0, int index = -1)
        {
            this.policy = policy;
            this.type = type;
            this.isNew = isNew;
            this.index = index;

            groups = type == 0 ? policy.BuyGroup : policy.SaleGroup;


            if (this.index < 0 || this.index > groups.Count)
                this.group = new DataLibrary.Policy.ConditionGroup()
                {
                    Name = "",
                    Conditions = new List<string>(),
                    DoCondition = "0",
                    GroupType = 0
                };
            else
                this.group = groups[index];


            InitializeComponent();
        }

        private void BT_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {

            TB_Type.Text = type == 0 ? "买入策略组" : "卖出策略组";
            TB_Name.Text = group.Name;
            TB_Formula.Text = group.DoCondition == "0" ? "" : group.DoCondition;
            TB_All.Checked = group.DoCondition == "0";
            TB_Logic.SelectedIndex = group.GroupType == 0 ? 0 : 1;
            ListCondition();
        }

        #region 列出列表
        private void ListCondition()
        {

            List<DataLibrary.Condition> conditions = Globe.UserSettings.Conditions.Values.Where(p => group.Conditions.Contains(p.UUID)).ToList();


            uuids = conditions.Select(p=>p.UUID).ToList();

            LV.Items.Clear();

            foreach (DataLibrary.Condition condition in conditions)
                AddToList(condition);
        }

        private ListViewItem AddToList(DataLibrary.Condition condition)
        {
            ListViewItem item = new ListViewItem(condition.Name);
            item.SubItems.Add(condition.Formula);
            item.Tag = condition.UUID;
            LV.Items.Add(item);
            return item;
        }

        #endregion

        #region 编辑
        private void BT_New_Click(object sender, EventArgs e)
        {
            ConditionSelectForm form = new ConditionSelectForm(uuids);
            if (form.ShowDialog(this) != DialogResult.OK) return ;

            uuids.Add(form.condition.UUID);
            ListViewItem item =  AddToList(form.condition);
            item.Selected = true;

            LV.Focus();
        }


        private void BT_Delete_Click(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count <= 0)
                return;

            ListViewItem item = LV.SelectedItems[0];
            string uuid = (string)item.Tag;

            uuids.Remove(uuid);

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
                BT_Delete.Enabled = true;
            else
                BT_Delete.Enabled = false;
        }

        private void TB_All_CheckedChanged(object sender, EventArgs e)
        {
            if (TB_All.Checked)
                TB_Formula.Enabled = false;
            else
                TB_Formula.Enabled = true;
        }

        private void BT_OK_Click(object sender, EventArgs e)
        {
            #region 验证输入
            if (string.IsNullOrEmpty(TB_Name.Text.Trim()))
            {
                MessageBox.Show("请输入条件组名称!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }

            if (!TB_All.Checked && string.IsNullOrEmpty(TB_Formula.Text.Trim()))
            {
                MessageBox.Show("请输入触发条件!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Formula.Focus();
                return;
            }

            //验证重名
            if (uuids.Count <= 0)
            {
                MessageBox.Show("请至少选择一个条件!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LV.Focus();
                return;
            }

            //验公式

            if (!TB_All.Checked && new OperationLibrary.Formula.Formula.FormulaReader().Read(TB_Formula.Text.Trim()) == null)
            {
                MessageBox.Show("请输入正确的触发条件公式!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Formula.Focus();
                return;
            }

            #endregion

            #region 验证用户码
            DataLibrary.Policy newPolicy = new DataLibrary.Policy()
            {
                UUID = policy.UUID,
                BuyGroup = new List<DataLibrary.Policy.ConditionGroup>(policy.BuyGroup),
                SaleGroup = new List<DataLibrary.Policy.ConditionGroup>(policy.SaleGroup)
            };

            DataLibrary.Policy.ConditionGroup newgroup = new DataLibrary.Policy.ConditionGroup()
            {
                Conditions = uuids,
                Name = TB_Name.Text.Trim(),
                GroupType = TB_Logic.SelectedIndex,
                DoCondition = TB_All.Checked ? "0" : TB_Formula.Text.Trim()
            };

            if (isNew)
            {
                if (type == 0)
                    newPolicy.BuyGroup.Add(newgroup);
                else
                    newPolicy.SaleGroup.Add(newgroup);
            }
            else
            {
                if (type == 0)
                    newPolicy.BuyGroup[index] = newgroup;
                else
                    newPolicy.SaleGroup[index] = newgroup;
            }

            string rt = Globe.POST<string>(newPolicy, "tz", "editGroup");
            if (rt == "err")
            {
                MessageBox.Show("设置错误, 请重新打开设置界面!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }
            else if (rt != "ok")
            {
                MessageBox.Show("用户码或服务器错误, 请使用 设置>系统设置 对用户码进行正确设置!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }
            #endregion


            group.Name = TB_Name.Text.Trim();
            group.GroupType = TB_Logic.SelectedIndex;
            group.DoCondition = TB_All.Checked ? "0" : TB_Formula.Text.Trim();
            group.Conditions = uuids;

            if (isNew)
                groups.Add(group);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
