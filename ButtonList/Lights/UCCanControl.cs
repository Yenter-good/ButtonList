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
    public partial class UCCanControl : UserControl
    {
        public UCCanControl()
        {
            InitializeComponent();
        }

        internal CanData Data
        {
            get
            {
                var text1 = this.textBox1.Text;
                var text2 = this.textBox2.Text;
                var text3 = this.textBox3.Text;

                if (text1.Length == 0)
                    text1 = "0";
                if (text2.Length == 0)
                    text2 = "0";
                if (text3.Length == 0)
                    text3 = "0";

                if (text2.Length % 2 == 1)
                    text2 = text2.Insert(0, "0");
                if (text3.Length % 2 == 1)
                    text3 = text3.Insert(0, "0");

                byte[] typeBytes = new byte[text2.Length / 2];
                for (int i = 0; i < text2.Length; i += 2)
                {
                    string byteString = text2.Substring(i, 2);
                    typeBytes[i / 2] = Convert.ToByte(byteString, 16);
                }

                byte[] dataBytes = new byte[text3.Length / 2];
                for (int i = 0; i < text3.Length; i += 2)
                {
                    string byteString = text3.Substring(i, 2);
                    dataBytes[i / 2] = Convert.ToByte(byteString, 16);
                }

                return new CanData()
                {
                    FirstFrame = Convert.ToByte(text1, 16),
                    TypeFrame = typeBytes,
                    DataFrame = dataBytes
                };
            }
            set
            {
                this.textBox1.Text = value.FirstFrame.ToString("X1");
                this.textBox2.Text = BitConverter.ToString(value.TypeFrame).Replace("-", "");
                this.textBox3.Text = BitConverter.ToString(value.DataFrame).Replace("-", "");
            }
        }
    }
}
