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
    public partial class PolicyEditForm : Form
    {

        public DataLibrary.Policy policy = null;
        
        public PolicyEditForm(DataLibrary.Policy policy = null)
        {
            this.policy = policy;
            if (this.policy == null)
                this.policy = new DataLibrary.Policy()
                {
                    UUID = Guid.NewGuid().ToString("N"),
                    Name = "",
                    BuyGroup = new List<DataLibrary.Policy.ConditionGroup>(),
                    SaleGroup = new List<DataLibrary.Policy.ConditionGroup>()
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

        private void Form_Load(object sender, EventArgs e)
        {
            TB_Name.Text = policy.Name;
            TB_Name.Focus();
        }


        private void BT_OK_Click(object sender, EventArgs e)
        {
            #region 验证输入
            if (string.IsNullOrEmpty(TB_Name.Text.Trim()))
            {
                MessageBox.Show("请输入策略名称!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }

            //验证重名
            if (Globe.UserSettings.Policies.Values.Select(p=>p.Name).Contains(TB_Name.Text.Trim()) && TB_Name.Text.Trim() != policy.Name)
            {
                MessageBox.Show("已存在同名策略, 请重新输入!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_Name.Focus();
                return;
            }


            #endregion

            #region 验证用户码
            string rt = Globe.POST<string>(new DataLibrary.Policy()
            {
                UUID = policy.UUID,
                Name = TB_Name.Text.Trim()
            }, "tz", "editpolicy");
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



            if (!Globe.UserSettings.Policies.ContainsKey(policy.UUID))
                Globe.UserSettings.Policies.Add(policy.UUID, policy);

            policy.Name = TB_Name.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
