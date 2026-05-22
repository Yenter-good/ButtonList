using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonList.Tables
{
    public partial class UCTableCell : UserControl
    {
        public UCTableCell()
        {
            InitializeComponent();
        }

        internal event EventHandler<TableCellClickArgs> CellClick;

        [Browsable(true)]
        public event EventHandler<MouseEventArgs> CellMouseDown;

        private bool _isActive;

        private Color _normalColor = Color.FromArgb(245, 247, 250);
        private Color _activeColor = Color.Silver;
        private TableCellData _data;

        internal TableCellData Data
        {
            get => _data; set
            {
                _data = value;
                this.label1.Text = _data.Text;
            }
        }

        [Browsable(true)]
        public bool IsTitle { get; set; }

        public void ClearSelection()
        {
            _isActive = false;
            this.label1.BackColor = _isActive ? _activeColor : _normalColor;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsTitle)
            {
                this.CellMouseDown?.Invoke(this, e);
                return;
            }

            _isActive = !_isActive;
            this.label1.BackColor = _isActive ? _activeColor : _normalColor;

            this.CellClick?.Invoke(this, new TableCellClickArgs()
            {
                Data = Data,
                IsActive = _isActive,
                Cell = this,
                Button = e.Button
            });
        }
    }

    internal class TableCellClickArgs : EventArgs
    {
        internal int ResistanceSource { get; set; }
        internal TableColumnType Type { get; set; }
        internal TableCellData Data { get; set; }
        internal bool IsActive { get; set; }
        internal UCTableCell Cell { get; set; }
        internal MouseButtons Button { get; set; }
    }
}
