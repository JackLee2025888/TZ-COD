
namespace TZ.TZForm
{
    partial class StartSettingForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.TB_CST = new System.Windows.Forms.TextBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.TB_Begin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_End = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.CB_60 = new System.Windows.Forms.CheckBox();
            this.CB_00 = new System.Windows.Forms.CheckBox();
            this.CB_30 = new System.Windows.Forms.CheckBox();
            this.CB_68 = new System.Windows.Forms.CheckBox();
            this.CB_ST = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CB_CST = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CB_Process = new System.Windows.Forms.CheckBox();
            this.TB_Process_LB = new System.Windows.Forms.Label();
            this.TB_Process = new System.Windows.Forms.TextBox();
            this.TB_Indicator1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_Indicator2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TB_Path = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.BT_Path = new System.Windows.Forms.Button();
            this.TB_DeriveAddress = new System.Windows.Forms.TextBox();
            this.LB_DeriveAddress = new System.Windows.Forms.Label();
            this.TB_DevireKey = new System.Windows.Forms.TextBox();
            this.LB_DevireKey = new System.Windows.Forms.Label();
            this.CB_Year = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "模拟时间段：";
            // 
            // TB_CST
            // 
            this.TB_CST.Enabled = false;
            this.TB_CST.Location = new System.Drawing.Point(111, 80);
            this.TB_CST.Multiline = true;
            this.TB_CST.Name = "TB_CST";
            this.TB_CST.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_CST.Size = new System.Drawing.Size(298, 162);
            this.TB_CST.TabIndex = 8;
            // 
            // BT_OK
            // 
            this.BT_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BT_OK.Location = new System.Drawing.Point(263, 465);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(70, 23);
            this.BT_OK.TabIndex = 15;
            this.BT_OK.Text = "开始";
            this.BT_OK.UseVisualStyleBackColor = true;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BT_Cancel.Location = new System.Drawing.Point(339, 465);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(70, 23);
            this.BT_Cancel.TabIndex = 16;
            this.BT_Cancel.Text = "取消";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
            // 
            // TB_Begin
            // 
            this.TB_Begin.Location = new System.Drawing.Point(111, 20);
            this.TB_Begin.Name = "TB_Begin";
            this.TB_Begin.Size = new System.Drawing.Size(118, 23);
            this.TB_Begin.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(235, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "-";
            // 
            // TB_End
            // 
            this.TB_End.Location = new System.Drawing.Point(254, 20);
            this.TB_End.Name = "TB_End";
            this.TB_End.Size = new System.Drawing.Size(118, 23);
            this.TB_End.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "模拟市场段：";
            // 
            // CB_60
            // 
            this.CB_60.AutoSize = true;
            this.CB_60.Checked = true;
            this.CB_60.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_60.Location = new System.Drawing.Point(111, 53);
            this.CB_60.Name = "CB_60";
            this.CB_60.Size = new System.Drawing.Size(41, 21);
            this.CB_60.TabIndex = 2;
            this.CB_60.Text = "60";
            this.CB_60.UseVisualStyleBackColor = true;
            // 
            // CB_00
            // 
            this.CB_00.AutoSize = true;
            this.CB_00.Checked = true;
            this.CB_00.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_00.Location = new System.Drawing.Point(158, 53);
            this.CB_00.Name = "CB_00";
            this.CB_00.Size = new System.Drawing.Size(41, 21);
            this.CB_00.TabIndex = 3;
            this.CB_00.Text = "00";
            this.CB_00.UseVisualStyleBackColor = true;
            // 
            // CB_30
            // 
            this.CB_30.AutoSize = true;
            this.CB_30.Location = new System.Drawing.Point(205, 53);
            this.CB_30.Name = "CB_30";
            this.CB_30.Size = new System.Drawing.Size(41, 21);
            this.CB_30.TabIndex = 4;
            this.CB_30.Text = "30";
            this.CB_30.UseVisualStyleBackColor = true;
            // 
            // CB_68
            // 
            this.CB_68.AutoSize = true;
            this.CB_68.Location = new System.Drawing.Point(252, 53);
            this.CB_68.Name = "CB_68";
            this.CB_68.Size = new System.Drawing.Size(41, 21);
            this.CB_68.TabIndex = 5;
            this.CB_68.Text = "68";
            this.CB_68.UseVisualStyleBackColor = true;
            // 
            // CB_ST
            // 
            this.CB_ST.AutoSize = true;
            this.CB_ST.Location = new System.Drawing.Point(299, 53);
            this.CB_ST.Name = "CB_ST";
            this.CB_ST.Size = new System.Drawing.Size(41, 21);
            this.CB_ST.TabIndex = 6;
            this.CB_ST.Text = "ST";
            this.CB_ST.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "自定义代码：";
            // 
            // CB_CST
            // 
            this.CB_CST.AutoSize = true;
            this.CB_CST.Location = new System.Drawing.Point(346, 53);
            this.CB_CST.Name = "CB_CST";
            this.CB_CST.Size = new System.Drawing.Size(63, 21);
            this.CB_CST.TabIndex = 7;
            this.CB_CST.Text = "自定义";
            this.CB_CST.UseVisualStyleBackColor = true;
            this.CB_CST.CheckedChanged += new System.EventHandler(this.CB_CST_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "多线程设置：";
            // 
            // CB_Process
            // 
            this.CB_Process.AutoSize = true;
            this.CB_Process.Location = new System.Drawing.Point(111, 251);
            this.CB_Process.Name = "CB_Process";
            this.CB_Process.Size = new System.Drawing.Size(111, 21);
            this.CB_Process.TabIndex = 9;
            this.CB_Process.Text = "使用多线程模拟";
            this.CB_Process.UseVisualStyleBackColor = true;
            this.CB_Process.CheckedChanged += new System.EventHandler(this.CB_Process_CheckedChanged);
            // 
            // TB_Process_LB
            // 
            this.TB_Process_LB.AutoSize = true;
            this.TB_Process_LB.Enabled = false;
            this.TB_Process_LB.Location = new System.Drawing.Point(228, 252);
            this.TB_Process_LB.Name = "TB_Process_LB";
            this.TB_Process_LB.Size = new System.Drawing.Size(56, 17);
            this.TB_Process_LB.TabIndex = 20;
            this.TB_Process_LB.Text = "线程数：";
            // 
            // TB_Process
            // 
            this.TB_Process.Enabled = false;
            this.TB_Process.Location = new System.Drawing.Point(290, 249);
            this.TB_Process.Name = "TB_Process";
            this.TB_Process.Size = new System.Drawing.Size(43, 23);
            this.TB_Process.TabIndex = 10;
            this.TB_Process.Text = "20";
            // 
            // TB_Indicator1
            // 
            this.TB_Indicator1.Location = new System.Drawing.Point(111, 334);
            this.TB_Indicator1.Name = "TB_Indicator1";
            this.TB_Indicator1.Size = new System.Drawing.Size(298, 23);
            this.TB_Indicator1.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 337);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "买：";
            // 
            // TB_Indicator2
            // 
            this.TB_Indicator2.Location = new System.Drawing.Point(111, 363);
            this.TB_Indicator2.Name = "TB_Indicator2";
            this.TB_Indicator2.Size = new System.Drawing.Size(298, 23);
            this.TB_Indicator2.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 366);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "卖：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 309);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "输出指标：";
            // 
            // TB_Path
            // 
            this.TB_Path.Location = new System.Drawing.Point(111, 278);
            this.TB_Path.Name = "TB_Path";
            this.TB_Path.Size = new System.Drawing.Size(264, 23);
            this.TB_Path.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 280);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 17);
            this.label9.TabIndex = 27;
            this.label9.Text = "输出路径：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(106, 309);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(308, 17);
            this.label10.TabIndex = 28;
            this.label10.Text = "只能输出当前日期指标，并且需在策略中使用，逗号分隔";
            // 
            // BT_Path
            // 
            this.BT_Path.Location = new System.Drawing.Point(381, 278);
            this.BT_Path.Name = "BT_Path";
            this.BT_Path.Size = new System.Drawing.Size(28, 23);
            this.BT_Path.TabIndex = 12;
            this.BT_Path.Text = "...";
            this.BT_Path.UseVisualStyleBackColor = true;
            this.BT_Path.Click += new System.EventHandler(this.BT_Path_Click);
            // 
            // TB_DeriveAddress
            // 
            this.TB_DeriveAddress.Location = new System.Drawing.Point(111, 392);
            this.TB_DeriveAddress.Name = "TB_DeriveAddress";
            this.TB_DeriveAddress.Size = new System.Drawing.Size(217, 23);
            this.TB_DeriveAddress.TabIndex = 29;
            this.TB_DeriveAddress.Visible = false;
            // 
            // LB_DeriveAddress
            // 
            this.LB_DeriveAddress.AutoSize = true;
            this.LB_DeriveAddress.Location = new System.Drawing.Point(25, 395);
            this.LB_DeriveAddress.Name = "LB_DeriveAddress";
            this.LB_DeriveAddress.Size = new System.Drawing.Size(68, 17);
            this.LB_DeriveAddress.TabIndex = 30;
            this.LB_DeriveAddress.Text = "衍生引擎：";
            this.LB_DeriveAddress.Visible = false;
            // 
            // TB_DevireKey
            // 
            this.TB_DevireKey.Location = new System.Drawing.Point(111, 421);
            this.TB_DevireKey.Name = "TB_DevireKey";
            this.TB_DevireKey.Size = new System.Drawing.Size(298, 23);
            this.TB_DevireKey.TabIndex = 31;
            this.TB_DevireKey.Visible = false;
            // 
            // LB_DevireKey
            // 
            this.LB_DevireKey.AutoSize = true;
            this.LB_DevireKey.Location = new System.Drawing.Point(25, 424);
            this.LB_DevireKey.Name = "LB_DevireKey";
            this.LB_DevireKey.Size = new System.Drawing.Size(65, 17);
            this.LB_DevireKey.TabIndex = 32;
            this.LB_DevireKey.Text = "衍生Key：";
            this.LB_DevireKey.Visible = false;
            // 
            // CB_Year
            // 
            this.CB_Year.AutoSize = true;
            this.CB_Year.Location = new System.Drawing.Point(334, 394);
            this.CB_Year.Name = "CB_Year";
            this.CB_Year.Size = new System.Drawing.Size(75, 21);
            this.CB_Year.TabIndex = 33;
            this.CB_Year.Text = "模拟年化";
            this.CB_Year.UseVisualStyleBackColor = true;
            // 
            // StartSettingForm
            // 
            this.AcceptButton = this.BT_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 500);
            this.Controls.Add(this.CB_Year);
            this.Controls.Add(this.TB_DevireKey);
            this.Controls.Add(this.LB_DevireKey);
            this.Controls.Add(this.TB_DeriveAddress);
            this.Controls.Add(this.LB_DeriveAddress);
            this.Controls.Add(this.BT_Path);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.TB_Path);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TB_Indicator2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TB_Indicator1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TB_Process);
            this.Controls.Add(this.TB_Process_LB);
            this.Controls.Add(this.CB_Process);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CB_CST);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CB_ST);
            this.Controls.Add(this.CB_68);
            this.Controls.Add(this.CB_30);
            this.Controls.Add(this.CB_00);
            this.Controls.Add(this.CB_60);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_End);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_Begin);
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.TB_CST);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StartSettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "策略模拟设置";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_CST;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.DateTimePicker TB_Begin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TB_End;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CB_60;
        private System.Windows.Forms.CheckBox CB_00;
        private System.Windows.Forms.CheckBox CB_30;
        private System.Windows.Forms.CheckBox CB_68;
        private System.Windows.Forms.CheckBox CB_ST;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CB_CST;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox CB_Process;
        private System.Windows.Forms.Label TB_Process_LB;
        private System.Windows.Forms.TextBox TB_Process;
        private System.Windows.Forms.TextBox TB_Indicator1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_Indicator2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TB_Path;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button BT_Path;
        private System.Windows.Forms.TextBox TB_DeriveAddress;
        private System.Windows.Forms.Label LB_DeriveAddress;
        private System.Windows.Forms.TextBox TB_DevireKey;
        private System.Windows.Forms.Label LB_DevireKey;
        private System.Windows.Forms.CheckBox CB_Year;
    }
}