using System.Collections;
using System.Data;
using System.Diagnostics;
using Mr1Ceng.Tools.ORMapping.V7.Common;
using Mr1Ceng.Tools.ORMapping.V7.PersistenceEntity;
using Mr1Ceng.Util;

namespace Mr1Ceng.Tools.ORMapping.V7;

public partial class FrmPersistenceEntity : Form
{
    public FrmPersistenceEntity()
    {
        InitializeComponent();
    }


    private void FrmPersistenceEntity_Load(object sender, EventArgs e)
    {
        BindGroupBoxs();
    }

    private void BindGroupBoxs()
    {
        var configuration = new ConfigurationPE();
        configuration.Retrieve();

        var connectinfo = configuration.ConnectInfo;
        TxtServer.Text = connectinfo.Server;
        TxtCatalog.Text = connectinfo.DataBase;
        TxtLoginID.Text = connectinfo.UserID;
        TxtPassword.Text = connectinfo.Password;
        TxtEncrypt.Text = connectinfo.Encrypt;

        var commoninfo = configuration.CommonInfo;
        TxtORMappingName.Text = commoninfo.ORMappingName;
        TxtOutputDir.Text = commoninfo.OutputDir;
        chkEntity.Checked = commoninfo.EntityClassFiles;
        chkConfig.Checked = commoninfo.AppSettingsFiles;

        var customerinfo = configuration.CustomerInfo;
        TxtDataSource.Text = customerinfo.DataSource;
        TxtNameSpace.Text = customerinfo.NameSpace;
    }

    private ConfigurationPE SaveGroupBoxs()
    {
        var configuration = new ConfigurationPE();

        var connectinfo = configuration.ConnectInfo;
        connectinfo.Server = TxtServer.Text;
        connectinfo.DataBase = TxtCatalog.Text;
        connectinfo.UserID = TxtLoginID.Text;
        connectinfo.Password = TxtPassword.Text;
        connectinfo.Encrypt = TxtEncrypt.Text;

        var commoninfo = configuration.CommonInfo;
        commoninfo.ORMappingName = TxtORMappingName.Text;
        commoninfo.OutputDir = TxtOutputDir.Text;
        commoninfo.EntityClassFiles = chkEntity.Checked;
        commoninfo.AppSettingsFiles = chkConfig.Checked;

        var customerinfo = configuration.CustomerInfo;
        customerinfo.DataSource = TxtDataSource.Text;
        customerinfo.NameSpace = TxtNameSpace.Text;
        customerinfo.Function = RadioInternal.Checked ? "internal" : "public";

        configuration.Save();

        return configuration;
    }

    private void BtnFileDlg_Click(object sender, EventArgs e)
    {
        folderBrowserDialog1.SelectedPath = TxtOutputDir.Text;
        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        {
            TxtOutputDir.Text = folderBrowserDialog1.SelectedPath;
        }
    }

