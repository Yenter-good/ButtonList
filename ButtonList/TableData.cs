using ButtonList.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList
{

    public class TableStateChangedArgs
    {
        public byte[] Data { get; set; }
        public int ResistanceSource { get; set; }
        public TableColumnType Type { get; set; }
    }

    internal class TableColumnData
    {
        public string Title { get; set; }
        public TableColumnType Type { get; set; }
        public int ResistanceSource { get; set; } = -1;
        public int Order { get; set; }
        public List<TableCellData> Cells { get; set; } = new List<TableCellData>();
    }

    internal class TableCellData
    {
        public string Text { get; set; }
        public int Order { get; set; }
        public string Value { get; set; }
    }

    public enum TableColumnType
    {
        [Description("电阻值")]
        [RangeF(0, 999.9f, 1)]
        [Factory(typeof(Times10Factory))]
        Resistance,

        [Description("电压值")]
        [RangeF(0, 5.0f, 1)]
        [Factory(typeof(Times50Factory))]
        Voltage,

        [Description("PWM")]
        [RangeF(0, 999.9f, 1)]
        [Factory(typeof(Times10Factory))]
        PWM,

        [Description("频率值")]
        [RangeF(0, 999.9f, 1)]
        [Factory(typeof(Times10Factory))]
        Frequency
    }

    internal class RangeFAttribute : Attribute
    {
        public RangeFAttribute(float min, float max, int decimalPlace)
        {
            Min = min;
            Max = max;
            DecimalPlace = decimalPlace;
        }

        internal float Min { get; set; }
        internal float Max { get; set; }
        internal int DecimalPlace { get; set; }
    }

    internal class FactoryAttribute : Attribute
    {
        public FactoryAttribute(Type factory)
        {
            Factory = factory;
        }

        public Type Factory { get; set; }
    }
}
