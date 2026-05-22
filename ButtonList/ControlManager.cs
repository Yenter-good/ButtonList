using ButtonList.Helper;
using ButtonList.Lights;
using ButtonList.Tables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList
{
    public class ControlManager
    {
        private List<IControl> _controls = new List<IControl>();
        private ControlData _controlData = new ControlData();

        public string ResourcePath { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources.zip");

        public event EventHandler<LightStateChangedArgs> LightStateChanged;
        public event EventHandler<TableStateChangedArgs> TableStateChanged;

        /// <summary>
        /// 务必在Register控件后，调用Init方法
        /// </summary>
        public void Init()
        {
            this.Lights = new Light(this);

            IconHelper.ResourcePath = ResourcePath;

            _controls.ForEach(p =>
            {
                p.Manager = this;
                p.Init();
            });
        }

        public Light Lights { get; set; }

        /// <summary>
        /// 注册控件，统一管理
        /// </summary>
        /// <param name="control"></param>
        public void Register(IControl control)
        {
            control.Manager = this;
            if (!_controls.Any(p => p == control))
                _controls.Add(control);
        }

        internal void OnLightStateChanged(LightStateChangedArgs e)
        {
            LightStateChanged?.Invoke(this, e);
        }

        internal void OnTableStateChanged(byte[] data, TableColumnType type, int ResistanceSource)
        {
            TableStateChanged?.Invoke(this, new TableStateChangedArgs()
            {
                Data = data,
                ResistanceSource = ResistanceSource,
                Type = type
            });
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            foreach (IControl control in _controls)
            {
                var config = control.GetConfig();
                if (control is UCLightList)
                    SaveLightData(control.Name, config as List<InternalLightData>);
                else if (control is UCTable)
                    SaveTable(control.Name, config as List<TableColumnData>);
            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_controlData);
            var data = AesHelper.Encrypt(json);
            File.WriteAllText(path, data);
        }

        private void SaveLightData(string name, List<InternalLightData> internalDatas)
        {
            var lightDatas = internalDatas.Select(p => new LightDataFileEntity()
            {
                Id = p.Id,
                CanData = p.CanData,
                IconName = p.IconEntry.Name,
                IsPositiveControl = p.IsPositiveControl,
                Tooltip = p.Tooltip,
                Type = p.Type,
            }).ToList();

            _controlData.LightDatas[name] = lightDatas;
        }

        private void SaveTable(string name, List<TableColumnData> datas)
        {
            _controlData.TableDatas[name] = datas;
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            var data = File.ReadAllText(path);
            var json = AesHelper.Decrypt(data);
            _controlData = Newtonsoft.Json.JsonConvert.DeserializeObject<ControlData>(json);

            foreach (IControl control in _controls)
            {
                if (control is UCLightList)
                {
                    if (_controlData.LightDatas.ContainsKey(control.Name))
                        LoadLightData(control, _controlData.LightDatas[control.Name]);
                }
                else if (control is UCTable)
                {
                    if (_controlData.TableDatas.ContainsKey(control.Name))
                        LoadTableData(control, _controlData.TableDatas[control.Name]);
                }
            }
        }

        private void LoadLightData(IControl control, List<LightDataFileEntity> lightDatas)
        {
            var internalLightDatas = lightDatas.Select(p => new InternalLightData()
            {
                CanData = p.CanData,
                Id = p.Id,
                IsPositiveControl = p.IsPositiveControl,
                Tooltip = p.Tooltip,
                Type = p.Type,
                IconEntry = IconHelper.GetIcon(p.IconName)
            }).ToList();

            control.SetConfig(internalLightDatas);
        }

        private void LoadTableData(IControl control, List<TableColumnData> tableDatas)
        {
            control.SetConfig(tableDatas);
        }

        public sealed class Light
        {
            private ControlManager _cm;

            public Light(ControlManager cm)
            {
                _cm = cm;
            }

            /// <summary>
            /// 获取所有数据
            /// </summary>
            /// <param name="control"></param>
            /// <returns></returns>
            public List<LightDataEntity> GetData(IControl control = null)
            {
                if (control == null)
                    control = _cm._controls.FirstOrDefault();
                if (control == null)
                    return null;

                if (control is UCLightList light)
                {
                    var config = light.GetConfig() as List<InternalLightData>;
                    return config.Select(p => new LightDataEntity()
                    {
                        CanValue = p.CanData,
                        IsActivated = p.IsActivated,
                        IsPositiveControl = p.IsPositiveControl,
                        Type = p.Type
                    }).ToList();
                }
                return null;
            }

            /// <summary>
            /// 仅获取信号量数据
            /// </summary>
            /// <param name="control"></param>
            /// <returns></returns>
            public long GetSerialDatas(IControl control = null)
            {
                if (control == null)
                    control = _cm._controls.FirstOrDefault();
                if (control == null)
                    return -1;

                if (control is UCLightList light)
                {
                    var config = (light.GetConfig() as List<InternalLightData>).Where(p => p.Type == LightDataType.Serial);
                    long result = 0;
                    foreach (var lightData in config)
                    {
                        if (lightData.IsPositiveControl == lightData.IsActivated)
                            result |= (1 << lightData.Id);
                    }
                    return result;
                }
                return -1;
            }

            /// <summary>
            /// 仅获取Can数据
            /// </summary>
            /// <param name="control"></param>
            /// <returns></returns>
            public List<CanData> GetCanDatas(IControl control = null)
            {
                if (control == null)
                    control = _cm._controls.FirstOrDefault();
                if (control == null)
                    return null;

                if (control is UCLightList light)
                {
                    var config = (light.GetConfig() as List<InternalLightData>).Where(p => p.Type == LightDataType.Can);
                    return config.Select(p => p.CanData).ToList();
                }
                return null;
            }
        }


    }

}
