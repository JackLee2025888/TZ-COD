
namespace TZ.TZForm
{
    partial class SystemParameterSettingEditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Parameter = new System.Windows.Forms.TextBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.TB_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_Indicator = new System.Windows.Forms.ComboBox();
            this.TB_Tip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "指标参数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "对应指标：";
            // 
            // TB_Parameter
            // 
            this.TB_Parameter.Location = new System.Drawing.Point(130, 93);
            this.TB_Parameter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_Parameter.Name = "TB_Parameter";
            this.TB_Parameter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_Parameter.Size = new System.Drawing.Size(252, 27);
            this.TB_Parameter.TabIndex = 3;
            // 
            // BT_OK
            // 
            this.BT_OK.Location = new System.Drawing.Point(181, 158);
            this.BT_OK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(90, 27);
            this.BT_OK.TabIndex = 5;
            this.BT_OK.Text = "确定";
            this.BT_OK.UseVisualStyleBackColor = true;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.Location = new System.Drawing.Point(293, 158);
            this.BT_Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(90, 27);
            this.BT_Cancel.TabIndex = 6;
            this.BT_Cancel.Text = "取消";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
            // 
            // TB_Name
            // 
            this.TB_Name.Location = new System.Drawing.Point(130, 22);
            this.TB_Name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_Name.Name = "TB_Name";
            this.TB_Name.Size = new System.Drawing.Size(100, 27);
            this.TB_Name.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "定义名称：";
            // 
            // TB_Indicator
            // 
            this.TB_Indicator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TB_Indicator.FormattingEnabled = true;
            this.TB_Indicator.Items.AddRange(new object[] {
            "MA",
            "KDJ",
            "RSI",
            "MACD",
            "ASI"});
            this.TB_Indicator.Location = new System.Drawing.Point(130, 56);
            this.TB_Indicator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_Indicator.Name = "TB_Indicator";
            this.TB_Indicator.Size = new System.Drawing.Size(100, 28);
            this.TB_Indicator.TabIndex = 9;
            this.TB_Indicator.SelectedIndexChanged += new System.EventHandler(this.TB_Indicator_SelectedIndexChanged);
            // 
            // TB_Tip
            // 
            this.TB_Tip.AutoSize = true;
            this.TB_Tip.Location = new System.Drawing.Point(130, 124);
            this.TB_Tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TB_Tip.Name = "TB_Tip";
            this.TB_Tip.Size = new System.Drawing.Size(116, 20);
            this.TB_Tip.TabIndex = 10;
            this.TB_Tip.Text = "LONG, M1, M2";
            // 
            // SystemParameterSettingEditForm
            // 
            this.AcceptButton = this.BT_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 204);
            this.Controls.Add(this.TB_Tip);
            this.Controls.Add(this.TB_Indicator);
            this.Controls.Add(this.TB_Name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.TB_Parameter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SystemParameterSettingEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "指标设置";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Parameter;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.TextBox TB_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TB_Indicator;
        private System.Windows.Forms.Label TB_Tip;
    }
}