    private void BtnConnect_Click(object sender, EventArgs e)
    {
        SaveGroupBoxs();

        treeView1.CheckBoxes = true;
        treeView1.Nodes.Clear();

        var configuration = new ConfigurationPE();
        configuration.Retrieve();

        var root = new TreeNode(configuration.ConnectInfo.DataBase);
        treeView1.Nodes.Add(root);

        var dbOperator = DBOperator.Instance(configuration);
        try
        {
            dbOperator.Open();
            var dset = dbOperator.GetTables();
            foreach (DataRow row in dset.Tables[0].Rows)
            {
                //循环所有表和试图
                if (!chkAllowView.Checked && row["TABLE_TYPE"].ToString().Trim() == "V")
                {
                    continue;
                }
                var node = new TreeNode(row["TABLE_NAME"].ToString(), 2, 2);
                root.Nodes.Add(node);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("数据库连接错误！" + ex);
        }
        finally
        {
            dbOperator.Close();
        }

        root.Expand();
    }

    private void BtnExport_Click(object sender, EventArgs e)
    {
        if (treeView1.Nodes.Count == 0)
        {
            return;
        }
        if (TxtNameSpace.Text.Trim() == "")
        {
            MessageBox.Show("NameSpace不能为空！");
            return;
        }
        if (TxtOutputDir.Text.Trim() == "")
        {
            MessageBox.Show("文件输出路径不能为空！");
            return;
        }
        if (TxtORMappingName.Text.Trim() == "")
        {
            TxtORMappingName.Text = "Sys.ORMapping.xml";
        }

        var configuration = SaveGroupBoxs();
        BtnExport.Enabled = false;

        var dbname = configuration.ConnectInfo.DataBase;


        if (!Directory.Exists(configuration.CommonInfo.OutputDir))
        {
            Directory.CreateDirectory(configuration.CommonInfo.OutputDir);
        }


        var doc = new Document(configuration.CommonInfo.OutputDir)
        {
            DataSource = configuration.CustomerInfo.DataSource,
            NameSpace = configuration.CustomerInfo.NameSpace,
            Function = configuration.CustomerInfo.Function
        };

        foreach (TreeNode tn in treeView1.Nodes[0].Nodes)
        {
            if (tn.Checked)
            {
                doc.EntityObjects.Add(FillEntityObject(dbname, tn.Text));
            }
        }

        try
        {
            //生成
            doc.BuildFile(configuration.CommonInfo);
        }
        catch (Exception ee)
        {
            MessageBox.Show(ee.Message);
        }

        MessageBox.Show(@"操作完成");

        //操作完成后，打开文件夹
        if (MessageBox.Show(@"是否打开文件夹", @"操作完成", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            Process.Start("explorer.exe", TxtOutputDir.Text.Trim());
        }

        BtnExport.Enabled = true;
    }

    private void BtnEncrypt_Click(object sender, EventArgs e)
    {
        TxtEncrypt.Text = NewCode.Secret;
    }

    private EntityObject FillEntityObject(string DbName, string tablename)
    {
        try
        {
            Configuration configuration = new ConfigurationPE();
            configuration.Retrieve();

            var dbOperator = DBOperator.Instance(configuration);
            dbOperator.Open();

            DataSet dset;
            DataRowCollection datarows;
            var fields = new FieldArrayList();
            var keys = new Hashtable();
            var entityObject = new EntityObject
            {
                EntityName = tablename,
                EntityType = dbOperator.GetTableType(tablename),
                EntityDescription = dbOperator.GetTableDescription(tablename),
                FieldRemarkLists = dbOperator.GetColumnRemarks(tablename)
            };

            //取出关键字
            dset = dbOperator.GetPrimary(tablename);
            datarows = dset.Tables[0].Rows;
            foreach (DataRow datarow in datarows)
            {
                var tempkey = datarow["COLUMN_NAME"].ToString();
                keys.Add(tempkey, tempkey);
            }

            //取出所有字段
            dset = dbOperator.GetColumn(tablename);
            datarows = dset.Tables[0].Rows;

            foreach (DataRow datarow in datarows)
            {
                var tempfield = new Field
                {
                    Name = datarow["COLUMN_NAME"].ToString()
                };
                if (datarow["TYPE_NAME"].ToString().IndexOf(":") > 0)
                {
                    tempfield.DataType = datarow["TYPE_NAME"].ToString().Substring(0, datarow["TYPE_NAME"].ToString().IndexOf(":"));
                }
                else
                {
                    tempfield.DataType = datarow["TYPE_NAME"].ToString();
                }
                tempfield.Length = (int)datarow["LENGTH"];
                tempfield.IsKey = keys[tempfield.Name] != null ? true : false;
                tempfield.IsIdentity = datarow["TYPE_NAME"].ToString().IndexOf(":") > 0 ? true : false;

                //临时代码
                if (entityObject.EntityType == "V")
                {
                    tempfield.IsKey = true;
                }

                fields.Add(tempfield);
            }
            entityObject.FieldArrayLists = fields;
            return entityObject;
        }
        catch (Exception ee)
        {
            throw ee;
        }
    }

    private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
    {
        if (e.Node.Parent == null)
        {
            foreach (TreeNode tn in e.Node.Nodes)
            {
                tn.Checked = e.Node.Checked;
            }
        }
    }
}
