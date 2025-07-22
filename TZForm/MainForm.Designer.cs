
namespace TZ.TZForm
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("天赢策略");
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TB_Policy_New = new System.Windows.Forms.ToolStripButton();
            this.TB_Policy_Save = new System.Windows.Forms.ToolStripButton();
            this.TB_Policy_Copy = new System.Windows.Forms.ToolStripButton();
            this.TB_Policy_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TB_Group_New = new System.Windows.Forms.ToolStripDropDownButton();
            this.TB_Group_NewBuy = new System.Windows.Forms.ToolStripMenuItem();
            this.TB_Group_NewSale = new System.Windows.Forms.ToolStripMenuItem();
            this.TB_Group_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TB_Run_Setting = new System.Windows.Forms.ToolStripComboBox();
            this.TB_Run_Start = new System.Windows.Forms.ToolStripButton();
            this.TB_Run2_Start = new System.Windows.Forms.ToolStripButton();
            this.TB_Run_Stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.条件设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.指标管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TP_Bar = new System.Windows.Forms.ToolStripProgressBar();
            this.TP_State = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.TB_Log = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TB_Policy_New,
            this.TB_Policy_Save,
            this.TB_Policy_Copy,
            this.TB_Policy_Delete,
            this.toolStripSeparator2,
            this.TB_Group_New,
            this.TB_Group_Delete,
            this.toolStripSeparator4,
            this.TB_Run_Setting,
            this.TB_Run_Start,
            this.TB_Run2_Start,
            this.TB_Run_Stop,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1520, 28);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TB_Policy_New
            // 
            this.TB_Policy_New.Image = ((System.Drawing.Image)(resources.GetObject("TB_Policy_New.Image")));
            this.TB_Policy_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Policy_New.Name = "TB_Policy_New";
            this.TB_Policy_New.Size = new System.Drawing.Size(93, 25);
            this.TB_Policy_New.Text = "新建策略";
            this.TB_Policy_New.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // TB_Policy_Save
            // 
            this.TB_Policy_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TB_Policy_Save.Image = ((System.Drawing.Image)(resources.GetObject("TB_Policy_Save.Image")));
            this.TB_Policy_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Policy_Save.Name = "TB_Policy_Save";
            this.TB_Policy_Save.Size = new System.Drawing.Size(29, 25);
            this.TB_Policy_Save.Text = "保存策略";
            this.TB_Policy_Save.Visible = false;
            this.TB_Policy_Save.Click += new System.EventHandler(this.CopyPolicy_Click);
            // 
            // TB_Policy_Copy
            // 
            this.TB_Policy_Copy.Image = ((System.Drawing.Image)(resources.GetObject("TB_Policy_Copy.Image")));
            this.TB_Policy_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Policy_Copy.Name = "TB_Policy_Copy";
            this.TB_Policy_Copy.Size = new System.Drawing.Size(93, 25);
            this.TB_Policy_Copy.Text = "复制策略";
            this.TB_Policy_Copy.Click += new System.EventHandler(this.CopyPolicy_Click);
            // 
            // TB_Policy_Delete
            // 
            this.TB_Policy_Delete.Enabled = false;
            this.TB_Policy_Delete.Image = ((System.Drawing.Image)(resources.GetObject("TB_Policy_Delete.Image")));
            this.TB_Policy_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Policy_Delete.Name = "TB_Policy_Delete";
            this.TB_Policy_Delete.Size = new System.Drawing.Size(93, 25);
            this.TB_Policy_Delete.Text = "删除策略";
            this.TB_Policy_Delete.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // TB_Group_New
            // 
            this.TB_Group_New.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TB_Group_NewBuy,
            this.TB_Group_NewSale});
            this.TB_Group_New.Enabled = false;
            this.TB_Group_New.Image = ((System.Drawing.Image)(resources.GetObject("TB_Group_New.Image")));
            this.TB_Group_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Group_New.Name = "TB_Group_New";
            this.TB_Group_New.Size = new System.Drawing.Size(118, 25);
            this.TB_Group_New.Text = "增加条件组";
            // 
            // TB_Group_NewBuy
            // 
            this.TB_Group_NewBuy.Name = "TB_Group_NewBuy";
            this.TB_Group_NewBuy.Size = new System.Drawing.Size(122, 26);
            this.TB_Group_NewBuy.Text = "买入";
            this.TB_Group_NewBuy.Click += new System.EventHandler(this.TB_Group_NewBuy_Click);
            // 
            // TB_Group_NewSale
            // 
            this.TB_Group_NewSale.Name = "TB_Group_NewSale";
            this.TB_Group_NewSale.Size = new System.Drawing.Size(122, 26);
            this.TB_Group_NewSale.Text = "卖出";
            this.TB_Group_NewSale.Click += new System.EventHandler(this.TB_Group_NewSale_Click);
            // 
            // TB_Group_Delete
            // 
            this.TB_Group_Delete.Enabled = false;
            this.TB_Group_Delete.Image = ((System.Drawing.Image)(resources.GetObject("TB_Group_Delete.Image")));
            this.TB_Group_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Group_Delete.Name = "TB_Group_Delete";
            this.TB_Group_Delete.Size = new System.Drawing.Size(108, 25);
            this.TB_Group_Delete.Text = "删除条件组";
            this.TB_Group_Delete.Click += new System.EventHandler(this.TB_Group_Delete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // TB_Run_Setting
            // 
            this.TB_Run_Setting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TB_Run_Setting.Enabled = false;
            this.TB_Run_Setting.Name = "TB_Run_Setting";
            this.TB_Run_Setting.Size = new System.Drawing.Size(160, 28);
            this.TB_Run_Setting.SelectedIndexChanged += new System.EventHandler(this.TB_Run_Setting_SelectedIndexChanged);
            // 
            // TB_Run_Start
            // 
            this.TB_Run_Start.Enabled = false;
            this.TB_Run_Start.Image = ((System.Drawing.Image)(resources.GetObject("TB_Run_Start.Image")));
            this.TB_Run_Start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Run_Start.Name = "TB_Run_Start";
            this.TB_Run_Start.Size = new System.Drawing.Size(93, 25);
            this.TB_Run_Start.Text = "运行策略";
            this.TB_Run_Start.Click += new System.EventHandler(this.TB_Run_Start_Click);
            // 
            // TB_Run2_Start
            // 
            this.TB_Run2_Start.Enabled = false;
            this.TB_Run2_Start.Image = ((System.Drawing.Image)(resources.GetObject("TB_Run2_Start.Image")));
            this.TB_Run2_Start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Run2_Start.Name = "TB_Run2_Start";
            this.TB_Run2_Start.Size = new System.Drawing.Size(93, 25);
            this.TB_Run2_Start.Text = "衍生引擎";
            this.TB_Run2_Start.Click += new System.EventHandler(this.TB_Run2_Start_Click);
            // 
            // TB_Run_Stop
            // 
            this.TB_Run_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TB_Run_Stop.Enabled = false;
            this.TB_Run_Stop.Image = ((System.Drawing.Image)(resources.GetObject("TB_Run_Stop.Image")));
            this.TB_Run_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Run_Stop.Name = "TB_Run_Stop";
            this.TB_Run_Stop.Size = new System.Drawing.Size(29, 25);
            this.TB_Run_Stop.Text = "停止运行";
            this.TB_Run_Stop.Click += new System.EventHandler(this.TB_Run_Stop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.参数设置ToolStripMenuItem,
            this.条件设置ToolStripMenuItem,
            this.指标管理ToolStripMenuItem});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(73, 25);
            this.toolStripButton1.Text = "设置";
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.参数设置ToolStripMenuItem.Text = "参数管理";
            this.参数设置ToolStripMenuItem.Click += new System.EventHandler(this.参数设置ToolStripMenuItem_Click);
            // 
            // 条件设置ToolStripMenuItem
            // 
            this.条件设置ToolStripMenuItem.Name = "条件设置ToolStripMenuItem";
            this.条件设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.条件设置ToolStripMenuItem.Text = "条件管理";
            this.条件设置ToolStripMenuItem.Click += new System.EventHandler(this.条件设置ToolStripMenuItem_Click);
            // 
            // 指标管理ToolStripMenuItem
            // 
            this.指标管理ToolStripMenuItem.Name = "指标管理ToolStripMenuItem";
            this.指标管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.指标管理ToolStripMenuItem.Text = "指标管理";
            this.指标管理ToolStripMenuItem.Click += new System.EventHandler(this.指标管理ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.TP_Bar,
            this.TP_State});
            this.statusStrip1.Location = new System.Drawing.Point(0, 615);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1520, 33);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 27);
            this.toolStripStatusLabel1.Text = "就绪";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(1262, 27);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // TP_Bar
            // 
            this.TP_Bar.Name = "TP_Bar";
            this.TP_Bar.Size = new System.Drawing.Size(257, 25);
            this.TP_Bar.Visible = false;
            // 
            // TP_State
            // 
            this.TP_State.AutoSize = false;
            this.TP_State.Name = "TP_State";
            this.TP_State.Size = new System.Drawing.Size(200, 27);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1520, 587);
            this.splitContainer1.SplitterDistance = 264;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "Node0";
            treeNode1.Text = "天赢策略";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(264, 587);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "SGM.png");
            this.imageList1.Images.SetKeyName(1, "xx.png");
            this.imageList1.Images.SetKeyName(2, "CL.png");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel1MinSize = 450;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.TB_Log);
            this.splitContainer2.Size = new System.Drawing.Size(1251, 587);
            this.splitContainer2.SplitterDistance = 450;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.listView2);
            this.splitContainer3.Size = new System.Drawing.Size(450, 587);
            this.splitContainer3.SplitterDistance = 291;
            this.splitContainer3.SplitterWidth = 5;
            this.splitContainer3.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(450, 291);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.Enter += new System.EventHandler(this.listView1_Enter);
            this.listView1.Leave += new System.EventHandler(this.listView1_Leave);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "买入条件组";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "触发条件";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "组合类型";
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Margin = new System.Windows.Forms.Padding(4);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(450, 291);
            this.listView2.SmallImageList = this.imageList1;
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.DoubleClick += new System.EventHandler(this.listView2_DoubleClick);
            this.listView2.Enter += new System.EventHandler(this.listView2_Enter);
            this.listView2.Leave += new System.EventHandler(this.listView2_Leave);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "卖出条件组";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "触发条件";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "组合类型";
            // 
            // TB_Log
            // 
            this.TB_Log.BackColor = System.Drawing.Color.White;
            this.TB_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_Log.Location = new System.Drawing.Point(0, 0);
            this.TB_Log.Margin = new System.Windows.Forms.Padding(4);
            this.TB_Log.Multiline = true;
            this.TB_Log.Name = "TB_Log";
            this.TB_Log.ReadOnly = true;
            this.TB_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_Log.Size = new System.Drawing.Size(796, 587);
            this.TB_Log.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1520, 648);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "天赢  -  量化模拟";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar TP_Bar;
        private System.Windows.Forms.ToolStripStatusLabel TP_State;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox TB_Log;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton TB_Run2_Start;
        private System.Windows.Forms.ToolStripButton TB_Policy_New;
        private System.Windows.Forms.ToolStripButton TB_Policy_Save;
        private System.Windows.Forms.ToolStripButton TB_Policy_Delete;
        private System.Windows.Forms.ToolStripComboBox TB_Run_Setting;
        private System.Windows.Forms.ToolStripButton TB_Run_Stop;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 条件设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton TB_Group_Delete;
        private System.Windows.Forms.ToolStripDropDownButton TB_Group_New;
        private System.Windows.Forms.ToolStripMenuItem TB_Group_NewBuy;
        private System.Windows.Forms.ToolStripMenuItem TB_Group_NewSale;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem 指标管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TB_Policy_Copy;
        private System.Windows.Forms.ToolStripButton TB_Run_Start;
    }
}