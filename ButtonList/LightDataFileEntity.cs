using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList
{
    internal class LightDataFileEntity
    {
        public int Id { get; set; }
        public string Tooltip { get; set; }
        public string IconName { get; set; }
        public CanData CanData { get; set; }
        /// <summary>
        /// 是否正控
        /// </summary>
        public bool IsPositiveControl { get; set; }
        /// <summary>
        /// 通讯类型
        /// </summary>
        public LightDataType Type { get; set; }
    }

    public class CanData
    {
        public byte FirstFrame { get; set; }
        public byte[] TypeFrame { get; set; }
        public byte[] DataFrame { get; set; }
    }
}
