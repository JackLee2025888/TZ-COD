
namespace TZ.TZForm
{
    partial class ParameterEditForm
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
            this.TB_Discription = new System.Windows.Forms.TextBox();
            this.TB_Formula = new System.Windows.Forms.TextBox();
            this.BT_Formula = new System.Windows.Forms.Button();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.TB_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "公式/值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "描述：";
            // 
            // TB_Discription
            // 
            this.TB_Discription.Location = new System.Drawing.Point(101, 48);
            this.TB_Discription.Name = "TB_Discription";
            this.TB_Discription.Size = new System.Drawing.Size(345, 23);
            this.TB_Discription.TabIndex = 1;
            // 
            // TB_Formula
            // 
            this.TB_Formula.Location = new System.Drawing.Point(101, 77);
            this.TB_Formula.Multiline = true;
            this.TB_Formula.Name = "TB_Formula";
            this.TB_Formula.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_Formula.Size = new System.Drawing.Size(345, 120);
            this.TB_Formula.TabIndex = 3;
            this.TB_Formula.Text = "0";
            // 
            // BT_Formula
            // 
            this.BT_Formula.Location = new System.Drawing.Point(101, 203);
            this.BT_Formula.Name = "BT_Formula";
            this.BT_Formula.Size = new System.Drawing.Size(88, 23);
            this.BT_Formula.TabIndex = 4;
            this.BT_Formula.Text = "公式编辑器";
            this.BT_Formula.UseVisualStyleBackColor = true;
            this.BT_Formula.Click += new System.EventHandler(this.BT_Path_Click);
            // 
            // BT_OK
            // 
            this.BT_OK.Location = new System.Drawing.Point(289, 203);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(70, 23);
            this.BT_OK.TabIndex = 5;
            this.BT_OK.Text = "确定";
            this.BT_OK.UseVisualStyleBackColor = true;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.Location = new System.Drawing.Point(376, 203);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(70, 23);
            this.BT_Cancel.TabIndex = 6;
            this.BT_Cancel.Text = "取消";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
            // 
            // TB_Name
            // 
            this.TB_Name.Location = new System.Drawing.Point(101, 19);
            this.TB_Name.Name = "TB_Name";
            this.TB_Name.Size = new System.Drawing.Size(136, 23);
            this.TB_Name.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "参数名：";
            // 
            // ParameterEditForm
            // 
            this.AcceptButton = this.BT_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 246);
            this.Controls.Add(this.TB_Name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.BT_Formula);
            this.Controls.Add(this.TB_Formula);
            this.Controls.Add(this.TB_Discription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ParameterEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.SystemSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Discription;
        private System.Windows.Forms.TextBox TB_Formula;
        private System.Windows.Forms.Button BT_Formula;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.TextBox TB_Name;
        private System.Windows.Forms.Label label3;
    }
}