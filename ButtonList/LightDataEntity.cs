using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList
{
    public class LightDataEntity
    {
        public int Index { get; set; }
        public bool IsActivated { get; set; }
        /// <summary>
        /// 是否正控
        /// </summary>
        public bool IsPositiveControl { get; set; }
        public CanData CanValue { get; set; }
        public LightDataType Type { get; set; }
    }
}
