using ButtonList.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonList.Lights
{
    public partial class FormSelectIcon : Form
    {
        public FormSelectIcon()
        {
            InitializeComponent();
        }

        private List<IconEntry> _icons;
        private PictureBox _currentPicture;

        internal IconEntry GetIcon
        {
            get
            {
                return _currentPicture?.Tag as IconEntry;
            }
        }

        private void FormSelectIcon_Load(object sender, EventArgs e)
        {
            _icons = IconHelper.GetIcons();
        }

        private void FormSelectIcon_Shown(object sender, EventArgs e)
        {
            foreach (var icon in _icons)
            {
                PictureBox pic = new PictureBox();
                pic.Size = new Size(100, 100);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.Image = icon.Icon;
                pic.Tag = icon;
                pic.MouseClick += Pic_MouseClick;

                this.flowLayoutPanel1.Controls.Add(pic);
            }
        }

        private void Pic_MouseClick(object sender, MouseEventArgs e)
        {
            if (_currentPicture != null)
                _currentPicture.BorderStyle = BorderStyle.None;

            _currentPicture = sender as PictureBox;
            _currentPicture.BorderStyle = BorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }


}
