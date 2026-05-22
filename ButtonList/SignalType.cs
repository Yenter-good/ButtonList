using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    internal class SignalType
    {
        [Description("40路开关量控制")]
        [Range(0, 1099511627775)]
        [Adapt(typeof(OriginDataAdapt))]
        public long Light { get; set; }

        [Description("电阻源1，0～999.9")]
        [RangeF(0, 999.9f)]
        [Adapt(typeof(Times10Adapt))]
        public float Resistance1 { get; set; }

        [Description("电阻源2，0～999.9")]
        [RangeF(0, 999.9f)]
        [Adapt(typeof(Times10Adapt))]
        public float Resistance2 { get; set; }

        [Description("电阻源3，0～999.9")]
        [RangeF(0, 999.9f)]
        [Adapt(typeof(Times10Adapt))]
        public float Resistance3 { get; set; }

        [Description("电阻源4，0～999.9")]
        [RangeF(0, 999.9f)]
        [Adapt(typeof(Times10Adapt))]
        public float Resistance4 { get; set; }

        [Description("频率信号，0～9999.9")]
        [RangeF(0, 999.9f)]
        [Adapt(typeof(Times10Adapt))]
        public float Frequency { get; set; }

        [Description("PWM,0～99.9")]
        [RangeF(0, 99.9f)]
        [Adapt(typeof(Times10Adapt))]
        public float PWM { get; set; }

        [Description("电压信号，DAC 控制字，0～5V，数据以 50 倍发出")]
        [RangeF(0, 5f)]
        [Adapt(typeof(OriginDataAdapt))]
        public float Voltage { get; set; }
    }

    internal class RangeAttribute : RangeFAttribute
    {
        public RangeAttribute(long min, long max)
        {
            Min = min;
            Max = max;
        }

        public new long Min { get; set; }
        public new long Max { get; set; }
    }

    internal class RangeFAttribute : Attribute
    {
        public RangeFAttribute()
        {
        }

        public RangeFAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Min { get; set; }
        public float Max { get; set; }
    }

    internal class AdaptAttribute : Attribute
    {
        public AdaptAttribute(Type adapt)
        {
            Adapt = adapt;
        }

        public Type Adapt { get; set; }
    }
}
