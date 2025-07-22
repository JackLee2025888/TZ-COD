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
    public partial class SystemParameterSettingEditForm : Form
    {

        Regex NameRegex = new Regex(@"^[a-zA-Z][0-9a-zA-Z]+$");

        string[][] Indicators = new string[][]
        {
            new string[]{"MA", "N"},
            new string[]{"KDJ", "LONG, M1, M2"},
            new string[]{"MACD", "SHORT, LONG, MID"},
            new string[]{"RSI", "N"},
                new string[]{"ASI", "M, N" },

        };

        public DataLibrary.SystemParameterSetting parameter = null;
        public SystemParameterSettingEditForm(DataLibrary.SystemParameterSetting parameter = null)
        {
            this.parameter = parameter;
            if (this.parameter == null)
                this.parameter = new DataLibrary.SystemParameterSetting()
                {
                    UUID = Guid.NewGuid().ToString("N"),
                    Name = "",
                    Type = "MA",
                    Settings = new List<double>() { 5 }
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
            TB_Indicator.Items.Clear();
            foreach (string[] indicator in Indicators)
            {
                TB_Indicator.Items.Add(indicator[0]);
                if (indicator[0] == parameter.Type)
                    TB_Indicator.SelectedItem = TB_Indicator.Items[TB_Indicator.Items.Count - 1];
            }


            TB_Name.Text = parameter.Name;
            TB_Parameter.Text = string.Join(", " , parameter.Settings);
            TB_Name.Focus();
        }


        private void BT_OK_Click(object sender, EventArgs e)
        {
            #region 验证输入
            if (string.IsNullOrEmpty(TB_Name.Text.Trim()))
            {
                MessageBox.Show("请输入自定义指标名称!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }

            if (!NameRegex.IsMatch(TB_Name.Text.Trim()))
            {
                MessageBox.Show("指标名称只能包含大小写字母及数字!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }

            if (TB_Indicator.SelectedItem== null)
            {
                MessageBox.Show("请选择指标类型!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Indicator.Focus();
                return;
            }

            if (string.IsNullOrEmpty(TB_Parameter.Text.Trim()))
            {
                MessageBox.Show("请输入指标参数!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Parameter.Focus();
                return;
            }

            //验证重名
            if (Globe.UserSettings.SystemParameterSettings.Keys.Contains(TB_Name.Text.Trim()) && TB_Name.Text.Trim() != parameter.Name)
            {
                MessageBox.Show("已存在同名指标名称, 请重新输入!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }

            //验证值与公式
            string[] ps = TB_Parameter.Text.Trim().Split(",");
            List<double> paras = new List<double>();
            foreach(string p in ps)
            {
                double v;
                if (double.TryParse(p.Trim(), out v))
                    paras.Add(v);
            }

            if (paras.Count != Indicators[TB_Indicator.SelectedIndex][1].Split(",").Length)
            {
                MessageBox.Show("请输入正确数量的参数值!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Parameter.Focus();
                return;
            }

            #endregion

            #region 验证用户码
            string rt = Globe.POST<string>(new DataLibrary.SystemParameterSetting()
            {
                UUID = parameter.UUID,
                Name = TB_Name.Text.Trim(),
                Settings = new List<double>(paras),
                Type = TB_Indicator.SelectedItem.ToString()
            }, "tz", "editsystemsetting");
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

            if (Globe.UserSettings.SystemParameterSettings.ContainsKey(parameter.Name) && TB_Name.Text.Trim() != parameter.Name)
            {
                Globe.UserSettings.SystemParameterSettings.Remove(parameter.Name);
                Globe.UserSettings.SystemParameterSettings.Add(TB_Name.Text.Trim(), parameter);
            }
            parameter.Name = TB_Name.Text.Trim();
            parameter.Type = TB_Indicator.SelectedItem.ToString();
            parameter.Settings = paras;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TB_Indicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            TB_Tip.Text = Indicators[TB_Indicator.SelectedIndex][1];

        }
    }
}
