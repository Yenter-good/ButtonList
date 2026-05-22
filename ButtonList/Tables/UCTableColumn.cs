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
    public partial class UCTableColumn : UserControl
    {
        public UCTableColumn()
        {
            InitializeComponent();
        }

        internal TableColumnData TableColumnData { get; private set; }

        internal event EventHandler<TableCellClickArgs> CellClick;
        internal event EventHandler<ResistanceSourceChangedArgs> ResistanceSourceChanged;


        internal int[] ResistanceSourceIndex { get; set; }
        internal List<UCTableCell> _cellControl;

        internal void Init(TableColumnData columnData = null)
        {
            if (columnData != null)
            {
                TableColumnData = columnData;

                _cellControl = new List<UCTableCell>();
                this.panel1.Controls.Clear();
                this.lbTitle.Data = new TableCellData() { Text = columnData.Title };
                foreach (var cell in columnData.Cells.OrderBy(p => p.Order))
                {
                    UCTableCell uccell = new UCTableCell();
                    uccell.CellClick += Uccell_CellClick;
                    uccell.Dock = DockStyle.Top;
                    uccell.Data = cell;
                    uccell.Height = 40;
                    this.panel1.Controls.Add(uccell);
                    uccell.BringToFront();
                    _cellControl.Add(uccell);
                }
            }
            else
                TableColumnData = new TableColumnData()
                {
                    Title = "未设置"
                };
        }

        private void Uccell_CellClick(object sender, TableCellClickArgs e)
        {
            foreach (var cell in _cellControl)
            {
                if (cell != sender)
                    cell.ClearSelection();
            }
            e.Type = this.TableColumnData.Type;
            e.ResistanceSource = this.TableColumnData.ResistanceSource;
            this.CellClick?.Invoke(this, e);
        }

        private void lbTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(MousePosition);
            }
        }

        private void menuSetting_Click(object sender, EventArgs e)
        {
            using (var form = new FormTableSetting())
            {
                form.Init(TableColumnData);
                form.SetResistanceSource(ResistanceSourceIndex);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ResistanceSourceChanged?.Invoke(this, new ResistanceSourceChangedArgs()
                    {
                        NewIndex = form.TableColumnData.ResistanceSource,
                        OldIndex = (this.TableColumnData == null ? -1 : this.TableColumnData.ResistanceSource)
                    });
                    this.Init(form.TableColumnData);
                }
            }

        }
    }

    class ResistanceSourceChangedArgs : EventArgs
    {
        public int OldIndex { get; set; }
        public int NewIndex { get; set; }
    }
}
