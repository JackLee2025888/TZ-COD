using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZ.DataLibrary;
using TZ.OperationLibrary;

namespace TZ.TZForm
{
    public partial class MainForm : Form
    {


        public MainForm()
        {
            Globe.SystemID = Globe.GetSystemID();
            Globe.SystemSettings = Globe.LoadFile<SystemSetting>(Path.Combine(Globe.LoadPath, "setting.dat"));
            if (Globe.SystemSettings == null)
                Globe.SystemSettings = new SystemSetting();
            InitializeComponent();
        }

        private void ShowUserKeyErr()
        {
            MessageBox.Show("验证用户码失败, 请使用 设置>系统设置 对用户码进行正确设置!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            #region 验证用户Key
            Globe.UserSettings = Globe.POST<UserSetting>(new UserSetting()
            {
                UUID = Globe.SystemSettings.SecretKey,
                PCID = Globe.SystemID
            }, "user", "login");
            #endregion

            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }


            LoadData();
        }


        private void LoadData()
        {
            Globe.UserSettings.LoadData();
            treeView1.Nodes[0].Nodes.Clear();
            foreach (Policy p in Globe.UserSettings.Policies.Values)
                AddtoPolicy(p);

            treeView1.Nodes[0].Expand();
            if (treeView1.Nodes[0].Nodes.Count > 0)
                treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];

            ListRunSetting();
        }

