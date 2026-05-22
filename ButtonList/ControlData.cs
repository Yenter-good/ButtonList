using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList
{
    internal class ControlData
    {
        public Dictionary<string, List<LightDataFileEntity>> LightDatas = new Dictionary<string, List<LightDataFileEntity>>();

        public Dictionary<string, List<TableColumnData>> TableDatas = new Dictionary<string, List<TableColumnData>>();
    }
}
