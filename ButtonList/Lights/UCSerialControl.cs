using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonList.Lights
{
    public partial class UCSerialControl : UserControl
    {
        public UCSerialControl()
        {
            InitializeComponent();
        }

        public bool Data
        {
            get
            {
                if (this.rbPositive.Checked)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.rbPositive.Checked = true;
                else
                    this.rbNegative.Checked = true;
            }
        }


    }
}