        #region 设置按钮
        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemSettingForm form = new SystemSettingForm();
            if (form.ShowDialog(this) == DialogResult.OK)
                LoadData();
        }

        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }
            ParameterListForm form = new ParameterListForm();
            form.ShowDialog(this);
        }

        private void 条件设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }
            ConditionListForm form = new ConditionListForm();
            form.ShowDialog(this);
        }

        private void 指标管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }
            SystemParameterSettingListForm form = new SystemParameterSettingListForm();
            form.ShowDialog(this);
        }
        #endregion

        #region 策略相关
        //新建策略
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }

            PolicyEditForm form = new PolicyEditForm();
            if( form.ShowDialog(this) != DialogResult.OK) return;

            #region 网络操作

            #endregion


            TreeNode node = AddtoPolicy(form.policy);
            treeView1.SelectedNode = node;
        }

        //复制策略
        private void CopyPolicy_Click(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
                return;

            Policy cp= (Policy)treeView1.SelectedNode.Tag;


            Policy newpolicy = new DataLibrary.Policy()
            {
                UUID = Guid.NewGuid().ToString("N"),
                Name = cp.Name + "(1)",
                BuyGroup = cp.BuyGroup.Select(p => new Policy.ConditionGroup() {
                    Name = p.Name,
                    DoCondition=p.DoCondition,
                     GroupType=p.GroupType,
                     Conditions=new List<string> (p.Conditions)
                }).ToList(),
                SaleGroup = cp.SaleGroup.Select(p => new Policy.ConditionGroup()
                {
                    Name = p.Name,
                    DoCondition = p.DoCondition,
                    GroupType = p.GroupType,
                    Conditions = new List<string>(p.Conditions)
                }).ToList()
            };
            TreeNode node = AddtoPolicy(newpolicy);
            treeView1.SelectedNode = node;


            #region 验证用户码
            string rt = Globe.POST<string>(newpolicy, "tz", "editpolicy");
            rt = Globe.POST<string>(newpolicy, "tz", "editGroup");
            #endregion


            if (!Globe.UserSettings.Policies.ContainsKey(newpolicy.UUID))
                Globe.UserSettings.Policies.Add(newpolicy.UUID, newpolicy);

        }

        //重命名策略
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }

            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
                return;

            PolicyEditForm form = new PolicyEditForm((Policy)treeView1.SelectedNode.Tag);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            #region 网络操作

            #endregion

            TreeNode node = AddtoPolicy(form.policy);
            treeView1.SelectedNode = node;

        }

        //删除策略
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }

            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
                return;

            if (MessageBox.Show("删除操作不可恢复, 是否要删除策略 "+ ((Policy)treeView1.SelectedNode.Tag).Name + " ?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            #region 网络操作
            string rt = Globe.GET<string>("tz", "deletepolicy", ((Policy)treeView1.SelectedNode.Tag).UUID);
            if (rt == "err")
            {
                MessageBox.Show("删除错误, 请重新打开设置界面!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                treeView1.Focus();
                return;
            }
            else if (rt != "ok")
            {
                MessageBox.Show("用户码或服务器错误, 请使用 设置>系统设置 对用户码进行正确设置!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                treeView1.Focus();
                return;
            }
            #endregion 
            Globe.UserSettings.Policies.Remove(((Policy)treeView1.SelectedNode.Tag).UUID);
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }

        private TreeNode AddtoPolicy(Policy policy)
        {
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
                if (((Policy)node.Tag).UUID == policy.UUID)
                {
                    node.Text = policy.Name;
                    return node;
                }

            TreeNode n = new TreeNode()
            {
                Text = policy.Name,
                Tag = policy,
                ImageIndex = 1,
                SelectedImageIndex = 1
            };
            treeView1.Nodes[0].Nodes.Add(n);

            return n;
        }


        #endregion

        #region 条件组相关
        private void ListGroups(Policy policy)
        {
            listView1.Items.Clear();
            foreach (Policy.ConditionGroup group in policy.BuyGroup)
            {
                ListViewItem item = listView1.Items.Add(group.Name);
                item.SubItems.Add(group.DoCondition == "0" ? "始终触发" : group.DoCondition);
                item.SubItems.Add(group.GroupType == 1 ? "OR" : "AND");
                item.Tag = group;
                item.ImageIndex = 2;
            }

            listView2.Items.Clear();
            foreach (Policy.ConditionGroup group in policy.SaleGroup)
            {
                ListViewItem item = listView2.Items.Add(group.Name);
                item.SubItems.Add(group.DoCondition == "0" ? "始终触发" : group.DoCondition);
                item.SubItems.Add(group.GroupType == 1 ? "OR" : "AND");
                item.Tag = group;
                item.ImageIndex = 2;
            }
        }

        private void TB_Group_NewBuy_Click(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }

            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
                return;


            GroupEditForm form = new GroupEditForm((Policy)treeView1.SelectedNode.Tag, true,  0, -1);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            //((Policy)treeView1.SelectedNode.Tag).BuyGroup.Add(form.group);

            ListViewItem item = listView1.Items.Add(form.group.Name);
            item.SubItems.Add(form.group.DoCondition == "0" ? "始终触发" : form.group.DoCondition);
            item.SubItems.Add(form.group.GroupType == 1 ? "OR" : "AND");
            item.Tag = form.group;
            item.ImageIndex = 2;
        }

        private void TB_Group_NewSale_Click(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }

            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
                return;


            GroupEditForm form = new GroupEditForm((Policy)treeView1.SelectedNode.Tag, true, 1, -1);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            //((Policy)treeView1.SelectedNode.Tag).SaleGroup.Add(form.group);

            ListViewItem item = listView2.Items.Add(form.group.Name);
            item.SubItems.Add(form.group.DoCondition == "0" ? "始终触发" : form.group.DoCondition);
            item.SubItems.Add(form.group.GroupType == 1 ? "OR" : "AND");
            item.Tag = form.group;
            item.ImageIndex = 2;
        }


        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }

            if (listView1.SelectedItems.Count <= 0 || listView1.SelectedItems[0].Tag == null)
                return;

            ListViewItem item = listView1.SelectedItems[0];

            GroupEditForm form = new GroupEditForm((Policy)treeView1.SelectedNode.Tag, false, 0, item.Index);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            item.Text = form.group.Name;
            item.SubItems[1].Text = form.group.DoCondition == "0" ? "始终触发" : form.group.DoCondition;
            item.SubItems[2].Text  = form.group.GroupType == 1 ? "OR" : "AND";
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (Globe.UserSettings == null)
            {
                ShowUserKeyErr();
                return;
            }

            if (treeView1.SelectedNode.Tag == null || listView2.SelectedItems.Count <= 0 || listView2.SelectedItems[0].Tag == null)
                return;

            ListViewItem item = listView2.SelectedItems[0];

            GroupEditForm form = new GroupEditForm((Policy)treeView1.SelectedNode.Tag, false, 1, item.Index);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            item.Text = form.group.Name;
            item.SubItems[1].Text = form.group.DoCondition == "0" ? "始终触发" : form.group.DoCondition;
            item.SubItems[2].Text = form.group.GroupType == 1 ? "OR" : "AND";
        }

        private void TB_Group_Delete_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag != null && listView1.Focused && listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Tag != null)
            {
                ListViewItem item = listView1.SelectedItems[0];

                if (MessageBox.Show("删除操作不可恢复, 是否要删除该条件组 " + ((Policy.ConditionGroup)item.Tag).Name + " ?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                #region 网络操作
                Policy newPolicy = new Policy()
                {
                    UUID = ((Policy)treeView1.SelectedNode.Tag).UUID,
                    BuyGroup = new List<Policy.ConditionGroup>(((Policy)treeView1.SelectedNode.Tag).BuyGroup),
                    SaleGroup = new List<Policy.ConditionGroup>(((Policy)treeView1.SelectedNode.Tag).SaleGroup)
                };
                newPolicy.BuyGroup.Remove(((Policy.ConditionGroup)item.Tag));

                string rt = Globe.POST<string>(newPolicy, "tz", "editGroup");
                if (rt == "err")
                {
                    MessageBox.Show("设置错误, 请重新打开设置界面!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listView1.Focus();
                    return;
                }
                else if (rt != "ok")
                {
                    MessageBox.Show("用户码或服务器错误, 请使用 设置>系统设置 对用户码进行正确设置!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listView1.Focus();
                    return;
                }
                #endregion 


                ((Policy)treeView1.SelectedNode.Tag).BuyGroup.Remove((Policy.ConditionGroup)item.Tag);

                int index = item.Index;
                listView1.Items.Remove(item);
                if (listView1.Items.Count - 1 < index)
                    index--;
                if (index >= 0)
                    listView1.Items[index].Selected = true;

                listView1.Focus();
            }
            else if (treeView1.SelectedNode.Tag != null && listView2.Focused && listView2.SelectedItems.Count > 0 && listView2.SelectedItems[0].Tag != null)
            {
                ListViewItem item = listView2.SelectedItems[0];

                if (MessageBox.Show("删除操作不可恢复, 是否要删除该条件组 " + ((Policy.ConditionGroup)item.Tag).Name + " ?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                #region 网络操作
                Policy newPolicy = new Policy()
                {
                    UUID = ((Policy)treeView1.SelectedNode.Tag).UUID,
                    BuyGroup = new List<Policy.ConditionGroup>(((Policy)treeView1.SelectedNode.Tag).BuyGroup),
                    SaleGroup = new List<Policy.ConditionGroup>(((Policy)treeView1.SelectedNode.Tag).SaleGroup)
                };
                newPolicy.SaleGroup.Remove(((Policy.ConditionGroup)item.Tag));

                string rt = Globe.POST<string>(newPolicy, "tz", "editGroup");
                if (rt == "err")
                {
                    MessageBox.Show("设置错误, 请重新打开设置界面!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listView2.Focus();
                    return;
                }
                else if (rt != "ok")
                {
                    MessageBox.Show("用户码或服务器错误, 请使用 设置>系统设置 对用户码进行正确设置!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listView2.Focus();
                    return;
                }
                #endregion 


                ((Policy)treeView1.SelectedNode.Tag).SaleGroup.Remove((Policy.ConditionGroup)item.Tag);

                int index = item.Index;
                listView2.Items.Remove(item);
                if (listView2.Items.Count - 1 < index)
                    index--;
                if (index >= 0)
                    listView2.Items[index].Selected = true;

                listView2.Focus();
            }
        }


        #endregion

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
            {
                TB_Run_Setting.Enabled = false;
                TB_Run_Start.Enabled = false;
                TB_Run2_Start.Enabled = false;
                TB_Policy_Delete.Enabled = false;
                TB_Policy_Save.Enabled = false;
                TB_Group_New.Enabled = false;

                listView1.Items.Clear();
                listView2.Items.Clear();
            }
            else
            {
                TB_Run_Setting.Enabled = true;
                TB_Run_Start.Enabled = true;
                TB_Run2_Start.Enabled = true;
                TB_Policy_Delete.Enabled = true;
                TB_Policy_Save.Enabled = true;
                TB_Group_New.Enabled = true;


                ListGroups((Policy)treeView1.SelectedNode.Tag);

            }


        }


        #region group 状态

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupDeleteEnable();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupDeleteEnable();
        }

        private void listView2_Leave(object sender, EventArgs e)
        {
            GroupDeleteEnable();
        }

        private void listView1_Leave(object sender, EventArgs e)
        {
            GroupDeleteEnable();
        }

        private void GroupDeleteEnable()
        {
            if (listView1.Focused && listView1.SelectedItems.Count > 0 ||
               listView2.Focused && listView2.SelectedItems.Count > 0)
                TB_Group_Delete.Enabled = true;
            else
                TB_Group_Delete.Enabled = false;
        }

        private void listView1_Enter(object sender, EventArgs e)
        {
            GroupDeleteEnable();
        }

        private void listView2_Enter(object sender, EventArgs e)
        {
            GroupDeleteEnable();
        }
        #endregion

        #region 运行
        private void ListRunSetting()
        {
            if (Globe.SystemSettings.RunSettings == null)
            {
                Globe.SystemSettings.RunSettings = new List<RunSetting>();
                Globe.SaveFile(Path.Combine(Globe.LoadPath, "setting.dat"), Globe.SystemSettings);
            }
             
            TB_Run_Setting.Items.Clear();
            TB_Run_Setting.Items.Add("<<前一次配置>>");

            for (int i = Globe.SystemSettings.RunSettings.Count - 1; i >= 0; i--)
                TB_Run_Setting.Items.Add(Globe.SystemSettings.RunSettings[i].BeginDate.ToShortDateString() + "-" +
                    Globe.SystemSettings.RunSettings[i].EndDate.ToShortDateString() + " (" +
                    (Globe.SystemSettings.RunSettings[i].isCST ? "自定义" :
                    ((Globe.SystemSettings.RunSettings[i].is00 ? "00," : "") +
                    (Globe.SystemSettings.RunSettings[i].is30 ? "30," : "") +
                    (Globe.SystemSettings.RunSettings[i].is60 ? "60," : "") +
                    (Globe.SystemSettings.RunSettings[i].is68 ? "68," : "") +
                    (Globe.SystemSettings.RunSettings[i].isST ? "ST," : "")).TrimEnd(',')
                    ) + ") 线程数:" + (Globe.SystemSettings.RunSettings[i].isProcess ? Globe.SystemSettings.RunSettings[i].Process.ToString() : "1"));

            TB_Run_Setting.SelectedIndex = 0;
        }

        private void TB_Run_Start_Click(object sender, EventArgs e)
        {
            RunSetting thisSetting = null;

            if (Globe.SystemSettings.RunSettings.Count > 0)
                thisSetting = Globe.SystemSettings.RunSettings[Globe.SystemSettings.RunSettings.Count - 1];

            StartSettingForm form = new StartSettingForm(thisSetting);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            thisSetting = form.setting;


            #region 更新运行设置
            foreach (RunSetting s in Globe.SystemSettings.RunSettings)
                if (s.Equals(thisSetting))
                {
                    Globe.SystemSettings.RunSettings.Remove(s);
                    break;
                }

            while (Globe.SystemSettings.RunSettings.Count > 9)
                Globe.SystemSettings.RunSettings.RemoveAt(0);

            Globe.SystemSettings.RunSettings.Add(thisSetting);

            Globe.SaveFile(Path.Combine(Globe.LoadPath, "setting.dat"), Globe.SystemSettings);
            ListRunSetting();
            #endregion



            RUN(thisSetting);
        }

        private void TB_Run_Setting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TB_Run_Setting.SelectedIndex > 0 && TB_Run_Setting.SelectedIndex <= Globe.SystemSettings.RunSettings.Count)
            {
                RunSetting thisSetting = Globe.SystemSettings.RunSettings[Globe.SystemSettings.RunSettings.Count - TB_Run_Setting.SelectedIndex];
                Globe.SystemSettings.RunSettings.Remove(thisSetting);
                Globe.SystemSettings.RunSettings.Add(thisSetting);
                Globe.SaveFile(Path.Combine(Globe.LoadPath, "setting.dat"), Globe.SystemSettings);
                ListRunSetting();

                RUN(thisSetting);
            }
        }

        private void RUN(RunSetting setting)
        {
            TB_Run_Setting.Enabled = false;
            TB_Run2_Start.Enabled = false;
            TB_Run_Start.Enabled = false;

            OperationLibrary.Run.RUN r = new OperationLibrary.Run.RUN(setting, (Policy)treeView1.SelectedNode.Tag);
            r.DO(new OperationLibrary.Run.RUN.CallbackDelegate(SetLog));
             
        }

        private void SetLog(int value, string str)
        {
            this.BeginInvoke(new OperationLibrary.Run.RUN.CallbackDelegate(SetLogDone), value, str);
        }

        private void SetLogDone(int value, string str)
        {
            if (value == -1) //停止
            {
                TP_Bar.Value = 0;
                TP_Bar.Visible = false;
                TP_State.Text = "";
                TB_Log.AppendText(str + "\r\n");
                TB_Log.ScrollToCaret();

                TB_Run_Setting.Enabled = true;
                TB_Run2_Start.Enabled = true;
                TB_Run_Start.Enabled = true;
            }
            else if (value < 0) //写日志
            {
                TB_Log.AppendText(str + "\r\n");
                TB_Log.ScrollToCaret();
            }
            else //写状态
            {
                TP_Bar.Value = value;
                TP_State.Text = str;
                TP_Bar.Visible = true;
            }
        }

        #endregion

        private void TB_Run_Stop_Click(object sender, EventArgs e)
        {
            Policy policy = (Policy)treeView1.SelectedNode.Tag;
            RunSetting rs = Globe.SystemSettings.RunSettings.FirstOrDefault();
            StoZoo.Dog.StockData.StockData datas = Globe.LoadFile<StoZoo.Dog.StockData.StockData>(Path.Combine(Globe.SystemSettings.DataPath, "stock.dat"));
            foreach (var market in datas.Markets)
            {
                foreach (var stock in market.Stocks)
                {
                    if ((!stock.Code.StartsWith("00"))&& (!stock.Code.StartsWith("60")))
                        continue;
                    OperationLibrary.Run.RunEnvironment re = new OperationLibrary.Run.RunEnvironment();
                    if (re.LoadBaseInfo(rs, policy) && re.LoadStock(stock))
                        Globe.testMemory.Add(stock.Code, re);
                }        
            }
        }

        private void TB_Run2_Start_Click(object sender, EventArgs e)
        {
            RunSetting thisSetting = null;

            if (Globe.SystemSettings.RunSettings.Count > 0)
                thisSetting = Globe.SystemSettings.RunSettings[Globe.SystemSettings.RunSettings.Count - 1];

            StartSettingForm form = new StartSettingForm(thisSetting, true);
            if (form.ShowDialog(this) != DialogResult.OK) return;

            thisSetting = form.setting;


            #region 更新运行设置
            foreach (RunSetting s in Globe.SystemSettings.RunSettings)
                if (s.Equals(thisSetting))
                {
                    Globe.SystemSettings.RunSettings.Remove(s);
                    break;
                }

            while (Globe.SystemSettings.RunSettings.Count > 9)
                Globe.SystemSettings.RunSettings.RemoveAt(0);

            Globe.SystemSettings.RunSettings.Add(thisSetting);

            Globe.SaveFile(Path.Combine(Globe.LoadPath, "setting.dat"), Globe.SystemSettings);
            ListRunSetting();
            #endregion


            RUNDevire(thisSetting); 
        }

        private void RUNDevire(RunSetting setting)
        {
            TB_Run_Setting.Enabled = false;
            TB_Run2_Start.Enabled = false;
            TB_Run_Start.Enabled = false;

            OperationLibrary.Run.RUNDevire r = new OperationLibrary.Run.RUNDevire(setting, (Policy)treeView1.SelectedNode.Tag);
            r.DO(new OperationLibrary.Run.RUNDevire.CallbackDelegate(SetLog));

        }
    }
}
