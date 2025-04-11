namespace Mr1Ceng.Tools.DataTransfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            splitContainer1 = new SplitContainer();
            txtSQL = new TextBox();
            panel1 = new Panel();
            chkTable = new CheckBox();
            label1 = new Label();
            btnQuery = new Button();
            cmbSourceDB = new ComboBox();
            dataGridView1 = new DataGridView();
            panel3 = new Panel();
            lblTips = new Label();
            panel2 = new Panel();
            btnExec = new Button();
            chkTruncate = new CheckBox();
            txtTableName = new TextBox();
            label3 = new Label();
            cmbTargetDB = new ComboBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(txtSQL);
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView1);
            splitContainer1.Panel2.Controls.Add(panel3);
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Size = new Size(2374, 1129);
            splitContainer1.SplitterDistance = 802;
            splitContainer1.TabIndex = 0;
            // 
            // txtSQL
            // 
            txtSQL.Dock = DockStyle.Fill;
            txtSQL.ImeMode = ImeMode.Disable;
            txtSQL.Location = new Point(0, 80);
            txtSQL.Multiline = true;
            txtSQL.Name = "txtSQL";
            txtSQL.Size = new Size(802, 1049);
            txtSQL.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(chkTable);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnQuery);
            panel1.Controls.Add(cmbSourceDB);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(802, 80);
            panel1.TabIndex = 0;
            // 
            // chkTable
            // 
            chkTable.AutoSize = true;
            chkTable.Location = new Point(495, 23);
            chkTable.Name = "chkTable";
            chkTable.Size = new Size(70, 35);
            chkTable.TabIndex = 3;
            chkTable.Text = "表";
            chkTable.UseVisualStyleBackColor = true;
            chkTable.CheckedChanged += chkTable_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 25);
            label1.Name = "label1";
            label1.Size = new Size(38, 31);
            label1.TabIndex = 2;
            label1.Text = "源";
            // 
            // btnQuery
            // 
            btnQuery.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuery.Location = new Point(633, 17);
            btnQuery.Name = "btnQuery";
            btnQuery.Size = new Size(150, 46);
            btnQuery.TabIndex = 1;
            btnQuery.Text = "查询";
            btnQuery.UseVisualStyleBackColor = true;
            btnQuery.Click += btnQuery_Click;
            // 
            // cmbSourceDB
            // 
            cmbSourceDB.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSourceDB.FormattingEnabled = true;
            cmbSourceDB.Location = new Point(74, 21);
            cmbSourceDB.Name = "cmbSourceDB";
            cmbSourceDB.Size = new Size(400, 39);
            cmbSourceDB.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 80);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.Size = new Size(1568, 989);
            dataGridView1.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblTips);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 1069);
            panel3.Name = "panel3";
            panel3.Size = new Size(1568, 60);
            panel3.TabIndex = 1;
            // 
            // lblTips
            // 
            lblTips.AutoSize = true;
            lblTips.Location = new Point(40, 20);
            lblTips.Name = "lblTips";
            lblTips.Size = new Size(0, 31);
            lblTips.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnExec);
            panel2.Controls.Add(chkTruncate);
            panel2.Controls.Add(txtTableName);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(cmbTargetDB);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1568, 80);
            panel2.TabIndex = 0;
            // 
            // btnExec
            // 
            btnExec.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExec.Location = new Point(1406, 17);
            btnExec.Name = "btnExec";
            btnExec.Size = new Size(150, 46);
            btnExec.TabIndex = 5;
            btnExec.Text = "执行";
            btnExec.UseVisualStyleBackColor = true;
            btnExec.Click += btnExec_Click;
            // 
            // chkTruncate
            // 
            chkTruncate.AutoSize = true;
            chkTruncate.Location = new Point(1209, 23);
            chkTruncate.Name = "chkTruncate";
            chkTruncate.Size = new Size(174, 35);
            chkTruncate.TabIndex = 4;
            chkTruncate.Text = "TRUNCATE";
            chkTruncate.UseVisualStyleBackColor = true;
            // 
            // txtTableName
            // 
            txtTableName.ImeMode = ImeMode.Disable;
            txtTableName.Location = new Point(566, 21);
            txtTableName.Name = "txtTableName";
            txtTableName.Size = new Size(500, 38);
            txtTableName.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(510, 25);
            label3.Name = "label3";
            label3.Size = new Size(38, 31);
            label3.TabIndex = 2;
            label3.Text = "表";
            // 
            // cmbTargetDB
            // 
            cmbTargetDB.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTargetDB.FormattingEnabled = true;
            cmbTargetDB.Location = new Point(79, 21);
            cmbTargetDB.Name = "cmbTargetDB";
            cmbTargetDB.Size = new Size(400, 39);
            cmbTargetDB.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 25);
            label2.Name = "label2";
            label2.Size = new Size(38, 31);
            label2.TabIndex = 0;
            label2.Text = "目";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2374, 1129);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(2400, 1200);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "数据迁移工具";
            Load += FrmMain_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel panel1;
        private Button btnQuery;
        private ComboBox cmbSourceDB;
        private Label label1;
        private Panel panel2;
        private ComboBox cmbTargetDB;
        private Label label2;
        private CheckBox chkTruncate;
        private TextBox txtTableName;
        private Label label3;
        private Button btnExec;
        private DataGridView dataGridView1;
        private Panel panel3;
        private Label lblTips;
        private TextBox txtSQL;
        private CheckBox chkTable;
    }
}