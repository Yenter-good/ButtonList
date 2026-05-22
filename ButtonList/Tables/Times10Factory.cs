using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList.Tables
{
    internal class Times10Factory : IFactory
    {
        public byte[] GetData(float value)
        {
            var tmp = value * 10;
            return BitConverter.GetBytes(tmp);
        }
    }
}
