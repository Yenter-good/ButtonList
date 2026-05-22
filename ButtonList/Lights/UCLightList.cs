using ButtonList.Helper;
using System;
using System.CodeDom;
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
    public partial class UCLightList : FlowLayoutPanel, IControl
    {
        public UCLightList()
        {
            InitializeComponent();
        }

        private UCLightItem _currentItem;
        private List<InternalLightData> _lightDatas;

        private List<UCLightItem> _lightItems;

        public int ButtonSize { get; set; } = 100;
        public ControlManager Manager { get; set; }

        public event EventHandler<LightStateChangedArgs> LightStateChanged;

        public void Init()
        {
            this.Controls.Clear();
            _lightDatas = new List<InternalLightData>();
            _lightItems = new List<UCLightItem>();

            this.menuSetting.Click += MenuSetting_Click;

            for (int i = 0; i < 45; i++)
            {
                UCLightItem item = new UCLightItem();
                item.Tag = i;
                item.NormalImage = IconHelper.GetNormalButton().Icon;
                item.PressImage = IconHelper.GetPressButton().Icon;
                item.ButtonSize = ButtonSize;
                item.PopMenu += Item_PopMenu;
                item.LightStateChanged += Item_LightStateChanged;

                item.Init();
                this.Controls.Add(item);

                _lightItems.Add(item);
            }
        }

        private void Item_LightStateChanged(object sender, LightStateChangedArgs e)
        {
            Manager.OnLightStateChanged(e);
        }

        private void Item_PopMenu(object sender, EventArgs e)
        {
            _currentItem = sender as UCLightItem;
            this.contextMenuStrip1.Show(MousePosition);
        }

        private void MenuSetting_Click(object sender, EventArgs e)
        {
            if (_currentItem == null)
                return;

            FormLightSetting form = new FormLightSetting();
            form.LightData = _currentItem.LightData;
            if (form.ShowDialog() == DialogResult.OK)
            {
                var lightData = form.LightData;
                lightData.Id = (int)_currentItem.Tag;
                _lightDatas.Add(lightData);
                _currentItem.Init(lightData);
            }
        }

        public object GetConfig()
        {
            return _lightDatas;
        }

        public void SetConfig<T>(T config)
        {
            _lightDatas = config as List<InternalLightData>;

            foreach (var data in _lightDatas)
            {
                var item = _lightItems.FirstOrDefault(p => (int)p.Tag == data.Id);
                item.Init(data);
            }
        }

    }
}