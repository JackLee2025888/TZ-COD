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
    public partial class ConditionEditForm : Form
    {

        public DataLibrary.Condition condition = null;
        public ConditionEditForm(DataLibrary.Condition condition = null)
        {
            this.condition = condition;
            if (this.condition == null)
                this.condition = new DataLibrary.Condition() { UUID = Guid.NewGuid().ToString("N") };
            InitializeComponent();
        }

        private void BT_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BT_Path_Click(object sender, EventArgs e)
        {
            //TB_Formula.Text = FB.SelectedPath;
            //TB_Formula.Focus();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            TB_Name.Text = condition.Name;
            TB_Formula.Text = condition.Formula;
            TB_Name.Focus();
        }


        private void BT_OK_Click(object sender, EventArgs e)
        {
            #region 验证输入
            if (string.IsNullOrEmpty(TB_Name.Text.Trim()))
            {
                MessageBox.Show("请输入条件名称!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }

            if (string.IsNullOrEmpty(TB_Formula.Text.Trim()))
            {
                MessageBox.Show("请输入条件公式!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Formula.Focus();
                return;
            }

            //验证重名
            if (Globe.UserSettings.Conditions.Keys.Contains(TB_Name.Text.Trim()) && TB_Name.Text.Trim() != condition.Name)
            {
                MessageBox.Show("已存在同名条件, 请重新输入!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Formula.Focus();
                return;
            }

            //验证公式
            OperationLibrary.Formula.Formula formula = new OperationLibrary.Formula.Formula.FormulaReader().Read(TB_Formula.Text.Trim());

            if (formula == null)
            {
                MessageBox.Show("请输入正确的条件公式!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Formula.Focus();
                return;
            }

            #endregion

            #region 验证用户码
            string rt = Globe.POST<string>(new DataLibrary.Condition()
            {
                UUID = condition.UUID,
                Name = TB_Name.Text.Trim(),
                Formula = TB_Formula.Text.Trim(),
                isLC = formula.isLC
            }, "tz", "editcondition");
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



            if (!Globe.UserSettings.Conditions.ContainsKey(condition.UUID))
                Globe.UserSettings.Conditions.Add(condition.UUID, condition);

            condition.Name = TB_Name.Text.Trim();
            condition.Formula = TB_Formula.Text.Trim();
            condition.isLC = formula.isLC;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
