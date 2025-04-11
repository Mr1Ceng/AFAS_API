namespace Mr1Ceng.Tools.ORMapping.V7
{
    partial class FrmPersistenceEntity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPersistenceEntity));
            folderBrowserDialog1 = new FolderBrowserDialog();
            treeView1 = new TreeView();
            GroupBoxConnection = new GroupBox();
            chkAllowView = new CheckBox();
            BtnConnect = new Button();
            TxtLoginID = new TextBox();
            TxtPassword = new TextBox();
            TxtCatalog = new TextBox();
            TxtServer = new TextBox();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            GroupBoxCustomer = new GroupBox();
            RadioInternal = new RadioButton();
            RadioPublic = new RadioButton();
            BtnEncrypt = new Button();
            TxtEncrypt = new TextBox();
            label1 = new Label();
            TxtDataSource = new TextBox();
            TxtNameSpace = new TextBox();
            label2 = new Label();
            label5 = new Label();
            GroupBoxCommon = new GroupBox();
            chkConfig = new CheckBox();
            chkEntity = new CheckBox();
            BtnExport = new Button();
            BtnFileDlg = new Button();
            TxtOutputDir = new TextBox();
            label3 = new Label();
            TxtOutputDirectory = new Label();
            TxtORMappingName = new TextBox();
            GroupBoxConnection.SuspendLayout();
            GroupBoxCustomer.SuspendLayout();
            GroupBoxCommon.SuspendLayout();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.Location = new Point(36, 38);
            treeView1.Margin = new Padding(8, 7, 8, 7);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(772, 1089);
            treeView1.TabIndex = 0;
            treeView1.AfterCheck += treeView1_AfterCheck;
            // 
            // GroupBoxConnection
            // 
            GroupBoxConnection.Controls.Add(chkAllowView);
            GroupBoxConnection.Controls.Add(BtnConnect);
            GroupBoxConnection.Controls.Add(TxtLoginID);
            GroupBoxConnection.Controls.Add(TxtPassword);
            GroupBoxConnection.Controls.Add(TxtCatalog);
            GroupBoxConnection.Controls.Add(TxtServer);
            GroupBoxConnection.Controls.Add(label11);
            GroupBoxConnection.Controls.Add(label12);
            GroupBoxConnection.Controls.Add(label13);
            GroupBoxConnection.Controls.Add(label14);
            GroupBoxConnection.Location = new Point(884, 38);
            GroupBoxConnection.Margin = new Padding(8, 7, 8, 7);
            GroupBoxConnection.Name = "GroupBoxConnection";
            GroupBoxConnection.Padding = new Padding(8, 7, 8, 7);
            GroupBoxConnection.Size = new Size(1042, 396);
            GroupBoxConnection.TabIndex = 0;
            GroupBoxConnection.TabStop = false;
            GroupBoxConnection.Text = "数据库连接";
            // 
            // chkAllowView
            // 
            chkAllowView.AutoSize = true;
            chkAllowView.Location = new Point(356, 321);
            chkAllowView.Margin = new Padding(6, 5, 6, 5);
            chkAllowView.Name = "chkAllowView";
            chkAllowView.Size = new Size(142, 35);
            chkAllowView.TabIndex = 9;
            chkAllowView.Text = "加载视图";
            chkAllowView.UseVisualStyleBackColor = true;
            // 
            // BtnConnect
            // 
            BtnConnect.Location = new Point(828, 306);
            BtnConnect.Margin = new Padding(8, 7, 8, 7);
            BtnConnect.Name = "BtnConnect";
            BtnConnect.Size = new Size(176, 55);
            BtnConnect.TabIndex = 10;
            BtnConnect.Text = "Connect";
            BtnConnect.UseVisualStyleBackColor = true;
            BtnConnect.Click += BtnConnect_Click;
            // 
            // TxtLoginID
            // 
            TxtLoginID.Location = new Point(350, 184);
            TxtLoginID.Margin = new Padding(8, 7, 8, 7);
            TxtLoginID.Name = "TxtLoginID";
            TxtLoginID.Size = new Size(648, 38);
            TxtLoginID.TabIndex = 6;
            // 
            // TxtPassword
            // 
            TxtPassword.Location = new Point(350, 248);
            TxtPassword.Margin = new Padding(8, 7, 8, 7);
            TxtPassword.Name = "TxtPassword";
            TxtPassword.Size = new Size(648, 38);
            TxtPassword.TabIndex = 8;
            // 
            // TxtCatalog
            // 
            TxtCatalog.Location = new Point(350, 120);
            TxtCatalog.Margin = new Padding(8, 7, 8, 7);
            TxtCatalog.Name = "TxtCatalog";
            TxtCatalog.Size = new Size(648, 38);
            TxtCatalog.TabIndex = 4;
            // 
            // TxtServer
            // 
            TxtServer.Location = new Point(350, 55);
            TxtServer.Margin = new Padding(8, 7, 8, 7);
            TxtServer.Name = "TxtServer";
            TxtServer.Size = new Size(648, 38);
            TxtServer.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(64, 259);
            label11.Margin = new Padding(8, 0, 8, 0);
            label11.Name = "label11";
            label11.Size = new Size(62, 31);
            label11.TabIndex = 7;
            label11.Text = "密码";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(64, 195);
            label12.Margin = new Padding(8, 0, 8, 0);
            label12.Name = "label12";
            label12.Size = new Size(62, 31);
            label12.TabIndex = 5;
            label12.Text = "账号";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(64, 129);
            label13.Margin = new Padding(8, 0, 8, 0);
            label13.Name = "label13";
            label13.Size = new Size(134, 31);
            label13.TabIndex = 3;
            label13.Text = "数据库名称";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(64, 66);
            label14.Margin = new Padding(8, 0, 8, 0);
            label14.Name = "label14";
            label14.Size = new Size(110, 31);
            label14.TabIndex = 1;
            label14.Text = "数据实例";
            // 
            // GroupBoxCustomer
            // 
            GroupBoxCustomer.Controls.Add(RadioInternal);
            GroupBoxCustomer.Controls.Add(RadioPublic);
            GroupBoxCustomer.Controls.Add(BtnEncrypt);
            GroupBoxCustomer.Controls.Add(TxtEncrypt);
            GroupBoxCustomer.Controls.Add(label1);
            GroupBoxCustomer.Controls.Add(TxtDataSource);
            GroupBoxCustomer.Controls.Add(TxtNameSpace);
            GroupBoxCustomer.Controls.Add(label2);
            GroupBoxCustomer.Controls.Add(label5);
            GroupBoxCustomer.Location = new Point(884, 469);
            GroupBoxCustomer.Margin = new Padding(8, 7, 8, 7);
            GroupBoxCustomer.Name = "GroupBoxCustomer";
            GroupBoxCustomer.Padding = new Padding(8, 7, 8, 7);
            GroupBoxCustomer.Size = new Size(1042, 343);
            GroupBoxCustomer.TabIndex = 11;
            GroupBoxCustomer.TabStop = false;
            GroupBoxCustomer.Text = "C#代码配置信息";
            // 
            // RadioInternal
            // 
            RadioInternal.AutoSize = true;
            RadioInternal.Location = new Point(500, 272);
            RadioInternal.Margin = new Padding(6, 5, 6, 5);
            RadioInternal.Name = "RadioInternal";
            RadioInternal.Size = new Size(132, 35);
            RadioInternal.TabIndex = 29;
            RadioInternal.Tag = "Function";
            RadioInternal.Text = "internal";
            RadioInternal.UseVisualStyleBackColor = true;
            // 
            // RadioPublic
            // 
            RadioPublic.AutoSize = true;
            RadioPublic.Checked = true;
            RadioPublic.Location = new Point(352, 272);
            RadioPublic.Margin = new Padding(6, 5, 6, 5);
            RadioPublic.Name = "RadioPublic";
            RadioPublic.Size = new Size(114, 35);
            RadioPublic.TabIndex = 28;
            RadioPublic.TabStop = true;
            RadioPublic.Tag = "Function";
            RadioPublic.Text = "public";
            RadioPublic.UseVisualStyleBackColor = true;
            // 
            // BtnEncrypt
            // 
            BtnEncrypt.Location = new Point(900, 49);
            BtnEncrypt.Margin = new Padding(8, 7, 8, 7);
            BtnEncrypt.Name = "BtnEncrypt";
            BtnEncrypt.Size = new Size(104, 60);
            BtnEncrypt.TabIndex = 23;
            BtnEncrypt.TabStop = false;
            BtnEncrypt.Text = "Get";
            BtnEncrypt.UseVisualStyleBackColor = true;
            BtnEncrypt.Click += BtnEncrypt_Click;
            // 
            // TxtEncrypt
            // 
            TxtEncrypt.Location = new Point(350, 58);
            TxtEncrypt.Margin = new Padding(8, 7, 8, 7);
            TxtEncrypt.Name = "TxtEncrypt";
            TxtEncrypt.Size = new Size(532, 38);
            TxtEncrypt.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(64, 67);
            label1.Margin = new Padding(8, 0, 8, 0);
            label1.Name = "label1";
            label1.Size = new Size(134, 31);
            label1.TabIndex = 21;
            label1.Text = "密码加密串";
            // 
            // TxtDataSource
            // 
            TxtDataSource.Location = new Point(350, 128);
            TxtDataSource.Margin = new Padding(8, 7, 8, 7);
            TxtDataSource.Name = "TxtDataSource";
            TxtDataSource.Size = new Size(648, 38);
            TxtDataSource.TabIndex = 25;
            // 
            // TxtNameSpace
            // 
            TxtNameSpace.Location = new Point(350, 195);
            TxtNameSpace.Margin = new Padding(8, 7, 8, 7);
            TxtNameSpace.Name = "TxtNameSpace";
            TxtNameSpace.Size = new Size(648, 38);
            TxtNameSpace.TabIndex = 27;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(64, 139);
            label2.Margin = new Padding(8, 0, 8, 0);
            label2.Name = "label2";
            label2.Size = new Size(134, 31);
            label2.TabIndex = 24;
            label2.Text = "数据源名称";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(64, 206);
            label5.Margin = new Padding(8, 0, 8, 0);
            label5.Name = "label5";
            label5.Size = new Size(110, 31);
            label5.TabIndex = 26;
            label5.Text = "命名空间";
            // 
            // GroupBoxCommon
            // 
            GroupBoxCommon.Controls.Add(chkConfig);
            GroupBoxCommon.Controls.Add(chkEntity);
            GroupBoxCommon.Controls.Add(BtnExport);
            GroupBoxCommon.Controls.Add(BtnFileDlg);
            GroupBoxCommon.Controls.Add(TxtOutputDir);
            GroupBoxCommon.Controls.Add(label3);
            GroupBoxCommon.Controls.Add(TxtOutputDirectory);
            GroupBoxCommon.Controls.Add(TxtORMappingName);
            GroupBoxCommon.Location = new Point(884, 848);
            GroupBoxCommon.Margin = new Padding(8, 7, 8, 7);
            GroupBoxCommon.Name = "GroupBoxCommon";
            GroupBoxCommon.Padding = new Padding(8, 7, 8, 7);
            GroupBoxCommon.Size = new Size(1042, 283);
            GroupBoxCommon.TabIndex = 40;
            GroupBoxCommon.TabStop = false;
            GroupBoxCommon.Text = "输出";
            // 
            // chkConfig
            // 
            chkConfig.AutoSize = true;
            chkConfig.Checked = true;
            chkConfig.CheckState = CheckState.Checked;
            chkConfig.Location = new Point(376, 212);
            chkConfig.Margin = new Padding(6, 5, 6, 5);
            chkConfig.Name = "chkConfig";
            chkConfig.Size = new Size(232, 35);
            chkConfig.TabIndex = 47;
            chkConfig.Text = "appsettings.json";
            chkConfig.UseVisualStyleBackColor = true;
            // 
            // chkEntity
            // 
            chkEntity.AutoSize = true;
            chkEntity.Checked = true;
            chkEntity.CheckState = CheckState.Checked;
            chkEntity.Location = new Point(44, 212);
            chkEntity.Margin = new Padding(6, 5, 6, 5);
            chkEntity.Name = "chkEntity";
            chkEntity.Size = new Size(266, 35);
            chkEntity.TabIndex = 46;
            chkEntity.Text = "Include Entity Class";
            chkEntity.UseVisualStyleBackColor = true;
            // 
            // BtnExport
            // 
            BtnExport.Location = new Point(828, 191);
            BtnExport.Margin = new Padding(8, 7, 8, 7);
            BtnExport.Name = "BtnExport";
            BtnExport.Size = new Size(176, 55);
            BtnExport.TabIndex = 45;
            BtnExport.Text = "Export";
            BtnExport.UseVisualStyleBackColor = true;
            BtnExport.Click += BtnExport_Click;
            // 
            // BtnFileDlg
            // 
            BtnFileDlg.Location = new Point(944, 122);
            BtnFileDlg.Margin = new Padding(8, 7, 8, 7);
            BtnFileDlg.Name = "BtnFileDlg";
            BtnFileDlg.Size = new Size(58, 55);
            BtnFileDlg.TabIndex = 44;
            BtnFileDlg.TabStop = false;
            BtnFileDlg.Text = "...";
            BtnFileDlg.UseVisualStyleBackColor = true;
            BtnFileDlg.Click += BtnFileDlg_Click;
            // 
            // TxtOutputDir
            // 
            TxtOutputDir.Location = new Point(350, 129);
            TxtOutputDir.Margin = new Padding(8, 7, 8, 7);
            TxtOutputDir.Name = "TxtOutputDir";
            TxtOutputDir.Size = new Size(578, 38);
            TxtOutputDir.TabIndex = 43;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 62);
            label3.Margin = new Padding(8, 0, 8, 0);
            label3.Name = "label3";
            label3.Size = new Size(224, 31);
            label3.TabIndex = 3;
            label3.Text = "ORMapping文件名";
            // 
            // TxtOutputDirectory
            // 
            TxtOutputDirectory.AutoSize = true;
            TxtOutputDirectory.Location = new Point(44, 140);
            TxtOutputDirectory.Margin = new Padding(8, 0, 8, 0);
            TxtOutputDirectory.Name = "TxtOutputDirectory";
            TxtOutputDirectory.Size = new Size(134, 31);
            TxtOutputDirectory.TabIndex = 42;
            TxtOutputDirectory.Text = "输出文件夹";
            // 
            // TxtORMappingName
            // 
            TxtORMappingName.Location = new Point(350, 57);
            TxtORMappingName.Margin = new Padding(8, 7, 8, 7);
            TxtORMappingName.Name = "TxtORMappingName";
            TxtORMappingName.Size = new Size(648, 38);
            TxtORMappingName.TabIndex = 41;
            // 
            // FrmPersistenceEntity
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1970, 1167);
            Controls.Add(GroupBoxCommon);
            Controls.Add(GroupBoxCustomer);
            Controls.Add(GroupBoxConnection);
            Controls.Add(treeView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 5, 6, 5);
            MinimumSize = new Size(1788, 977);
            Name = "FrmPersistenceEntity";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "实体类生成工具 v7【V2024.10.28】";
            Load += FrmPersistenceEntity_Load;
            Click += FrmPersistenceEntity_Load;
            GroupBoxConnection.ResumeLayout(false);
            GroupBoxConnection.PerformLayout();
            GroupBoxCustomer.ResumeLayout(false);
            GroupBoxCustomer.PerformLayout();
            GroupBoxCommon.ResumeLayout(false);
            GroupBoxCommon.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private FolderBrowserDialog folderBrowserDialog1;
        private TreeView treeView1;
        private GroupBox GroupBoxConnection;
        private CheckBox chkAllowView;
        private Button BtnConnect;
        private TextBox TxtLoginID;
        private TextBox TxtPassword;
        private TextBox TxtCatalog;
        private TextBox TxtServer;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private GroupBox GroupBoxCustomer;
        private RadioButton RadioInternal;
        private RadioButton RadioPublic;
        private Button BtnEncrypt;
        private TextBox TxtEncrypt;
        private Label label1;
        private TextBox TxtDataSource;
        private TextBox TxtNameSpace;
        private Label label2;
        private Label label5;
        private GroupBox GroupBoxCommon;
        private Button BtnExport;
        private Button BtnFileDlg;
        private TextBox TxtOutputDir;
        private Label TxtOutputDirectory;
        private Label label3;
        private TextBox TxtORMappingName;
        private CheckBox chkEntity;
        private CheckBox chkConfig;
    }
}