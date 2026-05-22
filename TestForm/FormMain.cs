using ButtonList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        ControlManager cm = new ControlManager();

        private void button2_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "json|*.json";
                dialog.DefaultExt = "*.json";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    cm.Save(dialog.FileName);
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "json|*.json";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    cm.Load(dialog.FileName);
                }
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            cm.Register(this.ucLightList1);
            cm.Register(this.ucLightList2);
            cm.Register(this.ucTable1);
            cm.Init();

            cm.LightStateChanged += Cm_LightStateChanged;
            cm.TableStateChanged += Cm_TableStateChanged;

        }

        private void Cm_TableStateChanged(object sender, TableStateChangedArgs e)
        {
        }

        private void Cm_LightStateChanged(object sender, LightStateChangedArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ucTable1.AddColumn();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var a = cm.Lights.GetSerialDatas();
        }
    }
}
