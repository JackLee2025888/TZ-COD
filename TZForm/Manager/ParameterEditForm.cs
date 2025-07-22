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
    public partial class ParameterEditForm : Form
    {

        Regex NameRegex = new Regex(@"^[a-zA-Z][0-9a-zA-Z]+$");



        public DataLibrary.Parameter parameter = null;
        public ParameterEditForm(DataLibrary.Parameter parameter = null)
        {
            this.parameter = parameter;
            if (this.parameter == null)
                this.parameter = new DataLibrary.Parameter()
                {
                    UUID = Guid.NewGuid().ToString("N"),
                    Name = "",
                    Discription = "",
                    value = 0
                };
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

        private void SystemSettingForm_Load(object sender, EventArgs e)
        {
            TB_Name.Text = parameter.Name;
            TB_Discription.Text = parameter.Discription;
            TB_Formula.Text = parameter.FormulaString != "" ? parameter.FormulaString : parameter.value.ToString();
            TB_Name.Focus();
        }


        private void BT_OK_Click(object sender, EventArgs e)
        {
            #region 验证输入
            if (string.IsNullOrEmpty(TB_Name.Text.Trim()))
            {
                MessageBox.Show("请输入参数名称!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }

            if (!NameRegex.IsMatch(TB_Name.Text.Trim()))
            {
                MessageBox.Show("参数名称只能包含大小写字母及数字!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }

            if (string.IsNullOrEmpty(TB_Discription.Text.Trim()))
            {
                MessageBox.Show("请输入参数描述!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Discription.Focus();
                return;
            }

            if (string.IsNullOrEmpty(TB_Formula.Text.Trim()))
            {
                MessageBox.Show("请输入参数公式/值!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Formula.Focus();
                return;
            }

            //验证重名
            if (Globe.UserSettings.SavedParameters.Keys.Contains(TB_Name.Text.Trim()) && TB_Name.Text.Trim() != parameter.Name)
            {
                MessageBox.Show("已存在同名参数, 请重新输入!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Formula.Focus();
                return;
            }

            //验证值与公式
            double value = 0;
            if (!double.TryParse(TB_Formula.Text.Trim(), out value)) //验证公式
            {
                value = double.NaN;
                OperationLibrary.Formula.Formula.FormulaReader reader = new OperationLibrary.Formula.Formula.FormulaReader();
                if (reader.Read(TB_Formula.Text.Trim()) == null)
                {
                    MessageBox.Show("请输入正确的参数公式/值!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TB_Formula.Focus();
                    return;
                }
            }

            #endregion

            #region 验证用户码
            string rt = Globe.POST<string>(new DataLibrary.Parameter()
            {
                UUID = parameter.UUID,
                Name = TB_Name.Text.Trim(),
                Discription = TB_Discription.Text.Trim(),
                FormulaString = double.IsNaN(value) ? TB_Formula.Text.Trim() : "",
                value = double.IsNaN(value) ? 0 : value
            }, "tz", "editparameter");
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

            if (Globe.UserSettings.SavedParameters.ContainsKey(parameter.Name) && parameter.Name != TB_Name.Text.Trim())
            {
                Globe.UserSettings.SavedParameters.Remove(parameter.Name);
                Globe.UserSettings.SavedParameters.Add(TB_Name.Text.Trim(), parameter);
            }
            parameter.Name = TB_Name.Text.Trim();
            parameter.Discription = TB_Discription.Text.Trim();
            parameter.value = value;
            parameter.FormulaString = double.IsNaN(value) ? TB_Formula.Text.Trim() : "";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
