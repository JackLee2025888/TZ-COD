
namespace TZ.TZForm
{
    partial class ConditionSelectForm
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
            this.LV = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Key = new System.Windows.Forms.TextBox();
            this.BT_Search = new System.Windows.Forms.Button();
            this.BT_Close = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LV
            // 
            this.LV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.LV.FullRowSelect = true;
            this.LV.GridLines = true;
            this.LV.HideSelection = false;
            this.LV.Location = new System.Drawing.Point(12, 49);
            this.LV.MultiSelect = false;
            this.LV.Name = "LV";
            this.LV.Size = new System.Drawing.Size(584, 167);
            this.LV.TabIndex = 0;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.View = System.Windows.Forms.View.Details;
            this.LV.DoubleClick += new System.EventHandler(this.LV_DoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "条件名称";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "公式";
            this.columnHeader3.Width = 400;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "搜索：";
            // 
            // TB_Key
            // 
            this.TB_Key.Location = new System.Drawing.Point(62, 15);
            this.TB_Key.Name = "TB_Key";
            this.TB_Key.Size = new System.Drawing.Size(475, 23);
            this.TB_Key.TabIndex = 2;
            this.TB_Key.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Key_KeyDown);
            // 
            // BT_Search
            // 
            this.BT_Search.Location = new System.Drawing.Point(543, 15);
            this.BT_Search.Name = "BT_Search";
            this.BT_Search.Size = new System.Drawing.Size(53, 23);
            this.BT_Search.TabIndex = 3;
            this.BT_Search.Text = "搜索";
            this.BT_Search.UseVisualStyleBackColor = true;
            this.BT_Search.Click += new System.EventHandler(this.BT_Search_Click);
            // 
            // BT_Close
            // 
            this.BT_Close.Location = new System.Drawing.Point(523, 222);
            this.BT_Close.Name = "BT_Close";
            this.BT_Close.Size = new System.Drawing.Size(73, 23);
            this.BT_Close.TabIndex = 7;
            this.BT_Close.Text = "取消";
            this.BT_Close.UseVisualStyleBackColor = true;
            this.BT_Close.Click += new System.EventHandler(this.BT_Close_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(444, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "选择";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConditionSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 258);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BT_Close);
            this.Controls.Add(this.BT_Search);
            this.Controls.Add(this.TB_Key);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ConditionSelectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "条件选择";
            this.Load += new System.EventHandler(this.ConditionListForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LV;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Key;
        private System.Windows.Forms.Button BT_Search;
        private System.Windows.Forms.Button BT_Close;
        private System.Windows.Forms.Button button1;
    }
}