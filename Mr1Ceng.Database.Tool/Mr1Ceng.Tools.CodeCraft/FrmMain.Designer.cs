using Mr1Ceng.Tools.CodeCraft.Services;

namespace Mr1Ceng.Tools.CodeCraft
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            flowInfoBindingSource = new BindingSource(components);
            chkEditable = new CheckBox();
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            btnRemove = new Button();
            btnCopy = new Button();
            btnSave = new Button();
            txtOutput = new TextBox();
            panel3 = new Panel();
            BtnClipCopyLink = new Button();
            linkUrl = new LinkLabel();
            panel2 = new Panel();
            BtnClipCopyCode = new Button();
            btnHelpTemplate = new Button();
            btnReadTemplate = new Button();
            btnSaveTemplate = new Button();
            ((System.ComponentModel.ISupportInitialize)flowInfoBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // chkEditable
            // 
            chkEditable.AutoSize = true;
            chkEditable.Location = new Point(32, 21);
            chkEditable.Name = "chkEditable";
            chkEditable.Size = new Size(142, 35);
            chkEditable.TabIndex = 5;
            chkEditable.Text = "启用编辑";
            chkEditable.UseVisualStyleBackColor = true;
            chkEditable.CheckedChanged += chkEditable_CheckedChanged;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(txtOutput);
            splitContainer1.Panel2.Controls.Add(panel3);
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Size = new Size(1774, 1129);
            splitContainer1.SplitterDistance = 1030;
            splitContainer1.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 80);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.Size = new Size(1030, 1049);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRemove);
            panel1.Controls.Add(btnCopy);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(chkEditable);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1030, 80);
            panel1.TabIndex = 0;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(384, 15);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(150, 46);
            btnRemove.TabIndex = 7;
            btnRemove.Text = "删除选中行";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnCopy
            // 
            btnCopy.Location = new Point(205, 15);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(150, 46);
            btnCopy.TabIndex = 6;
            btnCopy.Text = "复制选中行";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(857, 15);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 46);
            btnSave.TabIndex = 3;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Visible = false;
            btnSave.Click += btnSave_Click;
            // 
            // txtOutput
            // 
            txtOutput.Dock = DockStyle.Fill;
            txtOutput.Location = new Point(0, 80);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ScrollBars = ScrollBars.Both;
            txtOutput.Size = new Size(740, 956);
            txtOutput.TabIndex = 3;
            // 
            // panel3
            // 
            panel3.Controls.Add(BtnClipCopyLink);
            panel3.Controls.Add(linkUrl);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 1036);
            panel3.Name = "panel3";
            panel3.Size = new Size(740, 93);
            panel3.TabIndex = 2;
            // 
            // BtnClipCopyLink
            // 
            BtnClipCopyLink.Location = new Point(17, 22);
            BtnClipCopyLink.Name = "BtnClipCopyLink";
            BtnClipCopyLink.Size = new Size(150, 46);
            BtnClipCopyLink.TabIndex = 2;
            BtnClipCopyLink.Text = "复制链接";
            BtnClipCopyLink.UseVisualStyleBackColor = true;
            BtnClipCopyLink.Click += BtnClipCopyLink_Click;
            // 
            // linkUrl
            // 
            linkUrl.AutoSize = true;
            linkUrl.Location = new Point(190, 30);
            linkUrl.Name = "linkUrl";
            linkUrl.Size = new Size(0, 31);
            linkUrl.TabIndex = 1;
            linkUrl.LinkClicked += linkUrl_LinkClicked;
            // 
            // panel2
            // 
            panel2.Controls.Add(BtnClipCopyCode);
            panel2.Controls.Add(btnHelpTemplate);
            panel2.Controls.Add(btnReadTemplate);
            panel2.Controls.Add(btnSaveTemplate);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(740, 80);
            panel2.TabIndex = 0;
            // 
            // BtnClipCopyCode
            // 
            BtnClipCopyCode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnClipCopyCode.Location = new Point(569, 15);
            BtnClipCopyCode.Name = "BtnClipCopyCode";
            BtnClipCopyCode.Size = new Size(150, 46);
            BtnClipCopyCode.TabIndex = 10;
            BtnClipCopyCode.Text = "复制正文";
            BtnClipCopyCode.UseVisualStyleBackColor = true;
            BtnClipCopyCode.Click += BtnClipCopyCode_Click;
            // 
            // btnHelpTemplate
            // 
            btnHelpTemplate.Location = new Point(17, 15);
            btnHelpTemplate.Name = "btnHelpTemplate";
            btnHelpTemplate.Size = new Size(150, 46);
            btnHelpTemplate.TabIndex = 9;
            btnHelpTemplate.Text = "模板说明";
            btnHelpTemplate.UseVisualStyleBackColor = true;
            btnHelpTemplate.Click += btnHelpTemplate_Click;
            // 
            // btnReadTemplate
            // 
            btnReadTemplate.Location = new Point(190, 14);
            btnReadTemplate.Name = "btnReadTemplate";
            btnReadTemplate.Size = new Size(150, 46);
            btnReadTemplate.TabIndex = 8;
            btnReadTemplate.Text = "加载模板";
            btnReadTemplate.UseVisualStyleBackColor = true;
            btnReadTemplate.Click += btnReadTemplate_Click;
            // 
            // btnSaveTemplate
            // 
            btnSaveTemplate.Location = new Point(360, 15);
            btnSaveTemplate.Name = "btnSaveTemplate";
            btnSaveTemplate.Size = new Size(150, 46);
            btnSaveTemplate.TabIndex = 7;
            btnSaveTemplate.Text = "保存模板";
            btnSaveTemplate.UseVisualStyleBackColor = true;
            btnSaveTemplate.Click += btnSaveTemplate_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1774, 1129);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "代码生成工具";
            Load += FrmMain_Load;
            ((System.ComponentModel.ISupportInitialize)flowInfoBindingSource).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private BindingSource flowInfoBindingSource;
        private CheckBox chkEditable;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private DataGridViewTextBoxColumn environmentDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameSpaceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn assemblyIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn serviceNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn podCountDataGridViewTextBoxColumn;
        private Button btnSaveTemplate;
        private Panel panel2;
        private Button btnReadTemplate;
        private Button btnHelpTemplate;
        private Button btnSave;
        private DataGridView dataGridView1;
        private Button btnCopy;
        private Button btnRemove;
        private Panel panel3;
        private TextBox txtOutput;
        private LinkLabel linkUrl;
        private Button BtnClipCopyLink;
        private Button BtnClipCopyCode;
    }
}