using Mr1Ceng.Util;
using Mr1Ceng.Util.Database;

namespace Mr1Ceng.Tools.DataTransfer
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            _ = new EntityManager();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtSQL.Multiline = !chkTable.Checked;
            cmbSourceDB.DataSource = SystemEnvironment.Instance.GetDataSourceNames();
            cmbTargetDB.DataSource = SystemEnvironment.Instance.GetDataSourceNames();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            lblTips.Text = "";

            var sourcedb = cmbSourceDB.Text;
            var strsql = txtSQL.Text.Trim();
            if (chkTable.Checked)
            {
                strsql = "SELECT * FROM " + strsql;
            }

            try
            {
                var rowCount = DataAccess.RowCount(sourcedb, strsql);
                if (rowCount > 1000)
                {
                    dataGridView1.DataSource = DataAccess.ExecuteTopQuery(sourcedb, strsql, 1000);
                    lblTips.Text = $"��ѯ��ɣ���{rowCount}�����ݣ���ʾǰ1000�С�";
                }
                else
                {
                    dataGridView1.DataSource = DataAccess.ExecuteQuery(sourcedb, strsql);
                    lblTips.Text = $"��ѯ��ɣ���{rowCount}������";
                }
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message);
                lblTips.Text = $"��ѯʧ�ܣ�����";
            }
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            btnExec.Enabled = false;
            lblTips.Text = "";

            var sourcedb = cmbSourceDB.Text;
            var targetdb = cmbTargetDB.Text;
            var tableName = txtTableName.Text;
            var strsql = txtSQL.Text.Trim();
            if (chkTable.Checked)
            {
                if (tableName == "")
                {
                    tableName = strsql;
                }
                strsql = "SELECT * FROM " + strsql;
            }

            if (!targetdb.StartsWith("dev"))
            {
                DialogResult result = MessageBox.Show("Ŀ�����ݿ��dev������ȷ��Ҫ����������", "ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    btnExec.Enabled = true;
                    return;
                }
            }

            try
            {
                //��ȡԴ����
                var dt = DataAccess.ExecuteQuery(sourcedb, strsql);

                if (chkTruncate.Checked)
                {
                    //���Ŀ������
                    DataAccess.Execute(targetdb, $"TRUNCATE TABLE {tableName}");
                }

                //ȫ��д��Ŀ������
                DataAccess.SaveTableData(targetdb, tableName, dt);

                lblTips.Text = $"д����ɣ���{dt.Rows.Count}�����ݡ�";

                MessageBox.Show(lblTips.Text);
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message);
                lblTips.Text = $"д��ʧ�ܣ�����";
            }

            btnExec.Enabled = true;
        }

        private void chkTable_CheckedChanged(object sender, EventArgs e)
        {
            txtSQL.Multiline = !chkTable.Checked;
        }
    }
}