using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ButtonList.Helper;

namespace ButtonList.Tables
{
    public partial class FormTableSetting : Form
    {
        public FormTableSetting()
        {
            InitializeComponent();
        }

        private bool _showResistanceSource;
        private ComboItem _selectedItem;
        private List<RadioButton> _resistanceControl;

        private bool _valid;

        internal TableColumnData TableColumnData { get; private set; }

        internal void Init(TableColumnData data)
        {
            _resistanceControl = new List<RadioButton>()
            {
                this.rbResistance1,
                this.rbResistance2,
                this.rbResistance3,
                this.rbResistance4
            };

            //这里是为了在初始化的时候剪掉电阻源panel的高度，等会selectedindex改变的时候，会加上panel的高度，防止加两次
            this.flowLayoutPanel1.Height -= this.panel2.Height;
            this.Height -= this.panel2.Height;

            var enumType = typeof(TableColumnType);
            foreach (var name in enumType.GetEnumNames())
            {
                var type = (TableColumnType)Enum.Parse(enumType, name);
                var descriptionAttr = type.GetAttribute<DescriptionAttribute>();
                var rangeFAttr = ((TableColumnType)Enum.Parse(enumType, name)).GetAttribute<RangeFAttribute>();
                this.cbxType.Items.Add(new ComboItem(descriptionAttr.Description, rangeFAttr, type));
            }

            TableColumnData = new TableColumnData();
            if (data != null)
            {
                this.tbxTitle.Text = data.Title;
                this.cbxType.SelectedIndex = (int)data.Type;
                var control = this._resistanceControl.FirstOrDefault(p => Convert.ToInt32(p.Tag) == data.ResistanceSource);
                if (control != null)
                    control.Checked = true;

                foreach (var cell in data.Cells.OrderBy(p => p.Order))
                {
                    var row = this.dgvContent.Rows[this.dgvContent.Rows.Add()];
                    row.Cells[colTitle.Index].Value = cell.Text;
                    row.Cells[colValue.Index].Value = cell.Value;
                }

                return;
            }

            TableColumnData = new TableColumnData();
            this.cbxType.SelectedIndex = 0;
        }

        internal void SetResistanceSource(int[] resistanceSourceIndex)
        {
            if (resistanceSourceIndex == null)
                return;

            foreach (var control in _resistanceControl)
            {
                if (resistanceSourceIndex.Contains(Convert.ToInt32(control.Tag)))
                {
                    control.Enabled = false;
                }
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedItem = this.cbxType.SelectedItem as ComboItem;
            if (_selectedItem.Type == TableColumnType.Resistance)
            {
                this.panel2.Show();
                _showResistanceSource = true;
                this.flowLayoutPanel1.Height += this.panel2.Height;
                this.Height += this.panel2.Height;
            }
            else
            {
                this.panel2.Hide();
                if (!_showResistanceSource)
                    return;
                _showResistanceSource = false;
                this.flowLayoutPanel1.Height -= this.panel2.Height;
                this.Height -= this.panel2.Height;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.dgvContent.Rows.Add();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgvContent.RowCount; i++)
            {
                if (this.dgvContent.Rows[i].Cells[colValue.Index].ErrorText != "")
                {
                    MessageBox.Show("有单元格输入的值有问题，请检查");
                    return;
                }
            }

            if (_selectedItem.Type == TableColumnType.Resistance && _resistanceControl.Count(p => !p.Checked) == _resistanceControl.Count)
            {
                MessageBox.Show("电阻值必须选择一路控制");
                return;
            }
            TableColumnData.Title = this.tbxTitle.Text;
            TableColumnData.Type = _selectedItem.Type;
            if (_selectedItem.Type == TableColumnType.Resistance)
                TableColumnData.ResistanceSource = Convert.ToInt32(_resistanceControl.First(p => p.Checked).Tag);
            for (int i = 0; i < this.dgvContent.RowCount; i++)
            {
                var title = this.dgvContent.Rows[i].Cells[colTitle.Index].Value?.ToString();
                var value = this.dgvContent.Rows[i].Cells[colValue.Index].Value?.ToString();
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(value))
                    continue;

                TableColumnData.Cells.Add(new TableCellData()
                {
                    Order = i,
                    Text = title,
                    Value = value
                });
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var selectedCells = this.dgvContent.SelectedCells;
            if (selectedCells.Count > 0)
            {
                this.dgvContent.Rows.RemoveAt(selectedCells[0].RowIndex);
            }
        }

        private void dgvContent_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colValue.Index)
                return;

            var data = this.dgvContent.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
            if (data == null || data == "")
                return;

            var result = float.TryParse(data, out float number);
            if (!result || number < _selectedItem.Range.Min || number > _selectedItem.Range.Max)
            {
                this.dgvContent.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = $"当前输入的值不在范围内，应当为{_selectedItem.Range.Min}-{_selectedItem.Range.Max}";
                MessageBox.Show($"当前输入的值不在范围内，应当为{_selectedItem.Range.Min}-{_selectedItem.Range.Max}");
            }
            else if (number.GetDecimalPlace() > _selectedItem.Range.DecimalPlace)
            {
                this.dgvContent.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = $"当前输入的值小数位数超出，最大保留{_selectedItem.Range.DecimalPlace}位小数";
                MessageBox.Show($"当前输入的值小数位数超出，最大保留{_selectedItem.Range.DecimalPlace}位小数");
            }
            else
                this.dgvContent.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
        }
    }

    class ComboItem
    {
        public ComboItem(string text, RangeFAttribute range, TableColumnType type)
        {
            Text = text;
            Range = range;
            Type = type;
        }

        public string Text { get; set; }
        public RangeFAttribute Range { get; set; }
        public TableColumnType Type { get; set; }

        public override string ToString()
        {
            return $"{Text} 范围：({Range.Min}-{Range.Max})";
        }
    }
}
