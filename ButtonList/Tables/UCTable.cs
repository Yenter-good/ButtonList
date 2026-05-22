using ButtonList.Helper;
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
    public partial class UCTable : UserControl, IControl
    {
        public UCTable()
        {
            InitializeComponent();
        }

        public ControlManager Manager { get; set; }

        internal List<UCTableColumn> _columnControls;
        internal HashSet<int> _resistanceSourceIndex;

        public object GetConfig()
        {
            List<TableColumnData> result = new List<TableColumnData>();
            for (int i = 0; i < _columnControls.Count; i++)
            {
                var data = _columnControls[i].TableColumnData;
                data.Order = i;
                result.Add(data);
            }
            return result;
        }

        public void Init()
        {
            _resistanceSourceIndex = new HashSet<int>();
            _columnControls = new List<UCTableColumn>();
        }

        public void AddColumn()
        {
            UCTableColumn column = new UCTableColumn();
            column.Init();
            column.Dock = DockStyle.Left;
            column.CellClick -= Column_CellClick;
            column.CellClick += Column_CellClick;
            column.ResistanceSourceChanged += Column_ResistanceSourceChanged;

            this.Controls.Add(column);
            column.BringToFront();
            _columnControls.Add(column);
        }

        private void Column_ResistanceSourceChanged(object sender, ResistanceSourceChangedArgs e)
        {
            _resistanceSourceIndex.Remove(e.OldIndex);
            _resistanceSourceIndex.Add(e.NewIndex);
            foreach (var column in _columnControls)
            {
                column.ResistanceSourceIndex = _resistanceSourceIndex.ToArray();
            }
        }

        public void SetConfig<T>(T config)
        {
            if (config is List<TableColumnData> columnDatas)
            {
                this.Controls.Clear();
                columnDatas.ForEach(p => this._resistanceSourceIndex.Add(p.ResistanceSource));

                foreach (TableColumnData columnData in columnDatas.OrderBy(p => p.Order))
                {
                    UCTableColumn column = new UCTableColumn();
                    column.Init(columnData);
                    column.ResistanceSourceIndex = this._resistanceSourceIndex.ToArray();
                    column.Dock = DockStyle.Left;
                    column.CellClick -= Column_CellClick;
                    column.CellClick += Column_CellClick;

                    this.Controls.Add(column);
                    column.BringToFront();
                }


            }
        }

        private void Column_CellClick(object sender, TableCellClickArgs e)
        {
            var factoryAttr = e.Type.GetAttribute<FactoryAttribute>();
            var factory = Activator.CreateInstance(factoryAttr.Factory) as IFactory;
            var data = factory.GetData(Convert.ToSingle(e.Data.Value));

            Manager.OnTableStateChanged(data, e.Type, e.ResistanceSource);
        }
    }
}
