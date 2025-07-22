
namespace TZ.TZForm
{
    partial class SystemParameterSettingListForm
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
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Key = new System.Windows.Forms.TextBox();
            this.BT_Search = new System.Windows.Forms.Button();
            this.BT_New = new System.Windows.Forms.Button();
            this.BT_Edit = new System.Windows.Forms.Button();
            this.BT_Delete = new System.Windows.Forms.Button();
            this.BT_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LV
            // 
            this.LV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.LV.FullRowSelect = true;
            this.LV.GridLines = true;
            this.LV.HideSelection = false;
            this.LV.Location = new System.Drawing.Point(12, 49);
            this.LV.MultiSelect = false;
            this.LV.Name = "LV";
            this.LV.Size = new System.Drawing.Size(368, 333);
            this.LV.TabIndex = 0;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.View = System.Windows.Forms.View.Details;
            this.LV.SelectedIndexChanged += new System.EventHandler(this.LV_SelectedIndexChanged);
            this.LV.DoubleClick += new System.EventHandler(this.LV_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "指标名称";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "指标类型";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "参数";
            this.columnHeader3.Width = 150;
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
            this.TB_Key.Size = new System.Drawing.Size(259, 23);
            this.TB_Key.TabIndex = 2;
            this.TB_Key.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Key_KeyDown);
            // 
            // BT_Search
            // 
            this.BT_Search.Location = new System.Drawing.Point(327, 15);
            this.BT_Search.Name = "BT_Search";
            this.BT_Search.Size = new System.Drawing.Size(53, 23);
            this.BT_Search.TabIndex = 3;
            this.BT_Search.Text = "搜索";
            this.BT_Search.UseVisualStyleBackColor = true;
            this.BT_Search.Click += new System.EventHandler(this.BT_Search_Click);
            // 
            // BT_New
            // 
            this.BT_New.Location = new System.Drawing.Point(15, 388);
            this.BT_New.Name = "BT_New";
            this.BT_New.Size = new System.Drawing.Size(53, 23);
            this.BT_New.TabIndex = 4;
            this.BT_New.Text = "新建";
            this.BT_New.UseVisualStyleBackColor = true;
            this.BT_New.Click += new System.EventHandler(this.BT_New_Click);
            // 
            // BT_Edit
            // 
            this.BT_Edit.Enabled = false;
            this.BT_Edit.Location = new System.Drawing.Point(74, 388);
            this.BT_Edit.Name = "BT_Edit";
            this.BT_Edit.Size = new System.Drawing.Size(53, 23);
            this.BT_Edit.TabIndex = 5;
            this.BT_Edit.Text = "修改";
            this.BT_Edit.UseVisualStyleBackColor = true;
            this.BT_Edit.Click += new System.EventHandler(this.BT_Edit_Click);
            // 
            // BT_Delete
            // 
            this.BT_Delete.Enabled = false;
            this.BT_Delete.Location = new System.Drawing.Point(133, 388);
            this.BT_Delete.Name = "BT_Delete";
            this.BT_Delete.Size = new System.Drawing.Size(53, 23);
            this.BT_Delete.TabIndex = 6;
            this.BT_Delete.Text = "删除";
            this.BT_Delete.UseVisualStyleBackColor = true;
            this.BT_Delete.Click += new System.EventHandler(this.BT_Delete_Click);
            // 
            // BT_Close
            // 
            this.BT_Close.Location = new System.Drawing.Point(285, 388);
            this.BT_Close.Name = "BT_Close";
            this.BT_Close.Size = new System.Drawing.Size(95, 23);
            this.BT_Close.TabIndex = 7;
            this.BT_Close.Text = "关闭";
            this.BT_Close.UseVisualStyleBackColor = true;
            this.BT_Close.Click += new System.EventHandler(this.BT_Close_Click);
            // 
            // SystemParameterSettingListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 423);
            this.Controls.Add(this.BT_Close);
            this.Controls.Add(this.BT_Delete);
            this.Controls.Add(this.BT_Edit);
            this.Controls.Add(this.BT_New);
            this.Controls.Add(this.BT_Search);
            this.Controls.Add(this.TB_Key);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SystemParameterSettingListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "指标管理器";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LV;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Key;
        private System.Windows.Forms.Button BT_Search;
        private System.Windows.Forms.Button BT_New;
        private System.Windows.Forms.Button BT_Edit;
        private System.Windows.Forms.Button BT_Delete;
        private System.Windows.Forms.Button BT_Close;
    }
}