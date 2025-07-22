
namespace TZ.TZForm
{
    partial class SystemSettingForm
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
            this.TB_Key = new System.Windows.Forms.TextBox();
            this.TB_Path = new System.Windows.Forms.TextBox();
            this.BT_Key = new System.Windows.Forms.Button();
            this.BT_Path = new System.Windows.Forms.Button();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.TB_Server = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据位置：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户KEY：";
            // 
            // TB_Key
            // 
            this.TB_Key.Location = new System.Drawing.Point(101, 48);
            this.TB_Key.Name = "TB_Key";
            this.TB_Key.Size = new System.Drawing.Size(260, 23);
            this.TB_Key.TabIndex = 1;
            // 
            // TB_Path
            // 
            this.TB_Path.Location = new System.Drawing.Point(101, 77);
            this.TB_Path.Name = "TB_Path";
            this.TB_Path.Size = new System.Drawing.Size(285, 23);
            this.TB_Path.TabIndex = 3;
            this.TB_Path.Text = "E:\\StoZooData";
            // 
            // BT_Key
            // 
            this.BT_Key.Location = new System.Drawing.Point(367, 48);
            this.BT_Key.Name = "BT_Key";
            this.BT_Key.Size = new System.Drawing.Size(52, 23);
            this.BT_Key.TabIndex = 2;
            this.BT_Key.Text = "申请";
            this.BT_Key.UseVisualStyleBackColor = true;
            this.BT_Key.Click += new System.EventHandler(this.BT_Key_Click);
            // 
            // BT_Path
            // 
            this.BT_Path.Location = new System.Drawing.Point(392, 77);
            this.BT_Path.Name = "BT_Path";
            this.BT_Path.Size = new System.Drawing.Size(27, 23);
            this.BT_Path.TabIndex = 4;
            this.BT_Path.Text = "...";
            this.BT_Path.UseVisualStyleBackColor = true;
            this.BT_Path.Click += new System.EventHandler(this.BT_Path_Click);
            // 
            // BT_OK
            // 
            this.BT_OK.Location = new System.Drawing.Point(262, 115);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(70, 23);
            this.BT_OK.TabIndex = 5;
            this.BT_OK.Text = "保存";
            this.BT_OK.UseVisualStyleBackColor = true;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.Location = new System.Drawing.Point(349, 115);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(70, 23);
            this.BT_Cancel.TabIndex = 6;
            this.BT_Cancel.Text = "取消";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
            // 
            // TB_Server
            // 
            this.TB_Server.Location = new System.Drawing.Point(101, 19);
            this.TB_Server.Name = "TB_Server";
            this.TB_Server.Size = new System.Drawing.Size(318, 23);
            this.TB_Server.TabIndex = 0;
            this.TB_Server.Text = "http://localhost:8888";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "服务器：";
            // 
            // SystemSettingForm
            // 
            this.AcceptButton = this.BT_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 156);
            this.Controls.Add(this.TB_Server);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_Path);
            this.Controls.Add(this.BT_Key);
            this.Controls.Add(this.TB_Path);
            this.Controls.Add(this.TB_Key);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SystemSettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.SystemSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Key;
        private System.Windows.Forms.TextBox TB_Path;
        private System.Windows.Forms.Button BT_Key;
        private System.Windows.Forms.Button BT_Path;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.TextBox TB_Server;
        private System.Windows.Forms.Label label3;
    }
}