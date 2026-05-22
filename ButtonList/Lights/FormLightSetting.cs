using ButtonList.Helper;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ButtonList.Lights
{
    public partial class FormLightSetting : Form
    {
        public FormLightSetting()
        {
            InitializeComponent();
        }

        private IconEntry _iconEntry;

        internal InternalLightData LightData { get; set; }

        private void rbCAN_CheckedChanged(object sender, EventArgs e)
        {
            this.ucCanControl1.Visible = this.rbCAN.Checked;
            this.ucSerialControl1.Visible = this.rbSerial.Checked;

        }

        private void FormLightSetting_Shown(object sender, EventArgs e)
        {
            this.rbSerial.Checked = true;
            if (this.LightData != null)
            {
                _iconEntry = this.LightData.IconEntry;
                this.pictureBox1.Image = _iconEntry.Icon;
                if (this.LightData.Type == LightDataType.Serial)
                {
                    this.rbSerial.Checked = true;
                    this.ucSerialControl1.Data = this.LightData.IsPositiveControl;
                }
                else
                {
                    this.rbCAN.Checked = true;
                    this.ucCanControl1.Data = this.LightData.CanData;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            LightData = new InternalLightData();

            using (MemoryStream ms = new MemoryStream())
            {
                LightData.IconEntry = _iconEntry;
                LightData.Type = (this.rbSerial.Checked ? LightDataType.Serial : LightDataType.Can);
                if (this.rbSerial.Checked)
                    LightData.IsPositiveControl = this.ucSerialControl1.Data;
                else
                    LightData.CanData = this.ucCanControl1.Data;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnSelectIcon_Click(object sender, EventArgs e)
        {
            FormSelectIcon form = new FormSelectIcon();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _iconEntry = form.GetIcon;
                this.pictureBox1.Image = _iconEntry.Icon;
            }
        }


    }
}
