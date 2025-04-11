using System;
using System.Diagnostics;
using System.Security.Policy;
using Mr1Ceng.Tools.CodeCraft.Services;
using Mr1Ceng.Util;

namespace Mr1Ceng.Tools.CodeCraft
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            CreateGrid();
        }

        private readonly IList<ColumnConfig> ColumnList = ConfigHelper.GetColumnConfigList();


        private void FrmMain_Load(object sender, EventArgs e)
        {
            var list = ConfigHelper.GetConfigList(ColumnList);
            BindGrid(list);
        }


        #region 配置

        private void chkEditable_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = !chkEditable.Checked;
            dataGridView1.AllowUserToAddRows = chkEditable.Checked;

            btnCopy.Visible = !chkEditable.Checked;
            btnRemove.Visible = !chkEditable.Checked;
            btnSave.Visible = chkEditable.Checked;

            dataGridView1.SelectionMode = chkEditable.Checked ? DataGridViewSelectionMode.CellSelect : DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var rowsToCopy = new List<DataGridViewRow>();

                // 复制选中的行
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        DataGridViewRow newRow = (DataGridViewRow)row.Clone();
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            newRow.Cells[i].Value = row.Cells[i].Value;
                        }
                        rowsToCopy.Add(newRow);
                    }
                }

                // 将复制的行添加到 DataGridView
                foreach (var newRow in rowsToCopy)
                {
                    dataGridView1.Rows.Add(newRow);
                }

                SaveGrid(GetGrid());
            }
            else
            {
                MessageBox.Show("没有行数据被选中");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 由于正在删除行，最好从最后一行开始向前遍历
                for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[i];
                    if (!row.IsNewRow) // 确保这不是用于添加新行的空白行
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                }

                SaveGrid(GetGrid());
            }
            else
            {
                MessageBox.Show("没有行数据被选中");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var list = GetGrid();
            SaveGrid(list);

            MessageBox.Show("保存成功");

            chkEditable.Checked = false;
        }


        #region 私有方法

        private void CreateGrid()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.AllowUserToAddRows = false;

            #region 添加列

            // 清除现有的列（如果有）
            dataGridView1.Columns.Clear();

            foreach (var column in ColumnList)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = column.ColumnName,
                    HeaderText = column.HeaderText,
                    AutoSizeMode = column.AutoFill ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.None,
                    Width = column.Width
                });
            }

            #endregion


        }

        private void BindGrid(IList<IList<KeyValueText>> list)
        {
            // 清除现有的行（如果有）
            dataGridView1.Rows.Clear();

            // 添加行
            foreach (var item in list)
            {
                int rowIndex = dataGridView1.Rows.Add();
                for (int i = 0; i < item.Count; i++)
                {
                    dataGridView1.Rows[rowIndex].Cells[i].Value = item[i].Value;
                }
            }
        }


        private IList<IList<KeyValueText>> GetGrid()
        {
            var list = new List<IList<KeyValueText>>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) // 忽略用于添加新行的空白行
                {
                    list.Add(GetFlowConfigFromRowCells(row.Cells));
                }
            }

            return list;
        }


        private void SaveGrid(IList<IList<KeyValueText>>? list = null)
        {
            list ??= GetGrid();
            ConfigHelper.SaveConfigFile(list);
        }


        private IList<KeyValueText> GetFlowConfigFromRowCells(DataGridViewCellCollection cells)
        {
            var config = new List<KeyValueText>();
            for (int i = 0; i < ColumnList.Count; i++)
            {
                var columnItem = ColumnList[i];
                config.Add(new KeyValueText(columnItem.ColumnName, GetString.FromObject(cells[i].Value), columnItem.HeaderText));
            }
            return config;
        }

        #endregion


        #endregion


        #region 模板

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.ReadOnly)
            {
                if (e.RowIndex >= 0)// 检查点击的不是列头（列头的行索引是 -1）
                {
                    try
                    {
                        IList<KeyValueText> flow = GetFlowConfigFromRowCells(dataGridView1.Rows[e.RowIndex].Cells);
                        txtOutput.Text = CraftHelper.CraftCode(flow);

                        if (flow.Where(x => x.Key == "LINK").Any())
                        {
                            linkUrl.Text = GetString.FromObject(flow.Where(x => x.Key == "LINK").FirstOrDefault()?.Value);
                        }
                    }
                    catch (BusinessException ex)
                    {
                        txtOutput.Text = ex.Message;
                    }
                    catch (Exception ex)
                    {
                        txtOutput.Text = ex.Message;
                    }
                }
            }
        }


        private void btnReadTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Text = CraftHelper.GetTemplate();
            }
            catch (BusinessException ex)
            {
                txtOutput.Text = ex.Message;
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.Message;
            }
        }

        private void btnHelpTemplate_Click(object sender, EventArgs e)
        {
            txtOutput.Text = @"可配置的参数：

";
            foreach (var columnItem in ColumnList)
            {
                txtOutput.Text += $@"${{{columnItem.HeaderText}}}   【config.{columnItem.ColumnName}】

";
            }
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                CraftHelper.SaveTemplate(txtOutput.Text);
                MessageBox.Show("模板创建成功");
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion



        private void linkUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkUrl.Text != "")
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = linkUrl.Text,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void BtnClipCopyLink_Click(object sender, EventArgs e)
        {
            var textUrl = linkUrl.Text;
            if (textUrl != "")
            {
                Clipboard.SetText(textUrl);
            }
            else
            {
                Clipboard.Clear();
            }
        }

        private void BtnClipCopyCode_Click(object sender, EventArgs e)
        {
            var textUrl = txtOutput.Text;
            if (textUrl != "")
            {
                Clipboard.SetText(textUrl);
            }
            else
            {
                Clipboard.Clear();
            }
        }
    }
}