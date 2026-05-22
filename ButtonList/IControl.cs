using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList
{
    public interface IControl
    {
        ControlManager Manager { get; set; }

        string Name { get; }

        void Init();

        object GetConfig();

        void SetConfig<T>(T config);

    }
}
