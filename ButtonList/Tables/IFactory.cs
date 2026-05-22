using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList.Tables
{
    internal interface IFactory
    {
        byte[] GetData(float value);
    }
}
