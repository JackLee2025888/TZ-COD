
namespace TZ.TZForm
{
    partial class GroupEditForm
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
            this.TB_Name = new System.Windows.Forms.TextBox();
            this.BT_New = new System.Windows.Forms.Button();
            this.BT_Delete = new System.Windows.Forms.Button();
            this.BT_Close = new System.Windows.Forms.Button();
            this.LV = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Formula = new System.Windows.Forms.TextBox();
            this.TB_All = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_Logic = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BT_OK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_Type = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "条件组名称：";
            // 
            // TB_Name
            // 
            this.TB_Name.Location = new System.Drawing.Point(99, 47);
            this.TB_Name.Name = "TB_Name";
            this.TB_Name.Size = new System.Drawing.Size(498, 23);
            this.TB_Name.TabIndex = 2;
            // 
            // BT_New
            // 
            this.BT_New.Location = new System.Drawing.Point(101, 285);
            this.BT_New.Name = "BT_New";
            this.BT_New.Size = new System.Drawing.Size(53, 23);
            this.BT_New.TabIndex = 4;
            this.BT_New.Text = "添加";
            this.BT_New.UseVisualStyleBackColor = true;
            this.BT_New.Click += new System.EventHandler(this.BT_New_Click);
            // 
            // BT_Delete
            // 
            this.BT_Delete.Enabled = false;
            this.BT_Delete.Location = new System.Drawing.Point(160, 285);
            this.BT_Delete.Name = "BT_Delete";
            this.BT_Delete.Size = new System.Drawing.Size(53, 23);
            this.BT_Delete.TabIndex = 6;
            this.BT_Delete.Text = "删除";
            this.BT_Delete.UseVisualStyleBackColor = true;
            this.BT_Delete.Click += new System.EventHandler(this.BT_Delete_Click);
            // 
            // BT_Close
            // 
            this.BT_Close.Location = new System.Drawing.Point(518, 285);
            this.BT_Close.Name = "BT_Close";
            this.BT_Close.Size = new System.Drawing.Size(79, 23);
            this.BT_Close.TabIndex = 7;
            this.BT_Close.Text = "取消";
            this.BT_Close.UseVisualStyleBackColor = true;
            this.BT_Close.Click += new System.EventHandler(this.BT_Close_Click);
            // 
            // LV
            // 
            this.LV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.LV.FullRowSelect = true;
            this.LV.GridLines = true;
            this.LV.HideSelection = false;
            this.LV.Location = new System.Drawing.Point(99, 107);
            this.LV.MultiSelect = false;
            this.LV.Name = "LV";
            this.LV.Size = new System.Drawing.Size(498, 172);
            this.LV.TabIndex = 8;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.View = System.Windows.Forms.View.Details;
            this.LV.SelectedIndexChanged += new System.EventHandler(this.LV_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "条件名称";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "公式";
            this.columnHeader3.Width = 320;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "触发条件：";
            // 
            // TB_Formula
            // 
            this.TB_Formula.Location = new System.Drawing.Point(99, 78);
            this.TB_Formula.Name = "TB_Formula";
            this.TB_Formula.Size = new System.Drawing.Size(409, 23);
            this.TB_Formula.TabIndex = 10;
            // 
            // TB_All
            // 
            this.TB_All.AutoSize = true;
            this.TB_All.Location = new System.Drawing.Point(522, 80);
            this.TB_All.Name = "TB_All";
            this.TB_All.Size = new System.Drawing.Size(75, 21);
            this.TB_All.TabIndex = 11;
            this.TB_All.Text = "始终触发";
            this.TB_All.UseVisualStyleBackColor = true;
            this.TB_All.CheckedChanged += new System.EventHandler(this.TB_All_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(394, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "条件组关系：";
            // 
            // TB_Logic
            // 
            this.TB_Logic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TB_Logic.FormattingEnabled = true;
            this.TB_Logic.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.TB_Logic.Location = new System.Drawing.Point(480, 16);
            this.TB_Logic.Name = "TB_Logic";
            this.TB_Logic.Size = new System.Drawing.Size(116, 25);
            this.TB_Logic.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "条件列表：";
            // 
            // BT_OK
            // 
            this.BT_OK.Location = new System.Drawing.Point(429, 285);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(79, 23);
            this.BT_OK.TabIndex = 15;
            this.BT_OK.Text = "确定";
            this.BT_OK.UseVisualStyleBackColor = true;
            this.BT_OK.Click += new System.EventHandler(this.BT_OK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "条件组类型：";
            // 
            // TB_Type
            // 
            this.TB_Type.Location = new System.Drawing.Point(99, 16);
            this.TB_Type.Name = "TB_Type";
            this.TB_Type.ReadOnly = true;
            this.TB_Type.Size = new System.Drawing.Size(289, 23);
            this.TB_Type.TabIndex = 17;
            // 
            // GroupEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 324);
            this.Controls.Add(this.TB_Type);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_Logic);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_All);
            this.Controls.Add(this.TB_Formula);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LV);
            this.Controls.Add(this.BT_Close);
            this.Controls.Add(this.BT_Delete);
            this.Controls.Add(this.BT_New);
            this.Controls.Add(this.TB_Name);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GroupEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "条件组编辑";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Name;
        private System.Windows.Forms.Button BT_New;
        private System.Windows.Forms.Button BT_Delete;
        private System.Windows.Forms.Button BT_Close;
        private System.Windows.Forms.ListView LV;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Formula;
        private System.Windows.Forms.CheckBox TB_All;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TB_Logic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_Type;
    }
}