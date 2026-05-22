using ButtonList.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList.Lights
{
    internal class InternalLightData : LightDataFileEntity
    {
        public IconEntry IconEntry { get; set; }
        public bool IsActivated { get; set; }
    }
}
